using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyAppAPI.Interface;
using MyAppAPI.Models;
using System.Linq.Expressions;

namespace MyAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VenueController : ControllerBase
    {
        private readonly IDataRepository _repo;
        public VenueController(IDataRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            var venueDetails = await _repo.Get<Venue>();
            foreach (var item in venueDetails)
            {
                var cityDetails = await _repo.GetById<City>(item.CityId);
                item.City = cityDetails;
                Expression<Func<Photos, bool>> filter = null;
                filter = a => a.VenueId == item.VenueId;
                item.Photos = await _repo.FindAllAsync<Photos>(filter, null);
            }
            return Ok(venueDetails);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _repo.GetById<Venue>(id));
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Venue VenueUpdateData)
        {
            var existingVenueDetails = await _repo.GetById<Venue>(VenueUpdateData.VenueId);
            if (existingVenueDetails != null)
            {
                existingVenueDetails.Code = VenueUpdateData.Code;
                existingVenueDetails.Name = VenueUpdateData.Name;
                existingVenueDetails.CityId = VenueUpdateData.CityId;
                existingVenueDetails.LatLong = VenueUpdateData.LatLong;
                existingVenueDetails.ContactNumber = VenueUpdateData.ContactNumber;
                existingVenueDetails.Address = VenueUpdateData.Address;
                existingVenueDetails.IsActive = VenueUpdateData.IsActive;
                return Ok(await _repo.Update(existingVenueDetails));
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Venue VenueUpdateData)
        {
            return Ok(await _repo.Create(VenueUpdateData));

        }
    }
}
