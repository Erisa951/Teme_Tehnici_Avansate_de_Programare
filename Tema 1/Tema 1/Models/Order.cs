using ECommerce.Interfete;

namespace ECommerce.Models;

public class Order
{
    private static int _nextId = 1;

    public int Id { get; }
    public List<CartItem> Items { get; }
    public decimal Total { get; }
    public IShippingStrategy Shipping { get; }

    public Order(List<CartItem> items, decimal total, IShippingStrategy shipping)
    {
        Id = _nextId++;
        Total= total;
        Items = items;
        Shipping = shipping;
    }
}

// Reprezintă o comandă care conține produsele din coș, totalul și metoda de livrare.