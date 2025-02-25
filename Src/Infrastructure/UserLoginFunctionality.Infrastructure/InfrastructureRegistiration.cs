using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserLoginFunctionality.Application.Services;
using UserLoginFunctionality.Infrastructure.Tokens;

namespace UserLoginFunctionality.Infrastructure;

public static class InfrastructureRegistiration
{
    public static void AddInfrastructure(this IServiceCollection services,IConfiguration config)
    {
        services.Configure<TokenSettings>(config.GetSection("JWT"));     
        services.AddTransient<ITokenService, TokenService>();
    }
}
