
namespace ECommerce.DAL
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository ProductsRepository { get; }
        ICategoryRepository CategoriesRepository { get; }
        ICartRepository CartsRepository { get; }
        IOrderRepository OrdersRepository { get; }
        Task SaveAsync();
    }
}

// IDisposable is implemented to ensure that the UnitOfWork can
// be properly disposed of, which is important for releasing database
// connections and other resources. The SaveAsync method is used to commit
// changes to the database, ensuring that all operations performed through
// the repositories are saved in a single transaction.
// simply IDisposable is implemented to allow the UnitOfWork to be used in a using statement, which ensures that it is disposed of correctly after use. This is important for managing resources and preventing memory leaks, especially when dealing with database connections.
