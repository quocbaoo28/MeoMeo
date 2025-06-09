using AutoMapper;
using FPMG.GradingService.BussinessObjects;
using FPMG.GradingService.BussinessObjects.DTO.Request;
using FPMG.GradingService.BussinessObjects.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FPMG.GradingService.Repositories.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<PaymentRequestDTO, Payments>();
            CreateMap<Payments, PaymentResponseDTO>();
        }
    }
}
