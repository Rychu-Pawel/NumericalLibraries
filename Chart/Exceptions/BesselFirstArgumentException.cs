﻿using System;

namespace Rychusoft.NumericalLibraries.Chart.Exceptions
{
    public class BesselFirstArgumentException : Exception
    {
        public BesselFirstArgumentException()
            : base("First argument is wrong!")
        { }

        public BesselFirstArgumentException(string msg)
            : base(msg)
        { }
    }
}