﻿using System;

namespace NumericalLibraries.Chart.Exceptions
{
    public class BesseleSecondArgumentException : Exception
    {
        public BesseleSecondArgumentException()
            : base("Second argument is wrong!")
        { }

        public BesseleSecondArgumentException(string msg)
            : base(msg)
        { }
    }
}