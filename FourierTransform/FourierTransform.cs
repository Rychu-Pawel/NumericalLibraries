using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using System.Drawing;
using NumericalLibraries.Common;

namespace NumericalLibraries.FourierTransform
{
    public class FourierTransform
    {
        Complex complexMinusTwo = new Complex(-2.0, 0);
        Complex complexTwo = new Complex(2.0, 0);
        Complex complexPi = new Complex(Math.PI, 0);

        /// <summary>
        /// Compute Fourier Transform
        /// </summary>
        /// <param name="function">Function for generating samples</param>
        /// <param name="sampling">Sampling rate</param>
        /// <param name="start">Starting point</param>
        /// <param name="end">Last point</param>
        /// <returns></returns>
        public List<PointC> Compute(string function, int sampling, double start, double end)
        {
            Derivative.Derivative p = new Derivative.Derivative(function);

            List<PointC> result = new List<PointC>();

            double step = (end - start) / sampling;

            int n = 0;
            double k = start;
            Complex[] functionValues = new Complex[sampling + 1];

            while (n < functionValues.Length)
            {
                functionValues[n] = p.ComputeFunctionAtPoint(k);

                n++;
                k += step;
            }
            
            Complex numberOfComplexPoints = new Complex(functionValues.Length, 0);

            for (k = 0; k < functionValues.Length; k++)
            {
                Complex sum = new Complex();
                
                for (n = 0; n < functionValues.Length; n++)
                    sum += functionValues[n] * Complex.Exp((complexMinusTwo * complexPi * Complex.ImaginaryOne * n * k) / numberOfComplexPoints);

                result.Add(new PointC(k, sum));
            }

            return result;
        }

        /// <summary>
        /// Compute Inverse Fourier Transform
        /// </summary>
        /// <param name="points">Fourier Transform points</param>
        /// <param name="sampling">Sampling rate of given points</param>
        /// <param name="start">Fourier Transform starting point used to generate samples</param>
        /// <param name="end">Fourier Transform end point used to generate samples</param>
        /// <returns></returns>
        public List<PointC> ComputeInverse(List<PointC> points, int sampling, double start, double end)
        {
            double step = (end - start) / sampling;

            List<PointC> result = new List<PointC>();

            Complex numberOfComplexPoints = new Complex(points.Count, 0);

            for (int k = 0; k < points.Count; k++)
            {
                Complex sum = new Complex();

                for (int n = 0; n < points.Count; n++)
                    sum += points[n].Y * Complex.Exp((complexTwo * complexPi * Complex.ImaginaryOne * n * k) / numberOfComplexPoints);

                result.Add(new PointC(start + (points[k].X + 1) * step, sum / numberOfComplexPoints));
            }

            return result;
        }
    }
}
