using System;
using System.Collections.Generic;
using System.Text;
using Gap.Domain.Customer.Exceptions;
using Gap.Infrastructure.DDD;

namespace Gap.Domain.Customer.Model
{
    public class Customer : Entity, IAggregateRoot
    {
        private string _name;
        private string _email;
        private string _phoneNumber;

        public string Name => _name;

        public string Email => _email;

        public string PhoneNumber => _phoneNumber;

        public Customer(string name, string email, string phoneNumber)
        {
            if(string.IsNullOrWhiteSpace(name))
                throw new CustomerDomainException("The customer must have a name.");

            _name = name;
            _email = email;
            _phoneNumber = phoneNumber;
        }
    }
}
