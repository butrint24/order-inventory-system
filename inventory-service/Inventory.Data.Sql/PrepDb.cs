using Inventory.Data.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Inventory.Data.Sql;

public static class PrepDb
{
    public static void PrepPopulation(IApplicationBuilder app, bool isProd)
    {
        using( var serviceScope = app.ApplicationServices.CreateScope())
        {
            SeedData(serviceScope.ServiceProvider.GetService<ApplicationDbContext>(), isProd);
        }
    }
    
    private static void SeedData(ApplicationDbContext context, bool isProd)
    {
        if(isProd)
        {
            Console.WriteLine("--> Attempting to apply migrations...");
            try
            {
                context.Database.Migrate();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"--> Could not run migrations: {ex.Message}");
            }
        }
            
        if(!context.Products.Any())
        {
            Console.WriteLine("--> Seeding Data...");

            context.Products.AddRange(
                new Product {Name="Laptop", Details= "some details", Price= 2.00M, Stock = 20},
                new Product {Name="Mobile", Details="some details",  Price= 2.00M, Stock = 20},
                new Product {Name="Phone", Details="some details",  Price= 2.00M, Stock = 20}
            );

            context.SaveChanges();
        }
        else
        {
            Console.WriteLine("--> We already have data");
        }
    }
}
