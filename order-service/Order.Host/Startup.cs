using Order.Application;
using Order.Data.Sql;
using Order.Grpc.AspNetCore;
using Order.Rest.AspNetCore;

namespace Order.Host;

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
        services.AddGrpcModule();
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
