using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserLoginFunctionality.Domian.Entities;
using UserLoginFunctionality.Persistence.Contexts;

namespace UserLoginFunctionality.Persistence;

public static class PersistenceRegistiration
{
    public static void AddPersistence(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<UserLoginFunctionalityDbContext>(opt =>
        {
            opt.UseSqlServer(config.GetConnectionString("default"));

        });
        services.AddIdentityCore<AppUser>(opt =>
        {
            opt.Password.RequiredLength = 8;
            opt.Password.RequireNonAlphanumeric = true;
            opt.Password.RequireLowercase = true;
            opt.Password.RequireUppercase = true;
            opt.User.RequireUniqueEmail = true;
            opt.Password.RequireDigit = true;
            opt.User.AllowedUserNameCharacters = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM0123456789._";
        })  
            .AddRoles<Role>()
            .AddEntityFrameworkStores<UserLoginFunctionalityDbContext>();
    }
}
