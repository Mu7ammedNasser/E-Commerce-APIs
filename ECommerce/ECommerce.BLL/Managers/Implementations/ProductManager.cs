using ECommerce.Common;
using ECommerce.DAL;

namespace ECommerce.BLL
{
    public class ProductManager : IProductManager
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GeneralResult<IEnumerable<ProductDto>>> GetAllProductsAsync()
        {
            var result = await _unitOfWork.ProductsRepository.GetAllAsync();
            var data = result.Select(P => new ProductDto
            {
                Id = P.Id,
                Name = P.Name,
                Description = P.Description,
                Price = P.Price,
                ProductsInStock = P.ProductsInStock,
                ImageUrl = P.ImageUrl
            });
            return GeneralResult<IEnumerable<ProductDto>>.Success(data);
        }

        public async Task<GeneralResult<ProductDto>> CreateProductAsync(CreateProductDto createProductDto)
        {
            var Exist = await _unitOfWork.ProductsRepository.ExistsByNameAsync(createProductDto.Name);
            if (Exist)
            {
                return GeneralResult<ProductDto>.Failure("Product with the same name already exists.");
            }
            var newProduct = new Product
            {
                Name = createProductDto.Name,
                Description = createProductDto.Description,
                Price = createProductDto.Price,
                ProductsInStock = createProductDto.ProductsInStock,
                CategoryId = createProductDto.CategoryId,
            };
            await _unitOfWork.ProductsRepository.AddAsync(newProduct);
            await _unitOfWork.SaveAsync();
            var data = new ProductDto
            {
                Id = newProduct.Id,
                Name = newProduct.Name,
                Description = newProduct.Description,
                Price = newProduct.Price,
                ProductsInStock = newProduct.ProductsInStock,
                ImageUrl = newProduct.ImageUrl
            };
            return GeneralResult<ProductDto>.Success(data, "Product created successfully.");
        }

        public async Task<GeneralResult<ProductDto>> GetProductByIdAsync(int id)
        {
            var product = await _unitOfWork.ProductsRepository.GetByIdAsync(id);
            if (product == null)
            {
                return GeneralResult<ProductDto>.NotFound("Product not found.");
            }
            var data = new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                ProductsInStock = product.ProductsInStock,
                ImageUrl = product.ImageUrl
            };
            return GeneralResult<ProductDto>.Success(data);
        }

        public async Task<GeneralResult> DeleteProductAsync(int id)
        {
            var product = await _unitOfWork.ProductsRepository.GetByIdAsync(id);
            if (product == null)
            {
                return GeneralResult.NotFound("Product not found.");

            }
            await _unitOfWork.ProductsRepository.DeleteAsync(product);
            await _unitOfWork.SaveAsync();
            return GeneralResult.Success("Product deleted successfully.");
        }

        public async Task<GeneralResult<ProductDto>> UpdateProductAsync(int id, UpdateProductDto updateProductDto)
        {
            var product = await _unitOfWork.ProductsRepository.GetByIdAsync(id);
            if (product == null)
            {
                return GeneralResult<ProductDto>.NotFound("Product not found.");
            }
            product.Name = updateProductDto.Name;
            product.Description = updateProductDto.Description;
            product.Price = updateProductDto.Price;
            product.ProductsInStock = updateProductDto.ProductsInStock;
            product.CategoryId = updateProductDto.CategoryId;
            await _unitOfWork.SaveAsync();
            var data = new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                ProductsInStock = product.ProductsInStock,
                ImageUrl = product.ImageUrl
            };
            return GeneralResult<ProductDto>.Success(data, "Product updated successfully.");

        }
    }
}
