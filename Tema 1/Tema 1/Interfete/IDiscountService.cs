namespace ECommerce.Interfete;

public interface IDiscountService
{
    void RegisterDiscount(IDiscountStrategy discount);
    List<IDiscountStrategy> GetAvailableDiscounts();
}
// Definește metoda pentru înregistrarea unei strategii de reducere în sistem.