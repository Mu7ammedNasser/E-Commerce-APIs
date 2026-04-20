using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.DAL
{
    public class ECommerceDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ECommerceDbContext).Assembly);

            modelBuilder.Entity<Category>().HasData(
       new Category { Id = 1, Name = "Food", Description = "Food and groceries" },
            new Category { Id = 2, Name = "Electronics", Description = "Phones, laptops, accessories" },
            new Category { Id = 3, Name = "Clothes", Description = "Men, women and kids clothing" }
   );

            // -------------------- Seed Products
            modelBuilder.Entity<Product>().HasData(
               new Product { Id = 1, Name = "Rice 5kg", Description = "Premium rice", Price = 250m, ProductsInStock = 100, CategoryId = 1 },
                    new Product { Id = 2, Name = "Olive Oil 1L", Description = "Extra virgin", Price = 180m, ProductsInStock = 60, CategoryId = 1 },
                    new Product { Id = 7, Name = "Pasta 1kg", Description = "Italian pasta", Price = 40m, ProductsInStock = 120, CategoryId = 1 },
                    new Product { Id = 8, Name = "Sugar 1kg", Description = "White sugar", Price = 30m, ProductsInStock = 200, CategoryId = 1 },
                    new Product { Id = 9, Name = "Milk 1L", Description = "Fresh milk", Price = 25m, ProductsInStock = 150, CategoryId = 1 },

                    new Product { Id = 3, Name = "iPhone 15 Pro", Description = "Apple smartphone", Price = 999m, ProductsInStock = 10, CategoryId = 2 },
                    new Product { Id = 4, Name = "Sony WH-1000XM5", Description = "Noise cancelling headphones", Price = 349m, ProductsInStock = 20, CategoryId = 2 },
                    new Product { Id = 10, Name = "Samsung Galaxy S24", Description = "Android flagship phone", Price = 850m, ProductsInStock = 15, CategoryId = 2 },
                    new Product { Id = 11, Name = "Dell XPS 13", Description = "Ultrabook laptop", Price = 1200m, ProductsInStock = 8, CategoryId = 2 },
                    new Product { Id = 12, Name = "Logitech Mouse", Description = "Wireless mouse", Price = 50m, ProductsInStock = 70, CategoryId = 2 },

                    new Product { Id = 5, Name = "T-Shirt", Description = "Cotton t-shirt", Price = 150m, ProductsInStock = 80, CategoryId = 3 },
                    new Product { Id = 6, Name = "Jeans", Description = "Blue denim jeans", Price = 350m, ProductsInStock = 40, CategoryId = 3 },
                    new Product { Id = 13, Name = "Jacket", Description = "Winter jacket", Price = 600m, ProductsInStock = 25, CategoryId = 3 },
                    new Product { Id = 14, Name = "Sneakers", Description = "Running shoes", Price = 500m, ProductsInStock = 35, CategoryId = 3 },
                    new Product { Id = 15, Name = "Cap", Description = "Casual cap", Price = 100m, ProductsInStock = 90, CategoryId = 3 }
            );
        }



        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Cart> Carts => Set<Cart>();
        public DbSet<CartItem> CartItems => Set<CartItem>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<IAuditable>();

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                }
                else if (entry.State == EntityState.Modified)
                {
                    // prevent overwriting original CreatedAt
                    entry.Property(x => x.CreatedAt).IsModified = false;
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

    }
}
