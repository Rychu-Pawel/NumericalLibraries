using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pierwiastki_CS
{
    class Newton : Bisekcja
    {
    // ZMIENNE ------------------------------
        int licznik;

    // METODY -------------------------------
        double newton()
        {
            licznik++;

            if (licznik > 20)
                throw new SystemException("Metoda Newtona zawiodla. Sproboj metody bisekcji, lub hybrydowej.");

            Pochodna funkcjaWPunkcie = new Pochodna(funkcja);

            double a = funkcjaWPunkcie.obliczFunkcjeWPunkcie(przedzialOd); // f(x1)
            double b = funkcjaWPunkcie.obliczFunkcjeWPunkcie(przedzialDo); // f(x2)

            // SPRAWDZENIE CZY X1 I X2 TO NIE SĄ MIEJSCA ZEROWE
            if (a == 0)
                return przedzialOd;
            else if (b == 0)
                return przedzialDo;
            else // JAK NIE TO REKURENCJA Z NOWYM PRZEDZIAŁEM
            {
                double x = przedzialOd - a / funkcjaWPunkcie.obliczPochodna(przedzialOd); // NOWY PRZEDZIAL = (przedzialOd - f(x)/f'(x)
                if (double.IsNaN(x))
                    throw new SystemException("Metoda Newtona zawiodla. Sproboj metody bisekcji, lub hybrydowej.");

                double fx = funkcjaWPunkcie.obliczFunkcjeWPunkcie(x); // f(x)

                if (Math.Abs(a) <= 0.00000000000001 || Math.Abs(przedzialOd - x) <= 0.00000000000001 || fx == 0) // DOKLADNOSC OBLICZEN
                {
                    return x;
                }
                else
                {
                    przedzialOd = x;
                    return newton(); // REKURANCJA Z NOWYM PRZEDZIALEM
                }
            }
        }

        public override double Oblicz()
        {
            //Spawdzenie czy w przedziale jest pierwiastek
            Pochodna funkcjaWPunkcie = new Pochodna(funkcja);

            if (funkcjaWPunkcie.obliczFunkcjeWPunkcie(przedzialOd) * funkcjaWPunkcie.obliczFunkcjeWPunkcie(przedzialDo) > 0)
                throw new SystemException("Brak, lub kilka pierwiastkow na zadanym obszarze");

            sprawdzenieOdBledow();
            konwertujNaTablice();
            konwertujNaONP();

            licznik = 0;
            return newton();
        }

    // KONSTRUKTOR --------------------------
        public Newton(string funk, double pOd, double pDo) : base(funk, pOd, pDo)
        {
            licznik = 0;
        }
    }
}
