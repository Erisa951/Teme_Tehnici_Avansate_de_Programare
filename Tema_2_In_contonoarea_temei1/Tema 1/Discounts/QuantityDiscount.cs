using System.Linq;

namespace Tema_1.Discounts;

public class QuantityDiscount : IDiscountStrategy
{
    public int MinItems { get; }
    public decimal Percentage { get; }

    public QuantityDiscount(int minItems, decimal percentage)
    {
        if (minItems <= 0)
            throw new ArgumentException("Minimum items must be greater than 0.");

        if (percentage < 0 || percentage > 100)
            throw new ArgumentException("Invalid percentage value.");

        MinItems = minItems;
        Percentage = percentage;
    }

    public decimal ApplyDiscount(decimal amount, int itemCount)
    {
        var applicable =
            (from i in new[] { itemCount }
             where i >= MinItems
             select i).Any();

        if (!applicable)
            return amount;

        var discount = new[] { amount }
            .Select(a => a * Percentage / 100)
            .First();

        return amount - discount;
    }
}