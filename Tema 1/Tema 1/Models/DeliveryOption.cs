namespace ECommerce.Models;

public class DeliveryOption
{
    public string Name { get; }
    public decimal Cost { get; }

    public DeliveryOption(string name, decimal cost)
    {
        Name = name;
        Cost = cost;
    }
}
// Reprezintă o opțiune de livrare cu numele și costul acesteia.