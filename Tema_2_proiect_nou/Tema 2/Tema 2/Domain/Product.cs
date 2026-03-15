namespace Tema_2.Domain;

public class Product
{
    public int Id { get; }
    public string Name { get; }
    public string Category { get; }
    public decimal Price { get; private set; }

    public Product(int id, string name, string category, decimal price)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name is required");

        if (string.IsNullOrWhiteSpace(category))
            throw new ArgumentException("Category is required");

        if (price < 0)
            throw new ArgumentException("Price must be non-negative");

        Id = id;
        Name = name;
        Category = category;
        Price = price;
    }

    public void UpdatePrice(decimal newPrice)
    {
        if (newPrice < 0)
            throw new ArgumentException("Invalid price");

        Price = newPrice;
    }
}