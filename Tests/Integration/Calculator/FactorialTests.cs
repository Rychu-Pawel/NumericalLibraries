using System.Reflection;

namespace Tests.Integration.Calculator
{
    [TestClass]
    public class FactorialTests
    {
        private static double InvokeFactorial(double value)
        {
            var assembly = typeof(Rychusoft.NumericalLibraries.Calculator.Calculator).Assembly;
            var factorialType = assembly.GetType("Rychusoft.NumericalLibraries.Calculator.Factorial", throwOnError: true)!;
            var method = factorialType.GetMethod("Compute", BindingFlags.Public | BindingFlags.Static)!;

            return (double)method.Invoke(null, new object[] { value })!;
        }

        [DataTestMethod]
        [DataRow(0d, 1d)]
        [DataRow(5d, 120d)]
        [DataRow(-4d, -24d)]
        [DataRow(19d, 121645100408832000d)]
        public void Compute_ReturnsExpectedValueForIntegerInputs(double input, double expected)
        {
            var result = InvokeFactorial(input);

            Assert.AreEqual(expected, result, 1e-10);
        }

        [TestMethod]
        public void Compute_ForNonIntegerInput_UsesApproximation()
        {
            var result = InvokeFactorial(0.5);
            var expected = Math.Sqrt(Math.PI) / 2.0;

            Assert.AreEqual(expected, result, 2e-3);

            var advancedResult = InvokeFactorial(5.5);
            var advancedExpected = 5.5 * 4.5 * 3.5 * 2.5 * 1.5 * 0.5 * Math.Sqrt(Math.PI);

            Assert.AreEqual(advancedExpected, advancedResult, 3e-2);
        }
    }
}
