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
    /// Interaction logic for ProportionWindow.xaml
    /// </summary>
    public partial class ProportionWindow : Window
    {
        ProportionWindowLogic logic = new ProportionWindowLogic();

        public ProportionWindow()
        {
            InitializeComponent();

            //Ustawienie bindingu
            pnlProportion.DataContext = logic;
        }

        private void btnCompute_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double v1, v2, v3, v4;

                //Pobranie wartości
                if (string.IsNullOrEmpty(logic.First.Text) || logic.First.Text == "x")
                    v1 = double.NaN;
                else if (double.IsNaN(logic.First.Value))
                    throw new V1FormatException();
                else
                    v1 = logic.First.Value;

                if (string.IsNullOrEmpty(logic.Second.Text) || logic.Second.Text == "x")
                    v2 = double.NaN;
                else if (double.IsNaN(logic.Second.Value))
                    throw new V2FormatException();
                else
                    v2 = logic.Second.Value;

                if (string.IsNullOrEmpty(logic.Third.Text) || logic.Third.Text == "x")
                    v3 = double.NaN;
                else if (double.IsNaN(logic.Third.Value))
                    throw new V3FormatException();
                else
                    v3 = logic.Third.Value;

                if (string.IsNullOrEmpty(logic.Fourth.Text) || logic.Fourth.Text == "x")
                    v4 = double.NaN;
                else if (double.IsNaN(logic.Fourth.Value))
                    throw new V4FormatException();
                else
                    v4 = logic.Fourth.Value;

                //Obliczenia
                Proportions prop = new Proportions();
                logic.Result.Value = prop.Compute(v1, v2, v3, v4);
            }
            catch (V1FormatException)
            {
                MessageBox.Show(Translation.GetString("ProportionsForm_V1FormatException"), Translation.GetString("MessageBox_Caption_Error"), MessageBoxButton.OK, MessageBoxImage.Error);
                txtFirstValue.Focus();
            }
            catch (V2FormatException)
            {
                MessageBox.Show(Translation.GetString("ProportionsForm_V2FormatException"), Translation.GetString("MessageBox_Caption_Error"), MessageBoxButton.OK, MessageBoxImage.Error);
                txtFirstValue.Focus();
            }
            catch (V3FormatException)
            {
                MessageBox.Show(Translation.GetString("ProportionsForm_V3FormatException"), Translation.GetString("MessageBox_Caption_Error"), MessageBoxButton.OK, MessageBoxImage.Error);
                txtFirstValue.Focus();
            }
            catch (V4FormatException)
            {
                MessageBox.Show(Translation.GetString("ProportionsForm_V4FormatException"), Translation.GetString("MessageBox_Caption_Error"), MessageBoxButton.OK, MessageBoxImage.Error);
                txtFirstValue.Focus();
            }
            catch (Exception ex)
            {
                string message = Translation.GetString(ex.GetType().Name);

                if (string.IsNullOrEmpty(message))
                    message = ex.Message;

                MessageBox.Show(message, Translation.GetString("MessageBox_Caption_Error"), MessageBoxButton.OK, MessageBoxImage.Error);
                txtFirstValue.Focus();
            }
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }
    }

    public class V1FormatException : Exception
    { }

    public class V2FormatException : Exception
    { }

    public class V3FormatException : Exception
    { }

    public class V4FormatException : Exception
    { }
}
