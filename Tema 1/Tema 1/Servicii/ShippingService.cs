using ECommerce.Interfete;

namespace ECommerce.Servicii;

// Serviciu responsabil pentru gestionarea metodelor de livrare disponibile
public class ShippingService : IShippingService
{
    // Lista internă care stochează strategiile de livrare înregistrate
    private List<IShippingStrategy> _shippingMethods = new();

    // Înregistrează (adauga) o metodă de livrare în sistem
    // Folosește interfața IShippingStrategy pentru a permite mai multe tipuri de curieri
    public void RegisterShippingMethod(IShippingStrategy shipping)
    {
        _shippingMethods.Add(shipping);
    }

    // Returnează toate metodele de livrare disponibile
    // Acestea vor fi folosite de client pentru alegerea curierului
    public List<IShippingStrategy> GetAvailableMethods()
    {
        return _shippingMethods;
    }
}