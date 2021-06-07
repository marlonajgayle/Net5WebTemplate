using FluentValidation;

namespace Net5WebTemplate.Application.Profiles.Commands.DeleteProfile
{
    public class DeleteProfileCommandValidator : AbstractValidator<DeleteProfileCommand>
    {
        public DeleteProfileCommandValidator()
        {
            RuleFor(v => v.Id)
                .NotEmpty().WithMessage("Id field is required.")
                .GreaterThan(0).WithMessage("Invalid Id format");
        }
    }
}
