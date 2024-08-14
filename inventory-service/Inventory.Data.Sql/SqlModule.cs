using Inventory.Data.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Inventory.Data.Sql;

public static class SqlModule
{
    public static void AddSqlModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options => {
            options.UseSqlServer(configuration.GetConnectionString("ConnectionString"));
        });
        
        services.AddScoped<IDataRepository, DataRepository>();
    }
}
