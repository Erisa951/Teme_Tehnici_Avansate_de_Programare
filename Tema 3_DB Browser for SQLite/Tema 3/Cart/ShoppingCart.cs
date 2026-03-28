namespace Tema_3.Cart;

public class ShoppingCart
{
    public int Id { get; private set; }
    public List<CartItem> Items { get; private set; } = new();

    public ShoppingCart() { }

    public void AddItem(CartItem item)
    {
        var existing = Items.FirstOrDefault(i => i.ProductId == item.ProductId);
        if (existing != null)
            existing.UpdateQuantity(existing.Quantity + item.Quantity);
        else
            Items.Add(item);
    }

    public void RemoveItem(int productId)
    {
        var item = Items.FirstOrDefault(i => i.ProductId == productId);
        if (item != null)
            Items.Remove(item);
    }

    public decimal GetTotal() => Items.Sum(i => i.GetTotal());

    public bool IsEmpty() => !Items.Any();

    public void Clear() => Items.Clear();
}