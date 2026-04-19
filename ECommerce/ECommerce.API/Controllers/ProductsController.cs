using ECommerce.BLL;
using ECommerce.Common;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductManager _productManager;

        public ProductsController(IProductManager productManager)
        {
            _productManager = productManager;
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
        public async Task<ActionResult<GeneralResult>> Delete(int id)
        {
            var result = await _productManager.DeleteProductAsync(id);
            if (!result.IsSuccess)
            {
                return NotFound($"No product found with id {id}.");
            }
            return Ok(result);
        }
    }
}
