using Microsoft.AspNetCore.Mvc;
using MyAppAPI.Interface;
using MyAppAPI.Models;

namespace MyAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly IDataRepository _repo;
        public CityController(IDataRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _repo.Get<City>());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _repo.GetById<City>(id));
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] City CityUpdateData)
        {
            var existingCityDetails = await _repo.GetById<City>(CityUpdateData.CityId);
            if (existingCityDetails != null)
            {
                existingCityDetails.Code = CityUpdateData.Code;
                existingCityDetails.Name = CityUpdateData.Name;
                return Ok(await _repo.Update(existingCityDetails));
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] City CityUpdateData)
        {
            return Ok(await _repo.Create(CityUpdateData));

        }
    }
}
