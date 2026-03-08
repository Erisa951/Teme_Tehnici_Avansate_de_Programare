using ECommerce.Interfete;
using ECommerce.Models;

namespace ECommerce.Strategii;

public class Sameday : IShippingStrategy
{
    private DeliveryOption _selectedOption;

    public Sameday(DeliveryOption option)
    {
        _selectedOption = option;
    }

    public decimal CalculateShippingCost(decimal total)
    {
        return _selectedOption.Cost;
    }

    public string GetCourierName()
    {
        return $"Sameday - {_selectedOption.Name}";
    }

    public static List<DeliveryOption> GetAvailableOptions()
    {
        return new List<DeliveryOption>
        {
            new DeliveryOption("Domiciliu", 18),
            new DeliveryOption("Easybox", 8)
        };
    }
}

// Clasa implementeaza strategia de livrare pentru curierul Sameday si
// calculeaza costul transportului in functie de optiunea selectata