using System;

namespace NumericalLibraries.Calculator.Exceptions
{
    public class ForbiddenSignDetectedException : Exception
    {
        public char Sign { get; set; }

        public ForbiddenSignDetectedException(char sign)
        {
            Sign = sign;
        }
    }
}