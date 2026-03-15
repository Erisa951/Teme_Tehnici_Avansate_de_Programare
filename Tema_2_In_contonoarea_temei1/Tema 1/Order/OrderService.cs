using System.Linq;
using Tema_1.Catalog;
using Tema_1.Payments;
using Tema_1.Shipping;

namespace Tema_1.Order;

public class OrderService
{
    public Order CreateOrder(ShoppingCart cart, IPaymentStrategy payment, IShippingStrategy shipping)
    {
        var items =
            (from item in cart.Items
             select item).ToList();

        decimal total = items.Sum(i => i.GetTotalPrice());

        decimal shippingCost = shipping.CalculateShippingCost(total);

        decimal finalTotal = new[] { total, shippingCost }
            .Aggregate((acc, x) => acc + x);

        payment.ProcessPayment(finalTotal);

        var order = new Order(items, finalTotal);

        return order;
    }
}