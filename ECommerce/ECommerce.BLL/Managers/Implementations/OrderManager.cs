using ECommerce.Common;
using ECommerce.Common.Enums;
using ECommerce.DAL;

namespace ECommerce.BLL
{
    public class OrderManager : IOrderManager
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        public async Task<GeneralResult<OrderDto>> CreateOrderFromCartAsync(CreateOrderDto dto)
        {
            var cart = await _unitOfWork.CartsRepository.GetCartByUserIdAsync(dto.UserId);
            if (cart == null)
                return GeneralResult<OrderDto>.NotFound("Cart not found");

            if (cart.CartItems.Count == 0)
                return GeneralResult<OrderDto>.Failure("Cart is empty");

            // validate stock 
            foreach (var ci in cart.CartItems)
            {
                if (ci.Product == null)
                    return GeneralResult<OrderDto>.Failure("Product not found");
                if (ci.Quantity <= 0)
                    return GeneralResult<OrderDto>.Failure($"Invalid quantity for product {ci.Product.Name}");
                if (ci.Quantity > ci.Product.ProductsInStock)
                    return GeneralResult<OrderDto>.Failure($"Insufficient stock for product {ci.Product.Name}");
            }
            Order order = new Order
            {
                UserId = dto.UserId,
                OrderDate = DateTime.UtcNow,
                Status = OrderStatus.Pending,
            };

            foreach (var item in cart.CartItems)
            {
                item.Product.ProductsInStock -= item.Quantity; // reduce stock

                order.OrderItems.Add(new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.Product.Price
                });
            }
            order.TotalAmount = cart.CartItems.Sum(ci => (ci.Product?.Price ?? 0M) * ci.Quantity);

            await _unitOfWork.OrdersRepository.AddAsync(order);
            cart.CartItems.Clear();
            await _unitOfWork.SaveAsync();

            var orderDto = new OrderDto
            {
                Id = order.Id,
                UserId = order.UserId,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                Status = order.Status,
                OrderItems = order.OrderItems.Select(oi => new OrderItemDto
                {
                    ProductId = oi.ProductId,
                    Quantity = oi.Quantity,
                    UnitPrice = oi.UnitPrice
                }).ToList()
            };

            return GeneralResult<OrderDto>.Success(orderDto, "Order created successfully");  
        }

        public async Task<GeneralResult<OrderDto>> GetOrderByIdAsync(int orderId)
        {
            var order = await _unitOfWork.OrdersRepository.GetOrderDetailsAsync(orderId);
            if (order == null)
                return GeneralResult<OrderDto>.NotFound("Order not found");

            var orderDto = new OrderDto
            {
                Id = order.Id,
                UserId = order.UserId,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                Status = order.Status,
                OrderItems = order.OrderItems.Select(oi => new OrderItemDto
                {
                    ProductId = oi.ProductId,
                    Quantity = oi.Quantity,
                    UnitPrice = oi.UnitPrice
                }).ToList()
            };

            return GeneralResult<OrderDto>.Success(orderDto, "Order retrieved successfully");
        }

        public async Task<GeneralResult<IEnumerable<OrderDto>>> GetOrdersByUserIdAsync(string userId)
        {
            var orders = await _unitOfWork.OrdersRepository.GetOrdersByUserIdAsync(userId);
            if (orders == null || !orders.Any())
                return GeneralResult<IEnumerable<OrderDto>>.NotFound("No orders found for the user");

            var orderDtos = orders.Select(order => new OrderDto
            {
                Id = order.Id,
                UserId = order.UserId,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                Status = order.Status,
                OrderItems = order.OrderItems.Select(oi => new OrderItemDto
                {
                    ProductId = oi.ProductId,
                    Quantity = oi.Quantity,
                    UnitPrice = oi.UnitPrice
                }).ToList()
            }).ToList();

            return GeneralResult<IEnumerable<OrderDto>>.Success(orderDtos, "Orders retrieved successfully");
        }
    }
}
