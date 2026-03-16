using ECommerce.Interfete;

namespace ECommerce.Servicii;

// Serviciu care gestionează toate strategiile de discount disponibile în magazin
public class DiscountService : IDiscountService
{
    // Lista internă în care sunt stocate strategiile de discount
    private List<IDiscountStrategy> _discounts = new();

    // Înregistrează (adauga) un nou tip de discount în sistem
    public void RegisterDiscount(IDiscountStrategy discount)
    {
        _discounts.Add(discount);
    }

    // Returnează toate discounturile disponibile
    public List<IDiscountStrategy> GetAvailableDiscounts()
    {
        return _discounts;
    }
}