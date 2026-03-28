namespace Tema_3.Orders;

public class Order
{
    public int Id { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public decimal Total { get; private set; }
    public string Status { get; private set; }
    public List<OrderItem> Items { get; private set; } = new();
    public Order() { }

    public Order(decimal total, List<OrderItem> items)
    {
        CreatedAt = DateTime.UtcNow;
        Total = total;
        Status = "Pending";
        Items = items;
    }

    public void Complete() => Status = "Completed";

    public override string ToString() =>
        $"Order #{Id} | {CreatedAt:dd/MM/yyyy} | Total: {Total:C} | Status: {Status}";
}