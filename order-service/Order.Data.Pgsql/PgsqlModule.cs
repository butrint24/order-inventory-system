﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Order.Data.Contracts;

namespace Order.Data.Pgsql;

public static class PgsqlModule
{
    public static void AddPgsqlModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options => {
            options.UseSqlServer(configuration.GetConnectionString("ConnectionString"));
        });
        
        services.AddScoped<IDataRepository, DataRepository>();
    }
}
