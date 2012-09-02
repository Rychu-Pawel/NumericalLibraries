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
using NumericalCalculator.Exceptions;

namespace NumericalCalculator
{
    /// <summary>
    /// Interaction logic for InterpolationWindow.xaml
    /// </summary>
    public partial class InterpolationWindow : Window
    {
        //Event
        public delegate void FunctionAcceptedEventHandler(string function);
        public event FunctionAcceptedEventHandler FunctionAccepted;

        //Logic
        InterpolationWindowLogic logic;

        public InterpolationWindow()
        {
            InitializeComponent();

            logic = new InterpolationWindowLogic();

            pnlInterpolation.DataContext = logic;

            dgInterpolation.ItemsSource = logic.InterpolationDataList;
        }

        private void btnApply_Click(object sender, RoutedEventArgs e)
        {
            if (logic.Result != string.Empty)
            {
                FunctionAccepted(logic.Result);
                Close();
            }
            else
                MessageBox.Show(Translation.GetString("InterpolationForm_MessageBox_btnApply"), Translation.GetString("MessageBox_Caption_Error"), MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btnCompute_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Interpolacja
                if (logic.Interpolation)
                {
                    // ZMIENNE
                    Interpolation interpolation = new Interpolation();
                    interpolation.Points = logic.InterpolationDataList;
                    logic.Result.Text = interpolation.Compute();
                }
                //Aproksymacja
                else if (logic.Approximation)
                {
                    int level;

                    if (!int.TryParse(logic.Level, out level))
                        throw new WrongApproximationLevelException();

                    // ZMIENNE
                    Approximation approximation = new Approximation(level);

                    //Aproksymacja
                    approximation.Points = logic.InterpolationDataList;
                    logic.Result.Text = approximation.Compute();
                }
            }
            catch (WrongApproximationLevelException)
            {
                MessageBox.Show(Translation.GetString("InterpolationForm_WrongApproximationLevelException"), Translation.GetString("MessageBox_Caption_Error"), MessageBoxButton.OK, MessageBoxImage.Error);
                txtLevel.Focus();
            }
            catch (NoPointsProvidedException)
            {
                MessageBox.Show(Translation.GetString("InterpolationForm_NoPointsProvidedException"), Translation.GetString("MessageBox_Caption_Error"), MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (InconsistentSystemOfEquationsException)
            {
                MessageBox.Show(Translation.GetString("InterpolationForm_InconsistentSystemOfEquationsException"), Translation.GetString("MessageBox_Caption_Error"), MessageBoxButton.OK, MessageBoxImage.Error);
                txtLevel.Focus();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show(Translation.GetString("InterpolationForm_NullReferenceException"), Translation.GetString("MessageBox_Caption_Error"), MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (FormatException)
            {
                MessageBox.Show(Translation.GetString("InterpolationForm_FormatException"), Translation.GetString("MessageBox_Caption_Error"), MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception excep)
            {
                string message = Translation.GetString(excep.GetType().Name);

                if (string.IsNullOrEmpty(message))
                    message = excep.Message;

                MessageBox.Show(message, Translation.GetString("MessageBox_Caption_Error"), MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
