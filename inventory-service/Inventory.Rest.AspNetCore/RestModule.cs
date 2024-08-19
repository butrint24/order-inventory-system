using System.Text.Json.Serialization;
using Inventory.Rest.AspNetCore.Profiles;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

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
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Inventory", Version = "v1" });
            c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            c.CustomSchemaIds(type => type.FullName);
        });
        
        services.AddAutoMapper(typeof(MappingProfiles).Assembly);

        services.AddMediatR(typeof(Mediator).Assembly);;
        
        return services;
    }
}
