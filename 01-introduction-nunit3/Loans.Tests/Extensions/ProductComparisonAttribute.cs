using NUnit.Framework;
using System;

namespace Loans.Tests
{
    // Corresponds to a inherited custom category attribute for traits group by.
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
    public class ProductComparisonAttribute : CategoryAttribute
    {
    }
}
