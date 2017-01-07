using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using NumericalLibraries.Calculator.Exceptions;
using NumericalLibraries.Common;
using NumericalLibraries.Differential.Exceptions;

namespace NumericalLibraries.Differential
{
    public class Differential : Derivative.Derivative
    {
        //ZMIENNE -----------------------------------
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

        //METODY ------------------------------------

        /// <summary>
        /// Compute differential
        /// </summary>
        /// <param name="valueLookingPoint">Point at which return the value</param>
        /// <param name="startingPoint">Starting point</param>
        /// <param name="startingPointFunctionValue">Function value at starting point</param>
        /// <param name="applyResultFormatting">Makes output better look. For example outputs 4.0 instead 4,000000000000001 or 3,999999999999999</param>
        /// <param name="step"></param>
        /// <returns></returns>
        public double ComputeDifferential(double valueLookingPoint, double startingPoint, double startingPointFunctionValue, bool applyResultFormatting = true, double step = 0.001)
        {
            List<PointD> points = ComputeDifferentialPointsList(valueLookingPoint, startingPoint, startingPointFunctionValue, applyResultFormatting, step);

            return points.Last().Y;
        }

        /// <summary>
        /// Compute all function values from starting point to valueLookingPoint
        /// </summary>
        /// <param name="valueLookingPoint">Ending point</param>
        /// <param name="startingPoint">Starting point</param>
        /// <param name="startingPointFunctionValue">Function value at starting point</param>
        /// <param name="applyResultFormatting">Makes output better look. For example outputs 4.0 instead 4,000000000000001 or 3,999999999999999</param>
        /// <param name="step"></param>
        /// <returns></returns>
        public List<PointD> ComputeDifferentialPointsList(double valueLookingPoint, double startingPoint, double startingPointFunctionValue, bool applyResultFormatting = true, double step = 0.001)
        {
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

                if (applyResultFormatting)
                {
                    //Change e.g., 4,0000000000001 to 4
                    if (Math.Abs(_result - Math.Floor(_result)) < 0.000000001)
                        _result = Math.Floor(_result);
                    else if (Math.Abs(_result - Math.Ceiling(_result)) < 0.000000001)
                        _result = Math.Ceiling(_result);
                }

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

                if (applyResultFormatting)
                {
                    //Change e.g., 4,0000000000001 to 4
                    if (Math.Abs(_result - Math.Floor(_result)) < 0.000000001)
                        _result = Math.Floor(_result);
                    else if (Math.Abs(_result - Math.Ceiling(_result)) < 0.000000001)
                        _result = Math.Ceiling(_result);
                }

                points.Add(new PointD(_searchingPoint, _result));

                return points;
            }
            else //Szukany pkt jest == zadanemu punktowi
                return new List<PointD>() { new PointD(_searchingPoint, startingPointFunctionValue) };
        }

        /// <summary>
        /// Compute second order differential
        /// </summary>
        /// <param name="valueLookingPoint">Ending point</param>
        /// <param name="startingPoint">Starting point</param>
        /// <param name="startingPointFunctionValue">Function value at starting point</param>
        /// <param name="startingPointFunctionValueII">Derivative value at starting point</param>
        /// <param name="applyResultFormatting">Makes output better look. For example outputs 4.0 instead 4,000000000000001 or 3,999999999999999</param>
        /// <param name="step"></param>
        /// <returns></returns>
        public double ComputeDifferentialII(double valueLookingPoint, double startingPoint, double startingPointFunctionValue, double startingPointFunctionValueII, bool applyResultFormatting = true, double step = 0.001)
        {
            List<PointD> points = ComputeDifferentialIIPointsList(valueLookingPoint, startingPoint, startingPointFunctionValue, startingPointFunctionValueII, applyResultFormatting, step);

            return points.Last().Y;
        }

        /// <summary>
        /// Compute all function values from starting point to valueLookingPoint
        /// </summary>
        /// <param name="valueLookingPoint">Ending point</param>
        /// <param name="startingPoint">Starting point</param>
        /// <param name="startingPointFunctionValue">Function value at starting point</param>
        /// <param name="startingPointFunctionValueII">Derivative value at starting point</param>
        /// <param name="applyResultFormatting">Makes output better look. For example outputs 4.0 instead 4,000000000000001 or 3,999999999999999</param>
        /// <param name="step"></param>
        /// <returns></returns>
        public List<PointD> ComputeDifferentialIIPointsList(double valueLookingPoint, double startingPoint, double startingPointFunctionValue, double startingPointFunctionValueII, bool applyResultFormatting = true, double step = 0.001)
        {
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
                //Movinf forward
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

                if (applyResultFormatting)
                {
                    //Change e.g., 4,0000000000001 to 4
                    if (Math.Abs(_result - Math.Floor(_result)) < 0.000000001)
                        _result = Math.Floor(_result);
                    else if (Math.Abs(_result - Math.Ceiling(_result)) < 0.000000001)
                        _result = Math.Ceiling(_result);
                }

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

                if (applyResultFormatting)
                {
                    //Change e.g., 4,0000000000001 to 4
                    if (Math.Abs(_result - Math.Floor(_result)) < 0.000000001)
                        _result = Math.Floor(_result);
                    else if (Math.Abs(_result - Math.Ceiling(_result)) < 0.000000001)
                        _result = Math.Ceiling(_result);
                }

                points.Add(new PointD(_searchingPointII, _result));

                return points;
            }
            else //Szukany pkt jest == zadanemu punktowi
                return new List<PointD>() { new PointD(_searchingPoint, startingPointFunctionValue) };
        }

        private void ComputeFy()
        {
            y = _startingValue;
            x = _startingPoint;

            //Compute f0
            if (double.IsNaN(y))
                throw new NaNOccuredException();

            f0 = ComputeFunctionAtPoint();

            //Compute f1
            x += _step / 2;
            y = _startingValue + ((_step / 2) * f0);

            if (double.IsNaN(y))
                throw new NaNOccuredException();

            f1 = ComputeFunctionAtPoint();

            //Compute f2
            y = _startingValue + ((_step / 2) * f1);

            if (double.IsNaN(y))
                throw new NaNOccuredException();

            f2 = ComputeFunctionAtPoint();

            //Compute f3
            x += _step / 2;
            y = _startingValue + (_step * f2);

            if (double.IsNaN(y))
                throw new NaNOccuredException();

            f3 = ComputeFunctionAtPoint();
        }

        private void ComputeFyII()
        {
            u = _startingValueII;
            y = _startingValue;
            x = _startingPoint;

            //Compute f0
            if (double.IsNaN(y) || double.IsNaN(u))
                throw new NaNOccuredException();

            f0 = ComputeFunctionAtPoint();

            //Compute f0II
            string funkcjaTemp = function;
            function = _functionII;

            functionONP = _ONPII;

            f0II = ComputeFunctionAtPoint();

            //Compute f1
            function = funkcjaTemp;

            functionONP = _ONP;

            x += _step / 2;
            y = _startingValue + ((_step / 2) * f0);
            u = _startingValueII + ((_step / 2) * f0II);

            if (double.IsNaN(y) || double.IsNaN(u))
                throw new NaNOccuredException();

            f1 = ComputeFunctionAtPoint();

            //Compute f1II
            funkcjaTemp = function;
            function = _functionII;

            functionONP = _ONPII;

            f1II = ComputeFunctionAtPoint();

            //Compute f2
            function = funkcjaTemp;

            functionONP = _ONP;

            y = _startingValue + ((_step / 2) * f1);
            u = _startingValueII + ((_step / 2) * f1II);

            if (double.IsNaN(y) || double.IsNaN(u))
                throw new NaNOccuredException();

            f2 = ComputeFunctionAtPoint();

            //Compute f2II
            funkcjaTemp = function;
            function = _functionII;

            functionONP = _ONPII;

            f2II = ComputeFunctionAtPoint();

            //Compute f3
            function = funkcjaTemp;

            functionONP = _ONP;

            x += _step / 2;
            y = _startingValue + (_step * f2);
            u = _startingValueII + ((_step) * f2II);

            if (double.IsNaN(y) || double.IsNaN(u))
                throw new NaNOccuredException();

            f3 = ComputeFunctionAtPoint();

            //Compute f3II
            funkcjaTemp = function;
            function = _functionII;

            functionONP = _ONPII;

            f3II = ComputeFunctionAtPoint();

            //Restore function and ONP
            function = funkcjaTemp;

            functionONP = _ONP;
        }
        
        public Differential(string function)
            : base(function)
        { }
    }
}
