namespace ECommerce.Interfete;

public interface IShippingService
{
    void RegisterShippingMethod(IShippingStrategy shipping);
    List<IShippingStrategy> GetAvailableMethods();
}
// Definește metoda pentru înregistrarea unei strategii de livrare în sistem.