namespace ECommerce.Interfete;

public interface IDiscountStrategy
{
    decimal ApplyDiscount(decimal amount, int itemCount);
}

// Definește metoda pentru aplicarea unei reduceri asupra unei sume,
// în funcție de valoarea totală și numărul de produse.