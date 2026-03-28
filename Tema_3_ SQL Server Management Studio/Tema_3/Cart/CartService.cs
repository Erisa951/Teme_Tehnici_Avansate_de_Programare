using Tema3.Products;

namespace Tema_3.Cart;

public class CartService
{
    private readonly ICartRepository _cartRepository;
    private readonly IProductRepository _productRepository;

    public CartService(ICartRepository cartRepository, IProductRepository productRepository)
    {
        _cartRepository = cartRepository;
        _productRepository = productRepository;
    }

    public async Task<ShoppingCart> GetCartAsync()
    {
        return await _cartRepository.GetOrCreateCartAsync();
    }

    public async Task AddToCartAsync(int productId, int quantity)
    {
        var product = await _productRepository.GetByIdAsync(productId);
        if (product == null)
            throw new InvalidOperationException("Product not found.");

        if (!product.IsAvailable(quantity))
            throw new InvalidOperationException("Insufficient stock.");

        var cart = await _cartRepository.GetOrCreateCartAsync();
        var item = new CartItem(productId, quantity);
        await _cartRepository.AddItemAsync(cart.Id, item);
    }

    public async Task RemoveFromCartAsync(int productId)
    {
        var cart = await _cartRepository.GetOrCreateCartAsync();
        await _cartRepository.RemoveItemAsync(cart.Id, productId);
    }

    public async Task<decimal> GetCartTotalAsync()
    {
        var cart = await _cartRepository.GetOrCreateCartAsync();
        return cart.GetTotal();
    }
}