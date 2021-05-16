using MediatR;
using Microsoft.Extensions.Localization;
using Net5WebTemplate.Application;
using Net5WebTemplate.Application.Common.Interfaces;
using Net5WebTemplate.Application.Notifications.Email;
using System.Threading;
using System.Threading.Tasks;

namespace Net5WebTemplate.Application.Clients.Queries.GetClientDetail
{
    public class GetClientDetailQueryHandler : IRequestHandler<GetClientDetailQuery, string>
    {
        private readonly IStringLocalizer<Messages> _localizer;
        private readonly IEmailNotification _emailService;

        public GetClientDetailQueryHandler(IStringLocalizer<Messages> localizer, IEmailNotification emailService)
        {
            _localizer = localizer;
            _emailService = emailService;
        }
        public async Task<string> Handle(GetClientDetailQuery request, CancellationToken cancellationToken)
        {
            var emailMessage = new EmailMessage
            { 
                To = "testuser@test.com",
                Subject = "Confirm Email"
            };
            await _emailService.SendEmailAsync(emailMessage, new { UserName = "TestUser" }, EmailTemplate.EmailConfirmation);
            return _localizer["Welcome"].Value;
        }
    }
}