using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Tema_3.Data;

public class ShoppingDbContextFactory : IDesignTimeDbContextFactory<ShoppingDbContext>
{
    public ShoppingDbContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<ShoppingDbContext>()
    .UseSqlServer("Server=DESKTOP-KPL61UM\\SQLEXPRESS;Database=OnlineShoppingDb;Trusted_Connection=True;TrustServerCertificate=True;")
    .Options;

        return new ShoppingDbContext(options);
    }
}