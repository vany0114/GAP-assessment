using System.Collections.Generic;
using System.Threading.Tasks;
using Gap.Insurance.Web.ViewModels;

namespace Gap.Insurance.Web.Services
{
    public interface ICustomerService
    {
        Task<IList<Customer>> GetCustomersAsync();

        Task<Customer> GetCustomerAsync(int customerId);

        Task AssignInsuranceAsync(Assignment assignment);

        Task CancelInsuranceAsync(Assignment assignment);
    }
}
