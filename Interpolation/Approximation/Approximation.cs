using System;
using System.Collections.Generic;
using Rychusoft.NumericalLibraries.Approximation.Exceptions;
using Rychusoft.NumericalLibraries.Common;

namespace Rychusoft.NumericalLibraries.Approximation
{
    public class Approximation
    {
        //ZMIENNE ------------------------
        private int _degree;
        private List<PointD> _points;
        private double[] Eyx, Ex, x;
        private double[,] _factors;

        private double Ex1, Ey, Ex2, Ex1y;
        private double Ex3, Ex4, Ex2y;
        private double x1, x2;
        private double x3;

        string _result;

        public List<PointD> Points { get { return _points; } }
        public string Result { get { return _result; } }

        /// <summary>
        /// Approximation contructor
        /// </summary>
        /// <param name="points">Points which will be used for approximation</param>
        /// <param name="level">Approximation level</param>
        public Approximation(List<PointD> points, int level)
        {
            if (points == null)
                throw new ArgumentNullException(nameof(points));

            if (_points.Count == 0)
                throw new ArgumentException("No points provided", nameof(points));

            if (level <= 0)
                throw new WrongApproximationLevelException();

            this._points = points;
            this._degree = level;

            Ex1 = Ey = Ex2 = Ex1y = Ex3 = Ex4 = Ex2y = x1 = x2 = x3 = 0;

            Eyx = new double[level + 1];
            Ex = new double[2 * level + 1];
            x = new double[level + 1];

            _factors = new double[level + 1, level + 2];

            Validation();
        }

        private void Validation()
        {
            //Check if the points are distinct
            for (int i = 0; i < _points.Count; i++)
                for (int j = i + 1; j < _points.Count; j++)
                    if (_points[i].X == _points[j].X)
                        throw new SystemException("x = " + Convert.ToString(_points[i].X) + " appears at least twice");
        }

        private void ComputeLineSums()
        {
            Ex1 = Ey = Ex2 = Ex1y = 0;

            for (int i = 0; i < _points.Count; i++)
            {
                Ex1 += _points[i].X;
                Ey += _points[i].Y;
                Ex2 += Math.Pow(_points[i].X, 2.0);
                Ex1y += _points[i].X * _points[i].Y;
            }
        }

        private void ComputeSquaredSums()
        {
            Ex1 = Ey = Ex2 = Ex1y = Ex3 = Ex4 = Ex2y = 0;

            for (int i = 0; i < _points.Count; i++)
            {
                Ex1 += _points[i].X;
                Ey += _points[i].Y;
                Ex2 += Math.Pow(_points[i].X, 2.0);
                Ex1y += _points[i].X * _points[i].Y;
                Ex3 += Math.Pow(_points[i].X, 3.0);
                Ex4 += Math.Pow(_points[i].X, 4.0);
                Ex2y += Math.Pow(_points[i].X, 2.0) * _points[i].Y;
            }
        }

        private void ComputeSums()
        {
            //Ex[0]
            Ex[0] = _points.Count;

            //Eyx[0]
            for (int i = 0; i < _points.Count; i++)
                Eyx[0] += _points[i].Y;

            for (int i = 0; i < _points.Count; i++)
            {
                //Ex
                for (int j = 1; j < _degree * 2 + 1; j++)
                    Ex[j] += Math.Pow(_points[i].X, j);

                //Eyx
                for (int j = 1; j < _degree + 1; j++)
                    Eyx[j] += _points[i].Y * Math.Pow(_points[i].X, j);
            }
        }

        private void ComputeLineFactors()
        {
            x1 = x2 = 0;

            double denominator = 0;

            denominator = _points.Count * Ex2 - Math.Pow(Ex1, 2.0);

            x2 = (_points.Count * Ex1y - Ex1 * Ey) / denominator;

            x1 = (Ey * Ex2 - Ex1 * Ex1y) / denominator;
        }

        private void ComputeSquareFactors()
        {
            x1 = x2 = x3 = 0;

            double firstPart = 0, secondPart = 0;

            firstPart = _points.Count * Ex2 - Math.Pow(Ex1, 2.0);
            secondPart = _points.Count * Ex3 - Ex2 * Ex1;

            x3 = (firstPart * (_points.Count * Ex2y - Ex2 * Ey) - (_points.Count * Ex1y - Ey * Ex1) * (_points.Count * Ex3 - Ex2 * Ex1)) / (firstPart * (_points.Count * Ex4 - Ex2 * Ex2) - Math.Pow((_points.Count * Ex3 - Ex2 * Ex1), 2.0));

            x2 = (_points.Count * Ex1y - Ex1 * Ey - (_points.Count * Ex3 - Ex1 * Ex2) * x3) / (_points.Count * Ex2 - Ex1 * Ex1);

            x1 = (Ey - Ex2 * x3 - Ex1 * x2) / _points.Count;
        }

        private void ComputeFactors()
        {
            //Gauss matrix
            for (int i = 0; i < _degree + 1; i++)
                for (int j = 0; j < _degree + 1; j++)
                    _factors[j, i] = Ex[i + j];

            //Extend factorial matrix with results
            for (int i = 0; i < _degree + 1; i++)
                _factors[i, _degree + 1] = Eyx[i];

            //Compute factors
            LinearEquation.LinearEquation Gauss = new LinearEquation.LinearEquation(_factors);
            x = Gauss.Compute();
        }

        private string ComputeLinearRegression()
        {
            Validation();

            ComputeLineSums();

            ComputeLineFactors();

            string function = string.Empty;

            if (x2 == -1)
                function = "-x";
            else if (x2 == 1)
                function = "x";
            else if (x2 != 0)
                function = Convert.ToString(x2) + "*x";

            if (x1 < 0)
                function += Convert.ToString(x1);
            else if (x1 > 0)
            {
                if (x2 != 0) // If it is the constant function then make "2" instead of "+2"
                    function += "+" + Convert.ToString(x1);
                else
                    function += Convert.ToString(x1);
            }

            if (function == string.Empty) // Constant function = 0
                function = "0";

            return function;
        }

        private string ComputeSquaredRegression()
        {
            Validation();

            ComputeSquaredSums();

            ComputeSquareFactors();

            string function = string.Empty;

            if (x3 == -1)
                function = "-x^2";
            else if (x3 == 1)
                function = "x^2";
            else if (x3 != 0)
                function = Convert.ToString(x3) + "*x^2";

            if (x2 == -1)
                function += "-x";
            else if (x2 == 1)
                function += "+x";
            else if (x2 < 0)
                function += Convert.ToString(x2) + "*x";
            else if (x2 > 0)
                function += "+" + Convert.ToString(x2) + "*x";

            if (x1 < 0)
                function += Convert.ToString(x1);
            else if (x1 > 0)
            {
                if (x2 != 0 || x3 != 0) // If it is the constant function then make "2" instead of "+2"
                    function += "+" + Convert.ToString(x1);
                else
                    function += Convert.ToString(x1);
            }

            if (function == string.Empty) // Constant function = 0
                function = "0";

            //Remove "+" in front
            if (function.StartsWith("+"))
                function = function.Substring(1);

            //Remove "1*" in front
            if (function.StartsWith("1*"))
                function = function.Substring(2);

            return function;
        }

        private string FormatResult()
        {
            int sign = _degree;
            string function = string.Empty;

            //Format the output
            for (int i = _degree; i >= 0; i--)
            {
                if (sign++ % 2 == 0) // Plus sign by X
                {
                    //if (!(Math.Abs(x[i]) < 0.000000001)) // Skip small factors
                    //{
                    if (x[i] < 0 && x[i] != 1 && x[i] != -1) // + and - is -
                    {
                        if (i > 1)
                            function += (Convert.ToString(x[i]) + "*x^" + Convert.ToString(i)); // if 1 then just writes X if 0 then just skip it
                        else if (i == 1)
                            function += (Convert.ToString(x[i]) + "*x");
                        else if (i == 0)
                            function += (Convert.ToString(x[i]));
                    }
                    else if (x[i] > 0 && x[i] != 1 && x[i] != -1) // Just positive number
                    {
                        if (i > 1) // if 1 then just writes X if 0 then just skip it
                            function += ("+" + Convert.ToString(x[i]) + "*x^" + Convert.ToString(i));
                        else if (i == 1)
                            function += ("+" + Convert.ToString(x[i]) + "*x");
                        else if (i == 0)
                            function += ("+" + Convert.ToString(x[i]));
                    }
                    else if (x[i] == 1) // Skip 1* by x
                    {
                        if (i > 1) // if 1 then just writes X if 0 then just skip it
                            function += ("+x^" + Convert.ToString(i));
                        else if (i == 1)
                            function += ("+x");
                        else if (i == 0)
                            function += ("+1");
                    }
                    else if (x[i] == -1) // Skip 1* by x, adding -
                        if (i > 1) // if 1 then just writes X if 0 then just skip it
                            function += ("-x^" + Convert.ToString(i));
                        else if (i == 1)
                            function += ("-x");
                        else if (i == 0)
                            function += ("-1");
                    //}
                }
                else // Minus sign by X
                {
                    //if (!(Math.Abs(x[i]) < 0.000000001)) // Skip small factors
                    //{
                    if (x[i] < 0 && x[i] != 1 && x[i] != -1) // + and - is -
                    {
                        if (i > 1) // if 1 then just writes X if 0 then just skip it
                            function += ("+" + Convert.ToString(Math.Abs(x[i])) + "*x^" + Convert.ToString(i));
                        else if (i == 1)
                            function += ("+" + Convert.ToString(Math.Abs(x[i])) + "*x");
                        else if (i == 0)
                            function += ("+" + Convert.ToString(Math.Abs(x[i])));
                    }
                    else if (x[i] > 0 && x[i] != 1 && x[i] != -1) // Just positive number
                    {
                        if (i > 1) // if 1 then just writes X if 0 then just skip it
                            function += ("-" + Convert.ToString(Math.Abs(x[i])) + "*x^" + Convert.ToString(i));
                        else if (i == 1)
                            function += ("-" + Convert.ToString(Math.Abs(x[i])) + "*x");
                        else if (i == 0)
                            function += ("-" + Convert.ToString(Math.Abs(x[i])));
                    }
                    else if (x[i] == 1) // skip 1* by x
                    {
                        if (i > 1) // if 1 then just writes X if 0 then just skip it
                            function += ("-x^" + Convert.ToString(i));
                        else if (i == 1)
                            function += ("-x");
                        else if (i == 0)
                            function += ("-1");
                    }
                    else if (x[i] == -1) // skip 1* by x, adding -
                        if (i > 1) // if 1 then just writes X if 0 then just skip it
                            function += ("+x^" + Convert.ToString(i));
                        else if (i == 1)
                            function += ("+x");
                        else if (i == 0)
                            function += ("+1");
                    //}
                }
            }

            //Remove "+" in front
            if (function.StartsWith("+"))
                function = function.Substring(1);

            //Remove "1*" in front
            if (function.StartsWith("1*"))
                function = function.Substring(2);

            return function;
        }

        /// <summary>
        /// Approximates function from given points
        /// </summary>
        /// <returns>Approximated function</returns>
        public string Compute()
        {
            if (_degree == 1)
                _result = ComputeLinearRegression();
            else if (_degree == 2)
                _result = ComputeSquaredRegression();
            else
            {
                Validation();

                ComputeSums();

                ComputeFactors();

                _result = FormatResult();
            }

            return _result;
        }
    }
}
