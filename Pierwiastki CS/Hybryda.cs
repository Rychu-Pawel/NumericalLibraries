using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumericalCalculator
{
    class Hybryda: Pochodna
    {
    // ZMIENNE --------------------
        double przedzialOd, przedzialDo;
        double przedzialOdPrzedNewtonem, przedzialDoPrzedNewtonem;
        readonly int iloscIteracjiBisekcji = 8;
        int licznik;

    // METODY ---------------------
        double Bisekcja() //Zwraca NaN jak osiagnie zadana ilosc iteracji
        {
            if (licznik == iloscIteracjiBisekcji)
                return double.NaN;

            licznik++;

            double a = ObliczFunkcjeWPunkcie(przedzialOd); // f(x1)
            double b = ObliczFunkcjeWPunkcie(przedzialDo); // f(x2)

            // SPRAWDZENIE CZY JEST TU PIERWIASTEK I CZY X1 I X2 TO NIE SĄ MIEJSCA ZEROWE
            if (a * b > 0)
                throw new FunkcjaException("Brak, lub kilka pierwiastkow na zadanym obszarze");
            else if (a == 0)
                return przedzialOd;
            else if (b == 0)
                return przedzialDo;
            else // JAK NIE TO REKURENCJA Z NOWYM PRZEDZIAŁEM
            {
                double x = (przedzialOd + przedzialDo) / 2; // NOWY PRZEDZIAL (X1 + X2) / 2
                double fx = ObliczFunkcjeWPunkcie(x); // f(x)

                if (a * fx <= 0) // F(X1)*F(X) <= 0
                {
                    przedzialDo = x;

                    if (Math.Abs(przedzialOd - przedzialDo) > 0.00000000000001) // DOKLADNOSC OBLICZEN
                        return Bisekcja(); // REKURANCJA Z NOWYM PRZEDZIALEM
                    else
                        return x;
                }
                else if (fx * b <= 0) // F(X)*F(X2) <= 0
                {
                    przedzialOd = x;

                    if (Math.Abs(przedzialOd - przedzialDo) > 0.00000000000001) // DOKLADNOSC OBLICZEN
                        return Bisekcja(); // REKURANCJA Z NOWYM PRZEDZIALEM
                    else
                        return x;
                }
                else
                    throw new FunkcjaException("Brak, lub kilka pierwiastkow na zadanym obszarze");
            }
        }
        
        double Newton() //Zwraca NaN jak wymiekla
        {
            licznik++;

            if (licznik > 28)
                return double.NaN;

            double a = ObliczFunkcjeWPunkcie(przedzialOd); // f(x1)
            double b = ObliczFunkcjeWPunkcie(przedzialDo); // f(x2)

            // SPRAWDZENIE CZY X1 I X2 TO NIE SĄ MIEJSCA ZEROWE
            if (a == 0)
                return przedzialOd;
            else if (b == 0)
                return przedzialDo;
            else // JAK NIE TO REKURENCJA Z NOWYM PRZEDZIAŁEM
            {
                double x = przedzialOd - a / ObliczPochodna(przedzialOd); // NOWY PRZEDZIAL = (przedzialOd - f(x)/f'(x)
                if (double.IsNaN(x))
                    return double.NaN;

                double fx = ObliczFunkcjeWPunkcie(x); // f(x)

                if (Math.Abs(a) <= 0.00000000000001 || Math.Abs(przedzialOd - x) <= 0.00000000000001 || fx == 0) // DOKLADNOSC OBLICZEN
                    return x;
                else
                {
                    przedzialOd = x;
                    return Newton(); // REKURANCJA Z NOWYM PRZEDZIALEM
                }
            }
        }

        double HybrydaOblicz()
        {
            double wynik;

            licznik = 0;
            
            wynik = Bisekcja(); // 8 ITERACJI BISEKCJI

            if (!(double.IsNaN(wynik)))
                return wynik; // bisekcja znalazla wynik

            //Zachowanie przedzialow w razie niepowodzenia Newtona, bo Newton psuje przedzial
            przedzialDoPrzedNewtonem = przedzialDo;
            przedzialOdPrzedNewtonem = przedzialOd;

            wynik = Newton(); // NEWTON

            if (double.IsNaN(wynik)) // Czy newton zawiodl, jak tak to przywracam przedzial sprzed Newtona i dokanczam bisekcja
            {
                przedzialOd = przedzialOdPrzedNewtonem;
                przedzialDo = przedzialDoPrzedNewtonem;
                return Bisekcja(); // Dokonczenie bisekcja
            }
            else
                return wynik;                
        }

        public override double ObliczWnetrze()
        {
            double wynik;

            //Spawdzenie czy w przedziale jest pierwiastek, jeśli nie to próbujemy uruchomić tylko newtona i jak zawiedzie to walimy błąd
            if (ObliczFunkcjeWPunkcie(przedzialOd) * ObliczFunkcjeWPunkcie(przedzialDo) > 0)
            {
                wynik = Newton();

                if (double.IsNaN(wynik))
                    throw new FunkcjaException("Brak, lub kilka pierwiastkow na zadanym obszarze");
                else
                    return wynik;
            }

            wynik = HybrydaOblicz();

            //Formatowanie wyniku, żeby 4,0000000000001 wypluł jako 4
            if (Math.Abs(wynik - Math.Floor(wynik)) < 0.000000001)
                wynik = Math.Floor(wynik);
            else if (Math.Abs(wynik - Math.Ceiling(wynik)) < 0.000000001)
                wynik = Math.Ceiling(wynik);            

            return wynik;
        }


    // KONSTRUKTOR ----------------
        public Hybryda(string funkcja, double przedzialOd, double przedzialDo): base(funkcja)
        {
            this.przedzialOd = przedzialOd;
            this.przedzialDo = przedzialDo;
            licznik = 0;
            //iloscIteracjiBisekcji = 0;
            przedzialOdPrzedNewtonem = przedzialDoPrzedNewtonem = 0;

            SprawdzenieOdBledow();
            KonwertujNaTablice();
            KonwertujNaONP();
        }
    }
}
