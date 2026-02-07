using Rychusoft.NumericalLibraries.FunctionRoot.Exceptions;

namespace Tests.Integration.Hybrid
{
    [TestClass]
    public class HybridTests
    {
        [TestMethod]
        public void ComputeHybrid_ForIntervalContainingRoot_ReturnsExpectedValue()
        {
            var hybrid = new Rychusoft.NumericalLibraries.FunctionRoot.Hybrid("x^2-4", 0d, 3d);

            var result = hybrid.ComputeHybrid();

            Assert.AreEqual(2d, result, 1e-8);

            var advancedHybrid = new Rychusoft.NumericalLibraries.FunctionRoot.Hybrid("sin(x)-0.5", 0d, 1d);
            var advancedResult = advancedHybrid.ComputeHybrid();

            Assert.AreEqual(Math.PI / 6d, advancedResult, 1e-6);
        }

        [TestMethod]
        public void ComputeHybrid_WhenRootIsAtBoundary_ReturnsBoundaryValue()
        {
            var hybrid = new Rychusoft.NumericalLibraries.FunctionRoot.Hybrid("x-2", 2d, 5d);

            var result = hybrid.ComputeHybrid();

            Assert.AreEqual(2d, result, 1e-8);

            var advancedHybrid = new Rychusoft.NumericalLibraries.FunctionRoot.Hybrid("(x-2)*(x^2+1)", 2d, 4d);
            var advancedResult = advancedHybrid.ComputeHybrid();

            Assert.AreEqual(2d, advancedResult, 1e-8);
        }

        [TestMethod]
        public void ComputeHybrid_WhenNoRealRootExists_ThrowsException()
        {
            var hybrid = new Rychusoft.NumericalLibraries.FunctionRoot.Hybrid("x^2+1", -1d, 1d);

            Assert.ThrowsException<NoneOrFewRootsOnGivenIntervalException>(() => hybrid.ComputeHybrid());
        }
    }
}
