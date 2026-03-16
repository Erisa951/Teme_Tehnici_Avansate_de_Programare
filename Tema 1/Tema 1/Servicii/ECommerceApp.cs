using ECommerce.Models;
using ECommerce.Strategii;

namespace ECommerce.Servicii;

// Clasa principală a aplicației care inițializează serviciile și pornește aplicația
public class ECommerceApp
{
    // Referințe către componentele principale ale sistemului
    private Store _store;
    private OrderService _orderService;
    private InputService _input;

    // Constructorul inițializează toate serviciile aplicației
    public ECommerceApp()
    {
        // Creează magazinul și injectează serviciile principale
        _store = new Store(
            new ProductCatalog(),
            new PaymentService(),
            new ShippingService(),
            new DiscountService()
         );

        // Serviciu responsabil pentru crearea comenzilor
        _orderService = new OrderService();

        // Serviciu pentru citirea inputului din consolă
        _input = new InputService();
    }

    // Metoda principală care pornește aplicația
    public void Run()
    {
        try
        {
            // Populează magazinul cu produse, curieri, plăți și discounturi
            SeedStore();

            Console.WriteLine("Alege rolul:");
            Console.WriteLine("1 - Customer");
            Console.WriteLine("2 - Admin");

            // Citirea opțiunii utilizatorului
            var choice = _input.ReadString("Optiune:");

            // Dacă utilizatorul este client
            if (choice == "1")
            {
                var customerMenu = new CustomerMenuService(_store, _orderService, _input);
                customerMenu.Run();
            }
            // Dacă utilizatorul este admin
            else if (choice == "2")
            {
                AdminLogin();
            }
            else
            {
                throw new Exception("Optiune invalida.");
            }
        }
        catch (Exception ex)
        {
            // Tratarea erorilor generale ale aplicației
            Console.WriteLine(ex.Message);
        }
    }

    // Metodă pentru autentificarea administratorului
    private void AdminLogin()
    {
        var pass = _input.ReadString("Introdu parola admin:");

        // Verifică parola administratorului
        if (pass == "admin123")
        {
            var adminMenu = new AdminMenuService(_store, _input);
            adminMenu.Run();
        }
        else
        {
            Console.WriteLine("Parola gresita.");
        }
    }

    // Metodă care inițializează magazinul cu date de test (produse, plăți, curieri, discounturi)
    private void SeedStore()
    {
        // Adăugare produse în catalog
        _store.Admin.AddProduct(new Product(1, "Laptop", 3000));
        _store.Admin.AddProduct(new Product(2, "Mouse", 100));

        // Înregistrare metode de plată (Strategy Pattern)
        _store.Admin.AddPaymentMethod(new CardPayment());
        _store.Admin.AddPaymentMethod(new PayPalPayment());
        _store.Admin.AddPaymentMethod(new ApplePayPayment());

        // Înregistrare metode de livrare pentru FanCourier
        foreach (var option in FanCourier.GetAvailableOptions())
            _store.Admin.AddShippingMethod(new FanCourier(option));

        // Înregistrare metode de livrare pentru Sameday
        foreach (var option in Sameday.GetAvailableOptions())
            _store.Admin.AddShippingMethod(new Sameday(option));

        // Înregistrare metode de livrare pentru DPD
        foreach (var option in DPD.GetAvailableOptions())
            _store.Admin.AddShippingMethod(new DPD(option));

        // Înregistrare strategii de discount
        _store.Admin.AddDiscount(new PercentageDiscount("SAVE10", 10));
        _store.Admin.AddDiscount(new QuantityDiscount(3, 15));
        _store.Admin.AddDiscount(new MinimumOrderDiscount(500, 10));
    }
}