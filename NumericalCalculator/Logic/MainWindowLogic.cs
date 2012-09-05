using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NumericalCalculator.Translations;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows;
using NumericalCalculator.Exceptions;
using System.Diagnostics;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;
using System.Drawing;

namespace NumericalCalculator.Logic
{
    class MainWindowLogic
    {
        MainWindow window;
        ObjectDataProvider objectDataProvider;

        public bool IsFunctionDrawn { get; set; }

        readonly public double graphMax = 530000000.0;
        readonly public double graphMin = -530000000.0;

        PointF[] graphPoint;

        public Property Function { get; set; }
        public Property BesselFirst { get; set; }
        public Property BesselSecond { get; set; }
        public Property BesselThird { get; set; }
        public Property BesselFourth { get; set; }
        public Property Point { get; set; }
        public Property ConditionPoint { get; set; }
        public Property ConditionPointValue { get; set; }
        public Property ConditionIIPoint { get; set; }
        public Property ConditionIIPointValue { get; set; }
        public Property Result { get; set; }
        public Property Sampling { get; set; }
        public Property Cutoff { get; set; }
        public Property xFrom { get; set; }
        public Property xTo { get; set; }
        public Property yFrom { get; set; }
        public Property yTo { get; set; }

        public MainWindowLogic(MainWindow mw, ObjectDataProvider odp)
        {
            window = mw;
            objectDataProvider = odp;

            Function = new Property();
            BesselFirst = new Property();
            BesselSecond = new Property();
            BesselThird = new Property();
            BesselFourth = new Property();
            Point = new Property();
            ConditionPoint = new Property();
            ConditionPointValue = new Property();
            ConditionIIPoint = new Property();
            ConditionIIPointValue = new Property();
            Result = new Property();
            Sampling = new Property();
            Cutoff = new Property();
            xFrom = new Property();
            xTo = new Property();
            yFrom = new Property();
            yTo = new Property();

            Sampling.Value = 1000;
            Cutoff.Value = 0;

            xFrom.Value = -18.75;
            xTo.Value = 18.75;

            yFrom.Value = -16.25;
            yTo.Value = 16.25;
        }

        internal void RadioButtonChanged()
        {
            if (window.rbCalculator.IsChecked.GetValueOrDefault(false))
            {
                window.cmbSpecialFunction.Visibility = System.Windows.Visibility.Collapsed;
                window.txtFirstCommandArgument.Visibility = System.Windows.Visibility.Collapsed;
                window.txtSecondCommandArgument.Visibility = System.Windows.Visibility.Collapsed;
                window.txtThirdCommandArgument.Visibility = System.Windows.Visibility.Collapsed;
                window.txtFourthCommandArgument.Visibility = System.Windows.Visibility.Collapsed;

                window.lblFx.Visibility = System.Windows.Visibility.Collapsed;
                window.txtFunction.Visibility = System.Windows.Visibility.Visible;
                window.btnHelp.Visibility = System.Windows.Visibility.Visible;

                window.gbPoint.Visibility = System.Windows.Visibility.Collapsed;
                window.gbConditions.Visibility = System.Windows.Visibility.Collapsed;
                window.gbConditionsII.Visibility = System.Windows.Visibility.Collapsed;

                window.gbFunction.SetBinding(GroupBox.HeaderProperty, new Binding(window.gbFunction.Name) { Source = objectDataProvider });
            }
            else if (window.rbRoot.IsChecked.GetValueOrDefault(false))
            {
                window.cmbSpecialFunction.Visibility = System.Windows.Visibility.Collapsed;
                window.txtFirstCommandArgument.Visibility = System.Windows.Visibility.Collapsed;
                window.txtSecondCommandArgument.Visibility = System.Windows.Visibility.Collapsed;
                window.txtThirdCommandArgument.Visibility = System.Windows.Visibility.Collapsed;
                window.txtFourthCommandArgument.Visibility = System.Windows.Visibility.Collapsed;

                window.lblFx.Visibility = System.Windows.Visibility.Visible;
                window.txtFunction.Visibility = System.Windows.Visibility.Visible;
                window.btnHelp.Visibility = System.Windows.Visibility.Visible;

                window.gbPoint.Visibility = System.Windows.Visibility.Collapsed;
                window.gbConditions.Visibility = System.Windows.Visibility.Visible;
                window.gbConditionsII.Visibility = System.Windows.Visibility.Collapsed;

                window.lblFx.Content = "f(x) = ";

                window.lblFrom.SetBinding(Label.ContentProperty, new Binding(window.lblFrom.Name) { Source = objectDataProvider });
                window.lblTo.SetBinding(Label.ContentProperty, new Binding(window.lblTo.Name) { Source = objectDataProvider });

                window.gbConditions.SetBinding(GroupBox.HeaderProperty, new Binding(window.gbConditions.Name) { Source = objectDataProvider });
                window.gbFunction.SetBinding(GroupBox.HeaderProperty, new Binding(window.gbFunction.Name) { Source = objectDataProvider });
            }
            else if (window.rbSpecialFunction.IsChecked.GetValueOrDefault(false))
            {
                window.cmbSpecialFunction.Visibility = System.Windows.Visibility.Visible;
                window.txtFirstCommandArgument.Visibility = System.Windows.Visibility.Visible;
                window.txtSecondCommandArgument.Visibility = System.Windows.Visibility.Visible;
                window.txtThirdCommandArgument.Visibility = System.Windows.Visibility.Collapsed;
                window.txtFourthCommandArgument.Visibility = System.Windows.Visibility.Collapsed;

                ComboSpecialChanged();

                window.lblFx.Visibility = System.Windows.Visibility.Collapsed;
                window.txtFunction.Visibility = System.Windows.Visibility.Collapsed;
                window.btnHelp.Visibility = System.Windows.Visibility.Collapsed;

                window.gbPoint.Visibility = System.Windows.Visibility.Collapsed;
                window.gbConditions.Visibility = System.Windows.Visibility.Collapsed;
                window.gbConditionsII.Visibility = System.Windows.Visibility.Collapsed;

                window.gbFunction.SetBinding(GroupBox.HeaderProperty, new Binding(window.gbFunction.Name + "_SpecialFunction") { Source = objectDataProvider });
            }
            else if (window.rbDerivativePoint.IsChecked.GetValueOrDefault(false) || window.rbDerivativePointBis.IsChecked.GetValueOrDefault(false) || window.rbPoint.IsChecked.GetValueOrDefault(false))
            {
                window.cmbSpecialFunction.Visibility = System.Windows.Visibility.Collapsed;
                window.txtFirstCommandArgument.Visibility = System.Windows.Visibility.Collapsed;
                window.txtSecondCommandArgument.Visibility = System.Windows.Visibility.Collapsed;
                window.txtThirdCommandArgument.Visibility = System.Windows.Visibility.Collapsed;
                window.txtFourthCommandArgument.Visibility = System.Windows.Visibility.Collapsed;

                window.lblFx.Visibility = System.Windows.Visibility.Visible;
                window.txtFunction.Visibility = System.Windows.Visibility.Visible;
                window.btnHelp.Visibility = System.Windows.Visibility.Visible;

                window.gbPoint.Visibility = System.Windows.Visibility.Visible;
                window.gbConditions.Visibility = System.Windows.Visibility.Collapsed;
                window.gbConditionsII.Visibility = System.Windows.Visibility.Collapsed;

                window.lblFx.Content = "f(x) = ";

                window.gbFunction.SetBinding(GroupBox.HeaderProperty, new Binding(window.gbFunction.Name) { Source = objectDataProvider });
            }
            else if (window.rbIntegral.IsChecked.GetValueOrDefault(false))
            {
                window.cmbSpecialFunction.Visibility = System.Windows.Visibility.Collapsed;
                window.txtFirstCommandArgument.Visibility = System.Windows.Visibility.Collapsed;
                window.txtSecondCommandArgument.Visibility = System.Windows.Visibility.Collapsed;
                window.txtThirdCommandArgument.Visibility = System.Windows.Visibility.Collapsed;
                window.txtFourthCommandArgument.Visibility = System.Windows.Visibility.Collapsed;

                window.lblFx.Visibility = System.Windows.Visibility.Visible;
                window.txtFunction.Visibility = System.Windows.Visibility.Visible;
                window.btnHelp.Visibility = System.Windows.Visibility.Visible;

                window.gbPoint.Visibility = System.Windows.Visibility.Collapsed;
                window.gbConditions.Visibility = System.Windows.Visibility.Visible;
                window.gbConditionsII.Visibility = System.Windows.Visibility.Collapsed;

                window.lblFx.Content = "f(x) = ";

                window.lblFrom.SetBinding(Label.ContentProperty, new Binding(window.lblFrom.Name + "_Range") { Source = objectDataProvider });
                window.lblTo.SetBinding(Label.ContentProperty, new Binding(window.lblTo.Name + "_Range") { Source = objectDataProvider });

                window.gbConditions.SetBinding(GroupBox.HeaderProperty, new Binding(window.gbConditions.Name + "_Range") { Source = objectDataProvider });
                window.gbFunction.SetBinding(GroupBox.HeaderProperty, new Binding(window.gbFunction.Name) { Source = objectDataProvider });
            }
            else if (window.rbDifferential.IsChecked.GetValueOrDefault(false))
            {
                window.cmbSpecialFunction.Visibility = System.Windows.Visibility.Collapsed;
                window.txtFirstCommandArgument.Visibility = System.Windows.Visibility.Collapsed;
                window.txtSecondCommandArgument.Visibility = System.Windows.Visibility.Collapsed;
                window.txtThirdCommandArgument.Visibility = System.Windows.Visibility.Collapsed;
                window.txtFourthCommandArgument.Visibility = System.Windows.Visibility.Collapsed;

                window.lblFx.Visibility = System.Windows.Visibility.Visible;
                window.txtFunction.Visibility = System.Windows.Visibility.Visible;
                window.btnHelp.Visibility = System.Windows.Visibility.Visible;

                window.gbPoint.Visibility = System.Windows.Visibility.Visible;
                window.gbConditions.Visibility = System.Windows.Visibility.Visible;
                window.gbConditionsII.Visibility = System.Windows.Visibility.Collapsed;

                window.lblFx.Content = "f'(x) = ";

                window.lblFrom.Content = "f (";
                window.lblTo.Content = ") =";

                window.gbConditions.SetBinding(GroupBox.HeaderProperty, new Binding(window.gbConditions.Name) { Source = objectDataProvider });
                window.gbFunction.SetBinding(GroupBox.HeaderProperty, new Binding(window.gbFunction.Name) { Source = objectDataProvider });
            }
            else if (window.rbDifferentialII.IsChecked.GetValueOrDefault(false))
            {
                window.cmbSpecialFunction.Visibility = System.Windows.Visibility.Collapsed;
                window.txtFirstCommandArgument.Visibility = System.Windows.Visibility.Collapsed;
                window.txtSecondCommandArgument.Visibility = System.Windows.Visibility.Collapsed;
                window.txtThirdCommandArgument.Visibility = System.Windows.Visibility.Collapsed;
                window.txtFourthCommandArgument.Visibility = System.Windows.Visibility.Collapsed;

                window.lblFx.Visibility = System.Windows.Visibility.Visible;
                window.txtFunction.Visibility = System.Windows.Visibility.Visible;
                window.btnHelp.Visibility = System.Windows.Visibility.Visible;

                window.gbPoint.Visibility = System.Windows.Visibility.Visible;
                window.gbConditions.Visibility = System.Windows.Visibility.Visible;
                window.gbConditionsII.Visibility = System.Windows.Visibility.Visible;

                window.lblFx.Content = "f''(x) = ";

                window.lblFrom.Content = "f (";
                window.lblTo.Content = ") =";

                window.gbConditions.SetBinding(GroupBox.HeaderProperty, new Binding(window.gbConditions.Name) { Source = objectDataProvider });
                window.gbFunction.SetBinding(GroupBox.HeaderProperty, new Binding(window.gbFunction.Name) { Source = objectDataProvider });
            }

            //Checkboxy
            if (window.rbDifferential.IsChecked.GetValueOrDefault(false))
            {
                window.chkFunction.IsEnabled = false;
                window.chkFirstDerivative.IsEnabled = false;
                window.chkSecondDerivative.IsEnabled = false;
                window.chkDifferential.IsEnabled = true;
                window.chkDifferentialII.IsEnabled = false;
                window.chkSpecialFunction.IsEnabled = false;
                window.chkFT.IsEnabled = false;
                window.chkIFT.IsEnabled = false;

                window.txtSampling.IsEnabled = false;
                window.txtCutoff.IsEnabled = false;

                window.lblSampling.IsEnabled = false;
                window.lblCutoff.IsEnabled = false;

                window.chkRescaling.IsEnabled = false;
            }
            else if (window.rbDifferentialII.IsChecked.GetValueOrDefault(false))
            {
                window.chkFunction.IsEnabled = false;
                window.chkFirstDerivative.IsEnabled = false;
                window.chkSecondDerivative.IsEnabled = false;
                window.chkDifferential.IsEnabled = false;
                window.chkDifferentialII.IsEnabled = true;
                window.chkSpecialFunction.IsEnabled = false;
                window.chkFT.IsEnabled = false;
                window.chkIFT.IsEnabled = false;

                window.txtSampling.IsEnabled = false;
                window.txtCutoff.IsEnabled = false;

                window.lblSampling.IsEnabled = false;
                window.lblCutoff.IsEnabled = false;

                window.chkRescaling.IsEnabled = false;
            }
            else if (window.rbSpecialFunction.IsChecked.GetValueOrDefault(false))
            {
                window.chkFunction.IsEnabled = false;
                window.chkFirstDerivative.IsEnabled = false;
                window.chkSecondDerivative.IsEnabled = false;
                window.chkDifferential.IsEnabled = false;
                window.chkDifferentialII.IsEnabled = false;
                window.chkSpecialFunction.IsEnabled = true;
                window.chkFT.IsEnabled = false;
                window.chkIFT.IsEnabled = false;

                window.txtSampling.IsEnabled = false;
                window.txtCutoff.IsEnabled = false;

                window.lblSampling.IsEnabled = false;
                window.lblCutoff.IsEnabled = false;

                window.chkRescaling.IsEnabled = true;
            }
            else
            {
                window.chkFunction.IsEnabled = true;
                window.chkFirstDerivative.IsEnabled = true;
                window.chkSecondDerivative.IsEnabled = true;
                window.chkDifferential.IsEnabled = false;
                window.chkDifferentialII.IsEnabled = false;
                window.chkSpecialFunction.IsEnabled = false;
                window.chkFT.IsEnabled = true;
                window.chkIFT.IsEnabled = true;

                window.txtSampling.IsEnabled = true;
                window.txtCutoff.IsEnabled = true;

                window.lblSampling.IsEnabled = true;
                window.lblCutoff.IsEnabled = true;

                window.chkRescaling.IsEnabled = true;
            }

            //Wyłączenie reskallingu gdy FFT
            if ((window.chkFT.IsChecked.GetValueOrDefault(false) && window.chkFT.IsEnabled) || (window.chkIFT.IsChecked.GetValueOrDefault(false) && window.chkIFT.IsEnabled))
                window.chkRescaling.IsEnabled = false;

            //Jak FFT to nie RFFT i na odwrot
            if (window.chkFT.IsChecked.GetValueOrDefault(false) && window.chkFT.IsEnabled)
            {
                window.chkFunction.IsEnabled = false;
                window.chkFirstDerivative.IsEnabled = false;
                window.chkSecondDerivative.IsEnabled = false;
                window.chkIFT.IsEnabled = false;
            }
        }

        public void ComboSpecialChanged()
        {
            int index = window.cmbSpecialFunction.SelectedIndex;

            if (index < 7)
            {
                window.txtThirdCommandArgument.Visibility = System.Windows.Visibility.Collapsed;
                window.txtFourthCommandArgument.Visibility = System.Windows.Visibility.Collapsed;

                window.gridFunction.ColumnDefinitions[3].Width = new System.Windows.GridLength(1, System.Windows.GridUnitType.Auto);
                window.gridFunction.ColumnDefinitions[4].Width = new System.Windows.GridLength(1, System.Windows.GridUnitType.Auto);
            }
            else if (index == 7)
            {
                window.txtThirdCommandArgument.Visibility = System.Windows.Visibility.Visible;
                window.txtFourthCommandArgument.Visibility = System.Windows.Visibility.Collapsed;

                window.gridFunction.ColumnDefinitions[3].Width = new System.Windows.GridLength(1, System.Windows.GridUnitType.Star);
                window.gridFunction.ColumnDefinitions[4].Width = new System.Windows.GridLength(1, System.Windows.GridUnitType.Auto);
            }
            else
            {
                window.txtThirdCommandArgument.Visibility = System.Windows.Visibility.Visible;
                window.txtFourthCommandArgument.Visibility = System.Windows.Visibility.Visible;

                window.gridFunction.ColumnDefinitions[3].Width = new System.Windows.GridLength(1, System.Windows.GridUnitType.Star);
                window.gridFunction.ColumnDefinitions[4].Width = new System.Windows.GridLength(1, System.Windows.GridUnitType.Star);

            }
        }

        public double GetArgument(ArgumentTypeEnum argumentType)
        {
            //Pobranie argumentu jako string
            Property argument = null;

            switch (argumentType)
            {
                case ArgumentTypeEnum.Point:
                    argument = Point;
                    break;
                case ArgumentTypeEnum.From:
                    argument = ConditionPoint;
                    break;
                case ArgumentTypeEnum.To:
                    argument = ConditionPointValue;
                    break;
                case ArgumentTypeEnum.FromII:
                    argument = ConditionIIPoint;
                    break;
                case ArgumentTypeEnum.ToII:
                    argument = ConditionIIPointValue;
                    break;
                case ArgumentTypeEnum.BesselFirst:
                    argument = BesselFirst;
                    break;
                case ArgumentTypeEnum.BesselSecond:
                    argument = BesselSecond;
                    break;
                case ArgumentTypeEnum.BesselThird:
                    argument = BesselThird;
                    break;
                case ArgumentTypeEnum.BesselFourth:
                    argument = BesselFourth;
                    break;
                case ArgumentTypeEnum.Sampling:
                    argument = Sampling;
                    break;
                case ArgumentTypeEnum.Cutoff:
                    argument = Cutoff;
                    break;
                case ArgumentTypeEnum.xFrom:
                    argument = xFrom;
                    break;
                case ArgumentTypeEnum.xTo:
                    argument = xTo;
                    break;
                case ArgumentTypeEnum.yFrom:
                    argument = yFrom;
                    break;
                case ArgumentTypeEnum.yTo:
                    argument = yTo;
                    break;
                default:
                    break;
            }

            //Konwersja na double
            if (!double.IsNaN(argument))
                return argument;
            else
            {
                //Sprawdzenie może da się oszacować
                try
                {
                    Calculator calculator = new Calculator(argument.Text.Replace("E", Math.E.ToString()));
                    double result = calculator.Compute();

                    if (argument.Text.Contains('E'))
                        MessageBox.Show(Translation.GetString("MessageBox_EulerWarning"), Translation.GetString("MessageBox_Caption_Warning"), MessageBoxButton.OK, MessageBoxImage.Information);

                    return result;
                }
                catch
                {
                    switch (argumentType)
                    {
                        case ArgumentTypeEnum.Point:
                            throw new PointConversionException();
                        case ArgumentTypeEnum.From:
                            throw new FromConversionException();
                        case ArgumentTypeEnum.To:
                            throw new ToConversionException();
                        case ArgumentTypeEnum.FromII:
                            throw new FromIIConversionException();
                        case ArgumentTypeEnum.ToII:
                            throw new ToIIConversionException();
                        case ArgumentTypeEnum.BesselFirst:
                            throw new BesselFirstArgumentException();
                        case ArgumentTypeEnum.BesselSecond:
                            throw new BesseleSecondArgumentException();
                        case ArgumentTypeEnum.BesselThird:
                            throw new BesseleThirdArgumentException();
                        case ArgumentTypeEnum.BesselFourth:
                            throw new BesseleFourthArgumentException();
                        case ArgumentTypeEnum.Sampling:
                            throw new SamplingValueException();
                        case ArgumentTypeEnum.Cutoff:
                            throw new CutoffValueException();
                        case ArgumentTypeEnum.xFrom:
                            throw new xFromException();
                        case ArgumentTypeEnum.xTo:
                            throw new xToException();
                        case ArgumentTypeEnum.yFrom:
                            throw new yFromException();
                        case ArgumentTypeEnum.yTo:
                            throw new yToException();
                        default:
                            throw new Exception();
                    }
                }
            }
        }

        private void HandleException(Stopwatch stopWatch, string message)
        {
            stopWatch.Stop();
            window.lblTime.Content = stopWatch.Elapsed.ToString().Substring(3, 13);
            Result.Text = "";

            MessageBox.Show(message, Translation.GetString("MessageBox_Caption_Error"), MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void HandleException(string message)
        {
            Result.Text = "";

            MessageBox.Show(message, Translation.GetString("MessageBox_Caption_Error"), MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void HandleGraphException(string message)
        {
            Result.Text = "";
            IsFunctionDrawn = false;

            MessageBox.Show(message, Translation.GetString("MessageBox_Caption_Error"), MessageBoxButton.OK, MessageBoxImage.Error);

            try
            {
                ClearGraph();
            }
            catch { }
        }

        public void ClearGraph()
        {
            try
            {
                Chart graph = GetChart();
                graph.Clear();

                window.picGraph.Source = GetChartImage(graph);
            }
            catch (Exception excep)
            {
                MessageBox.Show(Translation.GetString("MessageBox_ClearGraph_Exception") + System.Environment.NewLine + excep.Message, Translation.GetString("MessageBox_Caption_Error"), MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public Chart GetChart()
        {
            return new Chart(Function.TextWithReplacedCommasAndDots, (int)window.chartContainer.ActualWidth, (int)window.chartContainer.ActualHeight, GetArgument(ArgumentTypeEnum.xFrom), GetArgument(ArgumentTypeEnum.xTo), GetArgument(ArgumentTypeEnum.yFrom), GetArgument(ArgumentTypeEnum.yTo));
        }

        public BitmapImage GetChartImage(Chart chart)
        {
            // ImageSource ...
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();

            MemoryStream ms = new MemoryStream();

            // Save to a memory stream...
            chart.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);

            // Rewind the stream...
            ms.Seek(0, SeekOrigin.Begin);

            // Tell the WPF image to use this stream...
            bi.StreamSource = ms;
            bi.EndInit();

            return bi;
        }

        public void Compute()
        {
            Stopwatch stopWatch = new Stopwatch();

            try
            {
                //Pobranie argumentów i funkcji
                double point = 0.0d, from = 0.0d, to = 0.0d, fromII = 0.0d, toII = 0.0d;
                double firstBesselArgument = 0.0d, secondBesselArgument = 0.0d, thirdBesselArgument = 0.0d, fourthBesselArgument = 0.0d;
                string function = string.Empty;

                if (window.rbPoint.IsChecked.GetValueOrDefault(false) || window.rbDerivativePoint.IsChecked.GetValueOrDefault(false) || window.rbDerivativePointBis.IsChecked.GetValueOrDefault(false) || window.rbDifferential.IsChecked.GetValueOrDefault(false) || window.rbDifferentialII.IsChecked.GetValueOrDefault(false))
                {
                    //Pobranie punktu
                    point = GetArgument(ArgumentTypeEnum.Point);
                }

                if (window.rbRoot.IsChecked.GetValueOrDefault(false) || window.rbIntegral.IsChecked.GetValueOrDefault(false) || window.rbDifferential.IsChecked.GetValueOrDefault(false) || window.rbDifferentialII.IsChecked.GetValueOrDefault(false))
                {
                    //Pobranie from i to
                    from = GetArgument(ArgumentTypeEnum.From);
                    to = GetArgument(ArgumentTypeEnum.To);
                }

                if (window.rbDifferentialII.IsChecked.GetValueOrDefault(false))
                {
                    //Pobranie fromII i toII
                    fromII = GetArgument(ArgumentTypeEnum.FromII);
                    toII = GetArgument(ArgumentTypeEnum.ToII);
                }

                if (window.rbSpecialFunction.IsChecked.GetValueOrDefault(false))
                {
                    //Pobranie argumentow besselowych
                    firstBesselArgument = GetArgument(ArgumentTypeEnum.BesselFirst);
                    secondBesselArgument = GetArgument(ArgumentTypeEnum.BesselSecond);

                    if (window.cmbSpecialFunction.SelectedIndex == 7 || window.cmbSpecialFunction.SelectedIndex == 8)
                        thirdBesselArgument = GetArgument(ArgumentTypeEnum.BesselThird);

                    if (window.cmbSpecialFunction.SelectedIndex == 8)
                        fourthBesselArgument = GetArgument(ArgumentTypeEnum.BesselFourth);
                }
                else
                {
                    //Pobranie funkcji
                    function = Function.TextWithReplacedCommasAndDots;

                    if (string.IsNullOrEmpty(function))
                        throw new FunctionNullReferenceException();
                }

                //Obliczenia
                stopWatch.Start();

                if (window.rbCalculator.IsChecked.GetValueOrDefault(false))
                {
                    Calculator calculator = new Calculator(function);
                    Result.Text = calculator.Compute().ToString();
                }
                else if (window.rbPoint.IsChecked.GetValueOrDefault(false))
                {
                    Derivative derivative = new Derivative(function, point);
                    Result.Text = derivative.ComputeFunctionAtPoint().ToString();
                }
                else if (window.rbDerivativePoint.IsChecked.GetValueOrDefault(false))
                {
                    Derivative derivative = new Derivative(function, point);
                    Result.Text = derivative.ComputeDerivative().ToString();
                }
                else if (window.rbDerivativePointBis.IsChecked.GetValueOrDefault(false))
                {
                    Derivative derivativeBis = new Derivative(function, point);
                    Result.Text = derivativeBis.ComputeDerivativeBis().ToString();
                }
                else if (window.rbRoot.IsChecked.GetValueOrDefault(false))
                {
                    Hybrid hybrid = new Hybrid(function, from, to);
                    Result.Text = hybrid.ComputeHybrid().ToString();
                }
                else if (window.rbIntegral.IsChecked.GetValueOrDefault(false))
                {
                    Integral integral = new Integral(function, from, to);
                    Result.Text = integral.ComputeIntegral().ToString();
                }
                else if (window.rbDifferential.IsChecked.GetValueOrDefault(false))
                {
                    Differential differential = new Differential(function);
                    Result.Text = differential.ComputeDifferential(point, from, to).ToString();
                }
                else if (window.rbDifferentialII.IsChecked.GetValueOrDefault(false))
                {
                    Differential differential = new Differential(function);
                    Result.Text = differential.ComputeDifferentialII(point, from, to, toII).ToString();
                }
                else if (window.rbSpecialFunction.IsChecked.GetValueOrDefault(false))
                {
                    BesselNeumanHyper bessel = new BesselNeumanHyper();

                    double result = 0.0d;

                    switch (window.cmbSpecialFunction.SelectedIndex)
                    {
                        case 0:
                            result = bessel.Bessel(firstBesselArgument, secondBesselArgument);
                            break;
                        case 1:
                            result = bessel.SphBessel(firstBesselArgument, secondBesselArgument);
                            break;
                        case 2:
                            result = bessel.SphBesselPrim(firstBesselArgument, secondBesselArgument);
                            break;
                        case 3:
                            result = bessel.Neumann(firstBesselArgument, secondBesselArgument);
                            break;
                        case 4:
                            result = bessel.SphNeuman(firstBesselArgument, secondBesselArgument);
                            break;
                        case 5:
                            result = bessel.SphNeumanPrim(firstBesselArgument, secondBesselArgument);
                            break;
                        case 6:
                            result = bessel.Hyperg_0F_1(firstBesselArgument, secondBesselArgument);
                            break;
                        case 7:
                            result = bessel.Hyperg_1F_1(firstBesselArgument, secondBesselArgument, thirdBesselArgument);
                            break;
                        case 8:
                            result = bessel.Hyperg_2F_1(firstBesselArgument, secondBesselArgument, thirdBesselArgument, fourthBesselArgument);
                            break;
                        default:
                            break;
                    }

                    Result.Text = result.ToString();
                }

                stopWatch.Stop();
                window.lblTime.Content = stopWatch.Elapsed.ToString().Substring(3, 13);
            }
            catch (Exception excep)
            {
                //Pobranie typu wyjatku
                string type = excep.GetType().Name;

                //Obsluga wyjatkow specyficznych
                if (type == "VariableFoundException")
                {
                    //Ten wyjatek ma swoja specyficzna obsluge - uruchamia rysowanie wykresu
                    stopWatch.Stop();
                    window.lblTime.Content = stopWatch.Elapsed.ToString().Substring(3, 13);

                    DrawGraph();

                    Result.Text = Translation.GetString("VariableFoundException");
                }
                else if (type == "ToConversionException")
                {
                    if (window.rbIntegral.IsChecked.GetValueOrDefault(false))
                        HandleException(stopWatch, Translation.GetString("ToConversionException_Integral"));
                    else if (window.rbDifferential.IsChecked.GetValueOrDefault(false) || window.rbDifferentialII.IsChecked.GetValueOrDefault(false))
                        HandleException(stopWatch, Translation.GetString("ToConversionException_Differential"));
                    else
                        HandleException(stopWatch, Translation.GetString("ToConversionException"));
                }
                else if (type == "FromConversionException")
                {
                    if (window.rbIntegral.IsChecked.GetValueOrDefault(false))
                        HandleException(stopWatch, Translation.GetString("FromConversionException_Integral"));
                    else if (window.rbDifferential.IsChecked.GetValueOrDefault(false) || window.rbDifferentialII.IsChecked.GetValueOrDefault(false))
                        HandleException(stopWatch, Translation.GetString("FromConversionException_Differential"));
                    else
                        HandleException(stopWatch, Translation.GetString("FromConversionException"));
                }
                else if (type == "FunctionNullReferenceException")
                {
                    if (window.rbCalculator.IsChecked.GetValueOrDefault(false))
                        HandleException(stopWatch, Translation.GetString("FunctionNullReferenceException_Calculator"));
                    else
                        HandleException(stopWatch, Translation.GetString("FunctionNullReferenceException"));
                }
                else if (type == "OperatorAtTheBeginningOfTheExpressionException")
                {
                    HandleException(stopWatch, "Operator " + (excep as OperatorAtTheBeginningOfTheExpressionException).Operator + " " + Translation.GetString(type));
                }
                else if (type == "OperatorAtTheEndOfTheExpressionException")
                {
                    HandleException(stopWatch, "Operator " + (excep as OperatorAtTheEndOfTheExpressionException).Operator + " " + Translation.GetString(type));
                }
                else if (type == "ForbiddenSignDetectedException")
                {
                    HandleException(stopWatch, Translation.GetString(type) + " " + (excep as ForbiddenSignDetectedException).Sign);
                }
                else if (type == "FunctionException")
                    HandleException(stopWatch, excep.Message);
                else
                    HandleException(stopWatch, Translation.GetString(type));

                //Ustawianie focusu na konkretym polu w zaleznosci od wyjatku
                switch (type)
                {
                    case "PointConversionException": window.txtPoint.Focus(); break;
                    case "IntegralInfinityRangeNotSupportedException":
                    case "FunctionException":
                    case "VariableFoundException":
                    case "NoneOrFewRootsOnGivenIntervalException":
                    case "NaNOccuredException":
                    case "IncorrectEOperatorOccurrenceException":
                    case "OperatorAtTheBeginningOfTheExpressionException":
                    case "OperatorAtTheEndOfTheExpressionException":
                    case "TwoOperatorsOccurredSideBySideException":
                    case "TwoFactorialsOccuredSideBySideException":
                    case "EmptyFunctionStringException":
                    case "LeftAndRightBracketsAmountDoesNotMatchException":
                    case "FunctionNullReferenceException": window.txtFunction.Focus(); break;
                    case "FromConversionException": window.txtFrom.Focus(); break;
                    case "FromIIConversionException": window.txtFromII.Focus(); break;
                    case "ToConversionException": window.txtTo.Focus(); break;
                    case "ToIIConversionException": window.txtToII.Focus(); break;
                    case "BesselFirstArgumentException": window.txtFirstCommandArgument.Focus(); break;
                    case "BesseleSecondArgumentException": window.txtSecondCommandArgument.Focus(); break;
                    case "BesseleThirdArgumentException": window.txtThirdCommandArgument.Focus(); break;
                    case "BesseleFourthArgumentException": window.txtFourthCommandArgument.Focus(); break;
                    default:
                        break;
                }
            }
        }

        public void DrawGraph(bool rescaling = true)
        {
            try
            {
                //Sprawdzenie czy jakas opcja wykresu jest zacheckowana
                if (window.rbSpecialFunction.IsChecked.GetValueOrDefault(false))
                {
                    if (!window.chkSpecialFunction.IsChecked.GetValueOrDefault(false))
                        throw new NoneGraphOptionCheckedException();
                }
                else if (window.rbDifferential.IsChecked.GetValueOrDefault(false))
                {
                    if (!window.chkDifferential.IsChecked.GetValueOrDefault(false))
                        throw new NoneGraphOptionCheckedException();
                }
                else if (window.rbDifferentialII.IsChecked.GetValueOrDefault(false))
                {
                    if (!window.chkDifferentialII.IsChecked.GetValueOrDefault(false))
                        throw new NoneGraphOptionCheckedException();
                }
                else
                {
                    if (!(window.chkFunction.IsChecked.GetValueOrDefault(false) || window.chkFirstDerivative.IsChecked.GetValueOrDefault(false) || window.chkSecondDerivative.IsChecked.GetValueOrDefault(false) || window.chkDifferential.IsChecked.GetValueOrDefault(false) || window.chkDifferentialII.IsChecked.GetValueOrDefault(false) || window.chkFT.IsChecked.GetValueOrDefault(false) || window.chkIFT.IsChecked.GetValueOrDefault(false)))
                        throw new NoneGraphOptionCheckedException();
                }

                //Konwersja zmiennych
                string function = Function.TextWithReplacedCommasAndDots;

                if (string.IsNullOrEmpty(function) && !window.chkSpecialFunction.IsEnabled)
                {
                    throw new FunctionNullReferenceException();
                }

                double xFrom, xTo, yFrom, yTo;

                xFrom = GetArgument(ArgumentTypeEnum.xFrom);
                xTo = GetArgument(ArgumentTypeEnum.xTo);
                yFrom = GetArgument(ArgumentTypeEnum.yFrom);
                yTo = GetArgument(ArgumentTypeEnum.yTo);

                //Sprawdzenie czy zakres jest OK
                if (xFrom >= xTo)
                {
                    throw new XFromIsGreaterThenXToException();
                }

                if (yFrom >= yTo)
                {
                    throw new YFromIsGreaterThenYToException();
                }

                //Pobranie Besselowych wartosci i znalezienie typu besselowego
                double first = 0.0, second = 0.0, thrid = 0.0, fourth = 0.0;
                BesselFunctionTypeEnum bft = BesselFunctionTypeEnum.Bessel;

                if (window.chkSpecialFunction.IsChecked.GetValueOrDefault(false) && window.chkSpecialFunction.IsEnabled)
                {

                    if (BesselFirst.Text == "x")
                        first = double.NaN;
                    else
                        first = GetArgument(ArgumentTypeEnum.BesselFirst);

                    if (BesselSecond.Text == "x")
                        second = double.NaN;
                    else
                        second = GetArgument(ArgumentTypeEnum.BesselSecond);

                    if (window.cmbSpecialFunction.SelectedIndex == 7 || window.cmbSpecialFunction.SelectedIndex == 8)
                    {
                        if (BesselThird.Text == "x")
                            thrid = double.NaN;
                        else
                            thrid = GetArgument(ArgumentTypeEnum.BesselThird);
                    }

                    if (window.cmbSpecialFunction.SelectedIndex == 8)
                    {
                        if (BesselFourth.Text == "x")
                            fourth = double.NaN;
                        else
                            fourth = GetArgument(ArgumentTypeEnum.BesselFourth);
                    }

                    //znalezienie typu besselowego
                    switch (window.cmbSpecialFunction.SelectedIndex)
                    {
                        case 0:
                            bft = BesselFunctionTypeEnum.Bessel;
                            break;
                        case 1:
                            bft = BesselFunctionTypeEnum.BesselSphere;
                            break;
                        case 2:
                            bft = BesselFunctionTypeEnum.BesselSphereDerivative;
                            break;
                        case 3:
                            bft = BesselFunctionTypeEnum.Neumann;
                            break;
                        case 4:
                            bft = BesselFunctionTypeEnum.NeumannSphere;
                            break;
                        case 5:
                            bft = BesselFunctionTypeEnum.NeumannSphereDerivative;
                            break;
                        case 6:
                            bft = BesselFunctionTypeEnum.Hypergeometric01;
                            break;
                        case 7:
                            bft = BesselFunctionTypeEnum.Hypergeometric11;
                            break;
                        case 8:
                            bft = BesselFunctionTypeEnum.Hypergeometric21;
                            break;
                        default:
                            break;
                    }
                }

                //Utworzenie klasy
                Chart chart = GetChart();

                //Reskalling
                if (window.chkRescaling.IsChecked.GetValueOrDefault(false) && window.chkRescaling.IsEnabled && rescaling)
                {
                    //Zbudowanie listy typow funkcji
                    List<FunctionTypeEnum> functionType = new List<FunctionTypeEnum>();

                    if (window.chkFunction.IsChecked.GetValueOrDefault(false))
                        functionType.Add(FunctionTypeEnum.Function);

                    if (window.chkFirstDerivative.IsChecked.GetValueOrDefault(false))
                        functionType.Add(FunctionTypeEnum.Derivative);

                    if (window.chkSecondDerivative.IsChecked.GetValueOrDefault(false))
                        functionType.Add(FunctionTypeEnum.SecondDerivative);

                    double[] reskalling = null;

                    //Obliczenie maxów i minów do reskalingu
                    if (window.chkSpecialFunction.IsChecked.GetValueOrDefault(false) && window.chkSpecialFunction.IsEnabled)
                        reskalling = chart.Reskalling(bft, first, second, thrid, fourth); //bessele
                    else
                        reskalling = chart.Reskalling(functionType.ToArray()); //normlanych

                    xFrom = reskalling[0];
                    xTo = reskalling[1];
                    yFrom = reskalling[2];
                    yTo = reskalling[3];

                    this.xFrom.Text = xFrom.ToString();
                    this.xTo.Text = xTo.ToString();
                    this.yFrom.Text = yFrom.ToString();
                    this.yTo.Text = yTo.ToString();

                    chart = GetChart();
                }

                //Rysowanie funkcji i pochodnych
                if (window.chkFunction.IsChecked.GetValueOrDefault(false) && window.chkFunction.IsEnabled)
                    chart.Draw(FunctionTypeEnum.Function);

                if (window.chkFirstDerivative.IsChecked.GetValueOrDefault(false) && window.chkFirstDerivative.IsEnabled)
                    chart.Draw(FunctionTypeEnum.Derivative);

                if (window.chkSecondDerivative.IsChecked.GetValueOrDefault(false) && window.chkSecondDerivative.IsEnabled)
                    chart.Draw(FunctionTypeEnum.SecondDerivative);

                //Rysowanie FT
                if ((window.chkFT.IsChecked.GetValueOrDefault(false) && window.chkFT.IsEnabled) || (window.chkIFT.IsChecked.GetValueOrDefault(false) && window.chkIFT.IsEnabled))
                {
                    int sampling = 1000;
                    double cutoff = 0.0;

                    sampling = (int)Math.Round(GetArgument(ArgumentTypeEnum.Sampling), 0);
                    cutoff = GetArgument(ArgumentTypeEnum.Cutoff);

                    if (window.chkFT.IsChecked.GetValueOrDefault(false))
                        chart.DrawFT(FunctionTypeEnum.FT, sampling, cutoff);

                    if (window.chkIFT.IsChecked.GetValueOrDefault(false))
                        chart.DrawFT(FunctionTypeEnum.IFT, sampling, cutoff);
                }

                //Rysowanie rozniczek
                if (window.chkDifferential.IsChecked.GetValueOrDefault(false) && window.chkDifferential.IsEnabled)
                {
                    double from, to;

                    from = GetArgument(ArgumentTypeEnum.From);
                    to = GetArgument(ArgumentTypeEnum.To);

                    chart.DrawDifferential(FunctionTypeEnum.Differential, from, to);
                }

                if (window.chkDifferentialII.IsChecked.GetValueOrDefault(false) && window.chkDifferentialII.IsEnabled)
                {
                    double from, to, fromII, toII;

                    from = GetArgument(ArgumentTypeEnum.From);
                    to = GetArgument(ArgumentTypeEnum.To);
                    fromII = GetArgument(ArgumentTypeEnum.FromII);
                    toII = GetArgument(ArgumentTypeEnum.ToII);

                    chart.DrawDifferential(FunctionTypeEnum.DifferentialII, from, to, toII);
                }

                //Rysowanie Bessela
                if (window.chkSpecialFunction.IsChecked.GetValueOrDefault(false) && window.chkSpecialFunction.IsEnabled)
                    chart.DrawBessel(bft, first, second, thrid, fourth);

                //Zakończenie
                IsFunctionDrawn = true;

                window.picGraph.Source = GetChartImage(chart);

                //Pobranie punktow wykresu
                graphPoint = chart.GraphPoints;
            }
            catch (Exception excep)
            {
                string type = excep.GetType().Name;

                if (type == "OperatorAtTheBeginningOfTheExpressionException")
                {
                    HandleGraphException("Operator " + (excep as OperatorAtTheBeginningOfTheExpressionException).Operator + " " + Translation.GetString(type));
                }
                else if (type == "OperatorAtTheEndOfTheExpressionException")
                {
                    HandleGraphException("Operator " + (excep as OperatorAtTheEndOfTheExpressionException).Operator + " " + Translation.GetString(type));
                }
                else if (type == "ForbiddenSignDetectedException")
                {
                    HandleGraphException(Translation.GetString(type) + " " + (excep as ForbiddenSignDetectedException).Sign);
                }

                HandleGraphException(Translation.GetString(type));

                switch (type)
                {
                    case "XFromIsGreaterThenXToException": window.txtXFrom.Focus(); break;
                    case "YFromIsGreaterThenYToException": window.txtYFrom.Focus(); break;
                    case "xFromException": window.txtXFrom.Focus(); break;
                    case "xToException": window.txtXTo.Focus(); break;
                    case "yFromException": window.txtYFrom.Focus(); break;
                    case "yToException": window.txtYTo.Focus(); break;
                    case "CoordinatesXException": window.txtXFrom.Focus(); break;
                    case "CoordinatesYException": window.txtYFrom.Focus(); break;
                    case "NaNOccuredException":
                    case "IncorrectEOperatorOccurrenceException":
                    case "OperatorAtTheBeginningOfTheExpressionException":
                    case "OperatorAtTheEndOfTheExpressionException":
                    case "TwoOperatorsOccurredSideBySideException":
                    case "TwoFactorialsOccuredSideBySideException":
                    case "EmptyFunctionStringException":
                    case "LeftAndRightBracketsAmountDoesNotMatchException":
                    case "FunctionNullReferenceException": window.txtFunction.Focus(); break;
                    case "FromConversionException": window.txtFrom.Focus(); break;
                    case "FromIIConversionException": window.txtFromII.Focus(); break;
                    case "ToConversionException": window.txtTo.Focus(); break;
                    case "ToIIConversionException": window.txtToII.Focus(); break;
                    case "BesselFirstArgumentException": window.txtFirstCommandArgument.Focus(); break;
                    case "BesseleSecondArgumentException": window.txtSecondCommandArgument.Focus(); break;
                    case "BesseleThirdArgumentException": window.txtThirdCommandArgument.Focus(); break;
                    case "BesseleFourthArgumentException": window.txtFourthCommandArgument.Focus(); break;
                    case "CutoffValueException": window.txtCutoff.Focus(); break;
                    case "SamplingValueException": window.txtSampling.Focus(); break;
                    default:
                        break;
                }
            }
        }
    }
}
