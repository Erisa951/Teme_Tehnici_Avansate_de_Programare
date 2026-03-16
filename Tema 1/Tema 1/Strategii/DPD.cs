using ECommerce.Interfete;
using ECommerce.Models;

namespace ECommerce.Strategii;

public class DPD : IShippingStrategy
{
    private DeliveryOption _selectedOption;

    public DPD(DeliveryOption option)
    {
        _selectedOption = option;
    }

    public decimal CalculateShippingCost(decimal total)
    {
        return _selectedOption.Cost;
    }

    public string GetCourierName()
    {
        return $"DPD - {_selectedOption.Name}";
    }

    public static List<DeliveryOption> GetAvailableOptions()
    {
        return new List<DeliveryOption>
        {
            new DeliveryOption("Domiciliu", 22),
            new DeliveryOption("Ridicare din depozit", 7)
        };
    }
}

// Clasa implementeaza strategia de livrare pentru curierul DPD si
// calculeaza costul transportului in functie de optiunea aleasa