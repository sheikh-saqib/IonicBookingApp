using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyAppAPI.DTOs;
using MyAppAPI.Interface;
using MyAppAPI.Models;
using MyAppAPI.Services;
using System.Linq.Expressions;

namespace MyAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IDataRepository _repo;
        private readonly BookingService _bookingService;
        public BookingController(IDataRepository repo,BookingService bookingService)
        {
            _repo = repo;
            _bookingService = bookingService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _repo.Get<Booking>());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _repo.GetById<Booking>(id));
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Booking BookingUpdateData)
        {
            var existingBookingDetails = await _repo.GetById<Booking>(BookingUpdateData.BookingId);
            if (existingBookingDetails != null)
            {
                existingBookingDetails.SlotDate = BookingUpdateData.SlotDate;
                existingBookingDetails.UserId = BookingUpdateData.UserId;
                existingBookingDetails.BookingStatus = BookingUpdateData.BookingStatus;
                existingBookingDetails.BookingDate = BookingUpdateData.BookingDate;
                existingBookingDetails.Amount = BookingUpdateData.Amount;
                existingBookingDetails.BookingUpdateDate = BookingUpdateData.BookingUpdateDate;

                return Ok(await _repo.Update(existingBookingDetails));
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Booking bookingPayload)
        {
            return Ok(await _repo.Create(bookingPayload));
        }

        [HttpPost]
        [Route("/api/[controller]/[action]")]
        public async Task<IActionResult> CancelBooking([FromBody] int bookingId)
        {
            var abc = _bookingService.CancelBooking(bookingId);
            return Ok(abc);
        }

    }
}
