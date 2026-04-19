using ECommerce.Common;

namespace ECommerce.BLL
{
    public interface IProductManager
    {
        Task<GeneralResult<IEnumerable<ProductDto>>> GetAllProductsAsync();
        Task<GeneralResult<ProductDto>> GetProductByIdAsync(int id);
        Task<GeneralResult<ProductDto>> CreateProductAsync(CreateProductDto createProductDto);
        Task<GeneralResult<ProductDto>> UpdateProductAsync(int id, UpdateProductDto updateProductDto);
        Task<GeneralResult<PatchProductDto>> PatchProductAsync(int id, PatchProductDto dto);
        Task<GeneralResult> DeleteProductAsync(int id);
    }
}
