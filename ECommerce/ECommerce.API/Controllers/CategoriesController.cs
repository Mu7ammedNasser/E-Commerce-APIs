using ECommerce.BLL;
using ECommerce.Common;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryManager _categoryManager;

        public CategoriesController(ICategoryManager categoryManager)
        {
            _categoryManager = categoryManager;
        }

        [HttpGet]
        public async Task<ActionResult<GeneralResult<IEnumerable<CategoryDto>>>> GetAll()
        {
            var result = await _categoryManager.GetAllCategoriesAsync();
            if(!result.IsSuccess)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<GeneralResult<CategoryDto>>> GetById(int id)
        {
            var result = await _categoryManager.GetCategoryByIdAsync(id);

            if (!result.IsSuccess)
                return NotFound(result);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<GeneralResult<CategoryDto>>> Create([FromBody] CreateCategoryDto dto)
        {
            var result = await _categoryManager.CreateCategoryAsync(dto);
            if (!result.IsSuccess)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<GeneralResult<CategoryDto>>> Update(int id, [FromBody] UpdateCategoryDto dto)
        {
            var result = await _categoryManager.UpdateCategoryAsync(id, dto);
            if (!result.IsSuccess)
                return BadRequest(result);
            return Ok(result);

        }
        [HttpPatch("{id:int}")]
        public async Task<ActionResult<GeneralResult<CategoryDto>>> PartialUpdate(int id, [FromBody] PatchCategoryDto dto)
        {
            var result = await _categoryManager.PatchCategoryAsync(id, dto);
            if (!result.IsSuccess)
                return BadRequest(result);
            return Ok(result);

        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<GeneralResult>> Delete(int id)
        {
            var result = await _categoryManager.DeleteCategoryAsync(id);
            if (!result.IsSuccess)
                return BadRequest(result);
            return Ok(result);
        }
    }
}
