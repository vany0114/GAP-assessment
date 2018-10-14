using System;
using System.Collections.Generic;

namespace Gap.Insurance.API.Application.Model
{
    public class Insurance
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime CreationDate { get; set; }

        public int CoveragePeriod { get; set; }

        public double Cost { get; set; }

        public RiskType Risk { get; set; }

        public Customer Customer { get; set; }

        public IList<InsuranceCoverage> Coverages { get; set; }
    }

    public class InsuranceCoverage
    {
        public CoverageType CoverageType { get; set; }

        public decimal Percentage { get; set; }
    }

    public class CoverageType
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }

    public enum RiskType
    {
        Low = 1,
        Medium = 2,
        MediumHigh = 3,
        High = 4
    }
}
