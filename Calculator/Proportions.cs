﻿using Rychusoft.NumericalLibraries.Calculator.Exceptions;

namespace Rychusoft.NumericalLibraries.Calculator
{
    public class Proportions
    {
        /// <summary>
        /// Compute proportion
        /// </summary>
        /// <param name="v1">First value. Put here double.NaN if this is your unknown value.</param>
        /// <param name="v2">Second value. Put here double.NaN if this is your unknown value.</param>
        /// <param name="v3">Third value. Put here double.NaN if this is your unknown value.</param>
        /// <param name="v4">Fourth value. Put here double.NaN if this is your unknown value.</param>
        /// <returns></returns>
        public double Compute(double v1, double v2, double v3, double v4)
        {
            //Check if there is and if there is only one variable
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

            //Compute
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
}
