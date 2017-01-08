using System;
using System.Collections.Generic;
using System.Linq;
using Rychusoft.NumericalLibraries.Common;
using Rychusoft.NumericalLibraries.Differential.Exceptions;

namespace Rychusoft.NumericalLibraries.Differential
{
    public class Differential : Derivative.Derivative
    {
        string _functionII;
        double _searchingPoint;
        double _searchingPointII;
        double _startingPoint, _startingValue;
        double _startingPointII, _startingValueII;
        double _step;

        string[] _ONP;
        string[] _ONPII;

        double f0, f1, f2, f3;
        double f0II, f1II, f2II, f3II;

        double _result;
        double _resultII;

        /// <summary>
        /// Compute differential
        /// </summary>
        /// <param name="valueLookingPoint">Point at which compute function value</param>
        /// <param name="startingPoint">Point at which function value is known</param>
        /// <param name="startingPointFunctionValue">Function value at starting point</param>
        /// <returns></returns>
        public double ComputeDifferential(double valueLookingPoint, double startingPoint, double startingPointFunctionValue)
        {
            List<PointD> points = ComputeDifferentialPointsList(valueLookingPoint, startingPoint, startingPointFunctionValue);

            return points.Last().Y;
        }

        /// <summary>
        /// Compute all function values from starting point to valueLookingPoint
        /// </summary>
        /// <param name="valueLookingPoint">Ending point</param>
        /// <param name="startingPoint">Point at which function value is known</param>
        /// <param name="startingPointFunctionValue">Function value at starting point</param>
        /// <returns></returns>
        public List<PointD> ComputeDifferentialPointsList(double valueLookingPoint, double startingPoint, double startingPointFunctionValue)
        {
            double step = 0.001;
            this._step = step;

            f0 = f1 = f2 = f3 = 0;
            _result = 0;

            List<PointD> points = new List<PointD>();

            _searchingPoint = valueLookingPoint;
            _startingPoint = startingPoint;
            _startingValue = startingPointFunctionValue;

            //Check if should move forward or backward
            if (_searchingPoint > _startingPoint)
            {
                //Moving forward
                for (double i = _startingPoint + step; i < _searchingPoint; i += step)
                {
                    ComputeFy();

                    _result = _startingValue + (step / 6) * (f0 + 2 * f1 + 2 * f2 + f3);

                    //Set values for next iteration
                    _startingValue = _result;
                    _startingPoint = i;

                    points.Add(new PointD(i, _result));
                }

                //Move to the searching value (e.g., to x = 3,4567 because now we are in 3,456)
                step = _searchingPoint - _startingPoint;
                ComputeFy();
                _result = _startingValue + (step / 6) * (f0 + 2 * f1 + 2 * f2 + f3);

                //Change e.g., 4,0000000000001 to 4
                if (Math.Abs(_result - Math.Floor(_result)) < 0.000000001)
                    _result = Math.Floor(_result);
                else if (Math.Abs(_result - Math.Ceiling(_result)) < 0.000000001)
                    _result = Math.Ceiling(_result);

                points.Add(new PointD(_searchingPoint, _result));

                return points;
            }
            else if (_searchingPoint < _startingPoint)
            {
                step = -step;

                //Moving backward
                for (double i = _startingPoint + step; i > _searchingPoint; i += step)
                {
                    ComputeFy();

                    _result = _startingValue + (step / 6) * (f0 + 2 * f1 + 2 * f2 + f3);

                    //Set values for next iteration
                    _startingValue = _result;
                    _startingPoint = i;

                    points.Add(new PointD(i, y));
                }

                //Move to the searching value (e.g., to x = 3,4567 because now we are in 3,456)
                step = -(_startingPoint - _searchingPoint);
                ComputeFy();
                _result = _startingValue + (step / 6) * (f0 + 2 * f1 + 2 * f2 + f3);

                //Change e.g., 4,0000000000001 to 4
                if (Math.Abs(_result - Math.Floor(_result)) < 0.000000001)
                    _result = Math.Floor(_result);
                else if (Math.Abs(_result - Math.Ceiling(_result)) < 0.000000001)
                    _result = Math.Ceiling(_result);

                points.Add(new PointD(_searchingPoint, _result));

                return points;
            }
            else //Looking point is not equal to any point
                return new List<PointD>() { new PointD(_searchingPoint, startingPointFunctionValue) };
        }

        /// <summary>
        /// Compute second order differential
        /// </summary>
        /// <param name="valueLookingPoint">Ending point</param>
        /// <param name="startingPoint">Point at which function and derivative values are known</param>
        /// <param name="startingPointFunctionValue">Function value at starting point</param>
        /// <param name="startingPointFunctionValueII">Derivative value at starting point</param>
        /// <returns></returns>
        public double ComputeDifferentialII(double valueLookingPoint, double startingPoint, double startingPointFunctionValue, double startingPointFunctionValueII)
        {
            List<PointD> points = ComputeDifferentialIIPointsList(valueLookingPoint, startingPoint, startingPointFunctionValue, startingPointFunctionValueII);

            return points.Last().Y;
        }

        /// <summary>
        /// Compute all function values from starting point to valueLookingPoint
        /// </summary>
        /// <param name="valueLookingPoint">Ending point</param>
        /// <param name="startingPoint">Point at which function and derivative values are known</param>
        /// <param name="startingPointFunctionValue">Function value at starting point</param>
        /// <param name="startingPointFunctionValueII">Derivative value at starting point</param>
        /// <returns></returns>
        public List<PointD> ComputeDifferentialIIPointsList(double valueLookingPoint, double startingPoint, double startingPointFunctionValue, double startingPointFunctionValueII)
        {
            double step = 0.001;
            this._step = step;

            _functionII = function.Replace("y'", "u");
            function = "u";

            //ONP for the first
            ConvertToTable();
            ConvertToONP();

            _ONP = functionONP;

            //ONP for the second
            function = _functionII;

            ConvertToTable();
            ConvertToONP();

            _ONPII = functionONP;

            //Restore previous ONP and function
            function = "u";
            functionONP = _ONP;

            //Compute
            f0 = f1 = f2 = f3 = f0II = f1II = f2II = f3II = 0;
            _result = _resultII = 0;

            List<PointD> points = new List<PointD>();
            List<PointD> pointsII = new List<PointD>();

            _searchingPointII = valueLookingPoint;
            _startingPoint = startingPoint;
            _startingPointII = startingPoint;
            _startingValue = startingPointFunctionValue;
            _startingValueII = startingPointFunctionValueII;

            //Check if should move forward or backward
            if (_searchingPointII > _startingPointII)
            {
                //Moving forward
                for (double i = _startingPointII + step; i < _searchingPointII; i += step)
                {
                    ComputeFyII();

                    _result = _startingValue + (step / 6) * (f0 + 2 * f1 + 2 * f2 + f3);
                    _resultII = _startingValueII + (step / 6) * (f0II + 2 * f1II + 2 * f2II + f3II);

                    //Set values for next iteration
                    _startingValue = _result;
                    _startingValueII = _resultII;
                    _startingPoint = i;
                    _startingPointII = i;

                    points.Add(new PointD(i, _result));
                    pointsII.Add(new PointD(i, _resultII));
                }

                //Move to the searching value (e.g., to x = 3,4567 because now we are in 3,456)
                step = _searchingPointII - _startingPointII;
                ComputeFyII();
                _result = _startingValue + (step / 6) * (f0 + 2 * f1 + 2 * f2 + f3);

                //Change e.g., 4,0000000000001 to 4
                if (Math.Abs(_result - Math.Floor(_result)) < 0.000000001)
                    _result = Math.Floor(_result);
                else if (Math.Abs(_result - Math.Ceiling(_result)) < 0.000000001)
                    _result = Math.Ceiling(_result);

                points.Add(new PointD(_searchingPointII, _result));

                return points;
            }
            else if (_searchingPointII < _startingPointII)
            {
                step = -step;

                //Moving backward
                for (double i = _startingPointII + step; i > _searchingPointII; i += step)
                {
                    ComputeFyII();

                    _result = _startingValue + (step / 6) * (f0 + 2 * f1 + 2 * f2 + f3);
                    _resultII = _startingValueII + (step / 6) * (f0II + 2 * f1II + 2 * f2II + f3II);

                    //Set values for next iteration
                    _startingValue = _result;
                    _startingValueII = _resultII;
                    _startingPoint = i;
                    _startingPointII = i;

                    points.Add(new PointD(i, _result));
                    pointsII.Add(new PointD(i, _resultII));
                }

                //Move to the searching value (e.g., to x = 3,4567 because now we are in 3,456)
                step = _searchingPointII - _startingPointII;
                ComputeFyII();
                _result = _startingValue + (step / 6) * (f0 + 2 * f1 + 2 * f2 + f3);

                //Change e.g., 4,0000000000001 to 4
                if (Math.Abs(_result - Math.Floor(_result)) < 0.000000001)
                    _result = Math.Floor(_result);
                else if (Math.Abs(_result - Math.Ceiling(_result)) < 0.000000001)
                    _result = Math.Ceiling(_result);

                points.Add(new PointD(_searchingPointII, _result));

                return points;
            }
            else //Looking point is not equal to any point
                return new List<PointD>() { new PointD(_searchingPoint, startingPointFunctionValue) };
        }

        private void ComputeFy()
        {
            y = _startingValue;
            x = _startingPoint;

            //Compute f0
            if (double.IsNaN(y))
                throw new NaNOccuredException();

            f0 = ComputeFunctionValueAtPoint();

            //Compute f1
            x += _step / 2;
            y = _startingValue + ((_step / 2) * f0);

            if (double.IsNaN(y))
                throw new NaNOccuredException();

            f1 = ComputeFunctionValueAtPoint();

            //Compute f2
            y = _startingValue + ((_step / 2) * f1);

            if (double.IsNaN(y))
                throw new NaNOccuredException();

            f2 = ComputeFunctionValueAtPoint();

            //Compute f3
            x += _step / 2;
            y = _startingValue + (_step * f2);

            if (double.IsNaN(y))
                throw new NaNOccuredException();

            f3 = ComputeFunctionValueAtPoint();
        }

        private void ComputeFyII()
        {
            u = _startingValueII;
            y = _startingValue;
            x = _startingPoint;

            //Compute f0
            if (double.IsNaN(y) || double.IsNaN(u))
                throw new NaNOccuredException();

            f0 = ComputeFunctionValueAtPoint();

            //Compute f0II
            string tempFunction = function;
            function = _functionII;

            functionONP = _ONPII;

            f0II = ComputeFunctionValueAtPoint();

            //Compute f1
            function = tempFunction;

            functionONP = _ONP;

            x += _step / 2;
            y = _startingValue + ((_step / 2) * f0);
            u = _startingValueII + ((_step / 2) * f0II);

            if (double.IsNaN(y) || double.IsNaN(u))
                throw new NaNOccuredException();

            f1 = ComputeFunctionValueAtPoint();

            //Compute f1II
            tempFunction = function;
            function = _functionII;

            functionONP = _ONPII;

            f1II = ComputeFunctionValueAtPoint();

            //Compute f2
            function = tempFunction;

            functionONP = _ONP;

            y = _startingValue + ((_step / 2) * f1);
            u = _startingValueII + ((_step / 2) * f1II);

            if (double.IsNaN(y) || double.IsNaN(u))
                throw new NaNOccuredException();

            f2 = ComputeFunctionValueAtPoint();

            //Compute f2II
            tempFunction = function;
            function = _functionII;

            functionONP = _ONPII;

            f2II = ComputeFunctionValueAtPoint();

            //Compute f3
            function = tempFunction;

            functionONP = _ONP;

            x += _step / 2;
            y = _startingValue + (_step * f2);
            u = _startingValueII + ((_step) * f2II);

            if (double.IsNaN(y) || double.IsNaN(u))
                throw new NaNOccuredException();

            f3 = ComputeFunctionValueAtPoint();

            //Compute f3II
            tempFunction = function;
            function = _functionII;

            functionONP = _ONPII;

            f3II = ComputeFunctionValueAtPoint();

            //Restore function and ONP
            function = tempFunction;

            functionONP = _ONP;
        }

        /// <summary>
        /// Differential constructor
        /// </summary>
        /// <param name="equation">Differential equation</param>
        public Differential(string equation)
            : base(equation)
        { }
    }
}
