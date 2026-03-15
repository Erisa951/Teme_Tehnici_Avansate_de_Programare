using System.Linq;
using Tema_1.Catalog;
using Tema_1.Users;

namespace Tema_1.Users.Customer;

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
        var role =
            (from r in new[] { "Customer" }
             select r).FirstOrDefault();

        return role ?? "Unknown";
    }
}