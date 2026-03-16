using Tema_2.Domain;

namespace Tema_2.Catalog;

public class ProductCatalog : IProductCatalog
{
    private readonly List<Product> _products = new();

    public void Add(Product product)
    {
        _products.Add(product);
    }

    public IReadOnlyList<Product> GetAll()
    {
        return _products;
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
        var query =
            from product in _products
            group product by product.Category;

        return query;
    }
}