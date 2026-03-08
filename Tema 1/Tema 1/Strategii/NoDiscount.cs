using ECommerce.Interfete;

namespace ECommerce.Strategii;

public class NoDiscount : IDiscountStrategy
{
    public decimal ApplyDiscount(decimal amount, int itemCount)
    {
        return amount;
    }
}

// Strategie de discount care nu aplica nicio reducere si returneaza suma initiala