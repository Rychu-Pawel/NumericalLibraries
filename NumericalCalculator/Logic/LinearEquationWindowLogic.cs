using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;

namespace NumericalCalculator.Logic
{
    class LinearEquationWindowLogic
    {
        LinearEquationWindow window;

        public LinearEquationBinding[] LinearEquationCoefficients { get; set; }
        public LinearEquationBinding[] LinearEquationResults { get; set; }

        private string variablesCount;

        public string VariablesCount
        {
            get { return variablesCount; }

            set
            {
                //Przypisanie wartosci
                variablesCount = value;

                //Zadeklarowanie tabel
                int i;

                if (int.TryParse(value, out i))
                {
                    window.dgEquations.Columns.Clear();
                    window.dgResults.Columns.Clear();

                    //Wspolczynniki
                    LinearEquationCoefficients = new LinearEquationBinding[i];

                    for (int j = 0; j < LinearEquationCoefficients.Length; j++)
                        LinearEquationCoefficients[j] = new LinearEquationBinding(i + 1);

                    for (int j = 0; j < i + 1; j++)
                    {
                        DataGridTextColumn col = new DataGridTextColumn();
                        col.Binding = new Binding("Values[" + j + "]");
                        col.Header = j < i ? "x" + (j + 1).ToString() : "r";
                        col.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
                        window.dgEquations.Columns.Add(col);
                    }

                    //Przepisanie wspolczynnikow do nowego datasource
                    LinearEquationBinding[] oldCoefficients = (LinearEquationBinding[])window.dgEquations.ItemsSource;

                    if (oldCoefficients != null)
                    {
                        for (int j = 0; j < oldCoefficients.Length && j < LinearEquationCoefficients.Length; j++)
                        {
                            LinearEquationBinding oldCoef = oldCoefficients[j];
                            LinearEquationBinding newCoef = LinearEquationCoefficients[j];

                            for (int k = 0; k < oldCoef.Values.Length && k < newCoef.Values.Length; k++)
                                newCoef.Values[k] = oldCoef.Values[k];
                        }
                    }

                    window.dgEquations.ItemsSource = LinearEquationCoefficients;

                    //Wyniki
                    LinearEquationResults = new LinearEquationBinding[1];
                    LinearEquationResults[0] = new LinearEquationBinding(i);

                    for (int j = 0; j < i; j++)
                    {
                        DataGridTextColumn col = new DataGridTextColumn();
                        col.Binding = new Binding("Values[" + j + "]");
                        col.Header = "x" + (j + 1).ToString();
                        col.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
                        window.dgResults.Columns.Add(col);
                    }

                    window.dgResults.ItemsSource = LinearEquationResults;
                }
            }
        }

        public LinearEquationWindowLogic(LinearEquationWindow lew)
        {
            window = lew;

            VariablesCount = new Property()
            {
                Text = "2"
            };
        }

        internal void ClearResults()
        {
            if (LinearEquationResults == null || LinearEquationResults[0] == null || LinearEquationResults[0].Values == null)
                return;

            for (int i = 0; i < LinearEquationResults[0].Values.Length; i++)
                LinearEquationResults[0].Values[i] = 0.0d;
        }

        internal double[,] GetCoefficients()
        {
            int variablesCount = LinearEquationCoefficients.Length;

            double[,] coefficients = new double[variablesCount, variablesCount + 1];

            for (int i = 0; i < variablesCount; i++)
            {
                for (int j = 0; j < variablesCount + 1; j++)
                    coefficients[i, j] = LinearEquationCoefficients[i].Values[j];
            }

            return coefficients;
        }
    }

    class LinearEquationBinding
    {
        public double[] Values { get; set; }

        public LinearEquationBinding(int columns)
        {
            Values = new double[columns];
        }
    }
}
