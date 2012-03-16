using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumericalCalculator
{
    static class Silnia
    {
        public static double Oblicz(double liczba)
        {
            double wynik = 1;

            if (liczba == 0)
                return wynik;

            double mnoznik = 1; //Mnożnik jeśli liczba jest ujemna to na końcu przemnożamy przez nią

            if (liczba < 0)
            {
                mnoznik = -1;
                liczba = liczba * (-1.0);
            }

            //Sprawdzenie czy mozna zrzutowac na int i czy jest on
            //odpowiednio maly by mozna bylo obliczyc w tradycyjny sposob
            int liczbaInt;
            
            if (int.TryParse(liczba.ToString(), out liczbaInt) && liczbaInt < 20)
            {
                for (int i = 2; i <= liczba; i++)
                    wynik *= i;

                return mnoznik * wynik;
            }

            double alfa = ((1 / (12 * liczba)) + (1 / (12 * liczba + 1))) / 2;
            wynik = Math.Sqrt(2 * Math.PI * liczba) * Math.Pow((liczba / Math.E), liczba) * Math.Pow(Math.E, alfa);
            
            return mnoznik * wynik;
        }
    }
}
