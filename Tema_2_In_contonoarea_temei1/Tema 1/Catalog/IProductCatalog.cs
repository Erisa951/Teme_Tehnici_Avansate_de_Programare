namespace Tema_1.Catalog;

public interface IProductCatalog
{
    void AddProduct(Product product);
    void RemoveProduct(int id);
    List<Product> GetAllProducts();
    Product? FindById(int id);
    IEnumerable<Product> Search(ProductFilter filter);
    IEnumerable<IGrouping<string, Product>> GroupByCategory();
}