using System.Linq;

namespace Tema_1.Catalog;

public class ProductCatalog : IProductCatalog
{
    private readonly List<Product> _products = new();

    public void SeedProducts()
    {
        AddProduct(new Product(1, "Laptop", "Electronics", 3500));
        AddProduct(new Product(2, "Mouse", "Electronics", 100));
        AddProduct(new Product(3, "Keyboard", "Electronics", 200));
        AddProduct(new Product(4, "Desk", "Furniture", 800));
        AddProduct(new Product(5, "Chair", "Furniture", 400));
        AddProduct(new Product(6, "Monitor", "Electronics", 1200));
        AddProduct(new Product(7, "Lamp", "Furniture", 150));
    }

    public void AddProduct(Product product)
    {
        if (!_products.Any(p => p.Id == product.Id))
            _products.Add(product);
    }

    public void RemoveProduct(int id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);

        if (product != null)
            _products.Remove(product);
    }

    public void UpdatePrice(int id, decimal newPrice)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);

        if (product != null)
            product.UpdatePrice(newPrice);
    }

    public List<Product> GetAllProducts()
    {
        return _products.ToList();
    }

    public Product? FindById(int id)
    {
        return _products.FirstOrDefault(p => p.Id == id);
    }

    public IEnumerable<Product> Search(ProductFilter filter)
    {
        var query = _products.AsEnumerable();

        if (!string.IsNullOrWhiteSpace(filter.Category))
            query = query.Where(p => p.Category == filter.Category);

        if (filter.MinPrice.HasValue)
            query = query.Where(p => p.Price >= filter.MinPrice);

        if (filter.MaxPrice.HasValue)
            query = query.Where(p => p.Price <= filter.MaxPrice);

        if (filter.OrderBy.HasValue)
        {
            query = filter.OrderBy switch
            {
                ProductOrderBy.Price => filter.Descending
                    ? query.OrderByDescending(p => p.Price)
                    : query.OrderBy(p => p.Price),

                ProductOrderBy.Name => filter.Descending
                    ? query.OrderByDescending(p => p.Name)
                    : query.OrderBy(p => p.Name),

                _ => query
            };
        }

        return query;
    }

    public IEnumerable<IGrouping<string, Product>> GroupByCategory()
    {
        return _products.GroupBy(p => p.Category);
    }

    public List<Product> GetProductsCheaperThan(decimal price)
    {
        return _products
            .Where(p => p.Price < price)
            .ToList();
    }

    public Product? GetMostExpensiveProduct()
    {
        return _products
            .OrderByDescending(p => p.Price)
            .FirstOrDefault();
    }

    public List<string> GetProductNames()
    {
        return _products
            .Select(p => p.Name)
            .ToList();
    }

    public decimal GetAveragePrice()
    {
        return _products.Average(p => p.Price);
    }
}