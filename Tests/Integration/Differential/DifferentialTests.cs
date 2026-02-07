using Rychusoft.NumericalLibraries.Differential.Exceptions;

namespace Tests.Integration.Differential
{
    [TestClass]
    public class DifferentialTests
    {
        [TestMethod]
        public void ComputeDifferential_ForSimpleEquation_ReturnsExpectedValue()
        {
            var differential = new Rychusoft.NumericalLibraries.Differential.Differential("x");

            var result = differential.ComputeDifferential(2d, 0d, 0d);

            Assert.AreEqual(2d, result, 1e-4);

            var advancedDifferential = new Rychusoft.NumericalLibraries.Differential.Differential("x+y");
            var advancedResult = advancedDifferential.ComputeDifferential(1d, 0d, 1d);

            Assert.AreEqual(2d * Math.Exp(1d) - 2d, advancedResult, 2e-3);
        }

        [TestMethod]
        public void ComputeDifferential_BackwardDirection_ReturnsExpectedValue()
        {
            var differential = new Rychusoft.NumericalLibraries.Differential.Differential("x");

            var result = differential.ComputeDifferential(-2d, 0d, 0d);

            Assert.AreEqual(2d, result, 3e-3);

            var advancedDifferential = new Rychusoft.NumericalLibraries.Differential.Differential("x+y");
            var advancedResult = advancedDifferential.ComputeDifferential(-1d, 0d, 1d);

            Assert.AreEqual(2d / Math.E, advancedResult, 3e-3);
        }

        [TestMethod]
        public void ComputeDifferentialPointsList_ReturnsAllIntermediatePoints()
        {
            var differential = new Rychusoft.NumericalLibraries.Differential.Differential("x");

            var points = differential.ComputeDifferentialPointsList(0.003d, 0d, 0d);

            Assert.AreEqual(3, points.Count);
            Assert.AreEqual(0.003d, points[^1].X, 1e-12);
            Assert.AreEqual(0.5d * 0.003d * 0.003d, points[^1].Y, 1e-8);

            var advancedDifferential = new Rychusoft.NumericalLibraries.Differential.Differential("x+y");
            var advancedPoints = advancedDifferential.ComputeDifferentialPointsList(0.003d, 0d, 1d);

            Assert.AreEqual(3, advancedPoints.Count);
            Assert.AreEqual(0.003d, advancedPoints[^1].X, 1e-12);
            Assert.AreEqual(2d * Math.Exp(0.003d) - 1.003d, advancedPoints[^1].Y, 1e-6);
        }

        [TestMethod]
        public void ComputeDifferentialII_ForSimpleEquation_ReturnsExpectedValue()
        {
            var differential = new Rychusoft.NumericalLibraries.Differential.Differential("-y");

            var result = differential.ComputeDifferentialII(Math.PI / 2d, 0d, 0d, 1d);

            Assert.AreEqual(1d, result, 5e-3);

            var advancedDifferential = new Rychusoft.NumericalLibraries.Differential.Differential("-y");
            var advancedResult = advancedDifferential.ComputeDifferentialII(Math.PI, 0d, 1d, 0d);

            Assert.AreEqual(-1d, advancedResult, 8e-3);
        }

        [TestMethod]
        public void ComputeDifferential_WhenNanStartingValueProvided_ThrowsException()
        {
            var differential = new Rychusoft.NumericalLibraries.Differential.Differential("x+y");

            Assert.ThrowsException<NaNOccuredException>(() => differential.ComputeDifferential(1d, 0d, double.NaN));
        }

        [TestMethod]
        public void ComputeDifferentialII_WhenNanStartingDerivativeProvided_ThrowsException()
        {
            var differential = new Rychusoft.NumericalLibraries.Differential.Differential("u");

            Assert.ThrowsException<NaNOccuredException>(() => differential.ComputeDifferentialII(1d, 0d, 0d, double.NaN));
        }
    }
}
