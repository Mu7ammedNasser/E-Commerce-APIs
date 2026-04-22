using ECommerce.Common;

namespace ECommerce.DAL
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(int categoryId);
        Task<IEnumerable<Product>> GetProductsByPriceRangeAsync(decimal minPrice, decimal maxPrice);

        Task<(List<Product> Items, int TotalCount)> GetPagedAsync(ProductFilterParameters p);
        Task<bool> ExistsByNameAsync(string name);
    }
}
