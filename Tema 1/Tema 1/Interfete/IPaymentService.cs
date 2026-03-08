namespace ECommerce.Interfete;

public interface IPaymentService
{
    void RegisterPaymentMethod(IPaymentStrategy payment);
}
// Definește metoda pentru înregistrarea unei strategii de plată în sistem.