using System.Collections.Generic;
using Gap.Domain.Insurance.Exceptions;
using Gap.Infrastructure.DDD;

namespace Gap.Domain.Insurance.Model
{
    public class InsuranceCoverage : ValueObject
    {
        public int CoverageId { get; }

        public int InsuranceId { get; }

        public decimal Percentage { get; }

        // EF navigation properties
        public CoverageType Coverage { get; }

        public Insurance Insurance { get; }

        // to prevent consumers create the entity, the aggregate root is the only one who can create this entity.
        protected InsuranceCoverage()
        {
        }

        internal InsuranceCoverage(int coverageId, int insuranceId, decimal percentage)
        {
            if (coverageId == default(int) || insuranceId == default(int))
                throw new InsuranceDomainException("Invalid relationship between insurance and coverage type.");

            if(percentage > 100)
                throw new InsuranceDomainException("Invalid percentage coverage.");

            CoverageId = coverageId;
            InsuranceId = insuranceId;
            Percentage = percentage;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return CoverageId;
            yield return InsuranceId;
            yield return Percentage;
        }
    }
}
