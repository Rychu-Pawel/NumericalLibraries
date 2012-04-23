using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumericalCalculator
{
    class Proportions
    {
        public double Compute(double v1, double v2, double v3, double v4)
        {
            //Sprawdzenie czy jest i czy tylko jedna zmienna
            int nanCount = 0;

            if (double.IsNaN(v1))
                nanCount++;

            if (double.IsNaN(v2))
                nanCount++;

            if (double.IsNaN(v3))
                nanCount++;

            if (double.IsNaN(v4))
                nanCount++;

            if (nanCount > 1)
                throw new ThereCanBeOnlyOneVariableInProportionException();
            else if (nanCount < 1)
                throw new VariableNotFoundException();

            //Obliczenia
            if (double.IsNaN(v1))
                return v2 * v3 / v4;
            else if (double.IsNaN(v2))
                return v1 * v4 / v3;
            else if (double.IsNaN(v3))
                return v1 * v4 / v2;
            else
                return v2 * v3 / v1;
        }
    }

    public class ThereCanBeOnlyOneVariableInProportionException : Exception
    { }

    public class VariableNotFoundException : Exception
    { }
}
