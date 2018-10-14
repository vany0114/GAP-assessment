using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gap.Domain.Customer.Model;
using Gap.Domain.Customer.Persistence;
using Gap.Infrastructure.Repository.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Gap.Domain.Customer.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerContext _context;

        public CustomerRepository(CustomerContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task AddCustomerAsync(Model.Customer customer) =>
            await _context.Customers.AddAsync(customer);

        public async Task<Model.Customer> GetCustomerAsync(int customerId) =>
            await _context.Customers
                .Include(x => x.Insurances)
                .SingleOrDefaultAsync(x => x.Id == customerId);

        public async Task<IList<Model.Customer>> GetCustomersAsync() =>
            await _context.Customers
                .Include(x => x.Insurances)
                .ToListAsync();

        public void UpdateCustomer(Model.Customer customer) =>
            _context.Entry(customer).State = EntityState.Modified;

        public async Task<IList<CustomerInsurance>> GetActiveInsurancesAsync(int insuranceId) =>
            await _context.CustomerInsurances
                .Where(x => x.InsuranceId == insuranceId && x.Status == Status.Assigned)
                .ToListAsync();

        public async Task<IList<CustomerInsurance>> GetInsurancesByIdAsync(int insuranceId) =>
            await _context.CustomerInsurances
                .Where(x => x.InsuranceId == insuranceId)
                .ToListAsync();
    }
}
