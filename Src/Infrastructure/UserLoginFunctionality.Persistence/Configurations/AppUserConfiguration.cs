using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserLoginFunctionality.Domian.Entities;

namespace UserLoginFunctionality.Persistence.Configurations;

public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.Property(u => u.FirstName).IsRequired().HasMaxLength(20);
        builder.Property(u=>u.LastName).IsRequired().HasMaxLength(30);
        builder.Property(u=>u.RefreshToken).IsRequired().HasMaxLength(250);
    }
}
