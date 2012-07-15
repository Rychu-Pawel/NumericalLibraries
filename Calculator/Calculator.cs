using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumericalCalculator
{
    public class Calculator: Interior
    {
    // METODY ---------------------------------
        new void ErrorCheck() // DODATKOWO SPRAWDZENIE CZY NIE MA GDZIES X
        {
            base.ErrorCheck();

            for (int i = 0; i < function.Length; i++)
            {
                if (function[i] == 'x')
                {
                    if (i == 0 || i == function.Length - 1)
                        throw new VariableFoundException();

                    if (function[i - 1] != 'e' || function[i + 1] != 'p')
                        throw new VariableFoundException();
                }
            }
        }

        /// <summary>
        /// Calculate given math
        /// </summary>
        /// <returns></returns>
        override public double ComputeInterior()
        {
            return EvaluateInterior();
        }

    // KONSTRUKTOR --------------------------
        public Calculator(string function) : base(function, 0.0)
        {
            ErrorCheck();
            ConvertToTable();
            ConvertToONP();
        }
    }
}
