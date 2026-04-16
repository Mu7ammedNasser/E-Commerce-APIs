namespace ECommerce.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ECommerceDbContext _context;    

        public IProductRepository ProductsRepository { get; }
        public ICategoryRepository CategoriesRepository { get; }
        public ICartRepository CartsRepository { get; }
        public IOrderRepository OrdersRepository { get; }

        public UnitOfWork(ECommerceDbContext context,
                            IProductRepository productRepository,
                            ICategoryRepository categoryRepository,
                            ICartRepository cartRepository,
                            IOrderRepository orderRepository)
            
            
        {
            _context = context;
            ProductsRepository = productRepository;
            CategoriesRepository = categoryRepository;
            CartsRepository = cartRepository;
            OrdersRepository = orderRepository;

        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
