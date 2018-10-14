using System;

namespace Gap.Insurance.API.Application.Exceptions
{
    public class InsuranceApplicationArgumentNullException : ArgumentNullException
    {
        public InsuranceApplicationArgumentNullException()
        { }

        public InsuranceApplicationArgumentNullException(string message)
            : base(message)
        { }

        public InsuranceApplicationArgumentNullException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
