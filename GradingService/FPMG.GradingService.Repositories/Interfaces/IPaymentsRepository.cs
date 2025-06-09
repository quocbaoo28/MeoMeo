using FPMG.GradingService.BussinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPMG.GradingService.Repositories.Interfaces
{
    public interface IPaymentsRepository
    {
        Task<Payments> AddAsync(Payments payment);
    }
}
