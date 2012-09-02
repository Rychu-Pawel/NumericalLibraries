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

        }
    }
}
