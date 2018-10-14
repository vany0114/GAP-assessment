using System.Collections.Generic;
using Gap.Domain.Insurance.Exceptions;
using Gap.Infrastructure.DDD;

namespace Gap.Domain.Insurance.Model
{
    public class InsuranceCoverage : ValueObject
    {
        // EF doesn't support auto-properties readonly to run migrations
        private int _coverageId;
        private int _insuranceId;
        private decimal _percentage;
        private CoverageType _coverage;
        private Insurance _insurance;

        public int CoverageId => _coverageId;

        public int InsuranceId => _insuranceId;

        public decimal Percentage => _percentage;

        // EF navigation properties
        public CoverageType Coverage => _coverage;

        public Insurance Insurance => _insurance;

        // to prevent consumers create the entity, the aggregate root is the only one who can create this entity.
        protected InsuranceCoverage()
        {
        }

        internal InsuranceCoverage(int coverageId, int insuranceId, decimal percentage)
        {
            if (coverageId == default(int) || insuranceId == default(int))
                throw new InsuranceDomainException("Invalid relationship between insurance and coverage type.");

            if(percentage > 100 || percentage < 0)
                throw new InsuranceDomainException("Invalid percentage coverage.");

            _coverageId = coverageId;
            _insuranceId = insuranceId;
            _percentage = percentage;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return CoverageId;
            yield return InsuranceId;
            yield return Percentage;
        }
    }
}
