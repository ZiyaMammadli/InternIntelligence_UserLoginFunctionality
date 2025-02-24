
using Microsoft.AspNetCore.Identity;

namespace UserLoginFunctionality.Domian.Entities;

public class AppUser:IdentityUser<Guid>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string RefreshToken { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime RefreshTokenExpireTime { get; set; }
    public DateTime CreatedDate { get; set; }
}
