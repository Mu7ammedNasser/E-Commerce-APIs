using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
namespace ECommerce.BLL
{
    public static class BLLServiceExtensions
    {
        public static IServiceCollection AddBLLServices(this IServiceCollection services)
        {
            services.AddScoped<ICategoryManager, CategoryManager>();
            services.AddScoped<IProductManager, ProductManager>();
            services.AddScoped<ICartManager, CartManager>();
            services.AddScoped<IOrderManager, OrderManager>();
            services.AddScoped<IImageManager, ImageManager>();
            services.AddScoped<IValidator<ImageUploadDto>, ImageUploadDtoValidators>();
            return services;
        }

    }
}
