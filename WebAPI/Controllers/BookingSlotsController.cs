using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyAppAPI.Interface;
using MyAppAPI.Models;

namespace MyAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingSlotsController : ControllerBase
    {
        private readonly IDataRepository _repo;
        public BookingSlotsController(IDataRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _repo.Get<BookingSlots>());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _repo.GetById<BookingSlots>(id));
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] BookingSlots BookingSlotsUpdateData)
        {
            var existingBookingSlotsDetails = await _repo.GetById<BookingSlots>(BookingSlotsUpdateData.BookingSlotId);
            if (existingBookingSlotsDetails != null)
            {
                existingBookingSlotsDetails.BookingId = BookingSlotsUpdateData.BookingId;
                existingBookingSlotsDetails.SlotId = BookingSlotsUpdateData.SlotId;
                return Ok(await _repo.Update(existingBookingSlotsDetails));
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BookingSlots BookingSlotsUpdateData)
        {
            return Ok(await _repo.Create(BookingSlotsUpdateData));

        }
    }
}
