using Rychusoft.NumericalLibraries.LinearEquation.Exceptions;

namespace Tests.Integration.LinearEquation
{
    [TestClass]
    public class LinearEquationTests
    {
        [TestMethod]
        public void Compute_ForValid2x2System_ReturnsExpectedVariables()
        {
            var coefficients = new double[,]
            {
                { 1, 1, 3 },
                { 2, -1, 0 },
            };
            var equation = new Rychusoft.NumericalLibraries.LinearEquation.LinearEquation(coefficients);

            var result = equation.Compute();

            Assert.AreEqual(2, result.Length);
            Assert.AreEqual(1d, result[0], 1e-10);
            Assert.AreEqual(2d, result[1], 1e-10);

            var advancedCoefficients = new double[,]
            {
                { 1, 1, 1, 6 },
                { 2, -1, 3, 9 },
                { -1, 4, 3, 16 },
            };
            var advancedEquation = new Rychusoft.NumericalLibraries.LinearEquation.LinearEquation(advancedCoefficients);
            var advancedResult = advancedEquation.Compute();

            Assert.AreEqual(3, advancedResult.Length);
            Assert.AreEqual(1d, advancedResult[0], 1e-10);
            Assert.AreEqual(2d, advancedResult[1], 1e-10);
            Assert.AreEqual(3d, advancedResult[2], 1e-10);
        }

        [TestMethod]
        public void Constructor_WhenMatrixSizeIsInvalid_ThrowsExpectedException()
        {
            try
            {
                _ = new Rychusoft.NumericalLibraries.LinearEquation.LinearEquation(new double[,] { { 1, 2 }, { 3, 4 } });
                Assert.Fail("Expected an exception to be thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("RowsNumberMustBeOneLessThenColumnsNumberException", ex.GetType().Name);
            }
        }

        [TestMethod]
        public void Compute_ForDegenerateSystem_ThrowsException()
        {
            var coefficients = new double[,]
            {
                { 1, 1, 2 },
                { 2, 2, 4 },
            };
            var equation = new Rychusoft.NumericalLibraries.LinearEquation.LinearEquation(coefficients);

            Assert.ThrowsException<InconsistentSystemOfEquationsException>(() => equation.Compute());
        }
    }
}
