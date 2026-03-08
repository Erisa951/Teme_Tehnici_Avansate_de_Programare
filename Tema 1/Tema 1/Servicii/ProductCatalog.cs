using ECommerce.Interfete;
using ECommerce.Models;

namespace ECommerce.Servicii;

// Clasa responsabilă pentru gestionarea produselor din catalogul magazinului
public class ProductCatalog : IProductCatalog
{
    // Lista internă în care sunt stocate produsele
    // Este privată pentru a proteja datele (encapsulare)
    private List<Product> _products = new();

    // Adaugă un produs nou în catalog
    public void AddProduct(Product product)
    {
        _products.Add(product);
    }

    // Șterge un produs din catalog după ID
    public void RemoveProduct(int id)
    {
        // Caută produsul în listă
        var product = _products.FirstOrDefault(p => p.Id == id);

        // Dacă produsul există, îl elimină
        if (product != null)
        {
            _products.Remove(product);
        }
    }

    // Modifică prețul unui produs
    public void UpdatePrice(int id, decimal newPrice)
    {
        // Caută produsul în listă
        var product = _products.FirstOrDefault(p => p.Id == id);

        // Dacă produsul există, actualizează prețul
        if (product != null)
        {
            // Metoda aparține clasei Product (încapsulare logică)
            product.UpdatePrice(newPrice);
        }
    }

    // Returnează toate produsele din catalog
    public List<Product> GetAllProducts()
    {
        // Returnează o copie a listei pentru a proteja lista originală
        return new List<Product>(_products);
    }

    // Caută și returnează un produs după ID
    public Product? FindById(int id)
    {
        return _products.FirstOrDefault(p => p.Id == id);
    }
}