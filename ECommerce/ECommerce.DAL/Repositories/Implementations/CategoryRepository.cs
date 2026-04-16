using Microsoft.EntityFrameworkCore;

namespace ECommerce.DAL.Repositories.Implementations
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ECommerceDbContext context) : base(context) { }

        public Task<bool> ExistsByNameAsync(string name)
        {
            return _context.Categories.AnyAsync(c => c.Name.ToLower() == name.ToLower());
        }
    }
}
