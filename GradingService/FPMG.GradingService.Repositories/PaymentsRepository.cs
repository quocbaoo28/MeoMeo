using FPMG.GradingService.BussinessObjects;
using FPMG.GradingService.DataAccessLayers;
using FPMG.GradingService.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPMG.GradingService.Repositories
{
    public class PaymentsRepository : IPaymentsRepository
    {
        private readonly GradingManagementDBContext _context;

        public PaymentsRepository(GradingManagementDBContext context)
        {
            _context = context;
        }

        public async Task<Payments> AddAsync(Payments payment)
        {
            _context.Set<Payments>().Add(payment);
            await _context.SaveChangesAsync();
            return payment;
        }
    }
}
