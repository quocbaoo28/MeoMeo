using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FPMG.ExamService.Services.Messaging
{
    public class EventConsumer : IEventConsumer
    {
        private readonly ConnectionFactory _factory;
        private readonly ILogger<EventConsumer> _logger;

        public EventConsumer(ILogger<EventConsumer> logger)
        {
            _logger = logger;

            // Cấu hình mặc định
            _factory = new ConnectionFactory()
            {
                HostName = "localhost",
                Port = 5672,
                //UserName = "admin",
                //Password = "admin",
                // Thêm các thông số kết nối
                RequestedHeartbeat = TimeSpan.FromSeconds(60),
                // Tăng thời gian timeout cho kết nối
                ContinuationTimeout = TimeSpan.FromSeconds(20),
                // Cấu hình phục hồi kết nối tự động
                AutomaticRecoveryEnabled = true,
                NetworkRecoveryInterval = TimeSpan.FromSeconds(10)
            };
        }

        public async Task ConsumeEventAsync<T>(string exchangeName, Action<T> onMessageReceived)
        {
            // Thêm vòng lặp thử kết nối nhiều lần nếu thất bại
            int retryCount = 0;
            const int maxRetries = 20; // Tăng số lần thử
            TimeSpan initialDelay = TimeSpan.FromSeconds(3);

            while (retryCount < maxRetries)
            {
                try
                {
                    _logger.LogInformation($"Đang thử kết nối đến RabbitMQ (lần thứ {retryCount + 1}/{maxRetries}) tại {_factory.HostName}:{_factory.Port}...");

                    // Thử kiểm tra kết nối TCP trước
                    using (var tcpClient = new System.Net.Sockets.TcpClient())
                    {
                        try
                        {
                            var connectTask = tcpClient.ConnectAsync(_factory.HostName, _factory.Port);
                            if (await Task.WhenAny(connectTask, Task.Delay(5000)) != connectTask)
                            {
                                throw new TimeoutException("Timeout kiểm tra kết nối TCP");
                            }
                            _logger.LogInformation("Cổng RabbitMQ đang mở và chấp nhận kết nối");
                        }
                        catch (Exception ex)
                        {
                            _logger.LogWarning($"Kiểm tra cổng TCP thất bại: {ex.Message}");
                            throw;
                        }
                    }

                    await using var connection = await _factory.CreateConnectionAsync();
                    _logger.LogInformation("Kết nối RabbitMQ thành công!");

                    await using var channel = await connection.CreateChannelAsync();
                    _logger.LogInformation($"Tạo channel thành công, đang cấu hình exchange: {exchangeName}");

                    await channel.ExchangeDeclareAsync(exchange: exchangeName, type: ExchangeType.Fanout);

                    var queueName = await channel.QueueDeclareAsync();
                    await channel.QueueBindAsync(queue: queueName, exchange: exchangeName, routingKey: string.Empty);

                    var consumer = new AsyncEventingBasicConsumer(channel);
                    consumer.ReceivedAsync += async (model, ea) =>
                    {
                        var body = ea.Body.ToArray();
                        var jsonMessage = Encoding.UTF8.GetString(body);

                        try
                        {
                            var message = JsonSerializer.Deserialize<T>(jsonMessage);
                            if (message != null)
                            {
                                _logger.LogInformation($"Đã nhận tin nhắn từ exchange {exchangeName}");
                                onMessageReceived?.Invoke(message);
                            }
                        }
                        catch (JsonException ex)
                        {
                            _logger.LogError($"Lỗi deserialize tin nhắn: {ex.Message}");
                        }

                        await Task.Yield();
                    };

                    await channel.BasicConsumeAsync(queue: queueName, autoAck: true, consumer: consumer);

                    _logger.LogInformation($"Đã bắt đầu lắng nghe tin nhắn từ exchange {exchangeName}");
                    await Task.Delay(Timeout.Infinite);

                    // Nếu kết nối thành công, thoát khỏi vòng lặp
                    break;
                }
                catch (Exception ex)
                {
                    retryCount++;
                    var nextDelay = TimeSpan.FromSeconds(Math.Min(30, initialDelay.TotalSeconds * Math.Pow(1.5, retryCount - 1)));

                    _logger.LogWarning($"Kết nối đến RabbitMQ thất bại: {ex.Message}. Lần thử: {retryCount}/{maxRetries}. Chờ {nextDelay.TotalSeconds} giây trước khi thử lại.");

                    if (retryCount >= maxRetries)
                    {
                        _logger.LogError("Đã vượt quá số lần thử kết nối tối đa. Dừng kết nối.");
                        throw;
                    }

                    // Chờ với thời gian tăng dần theo cấp số nhân (exponential backoff)
                    await Task.Delay(nextDelay);
                }
            }
        }
    }
}
