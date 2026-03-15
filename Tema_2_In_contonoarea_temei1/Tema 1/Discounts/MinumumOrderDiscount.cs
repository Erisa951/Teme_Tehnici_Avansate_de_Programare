using System.Linq;

namespace Tema_1.Discounts;

public class MinimumOrderDiscount : IDiscountStrategy
{
    public decimal Minimum { get; }
    public decimal Percentage { get; }

    public MinimumOrderDiscount(decimal minimum, decimal percentage)
    {
        Minimum = minimum;
        Percentage = percentage;
    }

    public decimal ApplyDiscount(decimal amount, int itemCount)
    {
        var applicable =
            (from x in new[] { amount }
             where x >= Minimum
             select x).Any();

        if (!applicable)
            return amount;

        var discount = new[] { amount }
            .Select(a => a * Percentage / 100)
            .First();

        return amount - discount;
    }
}