using Microsoft.EntityFrameworkCore;
using Tema_3.Data;

namespace Tema_3.Cart;

public class CartRepository : ICartRepository
{
    private readonly ShoppingDbContext _context;

    public CartRepository(ShoppingDbContext context)
    {
        _context = context;
    }

    public async Task<ShoppingCart?> GetCartWithItemsAsync(int cartId)
    {
        return await _context.Carts
            .Include(c => c.Items)
            .ThenInclude(i => i.Product)
            .FirstOrDefaultAsync(c => c.Id == cartId);
    }

    public async Task<ShoppingCart> GetOrCreateCartAsync()
    {
        var cart = await _context.Carts
            .Include(c => c.Items)
            .ThenInclude(i => i.Product)
            .FirstOrDefaultAsync();

        if (cart == null)
        {
            cart = new ShoppingCart();
            await _context.Carts.AddAsync(cart);
            await _context.SaveChangesAsync();
        }

        return cart;
    }

    public async Task AddItemAsync(int cartId, CartItem item)
    {
        var cart = await GetCartWithItemsAsync(cartId);
        if (cart == null)
            throw new InvalidOperationException("Cart not found.");

        cart.AddItem(item);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveItemAsync(int cartId, int productId)
    {
        var cart = await GetCartWithItemsAsync(cartId);
        if (cart == null)
            throw new InvalidOperationException("Cart not found.");

        cart.RemoveItem(productId);
        await _context.SaveChangesAsync();
    }

    public async Task ClearCartAsync(int cartId)
    {
        var cart = await GetCartWithItemsAsync(cartId);
        if (cart == null)
            throw new InvalidOperationException("Cart not found.");

        cart.Clear();
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(ShoppingCart cart)
    {
        await _context.SaveChangesAsync();
    }
}