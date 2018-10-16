using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gap.Domain.Insurance.Model;
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

                    if (!context.Insurances.Any())
                    {
                        context.Insurances.AddRange(GetPreconfiguredInsurances());
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

        private IEnumerable<CoverageType> GetPreconfiguredCoverageTypes() => new List<CoverageType>
        {
            new CoverageType(1, "Earthquake", null),
            new CoverageType(2, "Fire", null),
            new CoverageType(3, "Stole", null),
            new CoverageType(4, "Lost", null)
        };

        private IEnumerable<Model.Insurance> GetPreconfiguredInsurances() => new List<Model.Insurance>
        {
            new Model.Insurance("Insurance 1", null, DateTime.Now.AddDays(5), 5, 1500000, RiskType.MediumHigh),
            new Model.Insurance("Insurance 2", null, DateTime.Now.AddMonths(2), 5, 5000000, RiskType.Low),
            new Model.Insurance("Insurance 3", null, DateTime.Now.AddDays(5), 5, 1500000, RiskType.High)
        };
    }
}
