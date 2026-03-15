using Tema_1.Catalog;

namespace Tema_1.Catalog;

public class ProductSearchService
{
    private readonly IProductCatalog _catalog;

    public ProductSearchService(IProductCatalog catalog)
    {
        _catalog = catalog;
    }

    public IEnumerable<Product> SearchProducts(ProductFilter filter)
    {
        if (filter == null)
            throw new ArgumentNullException(nameof(filter));

        return _catalog.Search(filter);
    }

    public IEnumerable<IGrouping<string, Product>> GetProductsGrouped()
    {
        return _catalog.GroupByCategory();
    }
}