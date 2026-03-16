using ECommerce.Interfete;

namespace ECommerce.Strategii;

public class MinimumOrderDiscount : IDiscountStrategy
{
    private decimal _minimum;
    private decimal _percentage;

    public decimal Minimum => _minimum;
    public decimal Percentage => _percentage;

    public MinimumOrderDiscount(decimal minimum, decimal percentage)
    {
        _minimum = minimum;
        _percentage = percentage;
    }

    public decimal ApplyDiscount(decimal amount, int itemCount)
    {
        if (amount >= _minimum)
        {
            return amount - (amount * _percentage / 100);
        }

        return amount;
    }
}

// Aplica un discount procentual daca valoarea comenzii depaseste suma minima stabilita
