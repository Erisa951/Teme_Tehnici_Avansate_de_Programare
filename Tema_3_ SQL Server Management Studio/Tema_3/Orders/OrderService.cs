using Tema_3.Cart;
using Tema_3.Data;
using Tema3.Products;

namespace Tema_3.Orders;

public class OrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly ICartRepository _cartRepository;
    private readonly IProductRepository _productRepository;
    private readonly ShoppingDbContext _context;

    public OrderService(
        IOrderRepository orderRepository,
        ICartRepository cartRepository,
        IProductRepository productRepository,
        ShoppingDbContext context)
    {
        _orderRepository = orderRepository;
        _cartRepository = cartRepository;
        _productRepository = productRepository;
        _context = context;
    }

    public async Task<Order> CheckoutAsync(int cartId)
    {
        var cart = await _cartRepository.GetCartWithItemsAsync(cartId);

        if (cart == null || cart.IsEmpty())
            throw new InvalidOperationException("Cart is empty.");

        await using var transaction = await _context.Database.BeginTransactionAsync();

        await ReduceStockForItemsAsync(cart.Items);

        var order = CreateOrder(cart);
        await _orderRepository.AddAsync(order);

        cart.Clear();
        await _cartRepository.UpdateAsync(cart);

        await transaction.CommitAsync();

        return order;
    }

    public async Task<IEnumerable<Order>> GetAllOrdersAsync()
    {
        return await _orderRepository.GetAllAsync();
    }

    private async Task ReduceStockForItemsAsync(IEnumerable<CartItem> items)
    {
        foreach (var item in items)
        {
            var product = await _productRepository.GetByIdAsync(item.ProductId);

            if (product == null)
                throw new InvalidOperationException($"Product {item.ProductId} not found.");

            product.ReduceStock(item.Quantity);
            await _productRepository.UpdateAsync(product);
        }
    }

    private Order CreateOrder(ShoppingCart cart)
    {
        var orderItems = cart.Items
            .Select(i => new OrderItem(i.ProductId, i.Quantity, i.Product.Price))
            .ToList();

        var order = new Order(cart.GetTotal(), orderItems);
        order.Complete();
        return order;
    }
}