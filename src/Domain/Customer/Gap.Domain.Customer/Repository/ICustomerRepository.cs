using System.Collections.Generic;
using System.Threading.Tasks;
using Gap.Domain.Customer.Model;
using Gap.Infrastructure.Repository.Abstractions;

namespace Gap.Domain.Customer.Repository
{
    public interface ICustomerRepository : IRepository<Model.Customer>
    {
        Task<IList<Model.Customer>> GetCustomersAsync();

        Task<Model.Customer> GetCustomerAsync(int customerId);

        Task AddCustomerAsync(Model.Customer customer);

        void UpdateCustomer(Model.Customer customer);

        Task<IList<CustomerInsurance>> GetActiveInsurancesAsync(int insuranceId);

        Task<IList<CustomerInsurance>> GetInsurancesByIdAsync(int insuranceId);
    }
}
