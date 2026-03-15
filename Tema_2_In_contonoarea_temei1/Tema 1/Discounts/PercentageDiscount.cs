using System.Linq;

namespace Tema_1.Discounts;

public class PercentageDiscount : IDiscountStrategy
{
    public string Code { get; }
    public decimal Percentage { get; }

    public PercentageDiscount(string code, decimal percentage)
    {
        if (percentage < 0 || percentage > 100)
            throw new ArgumentException("Invalid percentage value.");

        Code = code;
        Percentage = percentage;
    }

    public decimal ApplyDiscount(decimal amount, int itemCount)
    {
        var discount =
            (from a in new[] { amount }
             select a * Percentage / 100).First();

        return new[] { amount - discount }
            .Select(v => v)
            .First();
    }
}