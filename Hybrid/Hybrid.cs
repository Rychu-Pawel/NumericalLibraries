using System;
using Rychusoft.NumericalLibraries.FunctionRoot.Exceptions;

namespace Rychusoft.NumericalLibraries.FunctionRoot
{
    public class Hybrid: Derivative.Derivative
    {
        double _rangeFrom, _rangeTo;
        double _rangeFromBeforeNewron, _rangeToBeforeNewton;
        readonly int _bisectionIterationCount = 8;
        int _counter;

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Returns NaN if the iteration limit is reached</returns>
        double Bisection()
        {
            if (_counter == _bisectionIterationCount)
                return double.NaN;

            _counter++;

            double a = ComputeFunctionAtPoint(_rangeFrom); // f(x1)
            double b = ComputeFunctionAtPoint(_rangeTo); // f(x2)

            // Check if the root is here and if the x1 and x2 are not the function's zeros
            if (a * b > 0)
                throw new NoneOrFewRootsOnGivenIntervalException();
            else if (a == 0)
                return _rangeFrom;
            else if (b == 0)
                return _rangeTo;
            else // If not the recuration with new range
            {
                double x = (_rangeFrom + _rangeTo) / 2; // new range (X1 + X2) / 2
                double fx = ComputeFunctionAtPoint(x); // f(x)

                if (a * fx <= 0) // F(X1)*F(X) <= 0
                {
                    _rangeTo = x;

                    if (Math.Abs(_rangeFrom - _rangeTo) > 0.00000000000001) // computation accuracy
                        return Bisection(); // recursion with new range
                    else
                        return x;
                }
                else if (fx * b <= 0) // F(X)*F(X2) <= 0
                {
                    _rangeFrom = x;

                    if (Math.Abs(_rangeFrom - _rangeTo) > 0.00000000000001) // computation accuracy
                        return Bisection(); // recursion with new range
                    else
                        return x;
                }
                else
                    throw new NoneOrFewRootsOnGivenIntervalException();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Returns NaN if the iteration limit is reached</returns>
        double NewtonMethod()
        {
            _counter++;

            if (_counter > 28)
                return double.NaN;

            double a = ComputeFunctionAtPoint(_rangeFrom); // f(x1)
            double b = ComputeFunctionAtPoint(_rangeTo); // f(x2)

            // Check if the root is here and if the x1 and x2 are not the function's zeros
            if (a == 0)
                return _rangeFrom;
            else if (b == 0)
                return _rangeTo;
            else // If not the recuration with new range
            {
                double x = _rangeFrom - a / ComputeDerivative(_rangeFrom); // new range = (rangeFrom - f(x)/f'(x)
                if (double.IsNaN(x))
                    return double.NaN;

                double fx = ComputeFunctionAtPoint(x); // f(x)

                if (Math.Abs(a) <= 0.00000000000001 || Math.Abs(_rangeFrom - x) <= 0.00000000000001 || fx == 0) // accuracy
                    return x;
                else
                {
                    _rangeFrom = x;
                    return NewtonMethod(); // recursion with new range
                }
            }
        }

        double HybridMethod()
        {
            double result;

            _counter = 0;
            
            result = Bisection(); // 8 bisection iterations

            if (!(double.IsNaN(result)))
                return result; // bisection found the result

            //Keep the ranges in case of newton failure
            _rangeToBeforeNewton = _rangeTo;
            _rangeFromBeforeNewron = _rangeFrom;

            result = NewtonMethod(); // NEWTON

            if (double.IsNaN(result)) // Newton failure
            {
                _rangeFrom = _rangeFromBeforeNewron;
                _rangeTo = _rangeToBeforeNewton;

                return Bisection(); // End with bisection
            }
            else
                return result;                
        }

        /// <summary>
        /// Compute function root
        /// </summary>
        /// <returns></returns>
        public double ComputeHybrid()
        {
            double result;

            //If simple check says that there is no root over that range then only fire newton
            if (ComputeFunctionAtPoint(_rangeFrom) * ComputeFunctionAtPoint(_rangeTo) > 0)
            {
                result = NewtonMethod();

                if (double.IsNaN(result))
                    throw new NoneOrFewRootsOnGivenIntervalException();
                else
                    return result;
            }

            result = HybridMethod();

            //Change e.g., 4,0000000000001 to 4
            if (Math.Abs(result - Math.Floor(result)) < 0.000000001)
                result = Math.Floor(result);
            else if (Math.Abs(result - Math.Ceiling(result)) < 0.000000001)
                result = Math.Ceiling(result);

            return result;
        }
        
        /// <summary>
        /// Hybrid constructor
        /// </summary>
        /// <param name="function">Formula</param>
        /// <param name="intervalFrom">Point at which you want to start looking for function root</param>
        /// <param name="intervalTo">Point at which you want to stop looking for function root</param>
        public Hybrid(string function, double intervalFrom, double intervalTo): base(function)
        {
            this._rangeFrom = intervalFrom;
            this._rangeTo = intervalTo;
            _counter = 0;
            
            _rangeFromBeforeNewron = _rangeToBeforeNewton = 0;

            ErrorCheck();
            ConvertToTable();
            ConvertToONP();
        }
    }
}
