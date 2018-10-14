using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Gap.Domain.Insurance.Persistence
{
    public class InsuranceContextSeed
    {
        public async Task SeedAsync(InsuranceContext context, ILogger<InsuranceContextSeed> logger)
        {
            try
            {
                using (context)
                {
                    if (!context.CoverageTypes.Any())
                    {
                        context.CoverageTypes.AddRange(GetPreconfiguredCoverageTypes());
                        await context.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogTrace(ex, $"Exception {ex.GetType().Name} with message ${ex.Message}");
                throw;
            }
        }

        private IEnumerable<Model.CoverageType> GetPreconfiguredCoverageTypes() => new List<Model.CoverageType>
        {
            new Model.CoverageType(1, "Earthquake", null),
            new Model.CoverageType(2, "Fire", null),
            new Model.CoverageType(3, "Stole", null),
            new Model.CoverageType(4, "Lost", null)
        };
    }
}
