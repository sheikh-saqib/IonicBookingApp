using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyAppAPI.Interface;
using MyAppAPI.Models;

namespace MyAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private readonly IDataRepository _repo;
        public PhotosController(IDataRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _repo.Get<Photos>());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _repo.GetById<Photos>(id));
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Photos PhotoUpdateData)
        {
            var existingPhotoDetails = await _repo.GetById<Photos>(PhotoUpdateData.PhotoId);
            if (existingPhotoDetails != null)
            {
                existingPhotoDetails.VenueId = PhotoUpdateData.VenueId;
                existingPhotoDetails.Image = PhotoUpdateData.Image;
                return Ok(await _repo.Update(existingPhotoDetails));
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Photos PhotoUpdateData)
        {
            return Ok(await _repo.Create(PhotoUpdateData));

        }
    }
}
