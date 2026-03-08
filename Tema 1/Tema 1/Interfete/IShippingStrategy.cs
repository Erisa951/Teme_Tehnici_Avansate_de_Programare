namespace ECommerce.Interfete;

public interface IShippingStrategy
{
    decimal CalculateShippingCost(decimal orderTotal);
    string GetCourierName();
}
// Definește metodele pentru calculul costului de livrare și numele curierului.