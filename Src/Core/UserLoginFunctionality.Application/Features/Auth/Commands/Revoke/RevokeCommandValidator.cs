using FluentValidation;

namespace UserLoginFunctionality.Application.Features.Auth.Commands.Revoke;

public class RevokeCommandValidator:AbstractValidator<RevokeCommandRequest>
{
    public RevokeCommandValidator()
    {
        RuleFor(r => r.Email)
            .EmailAddress().WithMessage("This area must be Email")
            .NotEmpty().WithMessage("Email can not be Empty")
            .NotNull().WithMessage("Email can not be null");
    }
}
