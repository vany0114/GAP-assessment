using Gap.Domain.Customer.Exceptions;
using NUnit.Framework;

namespace Gap.Domain.Customer.UnitTest
{
    [TestFixture]
    public class CustomerSpecs
    {
        [Test]
        [Category("Creation")]
        public void Should_Fails_Due_To_Name_Is_Empty()
        {
            var exception = Assert.Throws<CustomerDomainException>(() =>
            {
                var customer = new Model.Customer(string.Empty, "test@test.com", null);
            });

            Assert.IsInstanceOf<CustomerDomainException>(exception);
            Assert.AreEqual(exception.Message, "The customer must have a name.");
        }

        [Test]
        [Category("Creation")]
        public void Should_Fails_Due_To_Name_Is_Null()
        {
            var exception = Assert.Throws<CustomerDomainException>(() =>
            {
                var customer = new Model.Customer(null, "test@test.com", null);
            });

            Assert.IsInstanceOf<CustomerDomainException>(exception);
            Assert.AreEqual(exception.Message, "The customer must have a name.");
        }

        [Test]
        [Category("Creation")]
        public void Should_Fails_Due_To_Name_Is_A_WhiteSpace()
        {
            var exception = Assert.Throws<CustomerDomainException>(() =>
            {
                var customer = new Model.Customer(" ", "test@test.com", null);
            });

            Assert.IsInstanceOf<CustomerDomainException>(exception);
            Assert.AreEqual(exception.Message, "The customer must have a name.");
        }

        [Test]
        [Category("Creation")]
        public void Should_Create_The_Customer()
        {
            var customer = new Model.Customer("test customer", "test@test.com", null);

            Assert.IsInstanceOf<Model.Customer>(customer);
            Assert.AreEqual(customer.Name, "test customer");
            Assert.AreEqual(customer.Email, "test@test.com");
            Assert.IsNull(customer.PhoneNumber);
        }
    }
}
