using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumericalCalculator
{
    public class Derivative : Interior
    {
    // ZMIENNE ------------------------------
        double h;
        
    // METODY -------------------------------
        public double ComputeFunctionAtPoint() // Obliczanie wartosci funkcji w punkcie
        {
            double wynik = EvaluateInterior();

            //Formatowanie wyniku, żeby 4,0000000000001 wypluł jako 4
            if (Math.Abs(wynik - Math.Floor(wynik)) < 0.000000001)
                wynik = Math.Floor(wynik);
            else if (Math.Abs(wynik - Math.Ceiling(wynik)) < 0.000000001)
                wynik = Math.Ceiling(wynik);

            return wynik;
        }            

        public double ComputeDerivative() //Obliczanie pochodnej pierwszego rzedu z dokladnoscia h^4
        {
            double fx1, fx2, fx3, fx4;
            double wynik;
            double originalX = x; //zachowanie oryginalnego x

            x = originalX - 2 * h;
            fx1 = EvaluateInterior();

            x = originalX - h;
            fx2 = EvaluateInterior();

            x = originalX + h;
            fx3 = EvaluateInterior();

            x = originalX + 2 * h;
            fx4 = EvaluateInterior();

            wynik = (fx1 - 8 * fx2 + 8 * fx3 - fx4) / (12 * h);

            //Przywrocenie oryginalnego x
            x = originalX;

            //Formatowanie wyniku, żeby 4,0000000000001 wypluł jako 4
            if (Math.Abs(wynik - Math.Floor(wynik)) < 0.000000001)
                wynik = Math.Floor(wynik);
            else if (Math.Abs(wynik - Math.Ceiling(wynik)) < 0.000000001)
                wynik = Math.Ceiling(wynik);

            return wynik;
        }

        public double ComputeDerivativeBis()
        {
            double wynik;
            double fx, fx1, fx2, fx3, fx4;
            double originalX = x; //zachowanie oryginalnego x

            fx = EvaluateInterior();

            x = originalX - 2 * h;
            fx1 = EvaluateInterior();

            x = originalX - h;
            fx2 = EvaluateInterior();

            x = originalX + h;
            fx3 = EvaluateInterior();

            x = originalX + 2 * h;
            fx4 = EvaluateInterior();

            wynik = (-fx1 + 16 * fx2 - 30 * fx + 16 * fx3 - fx4) / (12 * h * h);

            //Przywrocenie oryginalnego x
            x = originalX;

            //Formatowanie wyniku, żeby 4,0000000000001 wypluł jako 4
            //TU JEST STRASZNIE SŁABA DOKŁADNOŚ DLATEGO JEST WIĘKSZA TOLERANCJA
            if (Math.Abs(wynik - Math.Floor(wynik)) < 0.000001)
                wynik = Math.Floor(wynik);
            else if (Math.Abs(wynik - Math.Ceiling(wynik)) < 0.000001)
                wynik = Math.Ceiling(wynik);

            return wynik;
        }

    // METODY PRZECIAZONE -------------------------------
        public double ComputeFunctionAtPoint(double x) // Obliczanie wartosci funkcji w punkcie
        {
            this.x = x;

            return this.ComputeFunctionAtPoint();
        }

        public double ComputeDerivative(double x) //Obliczanie pochodnej pierwszego rzedu z dokladnoscia h^4
        {
            this.x = x;

            return this.ComputeDerivative();
        }

        public double ComputeDerivativeBis(double x)
        {
            this.x = x;

            return this.ComputeDerivativeBis();
        }

    // KONSTRUKTOR --------------------------
        public Derivative(string function, double x) : base(function, x)
        {
            //bool czySaLiczbyZmiennoprzecinkowe = false;

            //W CELACH EDUKACYJNYCH: SPRAWDZANIE CZY SA LICZBY ZMIENNOPRZECINKOWE (JAK TAK TO h ustawialem na 1)
            //foreach (char c in funkcja)
            //{
            //    if (c == ',')
            //    {
            //        czySaLiczbyZmiennoprzecinkowe = true;
            //        break; // nie musi juz dalej sprawdzac bo wiem ze sa
            //    }
            //if (czySaLiczbyZmiennoprzecinkowe == false)
            //{
            //    string x2 = Convert.ToString(ix);

            //    foreach (char c in x2)
            //    {
            //        if (c == ',')
            //        {
            //            czySaLiczbyZmiennoprzecinkowe = true;
            //            break; // nie musi juz dalej sprawdzac bo wiem ze sa
            //        }
            //    }
            //}

            //if (czySaLiczbyZmiennoprzecinkowe == true) //Jezeli nie ma liczb zmiennoprzecinkowych nie oplaca sie duzej dokladnosci, bo wyniki beda wychodzic 4,999999999 zamiast 5.
            //    h = 0.0001;
            //else
            //    h = 1;

            h = 0.0001;

            ErrorCheck();
            ConvertToTable();
            ConvertToONP();
        }

        public Derivative(string funkcja) : base(funkcja, 0.0)
        {
            h = 0.0001;

            ErrorCheck();
            ConvertToTable();
            ConvertToONP();
        }
    }
}
