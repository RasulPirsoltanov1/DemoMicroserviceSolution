using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using ProductWebApi.Models;

namespace ProductWebApi
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {
            try
            {
                var databaseCreataor = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;

                if (!databaseCreataor.CanConnect()) databaseCreataor.Create();
                if (!databaseCreataor.HasTables()) databaseCreataor.CreateTables();
            }
            catch (Exception ex)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine($"Db error message: {ex.Message}");
                Console.ResetColor();
            }
        }
        public DbSet<Product> Products { get; set; }
    }
}
