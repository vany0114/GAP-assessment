using System.Collections.Generic;
using System.Threading.Tasks;
using Gap.Domain.Insurance.Persistence;
using Gap.Infrastructure.Repository.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Gap.Domain.Insurance.Repository
{
    public class InsuranceRepository : IInsuranceRepository
    {
        private readonly InsuranceContext _context;

        public InsuranceRepository(InsuranceContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<int> AddInsuranceAsync(Model.Insurance insurance)
        {
            var result = await _context.Insurances.AddAsync(insurance);
            return result.Entity.Id;
        }

        public async Task<Model.Insurance> GetInsuranceAsync(int insuranceId) =>
            await _context.Insurances
                .Include(x => x.Coverages)
                .ThenInclude(x => x.Coverage)
                .SingleOrDefaultAsync(x => x.Id == insuranceId);

        public async Task<IList<Model.Insurance>> GetInsurancesAsync() =>
            await _context.Insurances
                .Include(x => x.Coverages)
                .ThenInclude(x => x.Coverage)
                .ToListAsync();

        public void UpdateInsurance(Model.Insurance insurance) => 
            _context.Entry(insurance).State = EntityState.Modified;

        public void DeleteInsurance(Model.Insurance insurance) =>
            _context.Entry(insurance).State = EntityState.Deleted;
    }
}
