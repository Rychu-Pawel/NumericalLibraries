using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumericalCalculator
{
    class Calculator: Interior
    {
    // METODY ---------------------------------
        new void SprawdzenieOdBledow() // DODATKOWO SPRAWDZENIE CZY NIE MA GDZIES X
        {
            base.SprawdzenieOdBledow();

            for (int i = 0; i < funkcja.Length; i++)
            {
                if (funkcja[i] == 'x')
                {
                    if (i == 0 || i == funkcja.Length - 1)
                        throw new VariableFoundException();

                    if (funkcja[i - 1] != 'e' || funkcja[i + 1] != 'p')
                        throw new VariableFoundException();
                }
            }
        }

        override public double ComputeInterior()
        {
            return EvaluateWnetrze();
        }

    // KONSTRUKTOR --------------------------
        public Calculator(string funkcja) : base(funkcja, 0.0)
        {
            SprawdzenieOdBledow();
            KonwertujNaTablice();
            KonwertujNaONP();
        }
    }
}
