using System;

namespace Net5WebTemplate.Domain.Entities
{
    public class LoginAuditLog
    {
        public long Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string Username { get; set; }
        public string Description { get; set; }
        public bool IsSuccess { get; set; } 
        public string IpAddress { get; set; }
    }
}
