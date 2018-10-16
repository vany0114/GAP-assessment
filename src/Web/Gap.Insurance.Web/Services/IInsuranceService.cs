using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gap.Insurance.Web.Services
{
    public interface IInsuranceService
    {
        Task<ViewModels.Insurance> GetInsuranceAsync(int insuranceId);

        Task<IList<ViewModels.Insurance>> GetInsurancesAsync();

        Task DeleteAsync(int id);

        Task CreateAsync(ViewModels.Insurance insurance);

        //Task AddCoverageToInsurance(Application.Model.AddCoverageRequest request);
    }
}