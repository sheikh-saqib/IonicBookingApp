using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyAppAPI.Interface;
using MyAppAPI.Models;

namespace MyAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IDataRepository _repo;
        public PaymentController(IDataRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _repo.Get<Payment>());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _repo.GetById<Payment>(id));
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Payment PaymentUpdateData)
        {
            var existingPaymentDetails = await _repo.GetById<Payment>(PaymentUpdateData.PaymentId);
            if (existingPaymentDetails != null)
            {
                existingPaymentDetails.BookingId = PaymentUpdateData.BookingId;
                existingPaymentDetails.UserId = PaymentUpdateData.UserId;
                existingPaymentDetails.Amount = PaymentUpdateData.Amount;
                existingPaymentDetails.UserId = PaymentUpdateData.UserId;
                existingPaymentDetails.RazorPayOrderId = PaymentUpdateData.RazorPayOrderId;
                existingPaymentDetails.RazorPayPaymentId = PaymentUpdateData.RazorPayPaymentId;
                existingPaymentDetails.PaymentDate = PaymentUpdateData.PaymentDate;
                existingPaymentDetails.PaymentStatus = PaymentUpdateData.PaymentStatus;
                return Ok(await _repo.Update(existingPaymentDetails));
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Payment PaymentUpdateData)
        {
            return Ok(await _repo.Create(PaymentUpdateData));

        }
    }
}
