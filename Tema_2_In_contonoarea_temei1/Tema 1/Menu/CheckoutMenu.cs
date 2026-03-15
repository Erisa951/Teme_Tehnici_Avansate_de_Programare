using System;
using System.Linq;
using Tema_1.Core;
using Tema_1.Order;
using Tema_1.Payments;
using Tema_1.Shipping;
using Tema_1.Users.Customer;

namespace Tema_1.Menu;

public class CheckoutMenu
{
    private readonly Store _store;
    private readonly OrderService _orderService;
    private readonly InputService _input;

    public CheckoutMenu(Store store, OrderService orderService, InputService input)
    {
        _store = store;
        _orderService = orderService;
        _input = input;
    }

    public void ChooseShippingAndPayment(Customer customer)
    {
        var shipping = ChooseShippingMethod(customer);

        decimal productsTotal = customer.Cart.GetTotalPrice();
        decimal shippingCost = shipping.CalculateShippingCost(productsTotal);

        Console.WriteLine($"\nShipping cost: {shippingCost} RON");

        var payment = ChoosePaymentMethod();

        var order = _orderService.CreateOrder(customer.Cart, payment, shipping);

        Console.WriteLine($"\nOrder created: {order.Id}");
        Console.WriteLine($"Products: {productsTotal} RON");
        Console.WriteLine($"Shipping: {shippingCost} RON");
        Console.WriteLine($"Final total: {productsTotal + shippingCost} RON");
    }

    private IShippingStrategy ChooseShippingMethod(Customer customer)
    {
        var shippingMethods = _store.ShippingService.GetAvailableMethods();

        var couriers = shippingMethods
            .GroupBy(s => s.GetCourierName().Split(" - ")[0])
            .ToList();

        Console.WriteLine("\nChoose courier:");

        for (int i = 0; i < couriers.Count; i++)
            Console.WriteLine($"{i} - {couriers[i].Key}");

        int courierChoice = _input.ReadInt("Option:");

        var methods = couriers[courierChoice].ToList();

        Console.WriteLine("\nChoose delivery method:");

        for (int i = 0; i < methods.Count; i++)
        {
            decimal cost = methods[i].CalculateShippingCost(customer.Cart.GetTotalPrice());
            var name = methods[i].GetCourierName().Split(" - ")[1];

            Console.WriteLine($"{i} - {name} - {cost} RON");
        }

        int methodChoice = _input.ReadInt("Option:");

        return methods[methodChoice];
    }

    private IPaymentStrategy ChoosePaymentMethod()
    {
        Console.WriteLine("\nPayment methods:");
        Console.WriteLine("1 - Card");
        Console.WriteLine("2 - PayPal");
        Console.WriteLine("3 - ApplePay");

        var choice = _input.ReadString("Option:");

        return choice switch
        {
            "2" => new PayPalPayment(),
            "3" => new ApplePayPayment(),
            _ => new CardPayment()
        };
    }
}