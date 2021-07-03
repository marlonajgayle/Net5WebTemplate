using MediatR;
using Net5WebTemplate.Application.Common.Interfaces;
using Net5WebTemplate.Application.Notifications.Email;
using System.Threading;
using System.Threading.Tasks;

namespace Net5WebTemplate.Application.Auth.Command.ForgotPassword
{
    public class ForgotPasswordEventNotification : INotification
    {
        public string Email { get; set; }
        public string Token { get; set; }

        public class ForgotPasswordNotificationHandler : INotificationHandler<ForgotPasswordEventNotification>
        {
            private readonly IEmailNotification _emailNotification;

            public ForgotPasswordNotificationHandler(IEmailNotification emailNotification)
            {
                _emailNotification = emailNotification;
            }

            public async Task Handle(ForgotPasswordEventNotification notification, CancellationToken cancellationToken)
            {
                var emailMessage = new EmailMessage
                {
                    To = notification.Email,
                    Subject = "Reset Password"
                };

                await _emailNotification.SendEmailAsync(emailMessage, new { notification.Email, notification.Token }, EmailTemplate.ForgotPassword);
            }
        }

    }
}
