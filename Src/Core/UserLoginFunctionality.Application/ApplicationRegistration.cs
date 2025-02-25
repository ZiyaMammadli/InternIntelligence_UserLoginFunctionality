using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using UserLoginFunctionality.Application.Bases;
using UserLoginFunctionality.Application.Behaviours;
using UserLoginFunctionality.Application.ExceptionMiddleWares;
using UserLoginFunctionality.Application.Features.Auth.Commands.Login;

namespace UserLoginFunctionality.Application;

public static class ApplicationRegistration
{
    public static void AddApplication(this IServiceCollection services)
    {
        Assembly assembly = Assembly.GetExecutingAssembly();
        services.AddTransient<ExceptionHandlingMiddleWare>();
        services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(LoginCommandHandler).Assembly));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(FluentValidationBehaviour<,>));
        services.AddValidatorsFromAssemblyContaining(typeof(LoginCommandValidator));
        services.AddRulesFromAssemblyContaining(assembly, typeof(BaseRule));
    }
        
    private static IServiceCollection AddRulesFromAssemblyContaining(
        this IServiceCollection services,
        Assembly assembly,
        Type type)
    {
        var types = assembly.GetTypes().Where(t => t.IsSubclassOf(type) && type != t).ToList();
        foreach (var item in types)
        {
            services.AddTransient(item);
        }
        return services;
    }
}
