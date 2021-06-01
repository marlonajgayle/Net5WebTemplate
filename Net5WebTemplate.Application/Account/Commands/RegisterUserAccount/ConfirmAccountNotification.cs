using MediatR;
using Net5WebTemplate.Application.Common.Interfaces;
using Net5WebTemplate.Application.Notifications.Email;
using System.Threading;
using System.Threading.Tasks;

namespace Net5WebTemplate.Application.Account.Commands.RegisterUserAccount
{
    public class ConfirmAccountNotification : INotification
    {
        public string Email { get; set; }

        public class ConfirmAccountNotificationHandler : INotificationHandler<ConfirmAccountNotification>
        {
            private readonly IEmailNotification _emailNotification;

            public ConfirmAccountNotificationHandler(IEmailNotification emailNotification)
            {
                _emailNotification = emailNotification;
            }
            public async Task Handle(ConfirmAccountNotification notification, CancellationToken cancellationToken)
            {
                var emailMessage = new EmailMessage
                {
                    To = notification.Email,
                    Subject = "Confirm Email"
                };
                await _emailNotification.SendEmailAsync(emailMessage, new { UserName = "TestUser" }, EmailTemplate.EmailConfirmation);
            }
        }
    }
}
