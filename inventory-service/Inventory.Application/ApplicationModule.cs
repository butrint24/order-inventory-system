using System.Reflection;
using FluentValidation;
using Inventory.Application.Abstractions;
using Inventory.Application.Contracts.Abstractions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Inventory.Application;

public static class ApplicationModule
{
    public static IServiceCollection AddApplicationModule(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        
        services.AddValidatorsFromAssemblyContaining<BaseValidator<ValidatorBase>>();
        
        services.AddAutoMapper(Assembly.GetExecutingAssembly()).AddTransient<MappingRegister>();
        
        return services;
    }
}
