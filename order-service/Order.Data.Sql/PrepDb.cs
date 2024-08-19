using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Order.Data.Contracts;

namespace Order.Data.Sql;

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
            
        if(!context.Customers.Any())
        {
            Console.WriteLine("--> Seeding Data...");

            context.Customers.AddRange(
                new Customer {GivenName = "John", FamilyName = "Doe", Address = "1234 Elm Street, Springfield, IL 62704"}
            );

            context.SaveChanges();
        }
        else
        {
            Console.WriteLine("--> We already have data");
        }
    }
}
