using FluentValidation;

namespace Net5WebTemplate.Application.Account.Commands.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(v => v.Email)
                .NotEmpty().WithMessage("Email field is required.");

            RuleFor(v => v.Password)
                .NotEmpty().WithMessage("Password fiedl is required.");
        }
    }
}
