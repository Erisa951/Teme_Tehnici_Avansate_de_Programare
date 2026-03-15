using System.Linq;

namespace Tema_1.Shipping;

public interface IShippingStrategy
{
    decimal CalculateShippingCost(decimal orderTotal);
    string GetCourierName();

    bool CanShip(decimal orderTotal)
        => (from total in new[] { orderTotal }
            where total >= 0
            select true).Any();
}