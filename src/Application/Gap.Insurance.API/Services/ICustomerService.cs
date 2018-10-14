using Gap.Insurance.API.Application.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gap.Insurance.API.Services
{
    public interface ICustomerService
    {
        Task<IList<Customer>> GetCustomersAsync();

        Task<Customer> GetCustomerAsync(int customerId);

        Task AssignInsurance(AssignCancelInsuranceRequest request);

        Task CancelInsurance(AssignCancelInsuranceRequest request);
    }
}
