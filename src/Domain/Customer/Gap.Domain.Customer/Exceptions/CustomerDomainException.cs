using System;
using System.Collections.Generic;
using System.Text;

namespace Gap.Domain.Customer.Exceptions
{
    public class CustomerDomainException : Exception
    {
        public CustomerDomainException()
        { }

        public CustomerDomainException(string message)
            : base(message)
        { }

        public CustomerDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
