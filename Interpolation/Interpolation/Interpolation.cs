using System;
using System.Collections.Generic;
using Rychusoft.NumericalLibraries.Common;
using Rychusoft.NumericalLibraries.Interpolation.Exceptions;

namespace Rychusoft.NumericalLibraries.Interpolation
{
    public class Interpolation
    {
        private double[,] _base;
        private double[] _denominatorBase;
        private List<PointD> _points;

        private double[] _result; 

        private string _strResult; 

        public List<PointD> Points { get { return _points; } }
        public string Result { get { return _strResult; } }

        /// <summary>
        /// Constructor for interpolation
        /// </summary>
        /// <param name="points">Points which will be used for interpolation</param>
        public Interpolation(List<PointD> points)
        {
            if (points == null)
                throw new ArgumentNullException(nameof(points));

            this._points = points;

            ErrorCheck();
        }

        private void ErrorCheck()
        {
            if (_points.Count == 0)
                throw new NoPointsProvidedException();

            // Check if the points are distinct
            for (int i = 0; i < _points.Count; i++)
                for (int j = i + 1; j < _points.Count; j++)
                    if (_points[i].X == _points[j].X)
                        throw new Exception("x = " + Convert.ToString(_points[i].X) + " appears at least twice");
        }

        private void CreateBase(int j, int iteration) // j - skipped index in lagrange formula
        {
            if (iteration == -1)
            {
                //Create first iteration
                if (j == 1) // create l(j,2)
                    _base[j - 1, 0] = _points[1].X;
                else
                    _base[j - 1, 0] = _points[0].X;

                _base[j - 1, 1] = 1;
                
                if (_points.Count > 2)
                    CreateBase(j, iteration + 1);
            }
            else
            {
                //Create new power
                _base[j - 1, iteration + 2] = 1;

                //Create (n-i) powers
                int multiplier = iteration + 1;
                if (j - 1 <= multiplier)
                    multiplier++;

                for (int i = iteration + 1; i > 0; i--)
                        _base[j - 1, i] = _base[j - 1, i]*_points[multiplier].X + _base[j - 1, i - 1];

                //Create last element
                _base[j - 1, 0] *= _points[multiplier].X;

                // Recursion
                if (iteration < _points.Count - 3)
                    CreateBase(j, iteration + 1);
            }
        }

        private void CreateDenominatorBase(int j)
        {
            _denominatorBase[j - 1] = 1;

            for (int i = 1; i <= _points.Count; i++)
                if (i != j)
                    _denominatorBase[j - 1] *= (_points[j - 1].X - _points[i - 1].X);

            _denominatorBase[j - 1] = _points[j - 1].Y/_denominatorBase[j - 1];
        }

        private void Interpolate()
        {
            _base = new double[_points.Count, _points.Count];
            _denominatorBase = new double[_points.Count];
            _result = new double[_points.Count];
            
            for (int i = 1; i <= _points.Count; i++)
                CreateBase(i, -1);

            for (int i = 1; i <= _points.Count; i++)
                CreateDenominatorBase(i);
            
            for (int i = 0; i < _points.Count; i++)
                for (int j = 0; j < _points.Count; j++)
                    _result[i] += _base[j, i] * _denominatorBase[j];
        }

        private void FormatResult()
        {
            int sign = 2;
            
            for (int i = _points.Count - 1; i >= 0; i--)
            {
                if (sign++ % 2 == 0) // Plus sign by X
                {
                    if (!(Math.Abs(_result[i]) < 0.000000001)) // if 1 then just writes X if 0 then just skip it
                    {
                        if (_result[i] < 0 && _result[i] != 1 && _result[i] != -1) // + and - is -
                        {
                            if (i > 1) // if 1 then just writes X if 0 then just skip it
                                _strResult += (Convert.ToString(_result[i]) + "*x^" + Convert.ToString(i));
                            else if (i == 1)
                                _strResult += (Convert.ToString(_result[i]) + "*x");
                            else if (i == 0)
                                _strResult += (Convert.ToString(_result[i]));
                        }
                        else if (_result[i] > 0 && _result[i] != 1 && _result[i] != -1) // Just positive number
                        {
                            if (i > 1) // if 1 then just writes X if 0 then just skip it
                                _strResult += ("+" + Convert.ToString(_result[i]) + "*x^" + Convert.ToString(i));
                            else if (i == 1)
                                _strResult += ("+" + Convert.ToString(_result[i]) + "*x");
                            else if (i == 0)
                                _strResult += ("+" + Convert.ToString(_result[i]));
                        }
                        else if (_result[i] == 1) // skip 1* by x
                        {
                            if (i > 1) // if 1 then just writes X if 0 then just skip it
                                _strResult += ("+x^" + Convert.ToString(i));
                            else if (i == 1)
                                _strResult += ("+x");
                            else if (i == 0)
                                _strResult += ("+1");
                        }
                        else if (_result[i] == -1) // skip 1* by x, adding -
                            if (i > 1) // if 1 then just writes X if 0 then just skip it
                                _strResult += ("-x^" + Convert.ToString(i));
                            else if (i == 1)
                                _strResult += ("-x");
                            else if (i == 0)
                                _strResult += ("-1");
                    }
                }
                else // Minus sign by X
                {
                    if (!(Math.Abs(_result[i]) < 0.000000001)) //Skip small factors
                    {
                        if (_result[i] < 0 && _result[i] != 1 && _result[i] != -1) // + and - is -
                        {
                            if (i > 1) // if 1 then just writes X if 0 then just skip it
                                _strResult += ("+" + Convert.ToString(Math.Abs(_result[i])) + "*x^" + Convert.ToString(i));
                            else if (i == 1)
                                _strResult += ("+" + Convert.ToString(Math.Abs(_result[i])) + "*x");
                            else if (i == 0)
                                _strResult += ("+" + Convert.ToString(Math.Abs(_result[i])));
                        }
                        else if (_result[i] > 0 && _result[i] != 1 && _result[i] != -1) // just positive number
                        {
                            if (i > 1) // if 1 then just writes X if 0 then just skip it
                                _strResult += ("-" + Convert.ToString(Math.Abs(_result[i])) + "*x^" + Convert.ToString(i));
                            else if (i == 1)
                                _strResult += ("-" + Convert.ToString(Math.Abs(_result[i])) + "*x");
                            else if (i == 0)
                                _strResult += ("-" + Convert.ToString(Math.Abs(_result[i])));
                        }
                        else if (_result[i] == 1) // skip 1* by x
                        {
                            if (i > 1) // if 1 then just writes X if 0 then just skip it
                                _strResult += ("-x^" + Convert.ToString(i));
                            else if (i == 1)
                                _strResult += ("-x");
                            else if (i == 0)
                                _strResult += ("-1");
                        }
                        else if (_result[i] == -1) // skip 1* by x, adding -
                            if (i > 1) // if 1 then just writes X if 0 then just skip it
                                _strResult += ("+x^" + Convert.ToString(i));
                            else if (i == 1)
                                _strResult += ("+x");
                            else if (i == 0)
                                _strResult += ("+1");
                    }
                }
            }

            //Remove "+" in front
            if (_strResult.StartsWith("+"))
                _strResult = _strResult.Substring(1);

            //Remove "1*" in front
            if (_strResult.StartsWith("1*"))
                _strResult = _strResult.Substring(2);
        }

        /// <summary>
        /// Interpolate function from given points
        /// </summary>
        /// <returns>Interpolated function</returns>
        public string Compute()
        {
            _strResult = string.Empty;
            
            if (_points.Count == 1)
                return Convert.ToString(_points[0].Y);
            else if (_points.Count >= 2)
            {
                Interpolate();

                FormatResult();
            }

            if (_strResult == string.Empty)
                _strResult = "0";

            return _strResult;
        }
    }
}
