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

        public LinearEquationBinding[] LinearEquationCoefficients { get; set; }
        public LinearEquationBinding[] LinearEquationResults { get; set; }

        public LinearEquationWindowLogic(LinearEquationWindow lew)
        {
            window = lew;

            VariablesCount = new Property()
            {
                Text = "2"
            };
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
