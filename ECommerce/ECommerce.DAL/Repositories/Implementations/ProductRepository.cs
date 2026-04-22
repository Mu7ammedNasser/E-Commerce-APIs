using ECommerce.Common;
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

        public async Task<(List<Product> Items, int TotalCount)> GetPagedAsync(ProductFilterParameters p)
        {
            var query = _context.Set<Product>().AsNoTracking();
            if (p.CategoryId.HasValue)
                query = query.Where(x => x.CategoryId == p.CategoryId.Value);

            if (!string.IsNullOrWhiteSpace(p.Search))
            {
                var s = p.Search.Trim();
                query = query.Where(x => x.Name.Contains(s));
            }

            if (p.MinPrice.HasValue) query = query.Where(x => x.Price >= p.MinPrice.Value);
            if (p.MaxPrice.HasValue) query = query.Where(x => x.Price <= p.MaxPrice.Value);
            if (p.InStockOnly == true) query = query.Where(x => x.ProductsInStock > 0);

            var totalCount = await query.CountAsync();


            query = (p.SortedBy ?? "id").ToLower() switch
            {
                "price" => p.SortedDescending?? false ? query.OrderByDescending(x => x.Price) : query.OrderBy(x => x.Price),
                "name" => p.SortedDescending ?? false ? query.OrderByDescending(x => x.Name) : query.OrderBy(x => x.Name),
                _ => p.SortedDescending ?? false ? query.OrderByDescending(x => x.Id) : query.OrderBy(x => x.Id),
            };


            var items = await query
                .Skip((p.PageNumber - 1) * p.PageSize)
                .Take(p.PageSize)
                .ToListAsync();

            return (items, totalCount);
        }
    }
}