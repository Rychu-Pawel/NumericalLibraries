using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using System.Drawing;

namespace NumericalCalculator
{
    class FastFourierTransform
    {
        Complex complexMinusDwa = new Complex(-2.0, 0);
        Complex complexDwa = new Complex(2.0, 0);
        Complex complexPi = new Complex(Math.PI, 0);

        public List<PointC> Oblicz(string funkcja, int probkowanie, double poczatek, double koniec)
        {
            Pochodna p = new Pochodna(funkcja);

            List<PointC> wyniki = new List<PointC>();

            double krok = (koniec - poczatek) / probkowanie;

            int n = 0;
            double k = poczatek;
            Complex[] wartosciFunkcji = new Complex[probkowanie + 1];

            while (n < wartosciFunkcji.Length)
            {
                wartosciFunkcji[n] = p.ObliczFunkcjeWPunkcie(k);

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

        public List<PointC> ObliczOdwrocona(List<PointC> punkty, int probkowanie, double poczatek, double koniec)
        {
            double krok = (koniec - poczatek) / probkowanie;

            List<PointC> wyniki = new List<PointC>();

            Complex complexIloscPunktow = new Complex(punkty.Count, 0);

            for (int k = 0; k < punkty.Count; k++)
            {
                Complex suma = new Complex();

                for (int n = 0; n < punkty.Count; n++)
                    suma += punkty[n].Y * Complex.Exp((complexDwa * complexPi * Complex.ImaginaryOne * n * k) / complexIloscPunktow);

                wyniki.Add(new PointC(poczatek + (punkty[k].X + 1) * krok, suma / complexIloscPunktow));
            }

            return wyniki;
        }
    }
}
