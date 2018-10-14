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

        // EF doesn't support auto-properties readonly to run migrations
        private double _cost;
        private int _coveragePeriod;
        private DateTime _creationDate;
        private DateTime _startDate;
        private string _description;
        private string _name;
        private RiskType _risk;
        private int _customerId;

        public string Name => _name;

        public string Description => _description;

        public DateTime StartDate => _startDate;

        public DateTime CreationDate => _creationDate;

        public int CoveragePeriod => _coveragePeriod;

        public double Cost => _cost;

        public RiskType Risk => _risk;

        // Using List<>.AsReadOnly() 
        // This will create a read only wrapper around the private list so is protected against "external updates".
        // It's much cheaper than .ToList() because it will not have to copy all items in a new collection. (Just one heap alloc for the wrapper instance)
        //https://msdn.microsoft.com/en-us/library/e78dcd75(v=vs.110).aspx 
        public IReadOnlyCollection<InsuranceCoverage> Coverages => _coverage;

        public int CustomerId => _customerId;

        protected Insurance()
        {
            _coverage = new List<InsuranceCoverage>();
        }

        public Insurance(string name, string description, DateTime start, int coveragePeriod, double cost, RiskType risk, int customerId)
        : this()
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new InsuranceDomainException($"{nameof(name)} is required.");

            if (start < DateTime.UtcNow)
                throw new InsuranceDomainException("Invalid start date.");

            if (coveragePeriod == default(int))
                throw new InsuranceDomainException("Invalid coverage period.");

            if (cost <= 0)
                throw new InsuranceDomainException("Invalid insurance cost.");

            if (customerId == default(int))
                throw new InsuranceDomainException($"{nameof(customerId)} is required.");

            _name = name;
            _description = description;
            _startDate = start;
            _coveragePeriod = coveragePeriod;
            _cost = cost;
            _risk = risk;
            _customerId = customerId;
            _creationDate = DateTime.UtcNow;
        }

        public void AddCoverage(int coverageId, decimal percentage)
        {
            var existingCoverage = _coverage.ToList().FirstOrDefault(x => x.CoverageId == coverageId);
            if (existingCoverage != null)
                throw new InsuranceDomainException($"The coverage {existingCoverage?.Coverage?.Name} is already assigned to this insurance.");

            var percentageCoverage = Coverages.Sum(x => x.Percentage) + percentage;
            if (percentageCoverage > 100)
                throw new InsuranceDomainException("The percentage coverage can't be greater than 100%.");

            if (Risk == RiskType.High)
            {
                if (percentageCoverage > 50)
                    throw new InsuranceDomainException("The percentage coverage can't be greater than 50% since the risk of this insurance is high.");
            }

            _coverage.Add(new InsuranceCoverage(coverageId, Id, percentage));
        }
    }
}
