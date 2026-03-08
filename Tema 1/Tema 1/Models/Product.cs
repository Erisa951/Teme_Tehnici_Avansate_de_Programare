namespace ECommerce.Models;

public class Product
{
    public int Id { get; private set; }
    public string Name { get; private set; }

    private decimal _price;

    public decimal Price
    {
        get => _price;
        private set
        {
            if (value < 0) throw new ArgumentException("Pret negativ!");
            _price = value;
        }
    }

    public Product(int id, string name, decimal price)
    {
        Id = id;
        Name = name;
        Price = price;
    }

    public void UpdatePrice(decimal newPrice)
    {
        Price = newPrice;
    }
}

// Reprezintă un produs cu id, nume și preț, asigurând validarea prețului.