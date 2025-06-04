using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPMG.ExamService.Services.Messaging
{
    public interface IEventPushlisher
    {
        Task PublishEventAsync<T>(string exchangeName, T message);
    }
}
