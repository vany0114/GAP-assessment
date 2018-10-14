using System;
using System.Collections.Generic;
using System.Linq;
using Gap.Domain.Insurance.Events;
using Gap.Domain.Insurance.Exceptions;
using Gap.Infrastructure.DDD;

namespace Gap.Domain.Insurance.Model
{
    public class Insurance : Entity, IAggregateRoot
    {
        // EF doesn't support auto-properties readonly to run migrations
        private double _cost;
        private int _coveragePeriod;
        private DateTime _creationDate;
        private DateTime _startDate;
        private string _description;
        private string _name;
        private bool _hasActiveCustomers;
        private RiskType _risk;

        // Using a private collection field, better for DDD Aggregate's encapsulation
        // so _coverage cannot be added from "outside the AggregateRoot" directly to the collection,
        // but only through the method AddCoverage() which includes behaviour.
        private readonly List<InsuranceCoverage> _coverage;

        public string Name => _name;

        public string Description => _description;

        public DateTime StartDate => _startDate;

        public DateTime CreationDate => _creationDate;

        public int CoveragePeriod => _coveragePeriod;

        public double Cost => _cost;

        public RiskType Risk => _risk;

        public bool HasActiveCustomers => _hasActiveCustomers;

        // Using List<>.AsReadOnly() 
        // This will create a read only wrapper around the private list so is protected against "external updates".
        // It's much cheaper than .ToList() because it will not have to copy all items in a new collection. (Just one heap alloc for the wrapper instance)
        //https://msdn.microsoft.com/en-us/library/e78dcd75(v=vs.110).aspx 
        public IReadOnlyCollection<InsuranceCoverage> Coverages => _coverage;

        protected Insurance()
        {
            _coverage = new List<InsuranceCoverage>();
        }

        public Insurance(string name, string description, DateTime start, int coveragePeriod, double cost, RiskType risk)
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

            _name = name;
            _description = description;
            _startDate = start;
            _coveragePeriod = coveragePeriod;
            _cost = cost;
            _risk = risk;
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

        /// <summary>
        /// To indicate that the insurance is being used by customers.
        /// </summary>
        public void UseByCustomers()
        {
            _hasActiveCustomers = true;
        }

        /// <summary>
        /// To indicate the insurance isn't being used by any customer.
        /// </summary>
        public void Release()
        {
            _hasActiveCustomers = false;
        }

        public void Delete()
        {
            if(_hasActiveCustomers)
                throw new InsuranceDomainException("The current insurance can't be deleted because is being used.");

            AddDomainEvent(new InsuranceDeleted(Id));
        }
    }
}
