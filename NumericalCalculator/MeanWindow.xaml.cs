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
    /// Interaction logic for MeanWindow.xaml
    /// </summary>
    public partial class MeanWindow : Window
    {
        MeanWindowLogic logic;

        public MeanWindow()
        {
            InitializeComponent();

            logic = new MeanWindowLogic(this);

            pnlMean.DataContext = logic;

            dgValues.ItemsSource = logic.MeanDataList;
        }

        private void btnCompute_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (logic.Arithmetic)
                {
                    //Wczytanie punktow
                    List<double> values = logic.MeanDataList.Select(i => i.Value).ToList();

                    Mean mean = new Mean();
                    logic.Result.Value = mean.ComputeArithmetic(values);
                }
                else
                {
                    //Wczytanie punktow
                    List<double[]> values = logic.MeanDataList.Select(i => new double[] { i.Value, i.Weight }).ToList();

                    Mean mean = new Mean();
                    logic.Result.Value = mean.ComputeWeighted(values);
                }
            }
            catch (Exception excep)
            {
                //Próba wyciągnięcia messageu dla exceptiona
                string message = Translation.GetString("MeanForm_" + excep.GetType().Name);

                if (string.IsNullOrEmpty(message))
                {
                    message = Translation.GetString(excep.GetType().Name);

                    if (string.IsNullOrEmpty(message))
                        message = excep.Message;
                }

                //Komunikat
                MessageBox.Show(message, Translation.GetString("MessageBox_Caption_Error"), MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
