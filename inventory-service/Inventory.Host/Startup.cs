using Inventory.Application;
using Inventory.Data.Sql;
using Inventory.Rest.AspNetCore;

namespace Inventory.Host;

public class Startup
{
    private IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    // Register services
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSqlModule(Configuration);
        services.AddApplicationModule();
        services.AddRestModule();
    }
        
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseRouting();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
