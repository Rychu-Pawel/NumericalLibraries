using System;

namespace Rychusoft.NumericalLibraries.Calculator.Exceptions
{
    public class OperatorAtTheBeginningOfTheExpressionException : Exception
    {
        public char Operator { get; set; }

        public OperatorAtTheBeginningOfTheExpressionException(char op)
        {
            Operator = op;
        }
    }
}