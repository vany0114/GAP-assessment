using System;
using System.Collections.Generic;
using System.Linq;
using Gap.Domain.Insurance.Exceptions;
using Gap.Infrastructure.DDD;

namespace Gap.Domain.Insurance.Model
{
    public class Insurance : Entity, IAggregateRoot
    {
        // Using a private collection field, better for DDD Aggregate's encapsulation
        // so _coverage cannot be added from "outside the AggregateRoot" directly to the collection,
        // but only through the method AddCoverage() which includes behaviour.
        private readonly List<InsuranceCoverage> _coverage;

        public string Name { get; }

        public string Description { get; }

        public DateTime StartDate { get; }

        public DateTime CreationDate { get; }

        public int CoveragePeriod { get; }

        public double Cost { get; }

        public RiskType Risk { get; }

        // Using List<>.AsReadOnly() 
        // This will create a read only wrapper around the private list so is protected against "external updates".
        // It's much cheaper than .ToList() because it will not have to copy all items in a new collection. (Just one heap alloc for the wrapper instance)
        //https://msdn.microsoft.com/en-us/library/e78dcd75(v=vs.110).aspx 
        public IReadOnlyCollection<InsuranceCoverage> Coverages => _coverage;

        public int CustomerId { get; }

        protected Insurance()
        {
            _coverage = new List<InsuranceCoverage>();
        }

        public Insurance(string name, string description, DateTime start, int coveragePeriod, double cost, RiskType risk, int customerId)
        {
            if(string.IsNullOrWhiteSpace(name))
                throw  new InsuranceDomainException($"{nameof(name)} is required.");

            if (string.IsNullOrWhiteSpace(name))
                throw new InsuranceDomainException($"{nameof(description)} is required.");

            if(start < DateTime.UtcNow)
                throw new InsuranceDomainException("Invalid start date.");

            if(coveragePeriod == default(int))
                throw new InsuranceDomainException("Invalid coverage period.");

            if (cost <= 0)
                throw new InsuranceDomainException("Invalid insurance cost.");

            if (customerId == default(int))
                throw new InsuranceDomainException($"{nameof(customerId)} is required.");

            Name = name;
            Description = description;
            StartDate = start;
            CoveragePeriod = coveragePeriod;
            Cost = cost;
            Risk = risk;
            CustomerId = customerId;
            CreationDate = DateTime.UtcNow;
        }

        public void AddCoverage(int coverageId, int percentage)
        {
            if (Risk == RiskType.High)
            {
                var percentageCoverage = Coverages.Sum(x => x.Percentage);
                if (percentageCoverage > 50)
                {
                    throw new InsuranceDomainException($"The percentage coverage can't be greater than 50% since the risk of this insurance is high.");
                }
            }

            _coverage.Add(new InsuranceCoverage(coverageId, Id, percentage));
        }
    }
}
