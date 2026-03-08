using ECommerce.Interfete;

namespace ECommerce.Servicii;

// Serviciu responsabil pentru gestionarea metodelor de plată disponibile
public class PaymentService : IPaymentService
{
    // Lista internă care stochează strategiile de plată înregistrate
    private readonly List<IPaymentStrategy> _paymentMethods = new();

    // Metodă care înregistrează (adauga) o nouă metodă de plată în sistem
    // Folosește interfața IPaymentStrategy pentru a permite mai multe tipuri de plată
    public void RegisterPaymentMethod(IPaymentStrategy payment)
    {
        _paymentMethods.Add(payment);
    }

    // Returnează metodele de plată disponibile
    public IReadOnlyList<IPaymentStrategy> GetAvailableMethods()
    {
        return _paymentMethods;
    }
}