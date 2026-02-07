namespace Tests.Integration.Helpers
{
    internal static class ExpressionEvaluator
    {
        public static double EvaluateAt(string formula, double x)
        {
            var derivative = new Rychusoft.NumericalLibraries.Derivative.Derivative(formula);

            return derivative.ComputeFunctionValueAtPoint(x);
        }
    }
}
