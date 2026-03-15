using System.Linq;
using System.Collections.Generic;

namespace Tema_1.Shipping;

public class ShippingService : IShippingService
{
    private readonly List<IShippingStrategy> _shippingMethods = new();

    public void RegisterShippingMethod(IShippingStrategy shipping)
    {
        if (!_shippingMethods.Any(s => s.GetCourierName() == shipping.GetCourierName()))
            _shippingMethods.Add(shipping);
    }

    public IReadOnlyList<IShippingStrategy> GetAvailableMethods()
    {
        return _shippingMethods
            .Select(s => s)
            .ToList()
            .AsReadOnly();
    }
}