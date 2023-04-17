using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyAppAPI.DTOs;
using MyAppAPI.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentGatewayController : ControllerBase
    {
        private readonly BookingService _bookingService;
        public PaymentGatewayController(BookingService bookingService)
        {
            _bookingService = bookingService;
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BookingDTO bookingPayload)
        {
            var userId = HttpContext.User.Claims.First(c => c.Type == "UserId").Value;
            return Ok(await _bookingService.CreateBooking(bookingPayload, userId));

        }
    }
}
