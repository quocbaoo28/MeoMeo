using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FPMG.UserService.Services.Messaging
{
    public class EventPublisher : IEventPushlisher
    {
        private readonly ConnectionFactory _factory;

        public EventPublisher(IConfiguration configuration)
        {
            // Đọc cấu hình từ appsettings.json hoặc biến môi trường
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
                // Số lần thử kết nối
                AutomaticRecoveryEnabled = true,
                NetworkRecoveryInterval = TimeSpan.FromSeconds(10)
            };
        }

        // Gửi sự kiện đến exchange cho nhiều consumer
        public async Task PublishEventAsync<T>(string exchangeName, T message)
        {
            await using var connection = await _factory.CreateConnectionAsync();
            await using var channel = await connection.CreateChannelAsync();

            await channel.ExchangeDeclareAsync(exchange: exchangeName, type: ExchangeType.Fanout);

            string jsonMessage = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(jsonMessage);

            await channel.BasicPublishAsync(
                exchange: exchangeName,
                routingKey: string.Empty,
                body: body
            );

            Console.WriteLine($"Sent {exchangeName}");
        }

    }
}
