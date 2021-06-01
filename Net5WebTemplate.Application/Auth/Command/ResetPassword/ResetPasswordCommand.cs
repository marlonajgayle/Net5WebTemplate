using MediatR;

namespace Net5WebTemplate.Application.Auth.Command.ResetPassword
{
    public class ResetPasswordCommand : IRequest
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
