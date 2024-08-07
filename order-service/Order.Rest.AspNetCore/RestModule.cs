using System.Reflection;
using System.Text.Json.Serialization;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Order.Rest.AspNetCore;

public static class RestModule
{
    public static IServiceCollection AddRestModule(this IServiceCollection services)
    {
        services.AddControllers().AddJsonOptions(options => 
        { 
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); 
        });
        
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddMediatR(Assembly.GetExecutingAssembly());
        
        return services;
    }
}
