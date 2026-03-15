using System.Linq;
using Tema_1.Users;

namespace Tema_1.Users.Admin;

public class Admin : User
{
    public Admin(int id, string name) : base(id, name)
    {
    }

    public override string GetRole()
    {
        var role = new[] { "Admin" }
            .Select(r => r)
            .FirstOrDefault();

        return role ?? "Unknown";
    }
}