using FluentValidation;

namespace Net5WebTemplate.Application.Account.Commands.RegisterUserAccount
{
    public class CreateUserAccountCommandVaidator : AbstractValidator<CreateUserAccountCommand>
    {
        public CreateUserAccountCommandVaidator()
        {
            RuleFor(v => v.Email)
                .NotEmpty().WithMessage("Email field required.")
                .EmailAddress().WithMessage("Invalid email address format.");

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