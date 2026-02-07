namespace Tests.Integration.Derivative
{
    [TestClass]
    public class DerivativeTests
    {
        [TestMethod]
        public void ComputeFunctionValueAtPoint_ReturnsExpectedValue()
        {
            var derivative = new Rychusoft.NumericalLibraries.Derivative.Derivative("x^3");

            var result = derivative.ComputeFunctionValueAtPoint(2d);

            Assert.AreEqual(8d, result, 1e-10);

            var advancedDerivative = new Rychusoft.NumericalLibraries.Derivative.Derivative("sin(x)^2+cos(x)^2+exp(x/2)");
            var advancedResult = advancedDerivative.ComputeFunctionValueAtPoint(2d);

            Assert.AreEqual(1d + Math.Exp(1d), advancedResult, 1e-9);
        }

        [TestMethod]
        public void ComputeDerivative_ReturnsExpectedFirstDerivative()
        {
            var derivative = new Rychusoft.NumericalLibraries.Derivative.Derivative("x^3");

            var result = derivative.ComputeDerivative(2d);

            Assert.AreEqual(12d, result, 1e-6);

            var advancedDerivative = new Rychusoft.NumericalLibraries.Derivative.Derivative("x^4+sin(x)*exp(x)");
            var advancedResult = advancedDerivative.ComputeDerivative(1d);
            var expectedAdvanced = 4d + Math.Exp(1d) * (Math.Sin(1d) + Math.Cos(1d));

            Assert.AreEqual(expectedAdvanced, advancedResult, 1e-4);
        }

        [TestMethod]
        public void ComputeDerivativeBis_ReturnsExpectedSecondDerivative()
        {
            var derivative = new Rychusoft.NumericalLibraries.Derivative.Derivative("x^3");

            var result = derivative.ComputeDerivativeBis(2d);

            Assert.AreEqual(12d, result, 1e-4);

            var advancedDerivative = new Rychusoft.NumericalLibraries.Derivative.Derivative("x^4+sin(x)*exp(x)");
            var advancedResult = advancedDerivative.ComputeDerivativeBis(1d);
            var expectedAdvanced = 12d + 2d * Math.Exp(1d) * Math.Cos(1d);

            Assert.AreEqual(expectedAdvanced, advancedResult, 2e-3);
        }

        [TestMethod]
        public void ComputeDerivative_ForMixedExpression_ReturnsExpectedValue()
        {
            var derivative = new Rychusoft.NumericalLibraries.Derivative.Derivative("x^2+5-cos(2*PI*x)");

            var result = derivative.ComputeDerivative(1d);

            Assert.AreEqual(2d, result, 1e-6);

            var advancedDerivative = new Rychusoft.NumericalLibraries.Derivative.Derivative("x^3+exp(x/3)+sin(2*PI*x)");
            var advancedResult = advancedDerivative.ComputeDerivative(1d);
            var expectedAdvanced = 3d + Math.Exp(1d / 3d) / 3d + 2d * Math.PI;

            Assert.AreEqual(expectedAdvanced, advancedResult, 1e-4);
        }
    }
}
