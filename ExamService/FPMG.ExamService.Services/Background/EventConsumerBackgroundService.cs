using FPMG.ExamService.Services.Messaging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPMG.ExamService.Services.Background
{
    public class EventConsumerBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<EventConsumerBackgroundService> _logger;
        private readonly IServiceScopeFactory _scopeFactory;

        public EventConsumerBackgroundService(IServiceProvider serviceProvider, ILogger<EventConsumerBackgroundService> logger, IServiceScopeFactory scopeFactory)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // Đăng ký tất cả các consumer trước và chạy trên các task riêng biệt
            var tasks = new List<Task>
        {
            Task.Run(async () => await DummyExamServiceConsumer()),

        };

            await Task.WhenAll(tasks);
        }

       


        private async Task DummyExamServiceConsumer()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var eventConsumer = scope.ServiceProvider.GetRequiredService<IEventConsumer>();

                await eventConsumer.ConsumeEventAsync<List<Dictionary<string, string>>>("exam_serrvice", async stockList =>
                {
                });
            }
        }

       
    }
}
