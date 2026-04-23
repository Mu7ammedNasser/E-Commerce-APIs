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
            }).ToList();
            return GeneralResult<IEnumerable<ProductDto>>.Success(data);
        }

        public async Task<GeneralResult<ProductDto>> CreateProductAsync(CreateProductDto createProductDto)
        {
            var ProductExist = await _unitOfWork.ProductsRepository.ExistsByNameAsync(createProductDto.Name);
            if (ProductExist)
            {
                return GeneralResult<ProductDto>.Failure("Product with the same name already exists.");
            }

            var CategoryExist = await _unitOfWork.CategoriesRepository.GetByIdAsync(createProductDto.CategoryId);
            if (CategoryExist is null)
            {
                return GeneralResult<ProductDto>.Failure("Invalid CategoryId.");
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

        public async Task<GeneralResult<PatchProductDto>> PatchProductAsync(int id, PatchProductDto dto)
        {
            var product = await _unitOfWork.ProductsRepository.GetByIdAsync(id);
            if (product == null)
                return GeneralResult<PatchProductDto>.NotFound("Product Not Found.");

            if (dto.Name is not null)
                product.Name = dto.Name;

            if (dto.Description is not null)
                product.Description = dto.Description;

            if (dto.Price is not null)
                product.Price = dto.Price ?? 0;

            if (dto.ProductsInStock is not null)
                product.ProductsInStock = dto.ProductsInStock ?? 0;

            if (dto.CategoryId is not null)
            {
                var categoryExist = await _unitOfWork.CategoriesRepository.GetByIdAsync(dto.CategoryId ?? 0);
                if (categoryExist == null)
                {
                    return GeneralResult<PatchProductDto>.NotFound("Category not found.");
                }
                product.CategoryId = dto.CategoryId ?? 0;
            }

            await _unitOfWork.SaveAsync();
            var data = new PatchProductDto
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                ProductsInStock = product.ProductsInStock,
                CategoryId = product.CategoryId
            };
            return GeneralResult<PatchProductDto>.Success(data, "Product patched successfully.");
        }

        public async Task<GeneralResult<PageResult<ProductDto>>> GetPagedProductsAsync(ProductFilterParameters pagination)
        {
            var (items,totalCount) =await _unitOfWork.ProductsRepository.GetPagedAsync(pagination);
            var dtoItems = items.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                ProductsInStock = p.ProductsInStock,
                ImageUrl = p.ImageUrl
            }).ToList();

            var totalPages = (int)Math.Ceiling(totalCount / (double)pagination.PageSize);

            var paged = new PageResult<ProductDto>
            {
                Items = dtoItems,
                Metadata = new PaginationMetadata
                {
                    CurrentPage = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    TotalCount = totalCount,
                    TotalPages = totalPages,
                    HasNext = pagination.PageNumber < totalPages,
                    HasPrevious = pagination.PageNumber > 1
                }
            };

            return GeneralResult<PageResult<ProductDto>>.Success(paged);
        }

        public async Task<GeneralResult<ProductDto>> SetProductImageAsync(int productId, string imageUrl)
        {
            var product = await _unitOfWork.ProductsRepository.GetByIdAsync(productId);
            if (product == null)
            {
                return GeneralResult<ProductDto>.NotFound("Product not found.");
            }   
            product.ImageUrl = imageUrl;
            await _unitOfWork.SaveAsync();
            var dto = new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                ProductsInStock = product.ProductsInStock,
                ImageUrl = product.ImageUrl
            };
            return GeneralResult<ProductDto>.Success(dto, "Product image Added successfully.");
        }
    }
}










