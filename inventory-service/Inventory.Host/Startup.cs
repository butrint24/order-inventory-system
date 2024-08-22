using Inventory.Application;
using Inventory.Data.Sql;
using Inventory.Grpc.AspNetCore;
using Inventory.Messaging.RabbitMQ;
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
        services.AddGrpcModule();
        services.AddRabbitMqModule();
    }
        
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if(env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Inventory v1"));
        }

        app.UseRouting();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapGrpcService<ProductServiceImplement>();
        });
        
        PrepDb.PrepPopulation(app, env.IsProduction());
    }
}
