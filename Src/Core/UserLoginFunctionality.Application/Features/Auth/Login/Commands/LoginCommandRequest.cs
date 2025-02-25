using MediatR;

namespace UserLoginFunctionality.Application.Features.Auth.Login.Commands;

public class LoginCommandRequest:IRequest<LoginCommandResponse>
{
    public string Email { get; set; }
    public string Password { get; set; }    
}
