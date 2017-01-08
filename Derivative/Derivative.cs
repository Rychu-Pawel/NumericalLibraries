using System;
using Rychusoft.NumericalLibraries.Calculator;

namespace Rychusoft.NumericalLibraries.Derivative
{
    public class Derivative : Interior
    {
        double h;
        
        /// <summary>
        /// Compute function value at given point
        /// </summary>
        /// <returns></returns>
        protected double ComputeFunctionValueAtPoint()
        {
            double result = EvaluateInterior();

            //Change e.g., 4,0000000000001 to 4
            if (Math.Abs(result - Math.Floor(result)) < 0.000000001)
                result = Math.Floor(result);
            else if (Math.Abs(result - Math.Ceiling(result)) < 0.000000001)
                result = Math.Ceiling(result);

            return result;
        }

        /// <summary>
        /// Compute first order derivative
        /// </summary>
        /// <returns></returns>
        protected double ComputeDerivative() 
        {
            double fx1, fx2, fx3, fx4;
            double result;
            double originalX = x;

            x = originalX - 2 * h;
            fx1 = EvaluateInterior();

            x = originalX - h;
            fx2 = EvaluateInterior();

            x = originalX + h;
            fx3 = EvaluateInterior();

            x = originalX + 2 * h;
            fx4 = EvaluateInterior();

            result = (fx1 - 8 * fx2 + 8 * fx3 - fx4) / (12 * h);
            
            x = originalX;

            //Change e.g., 4,0000000000001 to 4
            if (Math.Abs(result - Math.Floor(result)) < 0.000000001)
                result = Math.Floor(result);
            else if (Math.Abs(result - Math.Ceiling(result)) < 0.000000001)
                result = Math.Ceiling(result);

            return result;
        }

        /// <summary>
        /// Compute second order derivative
        /// </summary>
        /// <returns></returns>
        protected double ComputeDerivativeBis()
        {
            double result;
            double fx, fx1, fx2, fx3, fx4;
            double originalX = x; 

            fx = EvaluateInterior();

            x = originalX - 2 * h;
            fx1 = EvaluateInterior();

            x = originalX - h;
            fx2 = EvaluateInterior();

            x = originalX + h;
            fx3 = EvaluateInterior();

            x = originalX + 2 * h;
            fx4 = EvaluateInterior();

            result = (-fx1 + 16 * fx2 - 30 * fx + 16 * fx3 - fx4) / (12 * h * h);
            
            x = originalX;

            //Change e.g., 4,0000000000001 to 4
            if (Math.Abs(result - Math.Floor(result)) < 0.000001)
                result = Math.Floor(result);
            else if (Math.Abs(result - Math.Ceiling(result)) < 0.000001)
                result = Math.Ceiling(result);

            return result;
        }
        
        /// <summary>
        /// Compute function value at given point
        /// </summary>
        /// <param name="x">Point in which you want to compute the value</param>
        /// <returns></returns>
        public double ComputeFunctionValueAtPoint(double x)
        {
            this.x = x;

            return this.ComputeFunctionValueAtPoint();
        }

        /// <summary>
        /// Compute first order derivative
        /// </summary>
        /// <param name="x">Point in which you want to compute the derivative</param>
        /// <returns></returns>
        public double ComputeDerivative(double x) //accuracy h^4
        {
            this.x = x;

            return this.ComputeDerivative();
        }

        /// <summary>
        /// Compute second order derivative
        /// </summary>
        /// <param name="x">Point in which you want to compute the derivative</param>
        /// <returns></returns>
        public double ComputeDerivativeBis(double x)
        {
            this.x = x;

            return this.ComputeDerivativeBis();
        }

        /// <summary>
        /// Derivative constructor
        /// </summary>
        /// <param name="function">Function formula</param>
        public Derivative(string function) : base(function, 0.0)
        {
            h = 0.0001;

            ErrorCheck();
            ConvertToTable();
            ConvertToONP();
        }
    }
}
