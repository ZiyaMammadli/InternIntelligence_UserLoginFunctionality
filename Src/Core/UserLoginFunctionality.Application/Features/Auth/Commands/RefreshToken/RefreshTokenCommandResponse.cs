using System.Globalization;

namespace UserLoginFunctionality.Application.Features.Auth.Commands.RefreshToken;

public class RefreshTokenCommandResponse
{
    public string AccessToken { get; set; }
    public string RefreshToken {  get; set; }
}
