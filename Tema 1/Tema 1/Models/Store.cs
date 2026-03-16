using ECommerce.Interfete;

namespace ECommerce.Models;

// Reprezintă magazinul și conține serviciile principale ale aplicației
public class Store
{
    // Serviciile magazinului definite prin interfețe (pentru flexibilitate și SOLID)
    public IProductCatalog Catalog { get; }
    public IPaymentService PaymentService { get; }
    public IShippingService ShippingService { get; }
    public IDiscountService DiscountService { get; }

    // Utilizatorul admin care gestionează magazinul
    public Admin Admin { get; }

    // Constructorul primește serviciile necesare (Dependency Injection)
    public Store(
        IProductCatalog catalog,
        IPaymentService paymentService,
        IShippingService shippingService,
        IDiscountService discountService)
    {
        // Inițializează serviciile magazinului
        Catalog = catalog;
        PaymentService = paymentService;
        ShippingService = shippingService;
        DiscountService = discountService;

        // Creează utilizatorul Admin și îi transmite serviciile
        Admin = new Admin(
            1,
            "Admin",
            Catalog,
            PaymentService,
            ShippingService,
            DiscountService
        );
    }
}