using Order.Application;
using Order.Data.Sql;
using Order.Grpc.AspNetCore;
using Order.Messaging.RabbitMQ;
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
        services.AddRabbitMqModule();
    }
        
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Order v1"));
        }
        
        app.UseRouting();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
        
        PrepDb.PrepPopulation(app, env.IsProduction());
    }
}
