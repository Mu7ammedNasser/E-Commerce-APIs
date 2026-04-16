using ECommerce.Common;
using ECommerce.DAL;

namespace ECommerce.BLL
{
    public class CategoryManager : ICategoryManager
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }  

        public async Task<GeneralResult<IEnumerable<CategoryDto>>> GetAllCategoriesAsync()
        {
            var categories = await _unitOfWork.CategoriesRepository.GetAllAsync();

            var data = categories.Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                ImageUrl = c.ImageUrl
            });

            return GeneralResult<IEnumerable<CategoryDto>>.Success(data);
        }

        public async Task<GeneralResult<CategoryDto>> GetCategoryByIdAsync(int id)
        {
            var category = await _unitOfWork.CategoriesRepository.GetByIdAsync(id);
            if (category == null)
            {
                return GeneralResult<CategoryDto>.NotFound(message: "Category not found.");
            }

            var data = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                ImageUrl = category.ImageUrl
            };
            return GeneralResult<CategoryDto>.Success(data);

        }


        public async Task<GeneralResult> CreateCategoryAsync(CreateCategoryDto createCategoryDto)
        {
            var exists = await _unitOfWork.CategoriesRepository.ExistsByNameAsync(createCategoryDto.Name);
            if (exists)
            {
                return GeneralResult.Failure(message: "A category with the same name already exists.");
            }

            var Category = new Category
            {
                Name = createCategoryDto.Name,
                Description = createCategoryDto.Description,
            };
            await _unitOfWork.CategoriesRepository.AddAsync(Category);
            await _unitOfWork.SaveAsync();
            return GeneralResult.Success("Category created successfully.");
        }

        public async Task<GeneralResult> DeleteCategoryAsync(int id)
        {
            var category = await _unitOfWork.CategoriesRepository.GetByIdAsync(id);
            if (category == null)
            {
                return GeneralResult.NotFound(message: "Category not found.");
            }

            await _unitOfWork.CategoriesRepository.DeleteAsync(category);
            await _unitOfWork.SaveAsync();
            return GeneralResult.Success("Category deleted successfully.");
        }

        public async Task<GeneralResult> UpdateCategoryAsync(int id, UpdateCategoryDto updateCategoryDto)
        {
            var category = await _unitOfWork.CategoriesRepository.GetByIdAsync(id);
            if (category == null)
            {
                return GeneralResult.NotFound(message: "Category not found.");
            }

            category.Name = updateCategoryDto.Name;
            category.Description = updateCategoryDto.Description;

            await _unitOfWork.SaveAsync();
            return GeneralResult.Success("Category updated successfully.");
        }

      
    }
}

