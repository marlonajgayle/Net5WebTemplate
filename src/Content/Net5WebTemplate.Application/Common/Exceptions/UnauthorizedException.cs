using System;

namespace Net5WebTemplate.Application.Common.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException(string message) :
            base(message)
        {

        }
    }
}
