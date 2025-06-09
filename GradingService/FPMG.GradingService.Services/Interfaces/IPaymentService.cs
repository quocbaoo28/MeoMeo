using FPMG.GradingService.BussinessObjects.DTO.Request;
using FPMG.GradingService.BussinessObjects.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPMG.GradingService.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<PaymentResponseDTO> CreateAsync(PaymentRequestDTO paymentRequestDTO);
    }
}
