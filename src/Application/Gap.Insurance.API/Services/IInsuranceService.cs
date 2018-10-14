using System.Threading.Tasks;

namespace Gap.Insurance.API.Services
{
    public interface IInsuranceService
    {
        Task<Application.Model.Insurance> GetInsuranceAsync(int insuranceId);

        Task<int> CreateInsurance(Application.Model.CreateInsuranceRequest insuranceRequest);

        Task AddCoverageToInsurance(Application.Model.AddCoverageRequest request);
    }
}