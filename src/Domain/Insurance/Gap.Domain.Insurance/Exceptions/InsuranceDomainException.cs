using System;

namespace Gap.Domain.Insurance.Exceptions
{
    public class InsuranceDomainException : Exception
    {
        public InsuranceDomainException()
        { }

        public InsuranceDomainException(string message)
            : base(message)
        { }

        public InsuranceDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
