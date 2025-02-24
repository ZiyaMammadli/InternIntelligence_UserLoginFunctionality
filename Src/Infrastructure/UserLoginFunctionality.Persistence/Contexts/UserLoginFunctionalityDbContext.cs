using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UserLoginFunctionality.Domian.Entities;
using UserLoginFunctionality.Persistence.Configurations;

namespace UserLoginFunctionality.Persistence.Contexts;

public class UserLoginFunctionalityDbContext:IdentityDbContext<AppUser,Role,Guid>
{
    public UserLoginFunctionalityDbContext(DbContextOptions<UserLoginFunctionalityDbContext> options) : base(options) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppUserConfiguration).Assembly);
        modelBuilder.Entity<IdentityUserLogin<Guid>>().HasKey(l => new { l.LoginProvider, l.ProviderKey });
        modelBuilder.Entity<IdentityUserRole<Guid>>().HasKey(r => new { r.UserId, r.RoleId });
        modelBuilder.Entity<IdentityUserToken<Guid>>().HasKey(t => new { t.UserId, t.LoginProvider, t.Name });
    }
}
