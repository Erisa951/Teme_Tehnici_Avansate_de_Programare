namespace Tema_1.Core;

public class DeliveryOption
{
    public string Name { get; }
    public decimal Cost { get; }

    public DeliveryOption(string name, decimal cost)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty");

        if (cost < 0)
            throw new ArgumentException("Cost cannot be negative");

        Name = name;
        Cost = cost;
    }
}