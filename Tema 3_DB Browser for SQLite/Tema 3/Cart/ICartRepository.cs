namespace Tema_3.Cart;

public interface ICartRepository
{
    Task<ShoppingCart?> GetCartWithItemsAsync(int cartId);
    Task<ShoppingCart> GetOrCreateCartAsync();
    Task AddItemAsync(int cartId, CartItem item);
    Task RemoveItemAsync(int cartId, int productId);
    Task ClearCartAsync(int cartId);
    Task UpdateAsync(ShoppingCart cart);
}