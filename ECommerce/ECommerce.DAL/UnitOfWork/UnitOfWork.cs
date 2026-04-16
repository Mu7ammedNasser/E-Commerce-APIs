namespace ECommerce.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ECommerceDbContext _context;    

        public IProductRepository Products { get; }
        public ICategoryRepository Categories { get; }
        public ICartRepository Carts { get; }
        public IOrderRepository Orders { get; }

        public UnitOfWork(ECommerceDbContext context,
                            IProductRepository productRepository,
                            ICategoryRepository categoryRepository,
                            ICartRepository cartRepository,
                            IOrderRepository orderRepository)
            
            
        {
            _context = context;
            Products = productRepository;
            Categories = categoryRepository;
            Carts = cartRepository;
            Orders = orderRepository;

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
