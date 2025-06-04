using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPMG.GradingService.Services.Messaging
{
    public interface IEventConsumer
    {
        Task ConsumeEventAsync<T>(string exchangeName, Action<T> onMessageReceived);
    }
}
