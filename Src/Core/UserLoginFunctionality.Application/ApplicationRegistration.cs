using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using UserLoginFunctionality.Application.Behaviours;
using UserLoginFunctionality.Application.ExceptionMiddleWares;
using UserLoginFunctionality.Application.Features.Auth.Login.Commands;

namespace UserLoginFunctionality.Application;

public static class ApplicationRegistration
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddTransient<ExceptionHandlingMiddleWare>();
        services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(LoginCommandHandler).Assembly));
        services.AddTransient(typeof(IPipelineBehavior<,>),typeof(FluentValidationBehaviour<,>));
        services.AddValidatorsFromAssemblyContaining(typeof(LoginCommandValidator));
    }
}
