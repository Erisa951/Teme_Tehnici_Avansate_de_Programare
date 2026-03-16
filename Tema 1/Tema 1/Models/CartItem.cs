namespace ECommerce.Models;

public class CartItem
{
    public Product Product { get; }
    public int Quantity { get; }

    public CartItem(Product product, int quantity)
    {
        Product = product;
        Quantity = quantity;
    }

    public decimal GetTotal()
    {
        return Product.Price * Quantity;
    }
}
// Reprezintă un produs din coșul de cumpărături împreună cu cantitatea și permite calcularea totalului.