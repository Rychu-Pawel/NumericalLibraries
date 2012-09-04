using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NumericalCalculator.Translations;

namespace NumericalCalculator.Logic
{
    class MainWindowLogic
    {
        MainWindow window;

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

        public MainWindowLogic(MainWindow mw)
        {
            window = mw;

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
            //Zarzadzanie GUI procz checkboxow
            if (window.rbDifferentialII.IsChecked.GetValueOrDefault(false))
            {
                window.lblFx.Content = "f''(x) =";

                window.gbConditions.Header = Translation.GetString(window.gbConditions.Name);
                //window.gbConditions.Width = 163;

                //window.pnlWarunki.Width = 163;

                window.lblFrom.Content = "f (";
                window.lblTo.Content = ") =";

                //window.txtFrom.IsEnabled = true;
                //window.txtTo.IsEnabled = true;
                //window.txtPoint.IsEnabled = true;

                //window.txtFrom.Width = 44;
                //window.txtTo.Width = 44;

                //window.gbConditionsII.IsEnabled = true;
                window.gbConditionsII.Visibility = System.Windows.Visibility.Visible;

                //window.lblFrom.Left = 6;
                //window.lblTo.Left = 80;
                //window.txtFrom.Left = 30;
                //window.txtTo.Left = 108;
            }
            else
            {
                window.lblFx.Content = "f(x) =";

                window.lblFrom.Content = Translation.GetString(window.lblFrom.Name);
                window.lblTo.Content = Translation.GetString(window.lblTo.Name);

                window.gbConditionsII.Visibility = System.Windows.Visibility.Collapsed;

                //window.txtFrom.Width = 96;
                //window.txtTo.Width = 96;

                window.txtFrom.IsEnabled = true;

                window.gbConditions.Header = Translation.GetString(window.gbConditions.Name);
                //window.gbConditions.Width = 332;

                //window.pnlWarunki.Width = 332;

                //lblFrom.Left = 33;
                //lblTo.Left = 169;
                //txtFrom.Left = 67;
                //txtTo.Left = 203;
            }

            if (window.rbDifferential.IsChecked.GetValueOrDefault(false))
            {
                window.lblFx.Content = "f'(x) =";

                window.gbConditions.Header = Translation.GetString(window.gbConditions.Name);

                window.lblFrom.Content = "f(";
                window.lblTo.Content = ") =";

                window.txtFrom.IsEnabled = true;
                window.txtTo.IsEnabled = true;
                window.txtPoint.IsEnabled = true;

                //lblFrom.Left = 43;
                //lblTo.Left = 166;
                //txtFrom.Left = 64;
                //txtTo.Left = 194;
            }
            else if (window.rbIntegral.IsChecked.GetValueOrDefault(false))
            {
                window.lblFx.Content = "f(x) =";

                window.gbConditions.Header = Translation.GetString(window.gbConditions.Name + "_Range");

                window.lblFrom.Content = Translation.GetString(window.lblFrom.Name + "_Range");
                window.lblTo.Content = Translation.GetString(window.lblTo.Name + "_Range");

                //lblFrom.Left = 19;
                //lblTo.Left = 169;
                //txtFrom.Left = 67;
                //txtTo.Left = 218;
            }
            else if (!window.rbDifferentialII.IsChecked.GetValueOrDefault(false))
            {
                window.lblFx.Content = "f(x) =";

                window.lblFrom.Content = Translation.GetString(window.lblFrom.Name);
                window.lblTo.Content = Translation.GetString(window.lblTo.Name);

                window.gbConditions.Header = Translation.GetString(window.gbConditions.Name);

                //lblFrom.Left = 33;
                //lblTo.Left = 169;
                //txtFrom.Left = 67;
                //txtTo.Left = 203;
            }

            if (window.rbSpecialFunction.IsChecked.GetValueOrDefault(false))
            {
                window.gbFunction.Header = Translation.GetString(window.gbFunction.Name + "_SpecialFunction");

                //window.pnlFunkcja.Visibility = false;
                //window.pnlKomenda.Visibility = true;

                window.lblFx.Visibility = System.Windows.Visibility.Collapsed;
                window.txtFunction.Visibility = System.Windows.Visibility.Collapsed;
                window.btnHelp.Visibility = System.Windows.Visibility.Collapsed;

                window.cmbSpecialFunction.Visibility = System.Windows.Visibility.Visible;
                window.txtFirstCommandArgument.Visibility = System.Windows.Visibility.Visible;
                window.txtSecondCommandArgument.Visibility = System.Windows.Visibility.Visible;

                ComboSpecialChanged();

                window.txtFrom.IsEnabled = false;
                window.txtTo.IsEnabled = false;
                window.txtPoint.IsEnabled = false;
            }
            else
            {
                window.gbFunction.Header = Translation.GetString(window.gbFunction.Name);

                //window.pnlFunkcja.Visibility = true;
                //window.pnlKomenda.Visibility = false;

                window.lblFx.Visibility = System.Windows.Visibility.Visible;
                window.txtFunction.Visibility = System.Windows.Visibility.Visible;
                window.btnHelp.Visibility = System.Windows.Visibility.Visible;

                window.cmbSpecialFunction.Visibility = System.Windows.Visibility.Collapsed;
                window.txtFirstCommandArgument.Visibility = System.Windows.Visibility.Collapsed;
                window.txtSecondCommandArgument.Visibility = System.Windows.Visibility.Collapsed;
                window.txtThirdCommandArgument.Visibility = System.Windows.Visibility.Collapsed;
                window.txtFourthCommandArgument.Visibility = System.Windows.Visibility.Collapsed;

                window.gridFunction.ColumnDefinitions[3].Width = new System.Windows.GridLength(1, System.Windows.GridUnitType.Star);
                window.gridFunction.ColumnDefinitions[4].Width = new System.Windows.GridLength(1, System.Windows.GridUnitType.Star);

                window.cmbSpecialFunction.Visibility = System.Windows.Visibility.Collapsed;
            }

            if (window.rbRoot.IsChecked.GetValueOrDefault(false) || window.rbIntegral.IsChecked.GetValueOrDefault(false))
            {
                window.txtFrom.IsEnabled = true;
                window.txtTo.IsEnabled = true;
                window.txtPoint.IsEnabled = false;
            }
            else if (window.rbPoint.IsChecked.GetValueOrDefault(false) || window.rbDerivativePoint.IsChecked.GetValueOrDefault(false) || window.rbDerivativePointBis.IsChecked.GetValueOrDefault(false))
            {
                window.txtFrom.IsEnabled = false;
                window.txtTo.IsEnabled = false;
                window.txtPoint.IsEnabled = true;
            }
            else if (window.rbCalculator.IsChecked.GetValueOrDefault(false))
            {
                window.txtFrom.IsEnabled = false;
                window.txtTo.IsEnabled = false;
                window.txtPoint.IsEnabled = false;
            }

            if (window.rbCalculator.IsChecked.GetValueOrDefault(false))
            {
                window.lblFx.Content = string.Empty;

                //window.txtFunction.Width = 278;
                //window.txtFunction.Left = 9;
            }
            else if (!window.rbDifferential.IsChecked.GetValueOrDefault(false) && !window.rbDifferentialII.IsChecked.GetValueOrDefault(false))
            {
                window.lblFx.Content = "f(x) =";

                //window.txtFunction.Width = 243;
                //window.txtFunction.Left = 44;
            }
            else
            {
                //window.txtFunction.Width = 243;
                //window.txtFunction.Left = 44;
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

                //cmbSpecialFunction.Width = 126;

                //txtFirstCommandArgument.Left = 133;
                //txtFirstCommandArgument.Width = 90;

                //txtSecondCommandArgument.Left = 226;
                //txtSecondCommandArgument.Width = 90;
            }
            else if (index == 7)
            {
                window.txtThirdCommandArgument.Visibility = System.Windows.Visibility.Visible;
                window.txtFourthCommandArgument.Visibility = System.Windows.Visibility.Collapsed;

                window.gridFunction.ColumnDefinitions[3].Width = new System.Windows.GridLength(1, System.Windows.GridUnitType.Star);
                window.gridFunction.ColumnDefinitions[4].Width = new System.Windows.GridLength(1, System.Windows.GridUnitType.Auto);

                //cmbSpecialFunction.Width = 99;

                //txtFirstCommandArgument.Left = 106;
                //txtFirstCommandArgument.Width = 68;

                //txtSecondCommandArgument.Left = 177;
                //txtSecondCommandArgument.Width = 68;

                //txtThirdCommandArgument.Left = 248;
                //txtThirdCommandArgument.Width = 68;
            }
            else
            {
                window.txtThirdCommandArgument.Visibility = System.Windows.Visibility.Visible;
                window.txtFourthCommandArgument.Visibility = System.Windows.Visibility.Visible;

                window.gridFunction.ColumnDefinitions[3].Width = new System.Windows.GridLength(1, System.Windows.GridUnitType.Star);
                window.gridFunction.ColumnDefinitions[4].Width = new System.Windows.GridLength(1, System.Windows.GridUnitType.Star);

                //cmbSpecialFunction.Width = 99;

                //txtFirstCommandArgument.Left = 107;
                //txtFirstCommandArgument.Width = 50;

                //txtSecondCommandArgument.Left = 160;
                //txtSecondCommandArgument.Width = 50;

                //txtThirdCommandArgument.Left = 213;
                //txtThirdCommandArgument.Width = 50;

                //txtFourthCommandArgument.Left = 266;
                //txtFourthCommandArgument.Width = 50;
            }
        }
    }
}
