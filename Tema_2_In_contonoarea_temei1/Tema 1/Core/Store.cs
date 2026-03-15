using Tema_1.Catalog;
using Tema_1.Discounts;
using Tema_1.Payments;
using Tema_1.Shipping;

namespace Tema_1.Core;

public class Store
{
    public IProductCatalog Catalog { get; }
    public IPaymentService PaymentService { get; }
    public IShippingService ShippingService { get; }
    public IDiscountService DiscountService { get; }
    public ProductSearchService SearchService { get; }

    public Store(
        IProductCatalog catalog,
        IPaymentService paymentService,
        IShippingService shippingService,
        IDiscountService discountService)
    {
        Catalog = catalog;
        PaymentService = paymentService;
        ShippingService = shippingService;
        DiscountService = discountService;

        SearchService = new ProductSearchService(catalog);
    }
}