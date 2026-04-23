using ECommerce.Common;

namespace ECommerce.BLL
{
    public interface ICategoryManager
    {
        Task<GeneralResult<IEnumerable<CategoryDto>>> GetAllCategoriesAsync();
        Task<GeneralResult<CategoryDto>> GetCategoryByIdAsync(int id);
        Task<GeneralResult<CategoryDto>> CreateCategoryAsync(CreateCategoryDto createCategoryDto);
        Task<GeneralResult<CategoryDto>> UpdateCategoryAsync(int id, UpdateCategoryDto updateCategoryDto);

        Task<GeneralResult<PatchCategoryDto>> SetCategoryImageAsync(int id, string imageUrl);
        Task<GeneralResult> PatchCategoryAsync(int id, PatchCategoryDto dto);
        Task<GeneralResult> DeleteCategoryAsync(int id);
    }
}
