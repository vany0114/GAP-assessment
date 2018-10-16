using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gap.Insurance.API.Services
{
    public interface IInsuranceService
    {
        Task<Application.Model.Insurance> GetInsuranceAsync(int insuranceId);

        Task<IList<Application.Model.Insurance>> GetInsurancesAsync();

        Task<int> CreateInsurance(Application.Model.CreateInsuranceRequest insuranceRequest);

        Task AddCoverageToInsurance(Application.Model.AddCoverageRequest request);

        Task DeleteInsurance(Application.Model.DeleteInsuranceRequest request);
    }
}