using FluentValidation;

namespace Net5WebTemplate.Application.Auth.Command.ForgotPassword
{
    public class ForgotPasswordCommandValidator : AbstractValidator<ForgotPasswordCommand>
    {
        public ForgotPasswordCommandValidator()
        {
            RuleFor(v => v.Email)
                .NotEmpty().WithMessage("Email is a requred field.")
                .EmailAddress().WithMessage("Invalid email format.");
        }
    }
}
