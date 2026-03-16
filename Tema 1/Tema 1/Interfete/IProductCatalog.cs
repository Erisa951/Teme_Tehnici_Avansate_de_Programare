using ECommerce.Models;

namespace ECommerce.Interfete;

public interface IProductCatalog
{
    void AddProduct(Product product);
    void RemoveProduct(int id);
    void UpdatePrice(int id, decimal newPrice);
    List<Product> GetAllProducts();
    Product? FindById(int id);
}
// Definește operațiile pentru gestionarea produselor din catalogul magazinului.