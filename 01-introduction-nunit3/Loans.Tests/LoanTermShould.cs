using Loans.Domain.Applications;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;

namespace Loans.Tests
{
    [TextFixture]
    public class LoanTermShould
    {
        [Test]
        public void ReturnTermInMonths()
        {
            var sut = new LoanTerm(1);
            Assert.That(sut.ToMonths(), Is.EqualTo(12), "Months should be 12 * numbers of years.");
        }

        [Test]
        public void StoreYears()
        {
            var sut = new LoanTerm(1);
            // Similar ways of asserting if the years from the loan term
            Assert.That(sut.Years, Is.EqualTo(1));
            Assert.That(sut.Years, new EqualConstraint(1));
        }

        [Test]
        public void RespectValueEquality()
        {
            // Value or memory reference type (e.g. classes) comparison
            var a = new LoanTerm(1);
            var b = new LoanTerm(1);
            Assert.That(a, Is.EqualTo(b));
        }

        [Test]
        public void RespectValueInequality()
        {
            var a = new LoanTerm(1);
            var b = new LoanTerm(2);
            Assert.That(a, Is.Not.EqualTo(b));
        }

        [Test]
        public void ReferenceEqualityExample()
        {
            // Checking if variables point to the same object in memory
            var a = new LoanTerm(1);
            var b = a;
            var c = new LoanTerm(1);

            Assert.That(a, Is.SameAs(b));
            Assert.That(a, Is.Not.SameAs(c));

            var x = new List<string> { "a", "b" };
            var y = x;
            var z = new List<string> { "a", "b" };

            Assert.That(y, Is.SameAs(x));
            Assert.That(z, Is.Not.SameAs(x));
        }

        [Test]
        public void Double()
        {
            double a = 1.0 / 3.0;
            Assert.That(a, Is.EqualTo(0.33).Within(0.004));
            Assert.That(a, Is.EqualTo(0.33).Within(10).Percent);
        }

        [Test]
        public void NotAllowZeroYears()
        {
            // Get exception throw assertion
            Assert.That(() => new LoanTerm(0), Throws.TypeOf<ArgumentOutOfRangeException>());
            Assert.That(() => new LoanTerm(0), Throws.TypeOf<ArgumentOutOfRangeException>()
                .With
                //.Property("Message")
                .Message
                .EqualTo("Please specify a value greater than 0.\r\nParameter name: years"));
            
            // Assert for exception type and parameter name without caring about the message
            Assert.That(() => new LoanTerm(0), Throws.TypeOf<ArgumentOutOfRangeException>()
                .With
                .Property("ParamName")
                .EqualTo("years"));

            // Asert same as above, but comparing with exact property from instance
            Assert.That(() => new LoanTerm(0), Throws.TypeOf<ArgumentOutOfRangeException>()
                .With
                .Matches<ArgumentOutOfRangeException>(
                    ex => ex.ParamName == "years"));
        }
    }
}
