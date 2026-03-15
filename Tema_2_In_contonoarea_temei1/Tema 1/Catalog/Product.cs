namespace Tema_1.Catalog;

public class Product
{
    public int Id { get; }
    public string Name { get; }
    public string Category { get; }
    public decimal Price { get; private set; }

    public Product(int id, string name, string category, decimal price)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty");

        if (string.IsNullOrWhiteSpace(category))
            throw new ArgumentException("Category cannot be empty");

        if (price < 0)
            throw new ArgumentException("Price cannot be negative");

        Id = id;
        Name = name;
        Category = category;
        Price = price;
    }

    public void UpdatePrice(decimal newPrice)
    {
        if (newPrice < 0)
            throw new ArgumentException("Price cannot be negative");

        Price = newPrice;
    }
}