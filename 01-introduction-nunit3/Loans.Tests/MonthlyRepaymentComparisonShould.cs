using Loans.Domain.Applications;
using NUnit.Framework;

namespace Loans.Tests
{
    [ProductComparison]
    public class MonthlyRepaymentComparisonShould
    {
        [Test]
        //[Category("Product Comparison")]
        public void RespectValueEquality()
        {
            var a = new MonthlyRepaymentComparison("a", 42.42m, 22.22m);
            var b = new MonthlyRepaymentComparison("a", 42.42m, 22.22m);

            Assert.That(a, Is.EqualTo(b));
        }

        [Test]
        public void RespectValueInequality()
        {
            var a = new MonthlyRepaymentComparison("a", 42.42m, 22.22m);
            var b = new MonthlyRepaymentComparison("b", 42.42m, 23.22m);

            Assert.That(a, Is.Not.EqualTo(b));
        }
    }
}
