using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using System.Drawing;

namespace Pierwiastki_CS
{
    class FastFourierTransform
    {
        public List<PointC> Oblicz(string funkcja, double poczatek, double koniec)
        {
            Pochodna p = new Pochodna(funkcja);

            List<PointC> wyniki = new List<PointC>();

            double krok = (koniec - poczatek) / 1000;

            int n = 0;
            double k = poczatek;
            double[] wartosciFunkcji = new double[1000];
            
            while (n < 1000)
            {
                wartosciFunkcji[n] = p.ObliczFunkcjeWPunkcie(k);

                n++;
                k += krok;
            }
            
            for (k = poczatek; k <= koniec; k += krok)
            {
                Complex suma = new Complex();

                for (n = 0; n < 1000; n++)
                {
                    suma += wartosciFunkcji[n] * Complex.Exp((-2 * Math.PI * Complex.ImaginaryOne * n * k) / 1000);
                }

                wyniki.Add(new PointC(k, suma));
            }

            return wyniki;
        }

        public List<PointC> ObliczOdwrocona(List<PointC> punkty)
        {
            List<PointC> wyniki = new List<PointC>();

            for (int i = 0; i < punkty.Count; i++)
            {
                Complex suma = new Complex();

                for (int n = 0; n < 1000; n++)
                {
                    suma += punkty[n].Y * Complex.Exp((2 * Math.PI * Complex.ImaginaryOne * n * i) / 1000);
                }

                wyniki.Add(new PointC(punkty[i].X, suma / 1000));
            }

            return wyniki;
        }
    }
}
