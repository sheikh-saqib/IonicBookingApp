using Microsoft.AspNetCore.Mvc;
using MyAppAPI.Interface;
using MyAppAPI.Models;
using System.Linq.Expressions;

namespace MyAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SlotDetailsController : ControllerBase
    {
        private readonly IDataRepository _repo;
        public SlotDetailsController(IDataRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _repo.Get<SlotDetails>());
        }

        [HttpGet("{selectedDate}")]
        public async Task<IActionResult> Get(string selectedDate)
        {
            Expression<Func<SlotDetails, bool>> filter = null;
            filter = a => a.SlotDate == selectedDate;
            var slotDetails = await _repo.FindAllAsync<SlotDetails>(filter, null);
            foreach (var item in slotDetails)
            {
                item.Category = await _repo.GetById<Category>(item.CategoryId); ;
                item.Category.Venue = await _repo.GetById<Venue>(item.Category.VenueId);
            }
            return Ok(slotDetails);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] SlotDetails SlotDetailsUpdateData)
        {
            var existingSlotDetails = await _repo.GetById<SlotDetails>(SlotDetailsUpdateData.SlotId);
            if (existingSlotDetails != null)
            {
                existingSlotDetails.SlotNumber = SlotDetailsUpdateData.SlotNumber;
                existingSlotDetails.CategoryId = SlotDetailsUpdateData.CategoryId;
                existingSlotDetails.SlotDate = SlotDetailsUpdateData.SlotDate;
                existingSlotDetails.SlotStatus = SlotDetailsUpdateData.SlotStatus;
                existingSlotDetails.IsEnabled = SlotDetailsUpdateData.IsEnabled;
                existingSlotDetails.SlotPriority = SlotDetailsUpdateData.SlotPriority;
                return Ok(await _repo.Update(existingSlotDetails));
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SlotDetails SlotDetailsUpdateData)
        {
            return Ok(await _repo.Create(SlotDetailsUpdateData));

        }
    }
}
