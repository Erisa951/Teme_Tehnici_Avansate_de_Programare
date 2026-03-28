using Tema3.Products;

namespace Tema_3.Cart;

public class CartItem
{
    public int Id { get; private set; }
    public int ProductId { get; private set; }
    public Product Product { get; private set; }
    public int Quantity { get; private set; }
    public int CartId { get; private set; }

    public CartItem() { }

    public CartItem(int productId, int quantity)
    {
        ProductId = productId;
        Quantity = quantity;
    }

    public void UpdateQuantity(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be positive.");
        Quantity = quantity;
    }

    public decimal GetTotal() => Product.Price * Quantity;
}