using FluentValidation;

namespace Net5WebTemplate.Application.Auth.Command.ResetPassword
{
    public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
    {
        public ResetPasswordCommandValidator()
        {
            RuleFor(v => v.Email)
                .NotEmpty().WithMessage("Email field required.")
                .EmailAddress().WithMessage("Invalid email address format.");

            RuleFor(v => v.Token)
                .NotEmpty().WithMessage("Token field required.");

            RuleFor(v => v.Password)
                .NotEmpty().WithMessage("Password field required.");

            RuleFor(v => v.ConfirmPassword)
                .NotEmpty().WithMessage("Confirm Password field required.");

            RuleFor(v => v).Custom((v, context) =>
            {
                if (v.Password != v.ConfirmPassword)
                {
                    context.AddFailure("Password does not match Confirm Password.");
                }
            });
        }
    }
}
