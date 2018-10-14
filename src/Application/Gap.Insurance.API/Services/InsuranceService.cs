using System.Threading.Tasks;
using AutoMapper;
using Gap.Domain.Customer.Repository;
using Gap.Domain.Insurance.Repository;
using Gap.Insurance.API.Application.Exceptions;
using ViewModel = Gap.Insurance.API.Application.Model;

namespace Gap.Insurance.API.Services
{
    public class InsuranceService : IInsuranceService
    {
        private readonly IMapper _mapper;
        private readonly IInsuranceRepository _insuranceRepository;
        private readonly ICustomerRepository _customerRepository;

        public InsuranceService(IInsuranceRepository insuranceRepository, IMapper mapper, ICustomerRepository customerRepository)
        {
            _insuranceRepository = insuranceRepository ?? throw new InsuranceApplicationArgumentNullException(nameof(insuranceRepository));
            _customerRepository = customerRepository ?? throw new InsuranceApplicationArgumentNullException(nameof(customerRepository));
            _mapper = mapper ?? throw new InsuranceApplicationArgumentNullException(nameof(mapper));
        }

        public async Task<ViewModel.Insurance> GetInsuranceAsync(int insuranceId)
        {
            var insurance = await _insuranceRepository.GetInsuranceAsync(insuranceId);
            var customer = await _customerRepository.GetCustomerAsync(insurance.CustomerId);

            var insuranceViewModel = _mapper.Map<ViewModel.Insurance>(insurance);
            var customerViewModel = _mapper.Map<ViewModel.Customer>(customer);

            insuranceViewModel.Customer = customerViewModel;

            return insuranceViewModel;
        }

        public async Task<int> CreateInsurance(ViewModel.CreateInsuranceRequest insuranceRequest)
        {
            var insuranceViewModel = _mapper.Map<ViewModel.Insurance>(insuranceRequest);
            var domainInsurance = _mapper.Map<Domain.Insurance.Model.Insurance>(insuranceViewModel);
            var result = await _insuranceRepository.AddInsuranceAsync(domainInsurance);

            await _insuranceRepository.UnitOfWork.SaveChangesAsync();
            return result;
        }

        public async Task AddCoverageToInsurance(ViewModel.AddCoverageRequest request)
        {
            var insurance = await _insuranceRepository.GetInsuranceAsync(request.InsuranceId);
            insurance.AddCoverage(request.CoverageId, request.Percentage);
            _insuranceRepository.UpdateInsurance(insurance);

            await _insuranceRepository.UnitOfWork.SaveChangesAsync();
        }
    }
}
