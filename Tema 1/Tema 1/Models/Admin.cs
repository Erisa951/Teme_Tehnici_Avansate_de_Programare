using ECommerce.Servicii;
using ECommerce.Interfete;

namespace ECommerce.Models;

// Clasa Admin reprezintă utilizatorul cu drepturi de administrare
// și permite gestionarea produselor, metodelor de plată, livrare și reduceri
public class Admin : User
{
    private IProductCatalog _catalog;
    private IPaymentService _paymentService;
    private IShippingService _shippingService;
    private IDiscountService _discountService;

    // Constructorul primește dependențele necesare (Dependency Injection)
    public Admin(int id, string name,
                 IProductCatalog catalog,
                 IPaymentService paymentService,
                 IShippingService shippingService,
                 IDiscountService discountService)
        : base(id, name)
    {
        _catalog = catalog;
        _paymentService = paymentService;
        _shippingService = shippingService;
        _discountService = discountService;
    }

    // Returnează rolul utilizatorului
    public override string GetRole()
    {
        return "Admin";
    }

    // Adaugă un produs în catalog
    public void AddProduct(Product product)
    {
        _catalog.AddProduct(product);
    }

    // Elimină un produs după ID
    public void RemoveProduct(int id)
    {
        _catalog.RemoveProduct(id);
    }

    // Actualizează prețul unui produs
    public void UpdateProductPrice(int id, decimal newPrice)
    {
        _catalog.UpdatePrice(id, newPrice);
    }

    // Înregistrează o metodă nouă de plată
    public void AddPaymentMethod(IPaymentStrategy payment)
    {
        _paymentService.RegisterPaymentMethod(payment);
    }

    // Înregistrează o metodă de livrare
    public void AddShippingMethod(IShippingStrategy shipping)
    {
        _shippingService.RegisterShippingMethod(shipping);
    }

    // Adaugă o strategie de reducere
    public void AddDiscount(IDiscountStrategy discount)
    {
        _discountService.RegisterDiscount(discount);
    }
}