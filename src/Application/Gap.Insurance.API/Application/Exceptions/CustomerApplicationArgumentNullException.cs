using System;

namespace Gap.Insurance.API.Application.Exceptions
{
    public class CustomerApplicationArgumentNullException : ArgumentNullException
    {
        public CustomerApplicationArgumentNullException()
        { }

        public CustomerApplicationArgumentNullException(string message)
            : base(message)
        { }

        public CustomerApplicationArgumentNullException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
