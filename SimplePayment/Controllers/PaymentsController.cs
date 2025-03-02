using Common.Interfaces;
using Common.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SimplePayment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentsService _paymentsService;
        public PaymentsController(IPaymentsService paymentsService)
        {
            _paymentsService = paymentsService;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePayment([FromBody] PaymentDTO payment)
        {
            bool created = await _paymentsService.CreatePayment(payment);
            if (created)
            {
                return Ok("Payment Created"); //TODO: RETURN ID
            }
            return BadRequest("User has not been created");
        }
    }
}
