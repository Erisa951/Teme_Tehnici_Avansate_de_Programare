using System.Linq;

namespace Tema_1.Discounts;

public class DiscountService : IDiscountService
{
    private readonly List<IDiscountStrategy> _discounts = new();

    public void RegisterDiscount(IDiscountStrategy discount)
    {
        var exists = _discounts.Any(d => d == discount);

        if (!exists)
            _discounts.Add(discount);
    }

    public IReadOnlyList<IDiscountStrategy> GetAvailableDiscounts()
    {
        var query =
            from d in _discounts
            select d;

        return query.ToList();
    }

    public IDiscountStrategy? FindDiscount(Func<IDiscountStrategy, bool> predicate)
    {
        return _discounts.FirstOrDefault(predicate);
    }
}