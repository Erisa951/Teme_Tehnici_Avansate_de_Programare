using System.Linq;

namespace Tema_1.Payments;

public class PayPalPayment : IPaymentStrategy
{
    public string Name => "PayPal";

    public bool ProcessPayment(decimal amount)
    {
        var result =
            (from a in new[] { amount }
             where a >= 0
             select true).FirstOrDefault();

        return result;
    }
}