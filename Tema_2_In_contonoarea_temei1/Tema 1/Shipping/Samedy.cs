using System.Linq;
using Tema_1.Core;

namespace Tema_1.Shipping;

public class Sameday : IShippingStrategy
{
    private readonly DeliveryOption _selectedOption;

    public Sameday(DeliveryOption option)
    {
        _selectedOption = option;
    }

    public decimal CalculateShippingCost(decimal total)
    {
        return new[] { _selectedOption.Cost }
            .Select(c => c)
            .First();
    }

    public string GetCourierName()
    {
        return (from o in new[] { _selectedOption }
                select $"Sameday - {o.Name}").First();
    }

    public static List<DeliveryOption> GetAvailableOptions()
    {
        var options =
            from o in new[]
            {
                new DeliveryOption("Home Delivery", 18),
                new DeliveryOption("Easybox", 8)
            }
            select o;

        return options.ToList();
    }
}