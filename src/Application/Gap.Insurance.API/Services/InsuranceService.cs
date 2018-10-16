using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Gap.Domain.Insurance.Repository;
using Gap.Insurance.API.Application.Exceptions;
using ViewModel = Gap.Insurance.API.Application.Model;

namespace Gap.Insurance.API.Services
{
    public class InsuranceService : IInsuranceService
    {
        private readonly IMapper _mapper;
        private readonly IInsuranceRepository _insuranceRepository;

        public InsuranceService(IInsuranceRepository insuranceRepository, IMapper mapper)
        {
            _insuranceRepository = insuranceRepository ?? throw new InsuranceApplicationArgumentNullException(nameof(insuranceRepository));
            _mapper = mapper ?? throw new InsuranceApplicationArgumentNullException(nameof(mapper));
        }

        public async Task<ViewModel.Insurance> GetInsuranceAsync(int insuranceId)
        {
            var insurance = await _insuranceRepository.GetInsuranceAsync(insuranceId);
            var insuranceViewModel = _mapper.Map<ViewModel.Insurance>(insurance);

            return insuranceViewModel;
        }

        public async Task<IList<ViewModel.Insurance>> GetInsurancesAsync()
        {
            var insurances = await _insuranceRepository.GetInsurancesAsync();
            var insurancesViewModel = _mapper.Map<List<ViewModel.Insurance>>(insurances);
            return insurancesViewModel;
        }

        public async Task<int> CreateInsurance(ViewModel.CreateInsuranceRequest insuranceRequest)
        {
            var insuranceViewModel = _mapper.Map<ViewModel.Insurance>(insuranceRequest);
            var domainInsurance = _mapper.Map<Domain.Insurance.Model.Insurance>(insuranceViewModel);
            var result = await _insuranceRepository.AddInsuranceAsync(domainInsurance);

            await _insuranceRepository.UnitOfWork.SaveEntitiesAsync();
            return result;
        }

        public async Task AddCoverageToInsurance(ViewModel.AddCoverageRequest request)
        {
            var insurance = await _insuranceRepository.GetInsuranceAsync(request.InsuranceId);
            insurance.AddCoverage(request.CoverageId, request.Percentage);
            _insuranceRepository.UpdateInsurance(insurance);

            await _insuranceRepository.UnitOfWork.SaveEntitiesAsync();
        }

        public async Task DeleteInsurance(ViewModel.DeleteInsuranceRequest request)
        {
            var insurance = await _insuranceRepository.GetInsuranceAsync(request.InsuranceId);
            insurance.Delete();
            _insuranceRepository.DeleteInsurance(insurance);

            await _insuranceRepository.UnitOfWork.SaveEntitiesAsync();
        }
    }
}
