using FluentValidation;

namespace Net5WebTemplate.Application.Profiles.Commands.CreateProfile
{
    public class CreateProfileCommandValidator : AbstractValidator<CreateProfileCommand>
    {
        public CreateProfileCommandValidator()
        {
            RuleFor(v => v.FirstName)
                .NotEmpty().WithMessage("FirstNAme field is required.");

            RuleFor(v => v.LastName)
                .NotEmpty().WithMessage("LastName fied is required.");

            RuleFor(v => v.AddressLine1)
                .NotEmpty().WithMessage("AddressLine1 field is required.");

            RuleFor(v => v.Parish)
                .NotEmpty().WithMessage("Parish field is required.");
        }
    }
}
