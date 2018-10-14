using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Gap.Domain.Customer.Repository;
using Gap.Domain.Insurance.Repository;
using Gap.Insurance.API.Application.Exceptions;
using ViewModel = Gap.Insurance.API.Application.Model;

namespace Gap.Insurance.API.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IMapper _mapper;
        private readonly ICustomerRepository _customerRepository;
        private readonly Lazy<IInsuranceRepository> _insuranceRepository;

        public CustomerService(ICustomerRepository customerRepository, IMapper mapper, Lazy<IInsuranceRepository> insuranceRepository)
        {
            _mapper = mapper ?? throw new CustomerApplicationArgumentNullException(nameof(mapper));
            _insuranceRepository = insuranceRepository ?? throw new CustomerApplicationArgumentNullException(nameof(insuranceRepository));
            _customerRepository = customerRepository ?? throw new CustomerApplicationArgumentNullException(nameof(customerRepository));
        }

        public async Task<IList<ViewModel.Customer>> GetCustomersAsync()
        {
            var customers = await _customerRepository.GetCustomersAsync();
            var customersViewModel = _mapper.Map<IEnumerable<ViewModel.Customer>>(customers);
            return customersViewModel.ToList();
        }

        public async Task<ViewModel.Customer> GetCustomerAsync(int customerId)
        {
            var customer = await _customerRepository.GetCustomerAsync(customerId);
            var customersViewModel = _mapper.Map<ViewModel.Customer>(customer);
            return customersViewModel;
        }

        public async Task AssignInsurance(ViewModel.AssignCancelInsuranceRequest request)
        {
            await EnsureInsurance(request.InsuranceId);
            var customer = await _customerRepository.GetCustomerAsync(request.CustomerId);
            customer.AssignInsurance(request.InsuranceId);
            await _customerRepository.UnitOfWork.SaveEntitiesAsync();
        }

        public async Task CancelInsurance(ViewModel.AssignCancelInsuranceRequest request)
        {
            await EnsureInsurance(request.InsuranceId);
            var customer = await _customerRepository.GetCustomerAsync(request.CustomerId);
            customer.CancelInsurance(request.InsuranceId);
            await _customerRepository.UnitOfWork.SaveEntitiesAsync();
        }

        private async Task EnsureInsurance(int insuranceId)
        {
            var insurance = await _insuranceRepository.Value.GetInsuranceAsync(insuranceId);
            if (insurance == null)
                throw new InsuranceApplicationArgumentNullException($"The insurance {insuranceId} you're trying to assign doesn't exists.");
        }
    }
}
