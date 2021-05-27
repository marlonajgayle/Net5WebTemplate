using MediatR;

namespace Net5WebTemplate.Application.Auth.Command.ForgotPassword
{
    public class ForgotPasswordCommand : IRequest
    {
        public string Email { get; set; }
    }
}
