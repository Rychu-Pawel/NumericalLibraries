using Rychusoft.NumericalLibraries.Calculator.Exceptions;

namespace Tests.Integration.Calculator
{
    [TestClass]
    public class ProportionsTests
    {
        [TestMethod]
        public void Compute_WhenFirstValueIsUnknown_ReturnsExpectedResult()
        {
            var proportions = new Rychusoft.NumericalLibraries.Calculator.Proportions();

            var result = proportions.Compute(double.NaN, 4d, 6d, 3d);

            Assert.AreEqual(8d, result, 1e-12);

            var advancedResult = proportions.Compute(double.NaN, 12.5d, 33.3d, 4.5d);

            Assert.AreEqual(92.5d, advancedResult, 1e-10);
        }

        [TestMethod]
        public void Compute_WhenSecondValueIsUnknown_ReturnsExpectedResult()
        {
            var proportions = new Rychusoft.NumericalLibraries.Calculator.Proportions();

            var result = proportions.Compute(8d, double.NaN, 6d, 3d);

            Assert.AreEqual(4d, result, 1e-12);

            var advancedResult = proportions.Compute(7.5d, double.NaN, 2.5d, 4d);

            Assert.AreEqual(12d, advancedResult, 1e-12);
        }

        [TestMethod]
        public void Compute_WhenThirdValueIsUnknown_ReturnsExpectedResult()
        {
            var proportions = new Rychusoft.NumericalLibraries.Calculator.Proportions();

            var result = proportions.Compute(8d, 4d, double.NaN, 3d);

            Assert.AreEqual(6d, result, 1e-12);

            var advancedResult = proportions.Compute(18d, 2.4d, double.NaN, 5d);

            Assert.AreEqual(37.5d, advancedResult, 1e-10);
        }

        [TestMethod]
        public void Compute_WhenFourthValueIsUnknown_ReturnsExpectedResult()
        {
            var proportions = new Rychusoft.NumericalLibraries.Calculator.Proportions();

            var result = proportions.Compute(8d, 4d, 6d, double.NaN);

            Assert.AreEqual(3d, result, 1e-12);

            var advancedResult = proportions.Compute(7.5d, 2.5d, 4d, double.NaN);

            Assert.AreEqual(1.3333333333333333d, advancedResult, 1e-12);
        }

        [TestMethod]
        public void Compute_WhenVariableNotProvided_ThrowsException()
        {
            var proportions = new Rychusoft.NumericalLibraries.Calculator.Proportions();

            Assert.ThrowsException<VariableNotFoundException>(() => proportions.Compute(8d, 4d, 6d, 3d));
        }

        [TestMethod]
        public void Compute_WhenMoreThanOneVariableProvided_ThrowsException()
        {
            var proportions = new Rychusoft.NumericalLibraries.Calculator.Proportions();

            Assert.ThrowsException<ThereCanBeOnlyOneVariableInProportionException>(
                () => proportions.Compute(double.NaN, 4d, double.NaN, 3d));
        }
    }
}
