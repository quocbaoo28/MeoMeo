using FPMG.GradingService.BussinessObjects.DTO.Request;
using FPMG.GradingService.BussinessObjects.DTO.Response;
using FPMG.GradingService.Services.Interfaces;
using FPMG.GradingService.WebAPIs.ResponseType;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FPMG.GradingService.WebAPIs.Controllers
{
    [ApiController]
    [Route("api/payment")]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost]
        public async Task<ActionResult<JsonResponse<PaymentResponseDTO>>> Create([FromBody] PaymentRequestDTO paymentRequestDTO)
        {
            try
            {
                var result = await _paymentService.CreateAsync(paymentRequestDTO);
                if (result == null)
                {
                    return BadRequest(new JsonResponse<string>(null, 400, "Failed to create payment."));
                }

                return Ok(new JsonResponse<PaymentResponseDTO>(result, 200, "Payment created successfully!"));
            }
            catch (Exception ex)
            {
                return ex.Message != null
                    ? BadRequest(new JsonResponse<string>(null, 400, ex.Message))
                    : StatusCode(500, new JsonResponse<string>(null, 500, "Internal Server Error"));
            }
        }

    }
}
