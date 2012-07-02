using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace NumericalCalculator
{
    public class BesselNeumanHyper
    {
        [DllImport("jm_specfun.dll")]
        public static extern double bessel_(ref double v, ref double z);

        [DllImport("jm_specfun.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern double sphbess_(ref double v, ref double z);

        [DllImport("jm_specfun.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern double sphbess_prim__(ref double v, ref double z);

        [DllImport("jm_specfun.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern double neumann_(ref double v, ref double z);

        [DllImport("jm_specfun.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern double sphneum_(ref double v, ref double z);

        [DllImport("jm_specfun.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern double sphneum_prim__(ref double v, ref double z);

        [DllImport("jm_specfun.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern double hyperg11_(ref double a, ref double c, ref double z);

        [DllImport("jm_specfun.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern double hyperg21_(ref double a, ref double b, ref double c, ref double z);

        [DllImport("jm_specfun.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern double hyperg01_(ref double v, ref double z);

        public double Bessel(double v, double z)
        {
            try
            {
                v = Math.Round(v, 15);
                z = Math.Round(z, 15);

                double wynik = bessel_(ref v, ref z);

                wynik = FormatujWynik(wynik);

                return wynik;
            }
            catch
            {
                return double.NaN;
            }
        }

        public double SphBessel(double v, double z)
        {
            try
            {
                v = Math.Round(v, 15);
                z = Math.Round(z, 15);

                double wynik = sphbess_(ref v, ref z);

                wynik = FormatujWynik(wynik);

                return wynik;
            }
            catch
            {
                return double.NaN;
            }
        }

        public double SphBesselPrim(double v, double z)
        {
            try
            {
                v = Math.Round(v, 15);
                z = Math.Round(z, 15);

                double wynik = sphbess_prim__(ref v, ref z);

                wynik = FormatujWynik(wynik);

                return wynik;
            }
            catch
            {
                return double.NaN;
            }
        }

        public double Neumann(double v, double z)
        {
            try
            {
                v = Math.Round(v, 15);
                z = Math.Round(z, 15);

                double wynik = neumann_(ref v, ref z);

                wynik = FormatujWynik(wynik);

                return wynik;
            }
            catch
            {
                return double.NaN;
            }
        }

        public double SphNeuman(double v, double z)
        {
            try
            {
                v = Math.Round(v, 15);
                z = Math.Round(z, 15);

                double wynik = sphneum_(ref v, ref z);

                wynik = FormatujWynik(wynik);

                return wynik;
            }
            catch
            {
                return double.NaN;
            }
        }

        public double SphNeumanPrim(double v, double z)
        {
            try
            {
                v = Math.Round(v, 15);
                z = Math.Round(z, 15);

                double wynik = sphneum_prim__(ref v, ref z);

                wynik = FormatujWynik(wynik);

                return wynik;
            }
            catch
            {
                return double.NaN;
            }
        }

        public double Hyperg_1F_1(double v, double c, double z)
        {
            try
            {
                v = Math.Round(v, 15);
                c = Math.Round(c, 15);
                z = Math.Round(z, 15);

                double wynik = hyperg11_(ref v, ref c, ref z);

                wynik = FormatujWynik(wynik);

                return wynik;
            }
            catch
            {
                return double.NaN;
            }
        }

        public double Hyperg_2F_1(double a, double b, double c, double z)
        {
            try
            {
                a = Math.Round(a, 15);
                b = Math.Round(b, 15);
                c = Math.Round(c, 15);
                z = Math.Round(z, 15);

                double wynik = hyperg21_(ref a, ref b, ref c, ref z);

                wynik = FormatujWynik(wynik);

                return wynik;
            }
            catch
            {
                return double.NaN;
            }
        }

        public double Hyperg_0F_1(double v, double z)
        {
            try
            {
                v = Math.Round(v, 15);
                z = Math.Round(z, 15);

                double wynik = hyperg01_(ref v, ref z);

                wynik = FormatujWynik(wynik);

                return wynik;
            }
            catch
            {
                return double.NaN;
            }
        }


        private double FormatujWynik(double wynik)
        {
            //Sprawdzenie czy czasem nie jest to NaN
            if (wynik > 1E60 || Math.Abs(wynik) < 1E-60 || wynik < -1E60)
                return double.NaN;

            //Formatowanie wyniku, żeby 4,0000000000001 wypluł jako 4
            if (Math.Abs(wynik - Math.Floor(wynik)) < 0.00000001)
                wynik = Math.Floor(wynik);
            else if (Math.Abs(wynik - Math.Ceiling(wynik)) < 0.00000001)
                wynik = Math.Ceiling(wynik);

            return wynik;
        }
    }
}
