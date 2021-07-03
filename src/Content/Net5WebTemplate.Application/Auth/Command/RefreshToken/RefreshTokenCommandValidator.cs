using FluentValidation;

namespace Net5WebTemplate.Application.Auth.Command.RefreshToken
{
    public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
    {
        public RefreshTokenCommandValidator()
        {
            RuleFor(v => v.Token)
                .NotEmpty().WithMessage("Token field required.");

            RuleFor(v => v.RefreshToken)
                .NotEmpty().WithMessage("RefreshToken field required.");
        }
    }
}
