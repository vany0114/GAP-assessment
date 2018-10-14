using System;
using Gap.Domain.Insurance.Exceptions;
using Gap.Domain.Insurance.Model;
using Moq;
using NUnit.Framework;

namespace Gap.Domain.Insurance.UnitTest
{
    [TestFixture]
    public class InsuranceSpecs
    {
        [Test]
        [Category("Creation")]
        public void Should_Fails_Due_To_Name_Is_Empty()
        {
            var exception = Assert.Throws<InsuranceDomainException>(() =>
            {
                var insurance = new Model.Insurance(string.Empty, null, DateTime.Now, 0, 0, RiskType.High, 0);
            });

            Assert.IsInstanceOf<InsuranceDomainException>(exception);
            Assert.AreEqual(exception.Message, "name is required.");
        }

        [Test]
        [Category("Creation")]
        public void Should_Fails_Due_To_StartDate_Is_Invalid()
        {
            var exception = Assert.Throws<InsuranceDomainException>(() =>
            {
                var insurance = new Model.Insurance("test", null, DateTime.Now.AddDays(-1), 0, 0, RiskType.High, 0);
            });

            Assert.IsInstanceOf<InsuranceDomainException>(exception);
            Assert.AreEqual(exception.Message, "Invalid start date.");
        }

        [Test]
        [Category("Creation")]
        public void Should_Fails_Due_To_CoveragePeriod_Is_Invalid()
        {
            var exception = Assert.Throws<InsuranceDomainException>(() =>
            {
                var insurance = new Model.Insurance("test", null, DateTime.Now.AddDays(30), 0, 0, RiskType.High, 0);
            });

            Assert.IsInstanceOf<InsuranceDomainException>(exception);
            Assert.AreEqual(exception.Message, "Invalid coverage period.");
        }

        [Test]
        [Category("Creation")]
        public void Should_Fails_Due_To_Cost_Is_Invalid()
        {
            var exception = Assert.Throws<InsuranceDomainException>(() =>
            {
                var insurance = new Model.Insurance("test", null, DateTime.Now.AddDays(30), 5, 0, RiskType.High, 0);
            });

            Assert.IsInstanceOf<InsuranceDomainException>(exception);
            Assert.AreEqual(exception.Message, "Invalid insurance cost.");
        }

        [Test]
        [Category("Creation")]
        public void Should_Fails_Due_To_CustomerId_Is_Invalid()
        {
            var exception = Assert.Throws<InsuranceDomainException>(() =>
            {
                var insurance = new Model.Insurance("test", null, DateTime.Now.AddDays(30), 5, 50000, RiskType.High, 0);
            });

            Assert.IsInstanceOf<InsuranceDomainException>(exception);
            Assert.AreEqual(exception.Message, "customerId is required.");
        }

        [Test]
        [Category("Creation")]
        public void Should_Create_The_Insurance()
        {
            var insurance = new Model.Insurance("test", null, DateTime.Now.AddDays(30), 5, 50000, RiskType.High, 1);

            Assert.IsInstanceOf<Model.Insurance>(insurance);
            Assert.AreEqual(insurance.Name, "test");
            Assert.AreEqual(insurance.CoveragePeriod, 5);
            Assert.AreEqual(insurance.Cost, 50000);
            Assert.AreEqual(insurance.Risk, RiskType.High);
            Assert.AreEqual(insurance.CustomerId, 1);
            Assert.IsNull(insurance.Description);
        }

        [Test]
        [Category("AddCoverage")]
        public void Should_Fails_Due_To_Coverage_Already_Exists()
        {
            // we need to mock the id since that value is only generated through the DB
            var insurance = new Mock<Model.Insurance>();
            insurance.Setup(x => x.Id).Returns(1);

            insurance.Object.AddCoverage(1, 50);
            var exception = Assert.Throws<InsuranceDomainException>(() =>
            {
                insurance.Object.AddCoverage(1, 50);
            });

            Assert.IsInstanceOf<InsuranceDomainException>(exception);
            Assert.AreEqual(exception.Message, "The coverage  is already assigned to this insurance.");
        }

        [Test]
        [Category("AddCoverage")]
        public void Should_Fails_Due_To_Coverage_Exceed_100_Percentage()
        {
            // we need to mock the id since that value is only generated through the DB
            var insurance = new Mock<Model.Insurance>();
            insurance.Setup(x => x.Id).Returns(1);

            insurance.Object.AddCoverage(1, 50);
            var exception = Assert.Throws<InsuranceDomainException>(() =>
            {
                insurance.Object.AddCoverage(2, 60);
            });

            Assert.IsInstanceOf<InsuranceDomainException>(exception);
            Assert.AreEqual(exception.Message, "The percentage coverage can't be greater than 100%.");
        }

        [Test]
        [Category("AddCoverage")]
        public void Should_Fails_When_The_Risk_Is_High_And_Exceed_50_Percentage()
        {
            // we need to mock the id since that value is only generated through the DB
            var insurance = new Mock<Model.Insurance>(MockBehavior.Strict, "test", null, DateTime.Now.AddDays(30), 5, 50000, RiskType.High, 1);
            insurance.Setup(x => x.Id).Returns(1);

            insurance.Object.AddCoverage(1, 30);
            var exception = Assert.Throws<InsuranceDomainException>(() =>
            {
                insurance.Object.AddCoverage(2, 30);
            });

            Assert.IsInstanceOf<InsuranceDomainException>(exception);
            Assert.AreEqual(exception.Message, "The percentage coverage can't be greater than 50% since the risk of this insurance is high.");
        }

        [Test]
        [Category("AddCoverage")]
        public void Should_Add_Coverage()
        {
            // we need to mock the id since that value is only generated through the DB
            var insurance = new Mock<Model.Insurance>();
            insurance.Setup(x => x.Id).Returns(1);

            insurance.Object.AddCoverage(1, 50);
            insurance.Object.AddCoverage(2, 50);

            Assert.AreEqual(insurance.Object.Coverages.Count, 2);
        }
    }
}
