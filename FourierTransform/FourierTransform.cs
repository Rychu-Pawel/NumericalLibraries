using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using System.Drawing;

namespace NumericalCalculator
{
    public class FourierTransform
    {
        Complex complexMinusDwa = new Complex(-2.0, 0);
        Complex complexDwa = new Complex(2.0, 0);
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
            Derivative p = new Derivative(function);

            List<PointC> wyniki = new List<PointC>();

            double krok = (end - start) / sampling;

            int n = 0;
            double k = start;
            Complex[] wartosciFunkcji = new Complex[sampling + 1];

            while (n < wartosciFunkcji.Length)
            {
                wartosciFunkcji[n] = p.ComputeFunctionAtPoint(k);

                n++;
                k += krok;
            }

            //Stałe
            Complex complexIloscPunktow = new Complex(wartosciFunkcji.Length, 0);

            for (k = 0; k < wartosciFunkcji.Length; k++)
            {
                Complex suma = new Complex();
                
                for (n = 0; n < wartosciFunkcji.Length; n++)
                    suma += wartosciFunkcji[n] * Complex.Exp((complexMinusDwa * complexPi * Complex.ImaginaryOne * n * k) / complexIloscPunktow);

                wyniki.Add(new PointC(k, suma));
            }

            return wyniki;
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
            double krok = (end - start) / sampling;

            List<PointC> wyniki = new List<PointC>();

            Complex complexIloscPunktow = new Complex(points.Count, 0);

            for (int k = 0; k < points.Count; k++)
            {
                Complex suma = new Complex();

                for (int n = 0; n < points.Count; n++)
                    suma += points[n].Y * Complex.Exp((complexDwa * complexPi * Complex.ImaginaryOne * n * k) / complexIloscPunktow);

                wyniki.Add(new PointC(start + (points[k].X + 1) * krok, suma / complexIloscPunktow));
            }

            return wyniki;
        }
    }
}
