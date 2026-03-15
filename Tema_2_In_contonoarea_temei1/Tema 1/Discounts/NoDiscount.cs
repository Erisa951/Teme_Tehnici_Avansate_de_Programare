using System.Linq;

namespace Tema_1.Discounts;

public class NoDiscount : IDiscountStrategy
{
    public decimal ApplyDiscount(decimal amount, int itemCount)
    {
        var result =
            (from a in new[] { amount }
             select a).First();

        return result;
    }
}