namespace ECommerce.Interfete;

public interface IPaymentStrategy
{
    string Name { get; }
    bool ProcessPayment(decimal amount);
}
// Definește metodele necesare pentru o strategie de plată (nume și procesarea plății).