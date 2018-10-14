using System;
using System.Threading.Tasks;
using Gap.Domain.Customer.Events;
using Gap.Domain.Insurance.Repository;
using MediatR;

namespace Gap.Insurance.API.Application.DomainEventHandlers
{
    public class InsuranceAssignedDomainEventHandler : IAsyncNotificationHandler<InsuranceAssigned>
    {
        private readonly IInsuranceRepository _insuranceRepository;

        public InsuranceAssignedDomainEventHandler(IInsuranceRepository insuranceRepository)
        {
            _insuranceRepository = insuranceRepository ?? throw new ArgumentNullException(nameof(insuranceRepository));
        }

        public async Task Handle(InsuranceAssigned notification)
        {
            var insurance = await _insuranceRepository.GetInsuranceAsync(notification.InsuranceId);
            insurance.UseByCustomers();
            await _insuranceRepository.UnitOfWork.SaveEntitiesAsync();
        }
    }
}
