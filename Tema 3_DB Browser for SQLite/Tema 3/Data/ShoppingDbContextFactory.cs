using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Tema_3.Data;

public class ShoppingDbContextFactory : IDesignTimeDbContextFactory<ShoppingDbContext>
{
    public ShoppingDbContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<ShoppingDbContext>()
            .UseSqlite("Data Source=shopping.db")
            .Options;

        return new ShoppingDbContext(options);
    }
}