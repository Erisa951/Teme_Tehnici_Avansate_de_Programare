using ECommerce.Interfete;

namespace ECommerce.Strategii;

public class QuantityDiscount : IDiscountStrategy
{
    private int _minItems;
    private decimal _percentage;

    public int MinItems => _minItems;
    public decimal Percentage => _percentage;

    public QuantityDiscount(int minItems, decimal percentage)
    {
        _minItems = minItems;
        _percentage = percentage;
    }

    public decimal ApplyDiscount(decimal amount, int itemCount)
    {
        if (itemCount >= _minItems)
        {
            return amount - (amount * _percentage / 100);
        }

        return amount;
    }
}

// Aplica un discount procentual daca numarul total de produse din cos depaseste limita minima stabilita
