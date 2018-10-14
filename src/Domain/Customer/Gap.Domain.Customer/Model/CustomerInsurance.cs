using System;
using System.Collections.Generic;
using Gap.Domain.Customer.Exceptions;
using Gap.Infrastructure.DDD;

namespace Gap.Domain.Customer.Model
{
    public class CustomerInsurance : ValueObject
    {
        // EF doesn't support auto-properties readonly to run migrations
        private int _customerId;
        private int _insuranceId;
        private DateTime _assigningDate;
        private DateTime? _cancellationDate;
        private Status _status;
        private Customer _customer;

        public int CustomerId => _customerId;

        public int InsuranceId => _insuranceId;

        public Status Status => _status;

        public DateTime AssigningDate => _assigningDate;

        public DateTime? CancellationDate => _cancellationDate;

        // EF navigation property
        public Customer Customer => _customer;

        // Only the aggregate root can manage this VO.
        internal CustomerInsurance(int customerId, int insuranceId)
        {
            if(customerId == default(int) || insuranceId == default(int))
                throw new CustomerDomainException("Invalid relationship between insurance and customer.");

            _customerId = customerId;
            _insuranceId = insuranceId;
            _status = Status.Assigned;
            _assigningDate = DateTime.UtcNow;
        }

        public void CancelInsurance()
        {
            if(_status == Status.Canceled)
                throw new CustomerDomainException("This insurance is already cancelled.");

            _status = Status.Canceled;
            _cancellationDate = DateTime.UtcNow;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return CustomerId;
            yield return InsuranceId;
            yield return Status;
            yield return AssigningDate;
            yield return CancellationDate;
        }
    }

    public enum Status
    {
        Assigned = 1,
        Canceled = 2
    }
}
