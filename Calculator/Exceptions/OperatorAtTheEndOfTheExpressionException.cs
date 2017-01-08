using System;

namespace Rychusoft.NumericalLibraries.Calculator.Exceptions
{
    public class OperatorAtTheEndOfTheExpressionException : Exception
    {
        public char Operator { get; set; }

        public OperatorAtTheEndOfTheExpressionException(char op)
        {
            Operator = op;
        }
    }
}