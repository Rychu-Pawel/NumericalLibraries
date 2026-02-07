using Rychusoft.NumericalLibraries.Common;
using Rychusoft.NumericalLibraries.Interpolation.Exceptions;
using Tests.Integration.Helpers;

namespace Tests.Integration.Interpolation
{
    [TestClass]
    public class InterpolationTests
    {
        [TestMethod]
        public void Constructor_WhenPointsAreNull_ThrowsException()
        {
            Assert.ThrowsException<ArgumentNullException>(
                () => new Rychusoft.NumericalLibraries.Interpolation.Interpolation(null!));
        }

        [TestMethod]
        public void Constructor_WhenPointsAreEmpty_ThrowsException()
        {
            Assert.ThrowsException<NoPointsProvidedException>(
                () => new Rychusoft.NumericalLibraries.Interpolation.Interpolation(new List<PointD>()));
        }

        [TestMethod]
        public void Constructor_WhenPointsContainSameX_ThrowsException()
        {
            var points = new List<PointD>
            {
                new PointD(1, 2),
                new PointD(1, 5),
            };

            Assert.ThrowsException<Exception>(
                () => new Rychusoft.NumericalLibraries.Interpolation.Interpolation(points));
        }

        [TestMethod]
        public void Compute_ForSinglePoint_ReturnsConstantFunction()
        {
            var interpolation = new Rychusoft.NumericalLibraries.Interpolation.Interpolation(
                new List<PointD> { new PointD(2d, 5d) });

            var formula = interpolation.Compute();

            Assert.AreEqual(5d, ExpressionEvaluator.EvaluateAt(formula, -100d), 1e-10);
            Assert.AreEqual(5d, ExpressionEvaluator.EvaluateAt(formula, 0d), 1e-10);
            Assert.AreEqual(5d, ExpressionEvaluator.EvaluateAt(formula, 100d), 1e-10);

            var advancedInterpolation = new Rychusoft.NumericalLibraries.Interpolation.Interpolation(
                new List<PointD> { new PointD(-7.5d, -3.25d) });
            var advancedFormula = advancedInterpolation.Compute();

            Assert.AreEqual(-3.25d, ExpressionEvaluator.EvaluateAt(advancedFormula, -100d), 1e-10);
            Assert.AreEqual(-3.25d, ExpressionEvaluator.EvaluateAt(advancedFormula, 0d), 1e-10);
            Assert.AreEqual(-3.25d, ExpressionEvaluator.EvaluateAt(advancedFormula, 100d), 1e-10);
        }

        [TestMethod]
        public void Compute_ForPolynomialPoints_ReturnsEquivalentFunction()
        {
            var points = new List<PointD>
            {
                new PointD(-1, 0),
                new PointD(0, 1),
                new PointD(2, 9),
            };
            var interpolation = new Rychusoft.NumericalLibraries.Interpolation.Interpolation(points);

            var formula = interpolation.Compute();

            Assert.AreEqual(formula, interpolation.Result);

            foreach (var point in points)
                Assert.AreEqual(point.Y, ExpressionEvaluator.EvaluateAt(formula, point.X), 1e-6);

            Assert.AreEqual(16d, ExpressionEvaluator.EvaluateAt(formula, 3d), 1e-6);

            var advancedPoints = new List<PointD>
            {
                new PointD(-1d, 2d),
                new PointD(0d, 1d),
                new PointD(2d, 5d),
                new PointD(3d, 22d),
            };
            var advancedInterpolation = new Rychusoft.NumericalLibraries.Interpolation.Interpolation(advancedPoints);
            var advancedFormula = advancedInterpolation.Compute();

            foreach (var point in advancedPoints)
                Assert.AreEqual(point.Y, ExpressionEvaluator.EvaluateAt(advancedFormula, point.X), 1e-6);

            Assert.AreEqual(0d, ExpressionEvaluator.EvaluateAt(advancedFormula, 1d), 1e-6);
        }
    }
}
