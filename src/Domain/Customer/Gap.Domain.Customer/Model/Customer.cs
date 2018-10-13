using Gap.Domain.Customer.Exceptions;
using Gap.Infrastructure.DDD;

namespace Gap.Domain.Customer.Model
{
    public class Customer : Entity, IAggregateRoot
    {
        public string Name { get; }

        public string Email { get; }

        public string PhoneNumber { get; }

        public Customer(string name, string email, string phoneNumber)
        {
            if(string.IsNullOrWhiteSpace(name))
                throw new CustomerDomainException("The customer must have a name.");

            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
        }
    }
}
