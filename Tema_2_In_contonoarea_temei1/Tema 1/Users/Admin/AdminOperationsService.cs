using System.Linq;
using Tema_1.Catalog;
using Tema_1.Discounts;
using Tema_1.Payments;
using Tema_1.Shipping;

namespace Tema_1.Users.Admin;

public class AdminOperationsService
{
    private readonly IProductCatalog _catalog;
    private readonly IPaymentService _paymentService;
    private readonly IShippingService _shippingService;
    private readonly IDiscountService _discountService;

    public AdminOperationsService(
        IProductCatalog catalog,
        IPaymentService paymentService,
        IShippingService shippingService,
        IDiscountService discountService)
    {
        _catalog = catalog;
        _paymentService = paymentService;
        _shippingService = shippingService;
        _discountService = discountService;
    }

    public void AddProduct(Product product)
    {
        var exists = _catalog
            .GetAllProducts()
            .Any(p => p.Id == product.Id);

        if (exists)
            throw new Exception("Product with same ID already exists.");

        _catalog.AddProduct(product);
    }

    public void RemoveProduct(int id)
    {
        var product =
            (from p in _catalog.GetAllProducts()
             where p.Id == id
             select p).FirstOrDefault();

        if (product == null)
            throw new Exception("Product not found.");

        _catalog.RemoveProduct(id);
    }

    public void UpdateProductPrice(int id, decimal newPrice)
    {
        var product =
            (from p in _catalog.GetAllProducts()
             where p.Id == id
             select p).FirstOrDefault();

        if (product == null)
            throw new Exception("Product not found.");

        product.UpdatePrice(newPrice);
    }

    public void AddPaymentMethod(IPaymentStrategy payment)
    {
        _paymentService.RegisterPaymentMethod(payment);
    }

    public void AddShippingMethod(IShippingStrategy shipping)
    {
        _shippingService.RegisterShippingMethod(shipping);
    }

    public void AddDiscount(IDiscountStrategy discount)
    {
        _discountService.RegisterDiscount(discount);
    }
}