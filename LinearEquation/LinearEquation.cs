﻿using System;
using Rychusoft.NumericalLibraries.LinearEquation.Exceptions;

namespace Rychusoft.NumericalLibraries.LinearEquation
{
    public class LinearEquation
    {
        private int _variablesCount;
        double[,] _factors;
        double[] _variables;
        
        private void Multiplication(int n)
        {
            for (int i = n; i < _variablesCount; i++)
            {
                double multiplier = (_factors[n - 1, n - 1] / _factors[n - 1, i]);

                if (multiplier != 1)
                    for (int j = n - 1; j <= _variablesCount; j++)
                        _factors[j, i] *= multiplier;
            }
        }

        private void Substraction(int n)
        {
            for (int i = n; i < _variablesCount; i++)
                for (int j = n - 1; j <= _variablesCount; j++)
                    _factors[j, i] -= _factors[j, n - 1];
        }

        private void ComputeVariables()
        {
            for (int i = _variablesCount - 1; i >= 0; i--)
            {
                for(int j = _variablesCount - i - 1; j > 0; j--)
                    _factors[_variablesCount, i] -= (_factors[_variablesCount - j, i] * _variables[_variablesCount - j]);
                
                _variables[i] = _factors[_variablesCount, i] / _factors[i, i];

                if (double.IsNaN(_variables[i]))
                    throw new InconsistentSystemOfEquationsException();

                // Change e.g., 4,0000000000001 to 4
                if (Math.Abs(_variables[i] - Math.Floor(_variables[i])) < 0.000000001)
                    _variables[i] = Math.Floor(_variables[i]);
                else if (Math.Abs(_variables[i] - Math.Ceiling(_variables[i])) < 0.000000001)
                    _variables[i] = Math.Ceiling(_variables[i]);
            }
        }

        /// <summary>
        /// Compute variables
        /// </summary>
        /// <returns></returns>
        public double[] Compute()
        {
            //Gauss elimination
            for (int i = 1; i < _variablesCount; i++)
            {
                Multiplication(i);

                Substraction(i);
            }

            ComputeVariables();

            return _variables;
        }

        /// <summary>
        /// Linear equation constructor
        /// </summary>
        /// <param name="coefficients">Coefficients standing by the variables. Double[rows, rows + 1]</param>
        public LinearEquation(double[,] coefficients)
        {
            //Check if algorithm can solve the matrix
            if (coefficients.GetLength(0) + 1 != coefficients.GetLength(1))
                throw new RowsNumberMustBeOneLessThenColumnsNumberException();

            //Need to transpose the table for the algorithm needs
            this._factors = new double[coefficients.GetLength(1), coefficients.GetLength(0)];

            for (int i = 0; i < coefficients.GetLength(0); i++)
                for (int j = 0; j < coefficients.GetLength(1); j++)
                    _factors[j, i] = coefficients[i, j];

            _variablesCount = _factors.GetLength(1);

            _variables = new double[_variablesCount];
        }
    }
}
