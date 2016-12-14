using System;

namespace NumericalLibraries.Chart.Exceptions
{
    public class BesseleFourthArgumentException : Exception
    {
        public BesseleFourthArgumentException()
            : base("Fourth argument is wrong!")
        { }

        public BesseleFourthArgumentException(string msg)
            : base(msg)
        { }
    }
}
