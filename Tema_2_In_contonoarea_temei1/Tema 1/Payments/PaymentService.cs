using System.Linq;

namespace Tema_1.Payments;

public class PaymentService : IPaymentService
{
    private readonly List<IPaymentStrategy> _paymentMethods = new();

    public void RegisterPaymentMethod(IPaymentStrategy payment)
    {
        if (!_paymentMethods.Any(p => p.Name == payment.Name))
            _paymentMethods.Add(payment);
    }

    public IReadOnlyList<IPaymentStrategy> GetAvailableMethods()
    {
        var query =
            (from p in _paymentMethods
             select p).ToList();

        return query.AsReadOnly();
    }
}