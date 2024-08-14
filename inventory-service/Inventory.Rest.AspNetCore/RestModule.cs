using System.Text.Json.Serialization;
using Inventory.Rest.AspNetCore.Profiles;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Inventory.Rest.AspNetCore;

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
