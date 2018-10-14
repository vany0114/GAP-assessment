using MediatR;

namespace Gap.Domain.Customer.Events
{
    public class InsuranceAssigned : INotification
    {
        public int InsuranceId { get; }

        public int CustomerId { get; }

        public InsuranceAssigned(int insuranceId, int customerId)
        {
            InsuranceId = insuranceId;
            CustomerId = customerId;
        }
    }
}
