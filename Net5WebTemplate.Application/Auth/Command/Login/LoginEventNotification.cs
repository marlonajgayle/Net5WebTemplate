using MediatR;
using Net5WebTemplate.Application.Common.Interfaces;
using Net5WebTemplate.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Net5WebTemplate.Application.Auth.Commands.Login
{
    public class LoginEventNotification : INotification
    {
        public string Username { get; set; }
        public string Description { get; set; }
        public bool IsSuccess { get; set; }
        public string IpAddress { get; set; }
        public DateTime Timestamp { get; set; }

        public class LoginEventNtificationHandler : INotificationHandler<LoginEventNotification>
        {
            private readonly INet5WebTemplateDbContext _dbContext;
            public LoginEventNtificationHandler(INet5WebTemplateDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task Handle(LoginEventNotification notification, CancellationToken cancellationToken)
            {
                var entity = new LoginAuditLog
                {
                    Username = notification.Username,
                    Description = notification.Description,
                    IsSuccess = notification.IsSuccess,
                    IpAddress = notification.IpAddress,
                    Timestamp = notification.Timestamp
                };

                _dbContext.LoginAuditLogs.Add(entity);

                await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}