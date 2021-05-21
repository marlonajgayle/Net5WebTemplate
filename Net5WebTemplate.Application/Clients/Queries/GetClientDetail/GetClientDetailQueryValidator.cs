using FluentValidation;

namespace Net5WebTemplate.Application.Clients.Queries.GetClientDetail
{
    public class GetClientDetailQueryValidator : AbstractValidator<GetClientDetailQuery>
    {
        public GetClientDetailQueryValidator()
        {
            RuleFor(v => v.ClientId)
                .NotEmpty().WithMessage("Client Id is required for request.")
                .NotEqual(0).WithMessage("Client Id can't be 0.");
        }
    }
}
