using System;
using System.Linq;
using System.Threading.Tasks;
using Gap.Domain.Customer.Repository;
using Gap.Domain.Insurance.Events;
using MediatR;

namespace Gap.Insurance.API.Application.DomainEventHandlers
{
    public class InsuranceDeletedDomainEventHandler : IAsyncNotificationHandler<InsuranceDeleted>
    {
        private readonly ICustomerRepository _customerRepository;

        public InsuranceDeletedDomainEventHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        }

        public async Task Handle(InsuranceDeleted notification)
        {
            var somethingToDelete = false;
            var customerInsurances = await _customerRepository.GetInsurancesByIdAsync(notification.InsuranceId);
            var customerIds = customerInsurances.GroupBy(x => x.CustomerId).Select(x => x.Key);

            foreach (var customerId in customerIds)
            {
                var customer = await _customerRepository.GetCustomerAsync(customerId);
                var insurancesByCustomer = customerInsurances
                    .Where(x => x.CustomerId == customerId)
                    .Select(x => x.InsuranceId)
                    .Distinct();

                foreach (var insuranceId in insurancesByCustomer)
                {
                    somethingToDelete = true;
                    customer.DeleteInsurance(insuranceId);
                }
            }

            if (somethingToDelete)
                await _customerRepository.UnitOfWork.SaveEntitiesAsync();
        }
    }
}
