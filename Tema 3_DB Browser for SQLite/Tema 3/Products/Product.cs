namespace Tema3.Products;

public class Product
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public int Stock { get; private set; }
    public string Category { get; private set; }

    public Product(string name, decimal price, int stock, string category)
    {
        Name = name;
        Price = price;
        Stock = stock;
        Category = category;
    }

    public void UpdatePrice(decimal newPrice)
    {
        if (newPrice <= 0)
            throw new ArgumentException("Price must be positive.");
        Price = newPrice;
    }

    public void ReduceStock(int quantity)
    {
        if (quantity > Stock)
            throw new InvalidOperationException("Insufficient stock.");
        Stock -= quantity;
    }

    public bool IsAvailable(int quantity) => Stock >= quantity;

    public override string ToString() =>
        $"{Name} | {Price:C} | Stock: {Stock} | Category: {Category}";
}