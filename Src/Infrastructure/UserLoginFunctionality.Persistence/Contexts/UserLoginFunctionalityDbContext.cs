using Microsoft.EntityFrameworkCore;

namespace UserLoginFunctionality.Persistence.Contexts;

public class UserLoginFunctionalityDbContext:DbContext
{
    public UserLoginFunctionalityDbContext(DbContextOptions<UserLoginFunctionalityDbContext> options):base(options)
    {
        
    }
}
