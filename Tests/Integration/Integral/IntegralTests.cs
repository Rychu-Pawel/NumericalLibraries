using Rychusoft.NumericalLibraries.Integral.Exceptions;

namespace Tests.Integration.Integral
{
    [TestClass]
    public class IntegralTests
    {
        [TestMethod]
        public void ComputeIntegral_ForPolynomialFunction_ReturnsExpectedValue()
        {
            var integral = new Rychusoft.NumericalLibraries.Integral.Integral("3*x^2-5", 5d, 10d);

            var result = integral.ComputeIntegral();

            Assert.AreEqual(850d, result, 1e-6);

            var advancedIntegral = new Rychusoft.NumericalLibraries.Integral.Integral("sin(x)+x^2", 0d, Math.PI);
            var advancedResult = advancedIntegral.ComputeIntegral();
            var advancedExpected = 2d + Math.Pow(Math.PI, 3d) / 3d;

            Assert.AreEqual(advancedExpected, advancedResult, 1e-3);
        }

        [TestMethod]
        public void ComputeIntegral_ForSymmetricOddFunction_IsCloseToZero()
        {
            var integral = new Rychusoft.NumericalLibraries.Integral.Integral("x", -1d, 1d);

            var result = integral.ComputeIntegral();

            Assert.AreEqual(0d, result, 1e-6);

            var advancedIntegral = new Rychusoft.NumericalLibraries.Integral.Integral("x^3+x", -2d, 2d);
            var advancedResult = advancedIntegral.ComputeIntegral();

            Assert.AreEqual(0d, advancedResult, 1e-6);
        }

        [TestMethod]
        public void Constructor_WhenInfinityBoundProvided_ThrowsException()
        {
            Assert.ThrowsException<IntegralInfinityRangeNotSupportedException>(
                () => new Rychusoft.NumericalLibraries.Integral.Integral("x", 0d, double.PositiveInfinity));
        }
    }
}
