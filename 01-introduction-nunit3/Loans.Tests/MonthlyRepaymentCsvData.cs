using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;

namespace Loans.Tests
{
    public class MonthlyRepaymentCsvData
    {
        public static IEnumerable GetTestCases(string csvFileName, string cultureInfo = null)
        {
            // Make sure that dots on numbers are equal to unit thousand separation instead of comma
            cultureInfo = cultureInfo ?? "en-US";
            CultureInfo oldCI = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = new CultureInfo(cultureInfo);

            var csvLines = File.ReadAllLines(csvFileName);
            var testCases = new List<TestCaseData>();
            foreach (var line in csvLines)
            {
                string[] values = line.Replace(" ", "").Split(",");

                decimal principal = decimal.Parse(values[0]);
                decimal interestRate = decimal.Parse(values[1]);
                int termInYears = int.Parse(values[2]);
                decimal expectedRepayment = decimal.Parse(values[3]);

                testCases.Add(new TestCaseData(principal, interestRate, termInYears, expectedRepayment));
            }

            return testCases;
        }
    }
}
