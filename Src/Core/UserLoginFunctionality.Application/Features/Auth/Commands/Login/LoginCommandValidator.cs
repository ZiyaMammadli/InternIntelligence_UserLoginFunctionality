using FluentValidation;

namespace UserLoginFunctionality.Application.Features.Auth.Commands.Login;

public class LoginCommandValidator : AbstractValidator<LoginCommandRequest>
{
    public LoginCommandValidator()
    {
        RuleFor(l => l.Email)
            .EmailAddress().WithMessage("This are must be email")
            .NotEmpty().WithMessage("Email can not be Empty")
            .NotNull().WithMessage("Email can not be null");
        RuleFor(l => l.Password)
            .NotEmpty().WithMessage("Password can not be Empty")
            .NotNull().WithMessage("Password can not be null");
    }
}
