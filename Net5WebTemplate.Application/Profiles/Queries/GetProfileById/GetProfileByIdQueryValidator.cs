using FluentValidation;

namespace Net5WebTemplate.Application.Profiles.Queries.GetProfileById
{
    public class GetProfileByIdQueryValidator : AbstractValidator<GetProfileByIdQuery>
    {
        public GetProfileByIdQueryValidator()
        {
            RuleFor(v => v.Id)
                .NotEmpty().WithMessage("Id field required.")
                .GreaterThan(0).WithMessage("Invalid Id format.");                
        }
    }
}
