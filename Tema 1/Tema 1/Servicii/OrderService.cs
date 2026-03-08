using ECommerce.Models;
using ECommerce.Interfete;

namespace ECommerce.Servicii;

// Serviciu responsabil pentru crearea și procesarea comenzilor
public class OrderService
{
    // Creează o comandă pe baza coșului, metodei de plată și metodei de livrare
    public Order CreateOrder(ShoppingCart cart, IPaymentStrategy payment, IShippingStrategy shipping)
    {
        // Calculează totalul produselor din coș (după eventualele discounturi)
        decimal total = cart.GetTotal();

        // Calculează costul transportului folosind strategia de livrare
        decimal shippingCost = shipping.CalculateShippingCost(total);

        // Procesează plata folosind strategia de plată aleasă
        payment.ProcessPayment(total + shippingCost);

        // Creează obiectul Order care conține produsele, totalul și metoda de livrare
        var order = new Order(cart.Items, total + shippingCost, shipping);

        // Returnează comanda creată
        return order;
    }
}