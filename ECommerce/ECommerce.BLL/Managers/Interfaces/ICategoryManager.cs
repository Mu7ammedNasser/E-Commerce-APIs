using ECommerce.Common;

namespace ECommerce.BLL
{
    public interface ICategoryManager
    {
        Task<GeneralResult<IEnumerable<CategoryDto>>> GetAllCategoriesAsync();
        Task<GeneralResult<CategoryDto>> GetCategoryByIdAsync(int id);
        Task<GeneralResult> CreateCategoryAsync(CreateCategoryDto createCategoryDto);
        Task<GeneralResult> UpdateCategoryAsync(int id, UpdateCategoryDto updateCategoryDto);
        Task<GeneralResult> DeleteCategoryAsync(int id);
    }
}
