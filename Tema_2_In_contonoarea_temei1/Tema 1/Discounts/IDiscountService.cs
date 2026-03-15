using System.Linq;

namespace Tema_1.Discounts;

public interface IDiscountService
{
    void RegisterDiscount(IDiscountStrategy discount);

    IReadOnlyList<IDiscountStrategy> GetAvailableDiscounts();

    IDiscountStrategy? FindDiscount(Func<IDiscountStrategy, bool> predicate);
}