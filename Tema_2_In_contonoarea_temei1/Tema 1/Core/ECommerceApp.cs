using System.Linq;
using Tema_1.Catalog;
using Tema_1.Payments;
using Tema_1.Shipping;
using Tema_1.Discounts;
using Tema_1.Order;
using Tema_1.Users.Admin;
using Tema_1.Users.Customer;
using Tema_1.Menu;

namespace Tema_1.Core;

public class ECommerceApp
{
    private readonly Store _store;
    private readonly OrderService _orderService;
    private readonly InputService _input;
    private readonly AdminOperationsService _adminOperations;
    private readonly ProductCatalog _productCatalog;

    public ECommerceApp()
    {
        _productCatalog = new ProductCatalog();

        var paymentService = new PaymentService();
        var shippingService = new ShippingService();
        var discountService = new DiscountService();

        _store = new Store(
            _productCatalog,
            paymentService,
            shippingService,
            discountService
        );

        _adminOperations = new AdminOperationsService(
            _productCatalog,
            paymentService,
            shippingService,
            discountService
        );

        _orderService = new OrderService();

        _input = new InputService();
    }

    public void Run()
    {
        try
        {
            SeedStore();

            var roles = new List<string> { "Customer", "Admin" };

            var menu =
                (from r in roles
                 select r).ToList();

            Console.WriteLine("Choose role:");

            menu
                .Select((role, index) => $"{index + 1} - {role}")
                .ToList()
                .ForEach(Console.WriteLine);

            var choice = _input.ReadString("Option:");

            if (choice == "1")
            {
                var customerMenu = new CustomerMenu(_store, _orderService, _input);
                customerMenu.Run();
            }
            else if (choice == "2")
            {
                AdminLogin();
            }
            else
            {
                throw new Exception("Invalid option.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private void AdminLogin()
    {
        var password = _input.ReadString("Enter admin password:");

        var validPasswords = new List<string> { "admin123" };

        if (validPasswords.Any(p => p == password))
        {
            var adminMenu = new AdminMenuService(_store, _adminOperations, _input);
            adminMenu.Run();
        }
        else
        {
            Console.WriteLine("Wrong password.");
        }
    }

    private void SeedStore()
    {
        _productCatalog.SeedProducts();

        new List<IPaymentStrategy>
        {
            new CardPayment(),
            new PayPalPayment(),
            new ApplePayPayment()
        }
        .ForEach(p => _adminOperations.AddPaymentMethod(p));

        FanCourier.GetAvailableOptions()
            .Select(option => new FanCourier(option))
            .ToList()
            .ForEach(s => _adminOperations.AddShippingMethod(s));

        Sameday.GetAvailableOptions()
            .Select(option => new Sameday(option))
            .ToList()
            .ForEach(s => _adminOperations.AddShippingMethod(s));

        DPD.GetAvailableOptions()
            .Select(option => new DPD(option))
            .ToList()
            .ForEach(s => _adminOperations.AddShippingMethod(s));

        new List<IDiscountStrategy>
        {
            new PercentageDiscount("SAVE10", 10),
            new QuantityDiscount(3, 15),
            new MinimumOrderDiscount(500, 10)
        }
        .ForEach(d => _adminOperations.AddDiscount(d));
    }
}