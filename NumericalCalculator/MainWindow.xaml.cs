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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.Globalization;
using NumericalCalculator.Translations;
using NumericalCalculator.Logic;

namespace NumericalCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainWindowLogic logic;

        //PRZESUWANIE WYKRESU
        bool graphMovingStarted = false;
        Point startingPoint, endPoint;

        public MainWindow()
        {
            InitializeComponent();

            //Utworzenie logiki
            ObjectDataProvider odp = (ObjectDataProvider)Application.Current.Resources.MergedDictionaries.Where(s => s.Source.OriginalString == "Resources.xaml").Single()["Language"];

            logic = new MainWindowLogic(this, odp);

            //Poustawianie GUI
            logic.RadioButtonChanged();
            
            //Data context
            pnlMainWindow.DataContext = logic;

            //Focus na funkcji
            txtFunction.Focus();
        }

        private void miClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void miMean_Click(object sender, RoutedEventArgs e)
        {
            MeanWindow mw = new MeanWindow();
            mw.Show();
        }

        private void miGraph_Click(object sender, RoutedEventArgs e)
        {

        }

        private void miLanguage_Click(object sender, RoutedEventArgs e)
        {
            Translation.ToggleLanguage();
        }

        private void miClick_OpenWindow(object sender, RoutedEventArgs e)
        {
            MenuItem mi = sender as MenuItem;

            //Wybranie okna
            Window window = null;

            if (mi.Name == miLinearEquations.Name)
                window = new LinearEquationWindow();
            else if (mi.Name == miInterpolation.Name)
            {
                window = new InterpolationWindow();
                (window as InterpolationWindow).FunctionAccepted += new InterpolationWindow.FunctionAcceptedEventHandler(MainWindow_FunctionAccepted);
            }
            else if (mi.Name == miProportion.Name)
                window = new ProportionWindow();
            else if (mi.Name == miMean.Name)
                window = new MeanWindow();
            else if (mi.Name == miAbout.Name)
                window = new AboutWindow();
            else if (mi.Name == miFunctions.Name)
                window = new FunctionsWindow();

            //Pokazanie okna
            if (window != null)
                window.Show();
        }

        void MainWindow_FunctionAccepted(string function)
        {
            txtFunction.Text = function;
        }

        private void btnSpecialFunction_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(Translation.GetString("MessageBox_btnSpecialFunction_Click"), Translation.GetString("MessageBox_btnSpecialFunction_Click_Caption"), MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btnHelp_Click(object sender, RoutedEventArgs e)
        {
            FunctionsWindow fw = new FunctionsWindow();
            fw.Show();
        }

        private void rb_Clicked(object sender, RoutedEventArgs e)
        {
            if (logic != null)
                logic.RadioButtonChanged();
        }

        private void cmbSpecialFunction_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (logic != null)
                logic.ComboSpecialChanged();
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            logic.ClearGraph();
        }

        private void btnCompute_Click(object sender, RoutedEventArgs e)
        {
            logic.Compute();
        }

        private void btnDraw_Click(object sender, RoutedEventArgs e)
        {
            logic.DrawGraph();
        }

        //Przesuwanie wykresu
        private void picGraph_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                startingPoint = e.GetPosition(picGraph);
                graphMovingStarted = true;
            }
        }

        private void picGraph_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left && graphMovingStarted && (!string.IsNullOrEmpty(txtFunction.Text) || rbSpecialFunction.IsChecked.GetValueOrDefault(false)))
            {
                graphMovingStarted = false;
                endPoint = e.GetPosition(picGraph);
            }
        }

        private void picGraph_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (graphMovingStarted && (!string.IsNullOrEmpty(txtFunction.Text) || rbSpecialFunction.IsChecked.GetValueOrDefault(false)))
                {
                    //Zmienne
                    double xFrom, xTo, yFrom, yTo;

                    xFrom = xTo = yFrom = yTo = 0d;

                    try
                    {
                        xFrom = logic.GetArgument(ArgumentTypeEnum.xFrom);
                        xTo = logic.GetArgument(ArgumentTypeEnum.xTo);
                        yFrom = logic.GetArgument(ArgumentTypeEnum.yFrom);
                        yTo = logic.GetArgument(ArgumentTypeEnum.yTo); ;
                    }
                    catch
                    {
                        MessageBox.Show(Translation.GetString("picGraph_MouseUp_CoordinatesException"), Translation.GetString("MessageBox_Caption_Error"), MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    double factorX = 0, factorY = 0;
                    endPoint = e.GetPosition(chartContainer);

                    // Obliczanie wspolczynnikow X
                    if (xFrom * xTo <= 0)
                        factorX = chartContainer.ActualWidth / (-xFrom + xTo);
                    else if (xFrom < 0)
                        factorX = chartContainer.ActualWidth / (-xFrom + xTo);
                    else
                        factorX = chartContainer.ActualWidth / (xTo - xFrom);

                    // Obliczanie wspolczynnikow Y
                    if (yFrom * yTo <= 0)
                        factorY = chartContainer.ActualHeight / (-yFrom + yTo);
                    else if (yFrom < 0 && yTo < 0)
                        factorY = chartContainer.ActualHeight / (-yFrom + yTo);
                    else
                        factorY = chartContainer.ActualHeight / (yTo - yFrom);

                    //Ustalenie przesuniecia
                    double differenceX = (endPoint.X - startingPoint.X) / factorX;
                    double differenceY = ((endPoint.Y - chartContainer.ActualWidth) - (startingPoint.Y - chartContainer.ActualWidth)) / factorY;

                    //Zapisanie przesuniec
                    xFrom -= differenceX;
                    xTo -= differenceX;
                    yFrom += differenceY;
                    yTo += differenceY;

                    startingPoint = e.GetPosition(chartContainer);

                    if (xFrom > logic.graphMax)
                        xFrom = logic.graphMax;
                    else if (xFrom < logic.graphMin)
                        xFrom = logic.graphMin;

                    if (xTo > logic.graphMax)
                        xTo = logic.graphMax;
                    else if (xTo < logic.graphMin)
                        xTo = logic.graphMin;

                    if (yFrom > logic.graphMax)
                        yFrom = logic.graphMax;
                    else if (yFrom < logic.graphMin)
                        yFrom = logic.graphMin;

                    if (yTo > logic.graphMax)
                        yTo = logic.graphMax;
                    else if (yTo < logic.graphMin)
                        yTo = logic.graphMin;


                    if (!chkX.IsChecked.GetValueOrDefault(false))
                    {
                        txtXFrom.Text = Convert.ToString(xFrom);
                        txtXTo.Text = Convert.ToString(xTo);
                    }

                    if (!chkY.IsChecked.GetValueOrDefault(false))
                    {
                        txtYFrom.Text = Convert.ToString(yFrom);
                        txtYTo.Text = Convert.ToString(yTo);
                    }

                    //Narysowanie nowego wykresu
                    if (logic.IsFunctionDrawn)
                        logic.DrawGraph(false);
                    else
                        logic.ClearGraph();
                }
            }
            catch (Exception ex)
            {
                //TODO: Lepsza obsluga bledow        
                MessageBox.Show(ex.Message, Translation.GetString("MessageBox_Caption_Error"), MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
