using System.Linq;
using Tema_1.Catalog;

namespace Tema_1.Order;

public class Order
{
    private static int _nextId = 1;

    public int Id { get; }
    public List<CartItem> Items { get; }
    public decimal Total { get; }

    public Order(List<CartItem> items, decimal total)
    {
        Id = _nextId++;
        Items = (from item in items
                 select item).ToList();
        Total = total;
    }

    public int GetTotalQuantity()
    {
        return Items.Sum(i => i.Quantity);
    }
}