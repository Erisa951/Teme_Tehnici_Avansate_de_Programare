using ECommerce.Interfete;

namespace ECommerce.Strategii;

public class ApplePayPayment : IPaymentStrategy
{
    public string Name => "Apple Pay";

    public bool ProcessPayment(decimal amount)
    {
        return true; 
    }
}
// Metoda proceseaza plata folosind Apple Pay (in proiect simulata ca fiind mereu reusita)
