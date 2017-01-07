using Rychusoft.NumericalLibraries.Calculator.Exceptions;

namespace Rychusoft.NumericalLibraries.Calculator
{
    public class Calculator: Interior
    {
        private new void ErrorCheck()
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
        public double Compute()
        {
            return EvaluateInterior();
        }
        
        public Calculator(string formula) : base(formula, 0.0)
        {
            ErrorCheck();
            ConvertToTable();
            ConvertToONP();
        }
    }
}
