using System;

namespace Net5WebTemplate.Application.Common.Exceptions
{
    public class ForbiddenException : Exception
    {
        public ForbiddenException(string message) :
            base(message)
        {
        }
    }
}
