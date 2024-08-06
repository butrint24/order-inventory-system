using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Order.Application.Abstractions;
using Order.Application.Contracts.Abstractions;

namespace Order.Application;

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
