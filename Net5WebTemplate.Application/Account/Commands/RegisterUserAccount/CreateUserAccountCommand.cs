using MediatR;

namespace Net5WebTemplate.Application.Account.Commands.RegisterUserAccount
{
    public class CreateUserAccountCommand : IRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
