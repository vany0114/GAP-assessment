using System.Collections.Generic;
using System.Threading.Tasks;
using Gap.Infrastructure.Repository.Abstractions;

namespace Gap.Domain.Customer.Repository
{
    public interface ICustomerRepository : IRepository<Model.Customer>
    {
        Task<IList<Model.Customer>> GetCustomersAsync();

        Task<Model.Customer> GetCustomerAsync(int customerId);

        Task AddCustomerAsync(Model.Customer customer);
    }
}
