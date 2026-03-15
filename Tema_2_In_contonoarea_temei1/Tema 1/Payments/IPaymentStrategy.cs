using System.Linq;

namespace Tema_1.Payments;

public interface IPaymentStrategy
{
    string Name { get; }

    bool ProcessPayment(decimal amount);

    bool CanProcess(decimal amount)
        => (from a in new[] { amount }
            where a >= 0
            select true).Any();
}