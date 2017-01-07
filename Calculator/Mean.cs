﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NumericalLibraries.Calculator.Exceptions;

namespace NumericalLibraries.Calculator
{
    public class Mean
    {
        /// <summary>
        /// Compute arithmetic mean
        /// </summary>
        /// <param name="values">Values for arithmetic mean computation</param>
        /// <returns></returns>
        public double ComputeArithmetic(IEnumerable<double> values)
        {
            //Validation
            double count = values.LongCount();

            if (count == 0)
                throw new NoValuesProvidedException();

            //Compute
            double result = 0.0d;

            foreach (double item in values)
                result += item;

            return result / count;
        }

        /// <summary>
        /// Compute weighted mean
        /// </summary>
        /// <param name="values">List of one tables with two values. First is value, second is this value weight.</param>
        /// <returns></returns>
        public double ComputeWeighted(IEnumerable<double[]> values)
        {
            //Validation
            double count = values.LongCount();

            if (count == 0)
                throw new NoValuesProvidedException();

            //Compute
            double result = 0.0d;
            double weight = 0.0d;

            foreach (double[] item in values)
            {
                if (item.GetLowerBound(0) != 0)
                    throw new ValueArrayLowerBoundDifferentThenZeroException();

                if (item.GetUpperBound(0) != 1)
                    throw new ValueArrayUpperBoundDifferentThenOneException();

                result += item[0] * item[1];
                weight += item[1];
            }

            return result / weight;
        }
    }
}
