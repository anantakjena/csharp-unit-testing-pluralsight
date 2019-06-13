using System;
using Loans.Domain.Applications;
using Moq;
using NUnit.Framework;

namespace Loans.Tests
{
    public class LoanApplicationProcessorShould
    {
        [Test]
        public void DeclineLowSalary()
        {
            LoanProduct product = new LoanProduct(99, "Loan", 5.25m);
            LoanAmount amount = new LoanAmount("USD", 200_000);
            var application = new LoanApplication(42, product, amount, "Sarah", 25, "133 Pluralsight Drive, Draper, Utah", 64_999);

            var mockIdentityVerifier = new Mock<IIdentityVerifier>();
            var mockCreditScorer = new Mock<ICreditScorer>();

            // System under test instance
            var sut = new LoanApplicationProcessor(mockIdentityVerifier.Object, mockCreditScorer.Object);
            sut.Process(application);
            Assert.That(application.GetIsAccepted(), Is.False);
        }

        delegate void ValidateCallback(string applicantName, int applicantAge, string applicantAddress, ref IdentityVerificationStatus status);

        [Test]
        public void AcceptApplication()
        {
            LoanProduct product = new LoanProduct(99, "Loan", 5.25m);
            LoanAmount amount = new LoanAmount("USD", 200_000);
            var application = new LoanApplication(42, product, amount, "Sarah", 25, "133 Pluralsight Drive, Draper, Utah", 65_000);

            // Mock object configuration is needed in this case.
            var mockIdentityVerifier = new Mock<IIdentityVerifier>();
            var mockCreditScorer = new Mock<ICreditScorer>();

            // Instead of setting a literal value for an argument, It class specifies different ways that can match with the arguments values from an class implementation.
            // Doesn't check if the right arguments are being passed.
            //mockIdentityVerifier.Setup(x => x.Validate(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>())).Returns(true);

            // Mock using literal parameter values setting a specific method for configuration and testing.
            //mockIdentityVerifier.Setup(x => x.Validate("Sarah", 25, "133 Pluralsight Drive, Draper, Utah")).Returns(true);

            // Mock with an output parameter configuration. Needs to add a proper param configuration for the specific overload of the interface method.
            //bool isValidOutValue = true;
            //mockIdentityVerifier.Setup(x => x.Validate("Sarah", 25, "133 Pluralsight Drive, Draper, Utah", out isValidOutValue));

            // Mock with a ref parameter configuration with callback that calls in conjunction with a delegate validate replacement method with by-ref parameters, specifying which param should be returned.
            mockIdentityVerifier.Setup(x => x.Validate("Sarah", 25, "133 Pluralsight Drive, Draper, Utah", ref It.Ref<IdentityVerificationStatus>.IsAny))
                .Callback(new ValidateCallback(
                    (string applicantName, int applicantAge, string applicantAddress, ref IdentityVerificationStatus status) =>
                        status = new IdentityVerificationStatus(true)));

            // System under test instance
            var sut = new LoanApplicationProcessor(mockIdentityVerifier.Object, mockCreditScorer.Object);
            sut.Process(application);
            Assert.That(application.GetIsAccepted(), Is.True);
        }

        [Test]
        public void NullReturnExample()
        {
            var mock = new Mock<INullExample>();
            // By default, Moq methods that return reference types by default return null, hence the needless explicit chain definition of a return.
            // However, whenever a null is being returned explicitly with the Returns method, a generic type parameter needs to be specified.
            mock.Setup(x => x.SomeMethod());
                //.Returns<string>(null);
            string mockReturnValue = mock.Object.SomeMethod();
            Assert.That(mockReturnValue, Is.Null);
        }
    }

    public interface INullExample
    {
        string SomeMethod();
    }
}
