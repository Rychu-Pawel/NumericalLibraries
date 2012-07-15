using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumericalCalculator
{
    public class FunctionNullReferenceException : Exception
    { }

    public class PointConversionException : Exception
    { }

    public class FromConversionException : Exception
    { }

    public class ToConversionException : Exception
    { }

    public class FromIIConversionException : Exception
    { }

    public class ToIIConversionException : Exception
    { }

    public class xFromException : Exception
    { }

    public class xToException : Exception
    { }

    public class yFromException : Exception
    { }

    public class yToException : Exception
    { }

    public class BesselFirstArgumentException : Exception
    {
        public BesselFirstArgumentException()
            : base("Niepoprawny pierwszy argument!")
        { }

        public BesselFirstArgumentException(string msg)
            : base(msg)
        { }
    }

    public class BesseleSecondArgumentException : Exception
    {
        public BesseleSecondArgumentException()
            : base("Niepoprawny drugi argument!")
        { }

        public BesseleSecondArgumentException(string msg)
            : base(msg)
        { }
    }

    public class BesseleThirdArgumentException : Exception
    {
        public BesseleThirdArgumentException()
            : base("Niepoprawny trzeci argument!")
        { }

        public BesseleThirdArgumentException(string msg)
            : base(msg)
        { }
    }

    public class BesseleFourthArgumentException : Exception
    {
        public BesseleFourthArgumentException()
            : base("Niepoprawny czwarty argument!")
        { }

        public BesseleFourthArgumentException(string msg)
            : base(msg)
        { }
    }

    public class VariableFoundException : Exception
    { }

    public class XFromIsGreaterThenXToException : Exception
    { }

    public class YFromIsGreaterThenYToException : Exception
    { }

    public class FunctionException : Exception
    {
        public FunctionException(string msg)
            : base(msg)
        { }
    }

    public class CoordinatesXException : Exception
    { }

    public class CoordinatesYException : Exception
    { }

    public class NoneGraphOptionCheckedException : Exception
    { }

    public class CutoffValueException : Exception
    { }

    public class SamplingValueException : Exception
    { }

    public class InconsistentSystemOfEquationsException : Exception
    { }

    public class NoPointsProvidedException : Exception
    { }

    public class WrongApproximationLevelException : Exception
    { }

    public class NaNOccuredException : Exception
    { }

    public class NoneOrFewRootsOnGivenIntervalException : Exception
    { }

    public class IntegralInfinityRangeNotSupportedException : Exception
    { }

    public enum FunctionTypeEnum
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

    public enum BesselFunctionType
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
}
