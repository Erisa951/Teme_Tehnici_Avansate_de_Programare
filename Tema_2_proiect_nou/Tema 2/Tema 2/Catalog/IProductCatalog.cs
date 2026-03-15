using Tema_2.Domain;

namespace Tema_2.Catalog;

public interface IProductCatalog
{
    void Add(Product product);

    IReadOnlyList<Product> GetAll();

    IEnumerable<Product> Search(ProductFilter filter);

    IEnumerable<IGrouping<string, Product>> GroupByCategory();
}