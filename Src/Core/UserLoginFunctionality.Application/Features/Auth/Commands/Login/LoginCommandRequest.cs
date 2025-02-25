using MediatR;
using System.ComponentModel;

namespace UserLoginFunctionality.Application.Features.Auth.Commands.Login;

public class LoginCommandRequest : IRequest<LoginCommandResponse>
{
    [DefaultValue("amin077@gmail.com")]
    public string Email { get; set; }
    [DefaultValue("Salam123@")]
    public string Password { get; set; }
}
