using Rychusoft.NumericalLibraries.Calculator.Exceptions;

namespace Tests.Integration.Calculator
{
    [TestClass]
    public class MeanTests
    {
        [TestMethod]
        public void ComputeArithmetic_ReturnsExpectedMean()
        {
            var mean = new Rychusoft.NumericalLibraries.Calculator.Mean();
            var values = new[] { 1d, 2d, 3d, 4d };

            var result = mean.ComputeArithmetic(values);

            Assert.AreEqual(2.5d, result, 1e-12);

            var advancedValues = new[] { -10d, 2.5d, 100.75d, -30.25d, 0.5d };
            var advancedResult = mean.ComputeArithmetic(advancedValues);

            Assert.AreEqual(12.7d, advancedResult, 1e-12);
        }

        [TestMethod]
        public void ComputeArithmetic_WhenNoValuesProvided_ThrowsException()
        {
            var mean = new Rychusoft.NumericalLibraries.Calculator.Mean();

            Assert.ThrowsException<NoValuesProvidedException>(() => mean.ComputeArithmetic(Array.Empty<double>()));
        }

        [TestMethod]
        public void ComputeWeighted_ReturnsExpectedMean()
        {
            var mean = new Rychusoft.NumericalLibraries.Calculator.Mean();
            var values = new[]
            {
                new[] { 10d, 1d },
                new[] { 20d, 3d },
                new[] { 40d, 1d },
            };

            var result = mean.ComputeWeighted(values);

            Assert.AreEqual(22d, result, 1e-12);

            var advancedValues = new[]
            {
                new[] { 3.5d, 2d },
                new[] { 10.2d, 0.5d },
                new[] { -4d, 1.5d },
            };
            var advancedResult = mean.ComputeWeighted(advancedValues);

            Assert.AreEqual(1.525d, advancedResult, 1e-12);
        }

        [TestMethod]
        public void ComputeWeighted_WhenNoValuesProvided_ThrowsException()
        {
            var mean = new Rychusoft.NumericalLibraries.Calculator.Mean();

            Assert.ThrowsException<NoValuesProvidedException>(() => mean.ComputeWeighted(Array.Empty<double[]>()));
        }

        [TestMethod]
        public void ComputeWeighted_WhenValueArrayHasWrongLength_ThrowsException()
        {
            var mean = new Rychusoft.NumericalLibraries.Calculator.Mean();
            var values = new[]
            {
                new[] { 10d, 1d, 999d },
            };

            Assert.ThrowsException<ValueArrayUpperBoundDifferentThenOneException>(() => mean.ComputeWeighted(values));
        }
    }
}
