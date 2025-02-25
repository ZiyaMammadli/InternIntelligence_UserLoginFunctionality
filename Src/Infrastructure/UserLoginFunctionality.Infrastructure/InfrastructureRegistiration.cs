using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using UserLoginFunctionality.Application.Services;
using UserLoginFunctionality.Infrastructure.Tokens;

namespace UserLoginFunctionality.Infrastructure;

public static class InfrastructureRegistiration
{
    public static void AddInfrastructure(this IServiceCollection services,IConfiguration config)
    {
        services.Configure<TokenSettings>(config.GetSection("JWT"));     
        services.AddTransient<ITokenService, TokenService>();
        services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme=JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme=JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
        {
            opt.SaveToken = true;
            opt.TokenValidationParameters = new()
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:SecretKey"])),
                ValidateLifetime = false,
                ValidIssuer = config["JWT:Issuer"],
                ValidAudience = config["JWT:Audience"],
                ClockSkew = TimeSpan.Zero,
            };
        });
    }
}
