using System.Linq;
using Tema_1.Discounts;

namespace Tema_1.Catalog;

public class ShoppingCart
{
    private readonly List<CartItem> _items = new();
    private IDiscountStrategy _discount;

    public ShoppingCart(IDiscountStrategy discount)
    {
        _discount = discount;
    }

    public void AddItem(Product product, int quantity)
    {
        var existing =
            (from i in _items
             where i.Product.Id == product.Id
             select i).FirstOrDefault();

        if (existing != null)
        {
            _items.Remove(existing);
            _items.Add(new CartItem(product, existing.Quantity + quantity));
        }
        else
        {
            _items.Add(new CartItem(product, quantity));
        }
    }

    public void SetDiscount(IDiscountStrategy discount)
    {
        _discount = discount;
    }

    public decimal GetTotalPrice()
    {
        var total =
            (from item in _items
             select item.GetTotalPrice()).Sum();

        return _discount.ApplyDiscount(total, GetTotalQuantity());
    }

    public int GetTotalQuantity()
    {
        return _items.Sum(i => i.Quantity);
    }

    public CartItem? GetMostExpensiveItem()
    {
        return _items
            .OrderByDescending(i => i.GetTotalPrice())
            .FirstOrDefault();
    }

    public List<CartItem> GetItemsWithQuantityGreaterThan(int quantity)
    {
        return _items
            .Where(i => i.Quantity > quantity)
            .ToList();
    }

    public IReadOnlyList<CartItem> Items => _items;
}