using Net5WebTemplate.Application.Notifications.Email;
using System;
using System.Threading.Tasks;

namespace Net5WebTemplate.Application.Common.Interfaces
{
    public interface IEmailNotification
    {
        Task SendEmailAsync(EmailMessage message, Object model, EmailTemplate template);
    }
}