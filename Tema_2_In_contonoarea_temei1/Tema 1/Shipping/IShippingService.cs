using System.Collections.Generic;

namespace Tema_1.Shipping;

public interface IShippingService
{
    void RegisterShippingMethod(IShippingStrategy shipping);

    IReadOnlyList<IShippingStrategy> GetAvailableMethods();
}