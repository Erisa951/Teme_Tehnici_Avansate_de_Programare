using System.Linq;
using Tema_1.Core;

namespace Tema_1.Shipping;

public class DPD : IShippingStrategy
{
    private readonly DeliveryOption _selectedOption;

    public DPD(DeliveryOption option)
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
                select $"DPD - {o.Name}").First();
    }

    public static List<DeliveryOption> GetAvailableOptions()
    {
        var options =
            from o in new[]
            {
                new DeliveryOption("Home Delivery", 22),
                new DeliveryOption("Warehouse Pickup", 7)
            }
            select o;

        return options.ToList();
    }
}