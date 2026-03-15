using System.Linq;

namespace Tema_1.Discounts;

public interface IDiscountStrategy
{
    decimal ApplyDiscount(decimal amount, int itemCount);

    bool IsApplicable(decimal amount, int itemCount)
        => (from x in new[] { (amount, itemCount) }
            where x.amount > 0 && x.itemCount > 0
            select x).Any();
}