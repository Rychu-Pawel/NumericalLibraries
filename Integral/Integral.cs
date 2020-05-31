using Rychusoft.NumericalLibraries.Integral.Exceptions;
using System;

namespace Rychusoft.NumericalLibraries.Integral
{
    public class Integral : Derivative.Derivative
    {
        private double[,] _quadrature;
        private double _xFrom, _xTo;

        private double _result;

        private void ChangeBoundaries()
        {
            double factor, freeComponent;

            factor = (_xTo - _xFrom) / 2;

            freeComponent = (_xFrom + _xTo) / 2;

            //Change x to e.g., (2*x+3)
            if (function.Length > 1)
            {
                //Search function for 'x'
                for (int i = 0; i < function.Length; i++)
                {
                    //Change x  if it is not a part of 'exp' because we don't want to get 'e(2*x+3)p
                    if (function[i] == 'x' && ((i > 0) ? (function[i - 1] != 'e') : true))
                    {
                        if (freeComponent > 0)
                        {
                            int lengthBefore = function.Length;
                            function = function.Substring(0, i) + "(" + Convert.ToString(factor) + "*x+" + Convert.ToString(freeComponent) + ")" + function.Substring(i + 1, function.Length - i - 1);
                            i += function.Length - lengthBefore + 1;
                        }
                        else if (freeComponent < 0)
                        {
                            int lengthBefore = function.Length;
                            function = function.Substring(0, i) + "(" + Convert.ToString(factor) + "*x" + Convert.ToString(freeComponent) + ")" + function.Substring(i + 1, function.Length - i - 1);
                            i += function.Length - lengthBefore + 1;
                        }
                        else // freeComponent == 0
                        {
                            int lengthBefore = function.Length;
                            function = function.Substring(0, i) + "(" + Convert.ToString(factor) + "*x)" + function.Substring(i + 1, function.Length - i - 1);
                            i += function.Length - lengthBefore + 1;
                        }
                    }
                }
            }
            else if (function == "x") // Function is not constant
            {
                if (freeComponent > 0)
                    function = Convert.ToString(factor) + "*x+" + Convert.ToString(freeComponent);
                else if (freeComponent < 0)
                    function = Convert.ToString(factor) + "*x" + Convert.ToString(freeComponent);
            }

            //Add prefix factor - e.g., 28*x^3+3*x => (32)*(28*x^3+3*x)
            function = "(" + Convert.ToString(factor) + ")*(" + function + ")";

            ErrorCheck();
            ConvertToTable();
            ConvertToONP();
        }

        private double ComputeIndirect()
        {
            string previousFunction = function;

            //Change indirect boundaries e.g., (-1, -0,9) => (-1, 1);
            ChangeBoundaries();

            double indirectResult = 0;

            //Calculate integral using Gaussa-Legendre'a quadrature
            for (int i = 0; i < 5; i++)
                indirectResult += _quadrature[0, i] * (ComputeFunctionValueAtPoint(_quadrature[1, i]) + ComputeFunctionValueAtPoint(-_quadrature[1, i]));

            function = previousFunction;

            return indirectResult;
        }

        public double ComputeIntegral()
        {
            //Change boundaries
            if (_xFrom != -1 || _xTo != 1)
                ChangeBoundaries();

            // Compute integral from -1 to 1 as a sum of 100 integrals
            for (double i = -1; i <= 1; i += 0.01)
            {
                _xFrom = i;
                _xTo = i + 0.01;
                _result += ComputeIndirect();
            }

            //Restore settings for original function
            ConvertToTable();
            ConvertToONP();

            //Change e.g., 4,0000000000001 to 4
            if (Math.Abs(_result - Math.Floor(_result)) < 0.000000001)
                _result = Math.Floor(_result);
            else if (Math.Abs(_result - Math.Ceiling(_result)) < 0.000000001)
                _result = Math.Ceiling(_result);

            return _result;
        }

        /// <summary>
        /// Integral constructor
        /// </summary>
        /// <param name="function">Formula</param>
        /// <param name="xFrom">Lower limit</param>
        /// <param name="xTo">Upper limit</param>
        public Integral(string function, double xFrom, double xTo) : base(function)
        {
            this._xFrom = xFrom;
            this._xTo = xTo;

            if (double.IsInfinity(xFrom) || double.IsInfinity(xTo))
                throw new IntegralInfinityRangeNotSupportedException();

            _quadrature = new double[2, 5]; // Integral for 10 quadratures

            //Quadratures [0,2] 0 <- weight, 2 <- function value at point 2 and -2 for weight = 0;
            _quadrature[0, 0] = 0.0666713443;
            _quadrature[1, 0] = 0.9739065285;
            _quadrature[0, 1] = 0.1494513492;
            _quadrature[1, 1] = 0.8650633667;
            _quadrature[0, 2] = 0.2190863625;
            _quadrature[1, 2] = 0.6794095683;
            _quadrature[0, 3] = 0.2692667193;
            _quadrature[1, 3] = 0.4333953941;
            _quadrature[0, 4] = 0.2955242247;
            _quadrature[1, 4] = 0.1488743390;

            _result = 0;

            ErrorCheck();
            ConvertToTable();
            ConvertToONP();
        }
    }
}