using System.Reflection;
using System.Text.Json.Serialization;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Order.Rest.AspNetCore.Profiles;

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
        services.AddSwaggerGen(c =>
        {
            c.CustomSchemaIds(type => type.ToString());
        });

        
        services.AddAutoMapper(typeof(MappingProfiles).Assembly);

        services.AddMediatR(typeof(Mediator).Assembly);;
        
        return services;
    }
}
