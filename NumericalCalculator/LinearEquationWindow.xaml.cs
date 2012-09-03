using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using NumericalCalculator.Logic;
using NumericalCalculator.Translations;

namespace NumericalCalculator
{
    /// <summary>
    /// Interaction logic for LinearEquationWindow.xaml
    /// </summary>
    public partial class LinearEquationWindow : Window
    {
        LinearEquationWindowLogic logic;

        public LinearEquationWindow()
        {
            InitializeComponent();

            logic = new LinearEquationWindowLogic(this);
            logic.VariablesCount = "2";

            pnlLinearEquation.DataContext = logic;

            dgEquations.ItemsSource = logic.LinearEquationCoefficients;
            dgResults.ItemsSource = logic.LinearEquationResults;
        }

        private void btnCompute_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double[,] coefficients = logic.GetCoefficients();

                LinearEquation gauss = new LinearEquation(coefficients);
                logic.LinearEquationResults[0].Values = gauss.Compute();

                dgResults.Items.Refresh();
            }
            catch (NullReferenceException)
            {
                HandleException(Translation.GetString("LinearEquation_NullReferenceException"));
            }
            catch (FormatException)
            {
                HandleException(Translation.GetString("LinearEquation_FormatException"));
            }
            catch (Exception excep)
            {
                string message = Translation.GetString(excep.GetType().Name);

                if (string.IsNullOrEmpty(message))
                    message = excep.Message;

                HandleException(message);
            }
        }

        private void HandleException(string message)
        {
            MessageBox.Show(message, Translation.GetString("MessageBox_Caption_Error"), MessageBoxButton.OK, MessageBoxImage.Error);

            //Czyszczenie resultow
            logic.ClearResults();
        }
    }
}
