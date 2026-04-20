using ECommerce.Common;
using ECommerce.DAL;

namespace ECommerce.BLL
{
    public class CartManager : ICartManager
    {
        private readonly IUnitOfWork _unitOfWork;
        public CartManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GeneralResult<CartDto>> GetCartByUserIdAsync(string userId)
        {
            var cart = await _unitOfWork.CartsRepository.GetCartByUserIdAsync(userId);
            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = userId,
                    CartItems = new List<CartItem>(),
                };

                await _unitOfWork.CartsRepository.AddAsync(cart);
                await _unitOfWork.SaveAsync();
            }
            var cartDto = MapCartToDto(cart);
            return GeneralResult<CartDto>.Success(cartDto);

        }
        public async Task<GeneralResult<CartDto>> AddToCartAsync(string userId, AddToCartDto addToCartDto)
        {
            var productExist = await _unitOfWork.ProductsRepository.GetByIdAsync(addToCartDto.ProductId);

            if (productExist == null)
                return GeneralResult<CartDto>.NotFound("Product not found.");

            if (addToCartDto.Quantity <= 0)
                return GeneralResult<CartDto>.Failure("Quantity must be greater than zero.");

            var cart = await _unitOfWork.CartsRepository.GetCartByUserIdAsync(userId);
            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = userId,
                    CartItems = new List<CartItem>(),
                };
                await _unitOfWork.CartsRepository.AddAsync(cart);
                await _unitOfWork.SaveAsync();
            }

            var cartItem = await _unitOfWork.CartsRepository.GetCartItemByProductIdAsync(cart.Id, addToCartDto.ProductId);

            if (cartItem != null)
                cartItem.Quantity += addToCartDto.Quantity;
            else
            {
                cartItem = new CartItem
                {
                    ProductId = addToCartDto.ProductId,
                    Quantity = addToCartDto.Quantity,
                    CartId = cart.Id,
                };
                cart.CartItems.Add(cartItem);
            }

            await _unitOfWork.SaveAsync();


            cart = await _unitOfWork.CartsRepository.GetCartByUserIdAsync(userId);
            if (cart is null)
                return GeneralResult<CartDto>.Failure("Cart not found after update.");

            var cartDto = MapCartToDto(cart);
            return GeneralResult<CartDto>.Success(cartDto, "Product added to cart successfully.");
        }
        public async Task<GeneralResult> ClearCartAsync(string userId)
        {
            var cart = await _unitOfWork.CartsRepository.GetCartByUserIdAsync(userId);
            if (cart == null)
                return GeneralResult.Failure("Cart not found.");

            cart.CartItems.Clear();
            await _unitOfWork.SaveAsync();

            return GeneralResult.Success("Cart cleared successfully.");
        }
        public async Task<GeneralResult> RemoveFromCartAsync(string userId, int productId)
        {
            var cart = await _unitOfWork.CartsRepository.GetCartByUserIdAsync(userId);

            if (cart == null)
                return GeneralResult.NotFound("Cart Not Found.");

            var cartItem = await _unitOfWork.CartsRepository.GetCartItemByProductIdAsync(cart.Id, productId);

            if (cartItem == null)
                return GeneralResult.NotFound("Product not found in cart.");

            cart.CartItems.Remove(cartItem);
            await _unitOfWork.SaveAsync();

            return GeneralResult.Success("Product removed from cart successfully.");

        }
        public async Task<GeneralResult<CartDto>> UpdateQuantityAsync(string userId, UpdateCartItemQuantityDto quantityDto)
        {
            if (quantityDto.Quantity < 0)
                return GeneralResult<CartDto>.Failure("Quantity can not be negative.");

            var cart = await _unitOfWork.CartsRepository.GetCartByUserIdAsync(userId);
            if (cart == null)
                return GeneralResult<CartDto>.NotFound("Cart not found.");

            var cartItem = await _unitOfWork.CartsRepository.GetCartItemByProductIdAsync(cart.Id, quantityDto.ProductId);
            if (cartItem == null)
                return GeneralResult<CartDto>.NotFound("Cart item not found.");

            if (quantityDto.Quantity == 0)
            {
                cart.CartItems.Remove(cartItem);
            }
            else
            {
                cartItem.Quantity = quantityDto.Quantity;
            }
            await _unitOfWork.SaveAsync();

            var cartDto = MapCartToDto(cart);

            return GeneralResult<CartDto>.Success(cartDto, "Cart updated.");

        }
        private static CartDto MapCartToDto(Cart cart)
        {
            var cartDto = new CartDto
            {
                Id = cart.Id,
                UserId = cart.UserId,
                TotalItems = cart.CartItems.Sum(ci => ci.Quantity),
                TotalPrice = cart.CartItems.Sum(ci => ci.Quantity * (ci.Product?.Price ?? 0)),

                CartItems = cart.CartItems.Select(ci => new CartItemDto
                {
                    Id = ci.Id,
                    ProductId = ci.ProductId,
                    ProductName = ci.Product?.Name ?? "",
                    Quantity = ci.Quantity,
                    ProductPrice = ci.Product?.Price ?? 0,
                    LineTotal = ci.Quantity * (ci.Product?.Price ?? 0),
                }).ToList()
            };
            return cartDto;
        }
    }
}
