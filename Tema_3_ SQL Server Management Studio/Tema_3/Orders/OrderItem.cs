using Tema3.Products;

namespace Tema_3.Orders;

public class OrderItem
{
    public int Id { get; private set; }
    public int ProductId { get; private set; }
    public Product Product { get; private set; }
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }
    public int OrderId { get; private set; }

    public OrderItem() { }

    public OrderItem(int productId, int quantity, decimal unitPrice)
    {
        ProductId = productId;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }

    public decimal GetTotal() => UnitPrice * Quantity;
}