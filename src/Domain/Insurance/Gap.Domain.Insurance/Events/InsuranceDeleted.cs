using MediatR;

namespace Gap.Domain.Insurance.Events
{
    public class InsuranceDeleted : INotification
    {
        public int InsuranceId { get; }

        public InsuranceDeleted(int insuranceId)
        {
            InsuranceId = insuranceId;
        }
    }
}
