﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumericalCalculator
{
    class Mean
    {
        public double ComputeArithmetic(IEnumerable<double> values)
        {
            //Walidacja ilosci punktow
            double count = values.LongCount();

            if (count == 0)
                throw new NoValuesProvidedException();

            //Obliczenia
            double result = 0.0d;

            foreach (double item in values)
                result += item;

            return result / count;
        }

        public double ComputeWeighted(IEnumerable<double[]> values)
        {
            //Walidacja ilosci punktow
            double count = values.LongCount();

            if (count == 0)
                throw new NoValuesProvidedException();

            //Obliczenia
            double result = 0.0d;
            double weight = 0.0d;

            foreach (double[] item in values)
            {
                result += item[0] * item[1];
                weight += item[1];
            }

            return result / weight;
        }
    }

    public class NoValuesProvidedException : Exception
    { }
}
