namespace ECommerce.Models;

public class Customer : User
{
    public ShoppingCart Cart { get; }

    public Customer(int id, string name, ShoppingCart cart)
        : base(id, name)
    {
        Cart = cart;
    }

    public override string GetRole()
    {
        return "Customer";
    }
}
// Reprezintă un utilizator de tip client care deține un coș de cumpărături.