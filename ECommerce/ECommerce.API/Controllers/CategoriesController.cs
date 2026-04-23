using ECommerce.BLL;
using ECommerce.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryManager _categoryManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IImageManager _imageManager;
        public CategoriesController(ICategoryManager categoryManager, IWebHostEnvironment webHostEnvironment, IImageManager imageManager)
        {
            _categoryManager = categoryManager;
            _webHostEnvironment = webHostEnvironment;
            _imageManager = imageManager;
        }

        [HttpGet]
        public async Task<ActionResult<GeneralResult<IEnumerable<CategoryDto>>>> GetAll()
        {
            var result = await _categoryManager.GetAllCategoriesAsync();
            if (!result.IsSuccess)
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
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<GeneralResult<CategoryDto>>> Create([FromBody] CreateCategoryDto dto)
        {
            var result = await _categoryManager.CreateCategoryAsync(dto);
            if (!result.IsSuccess)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<GeneralResult<CategoryDto>>> Update(int id, [FromBody] UpdateCategoryDto dto)
        {
            var result = await _categoryManager.UpdateCategoryAsync(id, dto);
            if (!result.IsSuccess)
                return BadRequest(result);
            return Ok(result);

        }
        [HttpPatch("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<GeneralResult<CategoryDto>>> PartialUpdate(int id, [FromBody] PatchCategoryDto dto)
        {
            var result = await _categoryManager.PatchCategoryAsync(id, dto);
            if (!result.IsSuccess)
                return BadRequest(result);
            return Ok(result);

        }
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<GeneralResult>> Delete(int id)
        {
            var result = await _categoryManager.DeleteCategoryAsync(id);
            if (!result.IsSuccess)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPost("{id:int}/image")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<GeneralResult<PatchCategoryDto>>> SetImage(int id, [FromForm] ImageUploadDto dto)
        {
            var basePath = _webHostEnvironment.WebRootPath ?? _webHostEnvironment.ContentRootPath;
            var schema = Request.Scheme;
            var host = Request.Host.Value;
            var uploadResult = await _imageManager.UploadImage(dto, basePath, schema, host);
            if (!uploadResult.IsSuccess)
            {
                return BadRequest(uploadResult);
            }
            var imageUrl = uploadResult.Data!.ImageURL;
            var result = await _categoryManager.SetCategoryImageAsync(id, imageUrl);
            if (!result.IsSuccess)
                return BadRequest(result);
            return Ok(result);

        }
    }
}
