namespace ECommerce.DAL
{
    public interface IGenericRepository<T> where T : class
    {
        /// we should use async methods for better performance and scalability
        /// we do not use synchronous methods because they can block the thread and cause performance issues
        /// in repositories we do not add update methods 
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task AddAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
