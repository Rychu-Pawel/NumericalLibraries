using Rychusoft.NumericalLibraries.Approximation.Exceptions;
using Rychusoft.NumericalLibraries.Common;
using Tests.Integration.Helpers;

namespace Tests.Integration.Interpolation
{
    [TestClass]
    public class ApproximationTests
    {
        [TestMethod]
        public void Constructor_WhenPointsAreNull_ThrowsException()
        {
            Assert.ThrowsException<ArgumentNullException>(
                () => new Rychusoft.NumericalLibraries.Approximation.Approximation(null!, 1));
        }

        [TestMethod]
        public void Constructor_WhenPointsAreEmpty_ThrowsException()
        {
            Assert.ThrowsException<ArgumentException>(
                () => new Rychusoft.NumericalLibraries.Approximation.Approximation(new List<PointD>(), 1));
        }

        [TestMethod]
        public void Constructor_WhenLevelIsInvalid_ThrowsException()
        {
            var points = new List<PointD> { new PointD(0, 0), new PointD(1, 1) };

            Assert.ThrowsException<WrongApproximationLevelException>(
                () => new Rychusoft.NumericalLibraries.Approximation.Approximation(points, 0));
        }

        [TestMethod]
        public void Constructor_WhenPointsContainSameX_ThrowsException()
        {
            var points = new List<PointD>
            {
                new PointD(1, 2),
                new PointD(1, 4),
            };

            Assert.ThrowsException<Exception>(
                () => new Rychusoft.NumericalLibraries.Approximation.Approximation(points, 1));
        }

        [TestMethod]
        public void Compute_ForLinearInput_ReturnsEquivalentLinearFunction()
        {
            var points = new List<PointD>
            {
                new PointD(-1, 1),
                new PointD(0, 3),
                new PointD(2, 7),
            };
            var approximation = new Rychusoft.NumericalLibraries.Approximation.Approximation(points, 1);

            var formula = approximation.Compute();

            Assert.AreEqual(formula, approximation.Result);
            Assert.AreEqual(1d, ExpressionEvaluator.EvaluateAt(formula, -1d), 1e-6);
            Assert.AreEqual(3d, ExpressionEvaluator.EvaluateAt(formula, 0d), 1e-6);
            Assert.AreEqual(7d, ExpressionEvaluator.EvaluateAt(formula, 2d), 1e-6);
            Assert.AreEqual(9d, ExpressionEvaluator.EvaluateAt(formula, 3d), 1e-6);

            var advancedPoints = new List<PointD>
            {
                new PointD(-2d, 11.2d),
                new PointD(0d, 4.2d),
                new PointD(1d, 0.7d),
                new PointD(3d, -6.3d),
            };
            var advancedApproximation = new Rychusoft.NumericalLibraries.Approximation.Approximation(advancedPoints, 1);
            var advancedFormula = advancedApproximation.Compute();

            Assert.AreEqual(11.2d, ExpressionEvaluator.EvaluateAt(advancedFormula, -2d), 1e-6);
            Assert.AreEqual(4.2d, ExpressionEvaluator.EvaluateAt(advancedFormula, 0d), 1e-6);
            Assert.AreEqual(0.7d, ExpressionEvaluator.EvaluateAt(advancedFormula, 1d), 1e-6);
            Assert.AreEqual(-6.3d, ExpressionEvaluator.EvaluateAt(advancedFormula, 3d), 1e-6);
        }

        [TestMethod]
        public void Compute_ForQuadraticInput_ReturnsEquivalentQuadraticFunction()
        {
            var points = new List<PointD>
            {
                new PointD(-1, 2),
                new PointD(0, 3),
                new PointD(2, 11),
            };
            var approximation = new Rychusoft.NumericalLibraries.Approximation.Approximation(points, 2);

            var formula = approximation.Compute();

            Assert.AreEqual(2d, ExpressionEvaluator.EvaluateAt(formula, -1d), 1e-6);
            Assert.AreEqual(3d, ExpressionEvaluator.EvaluateAt(formula, 0d), 1e-6);
            Assert.AreEqual(11d, ExpressionEvaluator.EvaluateAt(formula, 2d), 1e-6);
            Assert.AreEqual(18d, ExpressionEvaluator.EvaluateAt(formula, 3d), 1e-4);

            var advancedPoints = new List<PointD>
            {
                new PointD(-2d, 15d),
                new PointD(0d, 1d),
                new PointD(3d, 10d),
            };
            var advancedApproximation = new Rychusoft.NumericalLibraries.Approximation.Approximation(advancedPoints, 2);
            var advancedFormula = advancedApproximation.Compute();

            Assert.AreEqual(15d, ExpressionEvaluator.EvaluateAt(advancedFormula, -2d), 1e-6);
            Assert.AreEqual(1d, ExpressionEvaluator.EvaluateAt(advancedFormula, 0d), 1e-6);
            Assert.AreEqual(10d, ExpressionEvaluator.EvaluateAt(advancedFormula, 3d), 1e-6);
            Assert.AreEqual(0d, ExpressionEvaluator.EvaluateAt(advancedFormula, 1d), 1e-6);
        }

        [TestMethod]
        public void Compute_ForHigherOrderInput_ReturnsEquivalentPolynomial()
        {
            var points = new List<PointD>
            {
                new PointD(0, 1),
                new PointD(1, 2),
                new PointD(2, 5),
                new PointD(3, 10),
                new PointD(4, 17),
            };
            var approximation = new Rychusoft.NumericalLibraries.Approximation.Approximation(points, 3);

            var formula = approximation.Compute();

            foreach (var point in points)
                Assert.AreEqual(point.Y, ExpressionEvaluator.EvaluateAt(formula, point.X), 1e-3);
            Assert.AreEqual(7.25d, ExpressionEvaluator.EvaluateAt(formula, 2.5d), 1e-2);
        }
    }
}
