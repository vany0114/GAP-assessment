using MediatR;

namespace Gap.Domain.Customer.Events
{
    public class InsuranceCancelled : INotification
    {
        public int InsuranceId { get; }

        public int CustomerId { get; }

        public InsuranceCancelled(int insuranceId, int customerId)
        {
            InsuranceId = insuranceId;
            CustomerId = customerId;
        }
    }
}
