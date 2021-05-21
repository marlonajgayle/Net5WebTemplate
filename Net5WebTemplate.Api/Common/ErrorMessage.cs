using System;
using System.Collections;

namespace Net5WebTemplate.Api.Common
{
    public class ErrorMessage
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public IEnumerable Error { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
