﻿using System;

namespace Rychusoft.NumericalLibraries.Chart.Exceptions
{
    public class BesseleThirdArgumentException : Exception
    {
        public BesseleThirdArgumentException()
            : base("Third argument is wrong!")
        { }

        public BesseleThirdArgumentException(string msg)
            : base(msg)
        { }
    }
}