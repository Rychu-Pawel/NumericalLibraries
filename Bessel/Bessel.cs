using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace NumericalLibraries.Bessel
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

                double result = bessel_(ref v, ref z);

                result = FormatResult(result);

                return result;
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

                double result = sphbess_(ref v, ref z);

                result = FormatResult(result);

                return result;
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

                double result = sphbess_prim__(ref v, ref z);

                result = FormatResult(result);

                return result;
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

                double result = neumann_(ref v, ref z);

                result = FormatResult(result);

                return result;
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

                double result = sphneum_(ref v, ref z);

                result = FormatResult(result);

                return result;
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

                double result = sphneum_prim__(ref v, ref z);

                result = FormatResult(result);

                return result;
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

                double result = hyperg11_(ref v, ref c, ref z);

                result = FormatResult(result);

                return result;
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

                double result = hyperg21_(ref a, ref b, ref c, ref z);

                result = FormatResult(result);

                return result;
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

                double result = hyperg01_(ref v, ref z);

                result = FormatResult(result);

                return result;
            }
            catch
            {
                return double.NaN;
            }
        }


        private double FormatResult(double result)
        {
            if (result > 1E60 || Math.Abs(result) < 1E-60 || result < -1E60)
                return double.NaN;

            //Changing e.g., 4,0000000000001 to 4
            if (Math.Abs(result - Math.Floor(result)) < 0.00000001)
                result = Math.Floor(result);
            else if (Math.Abs(result - Math.Ceiling(result)) < 0.00000001)
                result = Math.Ceiling(result);

            return result;
        }
    }
}
