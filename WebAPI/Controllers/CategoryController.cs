using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyAppAPI.Interface;
using MyAppAPI.Models;

namespace MyAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IDataRepository _repo;
        public CategoryController(IDataRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _repo.Get<Category>());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _repo.GetById<Category>(id));
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Category CategoryUpdateData)
        {
            var existingCategoryDetails = await _repo.GetById<Category>(CategoryUpdateData.CategoryId);
            if (existingCategoryDetails != null)
            {
                existingCategoryDetails.Code = CategoryUpdateData.Code;
                existingCategoryDetails.Name = CategoryUpdateData.Name;
                existingCategoryDetails.VenueId = CategoryUpdateData.VenueId;
                existingCategoryDetails.Amount = CategoryUpdateData.Amount;
                existingCategoryDetails.Discount = CategoryUpdateData.Discount;
                existingCategoryDetails.Prority = CategoryUpdateData.Prority;
                return Ok(await _repo.Update(existingCategoryDetails));
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Category CategoryUpdateData)
        {
            return Ok(await _repo.Create(CategoryUpdateData));

        }
    }
}
