using System.Collections.Generic;
using System.Threading.Tasks;
using Gap.Infrastructure.Repository.Abstractions;

namespace Gap.Domain.Insurance.Repository
{
    public interface IInsuranceRepository : IRepository<Model.Insurance>
    {
        Task<IList<Model.Insurance>> GetInsurancesAsync();

        Task<Model.Insurance> GetInsuranceAsync(int insuranceId);

        Task<int> AddInsuranceAsync(Model.Insurance insurance);

        void UpdateInsurance(Model.Insurance insurance);
    }
}
