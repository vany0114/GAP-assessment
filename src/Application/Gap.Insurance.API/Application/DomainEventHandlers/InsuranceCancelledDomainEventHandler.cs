using System;
using System.Threading.Tasks;
using Gap.Domain.Customer.Events;
using Gap.Domain.Customer.Repository;
using Gap.Domain.Insurance.Repository;
using MediatR;

namespace Gap.Insurance.API.Application.DomainEventHandlers
{
    public class InsuranceCancelledDomainEventHandler : IAsyncNotificationHandler<InsuranceCancelled>
    {
        private readonly IInsuranceRepository _insuranceRepository;
        private readonly ICustomerRepository _customerRepository;

        public InsuranceCancelledDomainEventHandler(IInsuranceRepository insuranceRepository, ICustomerRepository customerRepository)
        {
            _insuranceRepository = insuranceRepository ?? throw new ArgumentNullException(nameof(insuranceRepository));
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        }

        public async Task Handle(InsuranceCancelled notification)
        {
            var activeInsurances = await _customerRepository.GetActiveInsurancesAsync(notification.InsuranceId);
            if (activeInsurances?.Count == 0)
            {
                var insurance = await _insuranceRepository.GetInsuranceAsync(notification.InsuranceId);
                insurance.Release();
                await _insuranceRepository.UnitOfWork.SaveEntitiesAsync();
            }
        }
    }
}
