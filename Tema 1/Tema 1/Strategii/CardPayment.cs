using ECommerce.Interfete;

namespace ECommerce.Strategii;

public class CardPayment : IPaymentStrategy
{
    public string Name => "Credit Card";

    public bool ProcessPayment(decimal amount)
    {
        return true;
    }
}

// Metoda proceseaza plata folosind cardul bancar (in proiect simulata ca fiind mereu reusita)