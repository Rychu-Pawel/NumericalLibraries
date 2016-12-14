using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumericalLibraries.Calculator
{
    static class Factorial
    {
        public static double Compute(double number)
        {
            double result = 1;

            if (number == 0)
                return result;

            double multiplier = 1; //Mnożnik jeśli liczba jest ujemna to na końcu przemnożamy przez nią

            if (number < 0)
            {
                multiplier = -1;
                number = number * (-1.0);
            }

            //Sprawdzenie czy mozna bezpośrednio zrzutowac na int i czy jest on
            //odpowiednio maly by mozna bylo obliczyc w tradycyjny sposob
            int numberInt;
            
            if (int.TryParse(number.ToString(), out numberInt) && numberInt < 20)
            {
                for (int i = 2; i <= number; i++)
                    result *= i;

                return multiplier * result;
            }

            double alpha = ((1 / (12 * number)) + (1 / (12 * number + 1))) / 2;
            result = Math.Sqrt(2 * Math.PI * number) * Math.Pow((number / Math.E), number) * Math.Pow(Math.E, alpha);
            
            return multiplier * result;
        }
    }
}
