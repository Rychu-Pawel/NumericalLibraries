using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumericalCalculator
{
    class FunctionNullReferenceException : Exception
    { }

    class PointConversionException : Exception
    { }

    class FromConversionException : Exception
    { }

    class ToConversionException : Exception
    { }

    class FromIIConversionException : Exception
    { }

    class ToIIConversionException : Exception
    { }

    class xFromException : Exception
    { }

    class xToException : Exception
    { }

    class yFromException : Exception
    { }

    class yToException : Exception
    { }

    class BesselFirstArgumentException : Exception
    {
        public BesselFirstArgumentException()
            : base("Niepoprawny pierwszy argument!")
        { }

        public BesselFirstArgumentException(string msg)
            : base(msg)
        { }
    }

    class BesseleSecondArgumentException : Exception
    {
        public BesseleSecondArgumentException()
            : base("Niepoprawny drugi argument!")
        { }

        public BesseleSecondArgumentException(string msg)
            : base(msg)
        { }
    }

    class BesseleThirdArgumentException : Exception
    {
        public BesseleThirdArgumentException()
            : base("Niepoprawny trzeci argument!")
        { }

        public BesseleThirdArgumentException(string msg)
            : base(msg)
        { }
    }

    class BesseleFourthArgumentException : Exception
    {
        public BesseleFourthArgumentException()
            : base("Niepoprawny czwarty argument!")
        { }

        public BesseleFourthArgumentException(string msg)
            : base(msg)
        { }
    }

    class VariableFoundException : Exception
    { }

    class XFromIsGreaterThenXToException : Exception
    { }

    class YFromIsGreaterThenYToException : Exception
    { }

    class FunctionException : Exception
    {
        public FunctionException(string msg)
            : base(msg)
        { }
    }

    class CoordinatesXException : Exception
    { }

    class CoordinatesYException : Exception
    { }

    class NoneGraphOptionCheckedException : Exception
    { }

    class CutoffValueException : Exception
    { }

    class SamplingValueException : Exception
    { }

    enum FunctionTypeEnum
    {
        Function,
        Derivative,
        SecondDerivative,
        Differential,
        DifferentialII,
        FT,
        IFT,
        Bessel,
        BesselSphere,
        BesselSphereDerivative,
        Neumann,
        NeumannSphere,
        NeumannSphereDerivative,
        Hypergeometric01,
        Hypergeometric11,
        Hypergeometric21,
    }

    enum BesselFunctionType
    {
        Bessel,
        BesselSphere,
        BesselSphereDerivative,
        Neumann,
        NeumannSphere,
        NeumannSphereDerivative,
        Hypergeometric01,
        Hypergeometric11,
        Hypergeometric21,
    }

    enum ArgumentTypeEnum
    {
        Point,
        From,
        To,
        FromII,
        ToII,
        BesselFirst,
        BesselSecond,
        BesselThird,
        BesselFourth,
        Sampling,
        Cutoff,
        xFrom,
        xTo,
        yFrom,
        yTo,
    }
}
