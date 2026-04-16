using Microsoft.EntityFrameworkCore;

namespace ECommerce.DAL.Repositories.Implementations
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(ECommerceDbContext context) : base(context) { }
        public async Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(int categoryId)
        {
            return await _context.Set<Product>().Where(p => p.CategoryId == categoryId).ToListAsync();
        }
        public async Task<IEnumerable<Product>> GetProductsByPriceRangeAsync(decimal minPrice, decimal maxPrice)
        {
            return await _context.Set<Product>().Where(p => p.Price >= minPrice && p.Price <= maxPrice).ToListAsync();
        }
        public async Task<bool> ExistsByNameAsync(string name)
        {
            return await _context.Set<Product>().AnyAsync(p => p.Name == name);
        }
    }
}