using ECommerce.Interfete;
using ECommerce.Models;

namespace ECommerce.Strategii;

public class FanCourier : IShippingStrategy
{
    private DeliveryOption _selectedOption;

    public FanCourier(DeliveryOption option)
    {
        _selectedOption = option;
    }

    public decimal CalculateShippingCost(decimal total)
    {
        return _selectedOption.Cost;
    }

    public string GetCourierName()
    {
        return $"Fan Courier - {_selectedOption.Name}";
    }

    public static List<DeliveryOption> GetAvailableOptions()
    {
        return new List<DeliveryOption>
        {
            new DeliveryOption("Domiciliu", 20),
            new DeliveryOption("Locker", 10),
            new DeliveryOption("Depozit", 5)
        };
    }
}

// Clasa implementeaza strategia de livrare pentru Fan Courier si
// returneaza costul transportului in functie de optiunea aleasa