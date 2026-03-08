using ECommerce.Interfete;

namespace ECommerce.Strategii;

public class PayPalPayment : IPaymentStrategy
{
    public string Name => "PayPal";

    public bool ProcessPayment(decimal amount)
    {
        return true; 
    }
}

// Metoda proceseaza plata folosind PayPal (in proiect simulata ca fiind mereu reusita)