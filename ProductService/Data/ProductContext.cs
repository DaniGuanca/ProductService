using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using ProductService.Models;

namespace ProductService.Data
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options) 
        {
            try
            {
                var dbCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
                if (dbCreator != null) 
                {
                    if (!dbCreator.CanConnect())
                        dbCreator.Create();

                    if (!dbCreator.HasTables())
                        dbCreator.CreateTables();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("ERROR: ProductContext.cs, ProductContext");
                Console.WriteLine(ex.Message);
            }
        }

        public DbSet<Product> Products { get; set; }
    }
}
