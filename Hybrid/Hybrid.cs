using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumericalCalculator
{
    public class Hybrid: Derivative
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

            double a = ComputeFunctionAtPoint(przedzialOd); // f(x1)
            double b = ComputeFunctionAtPoint(przedzialDo); // f(x2)

            // SPRAWDZENIE CZY JEST TU PIERWIASTEK I CZY X1 I X2 TO NIE SĄ MIEJSCA ZEROWE
            if (a * b > 0)
                throw new NoneOrFewRootsOnGivenIntervalException();
            else if (a == 0)
                return przedzialOd;
            else if (b == 0)
                return przedzialDo;
            else // JAK NIE TO REKURENCJA Z NOWYM PRZEDZIAŁEM
            {
                double x = (przedzialOd + przedzialDo) / 2; // NOWY PRZEDZIAL (X1 + X2) / 2
                double fx = ComputeFunctionAtPoint(x); // f(x)

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
                    throw new NoneOrFewRootsOnGivenIntervalException();
            }
        }
        
        double Newton() //Zwraca NaN jak wymiekla
        {
            licznik++;

            if (licznik > 28)
                return double.NaN;

            double a = ComputeFunctionAtPoint(przedzialOd); // f(x1)
            double b = ComputeFunctionAtPoint(przedzialDo); // f(x2)

            // SPRAWDZENIE CZY X1 I X2 TO NIE SĄ MIEJSCA ZEROWE
            if (a == 0)
                return przedzialOd;
            else if (b == 0)
                return przedzialDo;
            else // JAK NIE TO REKURENCJA Z NOWYM PRZEDZIAŁEM
            {
                double x = przedzialOd - a / ComputeDerivative(przedzialOd); // NOWY PRZEDZIAL = (przedzialOd - f(x)/f'(x)
                if (double.IsNaN(x))
                    return double.NaN;

                double fx = ComputeFunctionAtPoint(x); // f(x)

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
            double result;

            licznik = 0;
            
            result = Bisekcja(); // 8 ITERACJI BISEKCJI

            if (!(double.IsNaN(result)))
                return result; // bisekcja znalazla wynik

            //Zachowanie przedzialow w razie niepowodzenia Newtona, bo Newton psuje przedzial
            przedzialDoPrzedNewtonem = przedzialDo;
            przedzialOdPrzedNewtonem = przedzialOd;

            result = Newton(); // NEWTON

            if (double.IsNaN(result)) // Czy newton zawiodl, jak tak to przywracam przedzial sprzed Newtona i dokanczam bisekcja
            {
                przedzialOd = przedzialOdPrzedNewtonem;
                przedzialDo = przedzialDoPrzedNewtonem;

                return Bisekcja(); // Dokonczenie bisekcja
            }
            else
                return result;                
        }

        /// <summary>
        /// Compute function root
        /// </summary>
        /// <returns></returns>
        public double ComputeHybrid()
        {
            double result;

            //Spawdzenie czy w przedziale jest pierwiastek, jeśli nie to próbujemy uruchomić tylko newtona i jak zawiedzie to walimy błąd
            if (ComputeFunctionAtPoint(przedzialOd) * ComputeFunctionAtPoint(przedzialDo) > 0)
            {
                result = Newton();

                if (double.IsNaN(result))
                    throw new NoneOrFewRootsOnGivenIntervalException();
                else
                    return result;
            }

            result = HybrydaOblicz();

            //Formatowanie wyniku, żeby 4,0000000000001 wypluł jako 4
            if (Math.Abs(result - Math.Floor(result)) < 0.000000001)
                result = Math.Floor(result);
            else if (Math.Abs(result - Math.Ceiling(result)) < 0.000000001)
                result = Math.Ceiling(result);

            return result;
        }


    // KONSTRUKTOR ----------------
        /// <summary>
        /// 
        /// </summary>
        /// <param name="function">Formula</param>
        /// <param name="intervalFrom">Point at which you want to start looking for function root</param>
        /// <param name="intervalTo">Point at which you want to stop looking for function root</param>
        public Hybrid(string function, double intervalFrom, double intervalTo): base(function)
        {
            this.przedzialOd = intervalFrom;
            this.przedzialDo = intervalTo;
            licznik = 0;
            
            przedzialOdPrzedNewtonem = przedzialDoPrzedNewtonem = 0;

            ErrorCheck();
            ConvertToTable();
            ConvertToONP();
        }
    }
}
