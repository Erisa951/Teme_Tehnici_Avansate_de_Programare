using ECommerce.Interfete;

namespace ECommerce.Strategii;

public class PercentageDiscount : IDiscountStrategy
{
    public string Code { get; }
    private decimal _percentage;

    public PercentageDiscount(string code, decimal percentage)
    {
        Code = code;
        _percentage = percentage;
    }

    public decimal ApplyDiscount(decimal amount, int itemCount)
    {
        return amount - (amount * _percentage / 100);
    }
}

// Aplica un discount procentual asupra valorii totale a comenzii, folosind un cod de reducere