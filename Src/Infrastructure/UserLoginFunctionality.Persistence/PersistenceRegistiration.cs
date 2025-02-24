using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserLoginFunctionality.Persistence.Contexts;

namespace UserLoginFunctionality.Persistence;

public static class PersistenceRegistiration
{
    public static void AddPersistence(this IServiceCollection services,IConfiguration config)
    {
        services.AddDbContext<UserLoginFunctionalityDbContext>(opt =>
        {
            opt.UseSqlServer(config.GetConnectionString("default"));

        });
    }
}
