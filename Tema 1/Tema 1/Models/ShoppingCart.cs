using ECommerce.Interfete;

namespace ECommerce.Models;

public class ShoppingCart
{
    private List<CartItem> _items = new();
    private IDiscountStrategy _discount;

    public ShoppingCart(IDiscountStrategy discount)
    {
        _discount = discount;
    }

    public void Add(Product product, int quantity)
    {
        _items.Add(new CartItem(product, quantity));
    }

    public void SetDiscount(IDiscountStrategy discount)
    {
        _discount = discount;
    }

    public decimal GetTotal()
    {
        decimal total = _items.Sum(i => i.GetTotal());
        return _discount.ApplyDiscount(total, GetTotalQuantity());
    }

    public int GetTotalQuantity()
    {
        return _items.Sum(i => i.Quantity);
    }

    public List<CartItem> Items => _items;

    public List<CartItem> GetItems()
    {
        return _items;
    }
}

// Reprezintă coșul de cumpărături și gestionează produsele,
// cantitatea totală și aplicarea reducerilor prin Strategy Pattern.