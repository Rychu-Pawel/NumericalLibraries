using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pierwiastki_CS
{
    class Bisekcja: Funkcja
    {
    // ZMIENNE ------------------------------
        protected double przedzialOd, przedzialDo;

    // METODY -------------------------------
        double bisekcja()
        {
            Pochodna funkcjaWPunkcie = new Pochodna(funkcja);

            double a = funkcjaWPunkcie.obliczFunkcjeWPunkcie(przedzialOd); // f(x1)
            double b = funkcjaWPunkcie.obliczFunkcjeWPunkcie(przedzialDo); // f(x2)

            // SPRAWDZENIE CZY JEST TU PIERWIASTEKCZY I CZY X1 I X2 TO NIE SĄ MIEJSCA ZEROWE
            if (a * b > 0)
                throw new SystemException("Brak, lub kilka pierwiastkow na zadanym obszarze");
            else if (a == 0)
                return przedzialOd;
            else if (b == 0)
                return przedzialDo;
            else // JAK NIE TO REKURENCJA Z NOWYM PRZEDZIAŁEM
            {
                double x = (przedzialOd + przedzialDo) / 2; // NOWY PRZEDZIAL (X1 + X2) / 2
                double fx = funkcjaWPunkcie.obliczFunkcjeWPunkcie(x); // f(x)

                if (a * fx <= 0) // F(X1)*F(X) <= 0
                {
                    przedzialDo = x;

                    if (Math.Abs(przedzialOd - przedzialDo) > 0.00000000000001) // DOKLADNOSC OBLICZEN
                        return bisekcja(); // REKURANCJA Z NOWYM PRZEDZIALEM
                    else
                        return x;
                }
                else if (fx * b <= 0) // F(X)*F(X2) <= 0
                {
                    przedzialOd = x;

                    if (Math.Abs(przedzialOd - przedzialDo) > 0.00000000000001) // DOKLADNOSC OBLICZEN
                        return bisekcja(); // REKURANCJA Z NOWYM PRZEDZIALEM
                    else
                        return x;
                }
                else
                    throw new SystemException("Brak, lub kilka pierwiastkow na zadanym obszarze");
            }
        }

        public virtual double Oblicz()
        {
            sprawdzenieOdBledow();
            konwertujNaTablice();
            konwertujNaONP();

            double wynik = bisekcja();

            //Formatowanie wyniku, żeby 4,0000000000001 wypluł jako 4
            if (Math.Abs(wynik - Math.Floor(wynik)) < 0.000000001)
                wynik = Math.Floor(wynik);
            else if (Math.Abs(wynik - Math.Ceiling(wynik)) < 0.000000001)
                wynik = Math.Ceiling(wynik);

            return wynik;
        }

    // KONSTRUKTOR --------------------------
        public Bisekcja(string funk, double pOd, double pDo): base(funk)
        {
            przedzialOd = pOd;
            przedzialDo = pDo;
        }
    }
}
