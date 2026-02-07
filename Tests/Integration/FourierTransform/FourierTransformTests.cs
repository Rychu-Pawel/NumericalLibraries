namespace Tests.Integration.FourierTransform
{
    [TestClass]
    public class FourierTransformTests
    {
        private static List<double> GenerateSamples(string formula, int sampling, double start, double end)
        {
            var evaluator = new Rychusoft.NumericalLibraries.Derivative.Derivative(formula);
            var samples = new List<double>();
            var step = (end - start) / sampling;

            for (var n = 0; n <= sampling; n++)
                samples.Add(evaluator.ComputeFunctionValueAtPoint(start + n * step));

            return samples;
        }

        [TestMethod]
        public void Compute_ForConstantFunction_ReturnsExpectedSpectrum()
        {
            var transform = new Rychusoft.NumericalLibraries.FourierTransform.FourierTransform();

            var spectrum = transform.Compute("1", 8, 0d, 2d * Math.PI);

            Assert.AreEqual(9, spectrum.Count);
            Assert.AreEqual(9d, spectrum[0].Y.Real, 1e-9);
            Assert.AreEqual(0d, spectrum[0].Y.Imaginary, 1e-9);

            for (var i = 1; i < spectrum.Count; i++)
            {
                Assert.IsTrue(Math.Abs(spectrum[i].Y.Real) < 1e-9);
                Assert.IsTrue(Math.Abs(spectrum[i].Y.Imaginary) < 1e-9);
            }

            const string advancedFormula = "sin(x/2)+0.5*cos(3*x)+2";
            const int sampling = 16;
            var advancedSpectrum = transform.Compute(advancedFormula, sampling, 0d, 2d * Math.PI);
            var advancedSamples = GenerateSamples(advancedFormula, sampling, 0d, 2d * Math.PI);

            Assert.AreEqual(sampling + 1, advancedSpectrum.Count);
            Assert.AreEqual(advancedSamples.Sum(), advancedSpectrum[0].Y.Real, 1e-8);
            Assert.AreEqual(0d, advancedSpectrum[0].Y.Imaginary, 1e-8);
        }

        [TestMethod]
        public void ComputeInverse_ReconstructsOriginalSignal()
        {
            var transform = new Rychusoft.NumericalLibraries.FourierTransform.FourierTransform();
            var spectrum = transform.Compute("1", 8, 0d, 2d * Math.PI);

            var recovered = transform.ComputeInverse(spectrum, 8, 0d, 2d * Math.PI);

            Assert.AreEqual(spectrum.Count, recovered.Count);

            foreach (var point in recovered)
            {
                Assert.AreEqual(1d, point.Y.Real, 1e-9);
                Assert.AreEqual(0d, point.Y.Imaginary, 1e-9);
            }

            const string advancedFormula = "sin(x/2)+0.5*cos(3*x)+2";
            const int advancedSampling = 16;
            var advancedSpectrum = transform.Compute(advancedFormula, advancedSampling, 0d, 2d * Math.PI);
            var advancedRecovered = transform.ComputeInverse(advancedSpectrum, advancedSampling, 0d, 2d * Math.PI);
            var advancedSamples = GenerateSamples(advancedFormula, advancedSampling, 0d, 2d * Math.PI);

            Assert.AreEqual(advancedSamples.Count, advancedRecovered.Count);

            for (var i = 0; i < advancedRecovered.Count; i++)
            {
                Assert.AreEqual(advancedSamples[i], advancedRecovered[i].Y.Real, 1e-8);
                Assert.AreEqual(0d, advancedRecovered[i].Y.Imaginary, 1e-8);
            }
        }
    }
}
