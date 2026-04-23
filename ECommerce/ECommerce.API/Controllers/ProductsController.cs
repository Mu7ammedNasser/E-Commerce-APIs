using ECommerce.BLL;
using ECommerce.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductManager _productManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IImageManager _imageManager;  

        public ProductsController(IProductManager productManager , IWebHostEnvironment webHostEnvironment, IImageManager imageManager)
        {
            _productManager = productManager;
            _webHostEnvironment = webHostEnvironment;
            _imageManager = imageManager;
        }
        [HttpGet]
        public async Task<ActionResult<GeneralResult<IEnumerable<ProductDto>>>> GetAll()
        {
            var result = await _productManager.GetAllProductsAsync();
            if (!result.IsSuccess)
            {
                return NotFound("No products found.");
            }
            return Ok(result);
        }
        [HttpGet("paged")]
        [AllowAnonymous]
        public async Task<ActionResult<GeneralResult<PageResult<ProductDto>>>> GetPaged([FromQuery] ProductFilterParameters pagination)
        {
            var result = await _productManager.GetPagedProductsAsync(pagination);
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<GeneralResult<ProductDto>>> GetById(int id)
        {
            var result = await _productManager.GetProductByIdAsync(id);
            if (!result.IsSuccess)
            {
                return NotFound($"No product found with id {id}.");
            }
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<GeneralResult<ProductDto>>> Create([FromBody] CreateProductDto dto)
        {
            var result = await _productManager.CreateProductAsync(dto);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<GeneralResult<ProductDto>>> Update(int id, [FromBody] UpdateProductDto dto)
        {
            var result = await _productManager.UpdateProductAsync(id, dto);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPatch("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<GeneralResult<PatchProductDto>>> PartialUpdate(int id, [FromBody] PatchProductDto dto)
        {
            var result = await _productManager.PatchProductAsync(id, dto);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<GeneralResult>> Delete(int id)
        {
            var result = await _productManager.DeleteProductAsync(id);
            if (!result.IsSuccess)
            {
                return NotFound($"No product found with id {id}.");
            }
            return Ok(result);
        }

        [HttpPost("{id:int}/image")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<GeneralResult<ProductDto>>> SetImage(int id, [FromForm] ImageUploadDto dto)
        {
            var basePath = _webHostEnvironment.WebRootPath ?? _webHostEnvironment.ContentRootPath;
            var schema = Request.Scheme;
            var host = Request.Host.Value;

            var uploadResult = await _imageManager.UploadImage(dto, basePath, schema, host);
            if (!uploadResult.IsSuccess)
                return BadRequest(uploadResult);

            var result = await _productManager.SetProductImageAsync(id, uploadResult.Data!.ImageURL);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);

        }
    }
}
