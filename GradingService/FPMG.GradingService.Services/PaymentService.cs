using FPMG.GradingService.BussinessObjects.DTO.Response;
using FPMG.GradingService.BussinessObjects;
using FPMG.GradingService.Repositories;
using FPMG.GradingService.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FPMG.GradingService.BussinessObjects.DTO.Request;
using AutoMapper;
using FPMG.GradingService.Repositories.Interfaces;

namespace FPMG.GradingService.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentsRepository _repository;
        private readonly IMapper _mapper;

        public PaymentService(IPaymentsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PaymentResponseDTO> CreateAsync(PaymentRequestDTO paymentRequestDTO)
        {
            var payment = _mapper.Map<Payments>(paymentRequestDTO);
            var result = await _repository.AddAsync(payment);
            return _mapper.Map<PaymentResponseDTO>(result);
        }
    }
}
