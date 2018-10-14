using Gap.Domain.Customer.Exceptions;
using Gap.Infrastructure.DDD;
using System.Collections.Generic;
using System.Linq;
using Gap.Domain.Customer.Events;

namespace Gap.Domain.Customer.Model
{
    public class Customer : Entity, IAggregateRoot
    {
        // EF doesn't support auto-properties readonly to run migrations
        private string _name;
        private string _email;
        private string _phoneNumber;

        // Using a private collection field, better for DDD Aggregate's encapsulation
        // so _coverage cannot be added from "outside the AggregateRoot" directly to the collection,
        // but only through the method AddCoverage() which includes behaviour.
        private readonly List<CustomerInsurance> _insurances;

        public string Name => _name;

        public string Email => _email;

        public string PhoneNumber => _phoneNumber;

        // Using List<>.AsReadOnly() 
        // This will create a read only wrapper around the private list so is protected against "external updates".
        // It's much cheaper than .ToList() because it will not have to copy all items in a new collection. (Just one heap alloc for the wrapper instance)
        //https://msdn.microsoft.com/en-us/library/e78dcd75(v=vs.110).aspx 
        public IReadOnlyCollection<CustomerInsurance> Insurances => _insurances;

        protected Customer()
        {
            _insurances = new List<CustomerInsurance>();
        }

        public Customer(string name, string email, string phoneNumber) : this()
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new CustomerDomainException("The customer must have a name.");

            _name = name;
            _email = email;
            _phoneNumber = phoneNumber;
        }

        public void AssignInsurance(int insuranceId)
        {
            if (insuranceId == default(int))
                throw new CustomerDomainException($"You must specify the {nameof(insuranceId)} in order to assign it to the customer.");

            if (_insurances.Any(x => x.InsuranceId == insuranceId && x.Status == Status.Assigned))
                throw new CustomerDomainException("That insurance is already assigned to the customer.");

            _insurances.Add(new CustomerInsurance(Id, insuranceId));
            AddDomainEvent(new InsuranceAssigned(insuranceId, Id));
        }

        public void CancelInsurance(int insuranceId)
        {
            var insurance = _insurances.FirstOrDefault(x => x.InsuranceId == insuranceId && x.Status == Status.Assigned);
            if (insurance == null)
                throw new CustomerDomainException($"Customer doesn't have assigned the insurance {insuranceId}");

            insurance.CancelInsurance();
            AddDomainEvent(new InsuranceCancelled(insuranceId, Id));
        }

        public void DeleteInsurance(int insuranceId)
        {
            if (_insurances.Any(x => x.InsuranceId == insuranceId) == false)
                throw new CustomerDomainException($"Customer doesn't have assigned the insurance {insuranceId}");

            _insurances.RemoveAll(x => x.InsuranceId == insuranceId);
        }
    }
}
