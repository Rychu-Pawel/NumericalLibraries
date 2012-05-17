using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
using NumericalCalculator.Properties;
using System.Resources;
using System.Reflection;
using System.Globalization;

namespace NumericalCalculator
{
    public partial class Form1 : LanguageForm
    {
        bool IsFunctionDrawn = false;

        readonly double graphMax = 530000000.0;
        readonly double graphMin = -530000000.0;

        PointF[] graphPoint;

        public Form1()
        {
            try
            {
                InitializeComponent();

                //Sprawdzenie czy system przyjmuje kropkę czy przecinek
                string test = "1.1";
                double testDouble;

                if (double.TryParse(test, out testDouble))
                {
                    changeFrom = ",";
                    changeTo = ".";
                }
                else
                {
                    changeFrom = ".";
                    changeTo = ",";
                }

                cmbSpecialFunction.SelectedIndex = 0;

                //Ustawienia
                settings = new Settings();

                try
                {
                    LoadLanguage();
                    SetSettings();
                    Language.TranslateControl(this, language);
                    Language.TranslateControl(contextMenuStrip, language);
                }
                catch (SettingNullReferenceException)
                {
                    try
                    {
                        settings.RestoreDefaults();

                        LoadLanguage();
                        SetSettings();
                        Language.TranslateControl(this, language);
                        Language.TranslateControl(contextMenuStrip, language);
                    }
                    catch
                    {
                        MessageBox.Show(language.GetString("MessageBox_InstallationCorrupted"), language.GetString("MessageBox_InstallationCorrupted_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                //Dodatkowe zdarzenia
                this.graphToolStripMenuItem.Click += new System.EventHandler(this.ChangeSettings);
                this.btnHelp.Click += new EventHandler(ShowFunctionForm_Handler);
                this.chkFT.CheckedChanged += new EventHandler(radioButton_CheckedChanged);
                this.chkIFT.CheckedChanged += new EventHandler(radioButton_CheckedChanged);

                //Zrobienie gui
                SetRadioButtons();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void LoadLanguage()
        {
            if ((LanguageEnum)settings[SettingEnum.Language] == LanguageEnum.Polish)
                language = new ResourceManager("NumericalCalculator.Translations.LanguagePolish", GetType().Assembly);
            else
                language = new ResourceManager("NumericalCalculator.Translations.LanguageEnglish", GetType().Assembly);
        }

        private void SetSettings()
        {
            graphToolStripMenuItem.Checked = (bool)settings[SettingEnum.GraphMenuChecked];

            if (!graphToolStripMenuItem.Checked)
                graphToolStripMenuItem_Click(null, new EventArgs());

            graphPreviewWhileWindowsScalingToolStripMenuItem.Checked = (bool)settings[SettingEnum.GraphPreviewMenuChecked];
            chkFunction.Checked = (bool)settings[SettingEnum.FunctionChecked];
            chkFirstDerivative.Checked = (bool)settings[SettingEnum.FirstDerativeChecked];
            chkSecondDerivative.Checked = (bool)settings[SettingEnum.SecondDerativeChecked];
            chkDifferential.Checked = (bool)settings[SettingEnum.DifferentialChecked];
            chkDifferentialII.Checked = (bool)settings[SettingEnum.DifferentialIIChecked];
            chkSpecialFunction.Checked = (bool)settings[SettingEnum.SpecialFunctionChecked];
            chkRescaling.Checked = (bool)settings[SettingEnum.AutomaticRescallingChecked];
            chkFT.Checked = (bool)settings[SettingEnum.FourierTransformChecked];
            chkIFT.Checked = (bool)settings[SettingEnum.InverseFourierTransformChecked];

            if ((LanguageEnum)settings[SettingEnum.Language] == LanguageEnum.English)
                englishToolStripMenuItem.Checked = true;
            else
                polskiToolStripMenuItem.Checked = true;
        }

        /// <summary>
        /// USTAWIANIE RADIO BUTONOW
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void SetRadioButtons()
        {
            //Zarzadzanie GUI procz checkboxow
            if (rbDifferentialII.Checked)
            {
                lblFx.Text = "f''(x) =";

                gbConditions.Text = language.GetString(gbConditions.Name);
                gbConditions.Width = 163;

                pnlWarunki.Width = 163;

                lblFrom.Text = "f (";
                lblTo.Text = ") =";

                txtFrom.Enabled = true;
                txtTo.Enabled = true;
                txtPoint.Enabled = true;

                txtFrom.Width = 44;
                txtTo.Width = 44;

                gbConditionsII.Enabled = true;
                gbConditionsII.Visible = true;

                lblFrom.Left = 6;
                lblTo.Left = 80;
                txtFrom.Left = 30;
                txtTo.Left = 108;
            }
            else
            {
                lblFx.Text = "f(x) =";

                lblFrom.Text = language.GetString(lblFrom.Name);
                lblTo.Text = language.GetString(lblTo.Name);

                txtFrom.Width = 96;
                txtTo.Width = 96;

                txtFrom.Enabled = true;

                gbConditions.Text = language.GetString(gbConditions.Name);
                gbConditions.Width = 332;

                pnlWarunki.Width = 332;

                lblFrom.Left = 33;
                lblTo.Left = 169;
                txtFrom.Left = 67;
                txtTo.Left = 203;
            }

            if (rbDifferential.Checked)
            {
                lblFx.Text = "f'(x) =";

                gbConditions.Text = language.GetString(gbConditions.Name);

                lblFrom.Text = "f(";
                lblTo.Text = ") =";

                txtFrom.Enabled = true;
                txtTo.Enabled = true;
                txtPoint.Enabled = true;

                lblFrom.Left = 43;
                lblTo.Left = 166;
                txtFrom.Left = 64;
                txtTo.Left = 194;
            }
            else if (rbIntegral.Checked)
            {
                lblFx.Text = "f(x) =";

                gbConditions.Text = language.GetString(gbConditions.Name + "_Range");

                lblFrom.Text = language.GetString(lblFrom.Name + "_Range");
                lblTo.Text = language.GetString(lblTo.Name + "_Range");

                lblFrom.Left = 19;
                lblTo.Left = 169;
                txtFrom.Left = 67;
                txtTo.Left = 218;
            }
            else if (!rbDifferentialII.Checked)
            {
                lblFx.Text = "f(x) =";

                lblFrom.Text = language.GetString(lblFrom.Name);
                lblTo.Text = language.GetString(lblTo.Name);

                gbConditions.Text = language.GetString(gbConditions.Name);

                lblFrom.Left = 33;
                lblTo.Left = 169;
                txtFrom.Left = 67;
                txtTo.Left = 203;
            }

            if (rbSpecialFunction.Checked)
            {
                gbFunction.Text = language.GetString(gbFunction.Name + "_SpecialFunction");

                pnlFunkcja.Visible = false;
                pnlKomenda.Visible = true;

                txtFrom.Enabled = false;
                txtTo.Enabled = false;
                txtPoint.Enabled = false;
            }
            else
            {
                gbFunction.Text = language.GetString(gbFunction.Name);

                pnlFunkcja.Visible = true;
                pnlKomenda.Visible = false;

                cmbSpecialFunction.Visible = true;
            }

            if (rbRoot.Checked || rbIntegral.Checked)
            {
                txtFrom.Enabled = true;
                txtTo.Enabled = true;
                txtPoint.Enabled = false;
            }
            else if (rbPoint.Checked || rbDerivativePoint.Checked || rbDerivativePointBis.Checked)
            {
                txtFrom.Enabled = false;
                txtTo.Enabled = false;
                txtPoint.Enabled = true;
            }
            else if (rbCalculator.Checked)
            {
                txtFrom.Enabled = false;
                txtTo.Enabled = false;
                txtPoint.Enabled = false;
            }

            if (rbCalculator.Checked)
            {
                lblFx.Text = string.Empty;

                txtFunction.Width = 278;
                txtFunction.Left = 9;
            }
            else if (!rbDifferential.Checked && !rbDifferentialII.Checked)
            {
                lblFx.Text = "f(x) =";

                txtFunction.Width = 243;
                txtFunction.Left = 44;
            }
            else
            {
                txtFunction.Width = 243;
                txtFunction.Left = 44;
            }

            //Checkboxy
            if (rbDifferential.Checked)
            {
                chkFunction.Enabled = false;
                chkFirstDerivative.Enabled = false;
                chkSecondDerivative.Enabled = false;
                chkDifferential.Enabled = true;
                chkDifferentialII.Enabled = false;
                chkSpecialFunction.Enabled = false;
                chkFT.Enabled = false;
                chkIFT.Enabled = false;

                txtSampling.Enabled = false;
                txtCutoff.Enabled = false;

                lblSampling.Enabled = false;
                lblCutoff.Enabled = false;

                chkRescaling.Enabled = false;
            }
            else if (rbDifferentialII.Checked)
            {
                chkFunction.Enabled = false;
                chkFirstDerivative.Enabled = false;
                chkSecondDerivative.Enabled = false;
                chkDifferential.Enabled = false;
                chkDifferentialII.Enabled = true;
                chkSpecialFunction.Enabled = false;
                chkFT.Enabled = false;
                chkIFT.Enabled = false;

                txtSampling.Enabled = false;
                txtCutoff.Enabled = false;

                lblSampling.Enabled = false;
                lblCutoff.Enabled = false;

                chkRescaling.Enabled = false;
            }
            else if (rbSpecialFunction.Checked)
            {
                chkFunction.Enabled = false;
                chkFirstDerivative.Enabled = false;
                chkSecondDerivative.Enabled = false;
                chkDifferential.Enabled = false;
                chkDifferentialII.Enabled = false;
                chkSpecialFunction.Enabled = true;
                chkFT.Enabled = false;
                chkIFT.Enabled = false;

                txtSampling.Enabled = false;
                txtCutoff.Enabled = false;

                lblSampling.Enabled = false;
                lblCutoff.Enabled = false;

                chkRescaling.Enabled = true;
            }
            else
            {
                chkFunction.Enabled = true;
                chkFirstDerivative.Enabled = true;
                chkSecondDerivative.Enabled = true;
                chkDifferential.Enabled = false;
                chkDifferentialII.Enabled = false;
                chkSpecialFunction.Enabled = false;
                chkFT.Enabled = true;
                chkIFT.Enabled = true;

                txtSampling.Enabled = true;
                txtCutoff.Enabled = true;

                lblSampling.Enabled = true;
                lblCutoff.Enabled = true;

                chkRescaling.Enabled = true;
            }

            //Wyłączenie reskallingu gdy FFT
            if ((chkFT.Checked && chkFT.Enabled) || (chkIFT.Checked && chkIFT.Enabled))
                chkRescaling.Enabled = false;

            //Jak FFT to nie RFFT i na odwrot
            if (chkFT.Checked && chkFT.Enabled)
            {
                chkFunction.Enabled = false;
                chkFirstDerivative.Enabled = false;
                chkSecondDerivative.Enabled = false;
                chkIFT.Enabled = false;
            }
        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            SetRadioButtons();
        }

        /// <summary>
        /// Obliczanie pierwiastkow
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void btnCompute_Click(object sender, EventArgs e)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            try
            {
                //Pobranie argumentów i funkcji
                double point = 0.0d, from = 0.0d, to = 0.0d, fromII = 0.0d, toII = 0.0d;
                double firstBesselArgument = 0.0d, secondBesselArgument = 0.0d, thirdBesselArgument = 0.0d, fourthBesselArgument = 0.0d;
                string function = string.Empty;

                if (rbPoint.Checked || rbDerivativePoint.Checked || rbDerivativePointBis.Checked || rbDifferential.Checked || rbDifferentialII.Checked)
                {
                    //Pobranie punktu
                        point = GetArgument(ArgumentTypeEnum.Point);
                }

                if (rbRoot.Checked || rbIntegral.Checked || rbDifferential.Checked || rbDifferentialII.Checked)
                {
                    //Pobranie from i to
                        from = GetArgument(ArgumentTypeEnum.From);
                        to = GetArgument(ArgumentTypeEnum.To);
                }

                if (rbDifferentialII.Checked)
                {
                    //Pobranie fromII i toII
                        fromII = GetArgument(ArgumentTypeEnum.FromII);
                        toII = GetArgument(ArgumentTypeEnum.ToII);
                }

                if (rbSpecialFunction.Checked)
                {
                    //Pobranie argumentow besselowych
                    firstBesselArgument = GetArgument(ArgumentTypeEnum.BesselFirst);
                    secondBesselArgument = GetArgument(ArgumentTypeEnum.BesselSecond);

                    if (cmbSpecialFunction.SelectedIndex == 7 || cmbSpecialFunction.SelectedIndex == 8)
                        thirdBesselArgument = GetArgument(ArgumentTypeEnum.BesselThird);

                    if (cmbSpecialFunction.SelectedIndex == 8)
                        fourthBesselArgument = GetArgument(ArgumentTypeEnum.BesselFourth);
                }
                else
                {
                    //Pobranie funkcji
                    function = txtFunction.Text.Replace(changeFrom, changeTo);

                    if (string.IsNullOrEmpty(function))
                    {
                        throw new FunctionNullReferenceException();
                    }
                }   

                //Obliczenia
                if (rbCalculator.Checked)
                {
                    Calculator calculator = new Calculator(function);
                    txtResult.Text = calculator.ComputeInterior().ToString();
                }
                else if (rbPoint.Checked)
                {
                    Derivative derivative = new Derivative(function, point);
                    txtResult.Text = derivative.ComputeFunctionAtPoint().ToString();
                }
                else if (rbDerivativePoint.Checked)
                {              
                    Derivative derivative = new Derivative(function, point);
                    txtResult.Text = derivative.ComputeDerivative().ToString();
                }
                else if (rbDerivativePointBis.Checked)
                {
                    Derivative derivativeBis = new Derivative(function, point);
                    txtResult.Text = derivativeBis.ComputeDerivativeBis().ToString();
                }
                else if (rbRoot.Checked)
                {
                    Hybrid hybrid = new Hybrid(function, from, to);
                    txtResult.Text = hybrid.ComputeInterior().ToString();
                }
                else if (rbIntegral.Checked)
                {
                    Integral integral = new Integral(function, from, to);
                    txtResult.Text = integral.ComputeInterior().ToString();
                }
                else if (rbDifferential.Checked)
                {
                    Differential derivative = new Differential(function);
                    List<PointD> points = derivative.ComputeDifferential(point, from, to);

                    txtResult.Text = points.Last().Y.ToString();
                }
                else if (rbDifferentialII.Checked)
                {
                    Differential derivative = new Differential(function);
                    List<PointD> points = derivative.ComputeDifferentialII(point, from, to, fromII, toII);

                    txtResult.Text = points.Last().Y.ToString();
                }
                else if (rbSpecialFunction.Checked)
                {
                    BesselNeumanHyper bessel = new BesselNeumanHyper();

                    double wynik = 0.0d;

                    switch (cmbSpecialFunction.SelectedIndex)
                    {
                        case 0:
                            wynik = bessel.Bessel(firstBesselArgument, secondBesselArgument);
                            break;
                        case 1:
                            wynik = bessel.SphBessel(firstBesselArgument, secondBesselArgument);
                            break;
                        case 2:
                            wynik = bessel.SphBesselPrim(firstBesselArgument, secondBesselArgument);
                            break;
                        case 3:
                            wynik = bessel.Neumann(firstBesselArgument, secondBesselArgument);
                            break;
                        case 4:
                            wynik = bessel.SphNeuman(firstBesselArgument, secondBesselArgument);
                            break;
                        case 5:
                            wynik = bessel.SphNeumanPrim(firstBesselArgument, secondBesselArgument);
                            break;
                        case 6:
                            wynik = bessel.Hyperg_0F_1(firstBesselArgument, secondBesselArgument);
                            break;
                        case 7:
                            wynik = bessel.Hyperg_1F_1(firstBesselArgument, secondBesselArgument, thirdBesselArgument);
                            break;
                        case 8:
                            wynik = bessel.Hyperg_2F_1(firstBesselArgument, secondBesselArgument, thirdBesselArgument, fourthBesselArgument);
                            break;
                        default:
                            break;
                    }

                    txtResult.Text = wynik.ToString();
                }

                stopWatch.Stop();
                lblTime.Text = stopWatch.Elapsed.ToString().Substring(3, 13);
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
                    lblTime.Text = stopWatch.Elapsed.ToString().Substring(3, 13);

                    DrawGraph();

                    txtResult.Text = language.GetString("VariableFoundException");
                }
                else if (type == "ToConversionException")
                {
                    if (rbIntegral.Checked)
                        HandleException(stopWatch, language.GetString("ToConversionException_Integral"));
                    else if (rbDifferential.Checked || rbDifferentialII.Checked)
                        HandleException(stopWatch, language.GetString("ToConversionException_Differential"));
                    else
                        HandleException(stopWatch, language.GetString("ToConversionException"));
                }
                else if (type == "FromConversionException")
                {
                    if (rbIntegral.Checked)
                        HandleException(stopWatch, language.GetString("FromConversionException_Integral"));
                    else if (rbDifferential.Checked || rbDifferentialII.Checked)
                        HandleException(stopWatch, language.GetString("FromConversionException_Differential"));
                    else
                        HandleException(stopWatch, language.GetString("FromConversionException"));
                }
                else if (type == "FunctionNullReferenceException")
                {
                    if (rbCalculator.Checked)
                        HandleException(stopWatch, language.GetString("FunctionNullReferenceException_Calculator"));
                    else
                        HandleException(stopWatch, language.GetString("FunctionNullReferenceException"));
                }
                else if (type == "OperatorAtTheBeginningOfTheExpressionException")
                {
                    HandleException(stopWatch, "Operator " + (excep as OperatorAtTheBeginningOfTheExpressionException).Operator + " " + language.GetString(type));
                }
                else if (type == "OperatorAtTheEndOfTheExpressionException")
                {
                    HandleException(stopWatch, "Operator " + (excep as OperatorAtTheEndOfTheExpressionException).Operator + " " + language.GetString(type));
                }
                else if (type == "ForbiddenSignDetectedException")
                {
                    HandleException(stopWatch, language.GetString(type) + " " + (excep as ForbiddenSignDetectedException).Sign);
                }
                else if (type == "FunctionException")
                    HandleException(stopWatch, excep.Message);
                else
                    HandleException(stopWatch, language.GetString(type));

                //Ustawianie focusu na konkretym polu w zaleznosci od wyjatku
                switch (type)
                {
                    case "PointConversionException": txtPoint.Focus(); break;
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
                    case "FunctionNullReferenceException": txtFunction.Focus(); break;
                    case "FromConversionException": txtFrom.Focus(); break;
                    case "FromIIConversionException": txtFromII.Focus(); break;
                    case "ToConversionException": txtTo.Focus(); break;
                    case "ToIIConversionException": txtToII.Focus(); break;
                    case "BesselFirstArgumentException": txtFirstCommandArgument.Focus(); break;
                    case "BesseleSecondArgumentException": txtSecondCommandArgument.Focus(); break;
                    case "BesseleThirdArgumentException": txtThirdCommandArgument.Focus(); break;
                    case "BesseleFourthArgumentException": txtFourthCommandArgument.Focus(); break;
                    default:
                        break;
                }
            }
        }

        private double GetArgument(ArgumentTypeEnum argumentType)
        {
            //Pobranie argumentu jako string
            string argument = string.Empty;

            switch (argumentType)
            {
                case ArgumentTypeEnum.Point:
                    argument = txtPoint.Text.Replace(changeFrom, changeTo);
                    break;
                case ArgumentTypeEnum.From:
                    argument = txtFrom.Text.Replace(changeFrom, changeTo);
                    break;
                case ArgumentTypeEnum.To:
                    argument = txtTo.Text.Replace(changeFrom, changeTo);
                    break;
                case ArgumentTypeEnum.FromII:
                    argument = txtFromII.Text.Replace(changeFrom, changeTo);
                    break;
                case ArgumentTypeEnum.ToII:
                    argument = txtToII.Text.Replace(changeFrom, changeTo);
                    break;
                case ArgumentTypeEnum.BesselFirst:
                    argument = txtFirstCommandArgument.Text.Replace(changeFrom, changeTo);
                    break;
                case ArgumentTypeEnum.BesselSecond:
                    argument = txtSecondCommandArgument.Text.Replace(changeFrom, changeTo);
                    break;
                case ArgumentTypeEnum.BesselThird:
                    argument = txtThirdCommandArgument.Text.Replace(changeFrom, changeTo);
                    break;
                case ArgumentTypeEnum.BesselFourth:
                    argument = txtFourthCommandArgument.Text.Replace(changeFrom, changeTo);
                    break;
                case ArgumentTypeEnum.Sampling:
                    argument = txtSampling.Text.Replace(changeFrom, changeTo);
                    break;
                case ArgumentTypeEnum.Cutoff:
                    argument = txtCutoff.Text.Replace(changeFrom, changeTo);
                    break;
                case ArgumentTypeEnum.xFrom:
                    argument = txtXFrom.Text.Replace(changeFrom, changeTo);
                    break;
                case ArgumentTypeEnum.xTo:
                    argument = txtXTo.Text.Replace(changeFrom, changeTo);
                    break;
                case ArgumentTypeEnum.yFrom:
                    argument = txtYFrom.Text.Replace(changeFrom, changeTo);
                    break;
                case ArgumentTypeEnum.yTo:
                    argument = txtYTo.Text.Replace(changeFrom, changeTo);
                    break;
                default:
                    break;
            }

            //Konwersja na double
            try
            {
                return Convert.ToDouble(argument);
            }
            catch (Exception)
            {
                //Sprawdzenie może da się oszacować
                try
                {
                    Calculator calculator = new Calculator(argument.Replace("E", Math.E.ToString()));
                    double result = calculator.ComputeInterior();

                    if (argument.Contains('E'))
                        MessageBox.Show(language.GetString("MessageBox_EulerWarning"), language.GetString("MessageBox_Caption_Warning"), MessageBoxButtons.OK, MessageBoxIcon.Information);

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
            lblTime.Text = stopWatch.Elapsed.ToString().Substring(3, 13);
            txtResult.Text = "";

            MessageBox.Show(message, language.GetString("MessageBox_Caption_Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void HandleException(string message)
        {
            txtResult.Text = "";

            MessageBox.Show(message, language.GetString("MessageBox_Caption_Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void HandleGraphException(string message)
        {
            txtResult.Text = "";
            IsFunctionDrawn = false;

            MessageBox.Show(message, language.GetString("MessageBox_Caption_Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);

            try
            {
                ClearGraph();
            }
            catch { }
        }

        //WYJSCIE Z PROGRAMU Z MENU
        private void wyjscieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //USTAWIENIE FOCUSA PO ZALADOWANIU FORMY NA txtFunkcja i narysowanie siatki wykresu i zadeklarowanie Formy Interpolacji i Gaussa
        private void Form1_Shown(object sender, EventArgs e)
        {
            //Focus
            txtFunction.Focus();

            ClearGraph();
        }

        private void ClearGraph()
        {
            try
            {
                Graph graph = new Graph(txtFunction.Text, picGraph, Convert.ToDouble(txtXFrom.Text.Replace(changeFrom, changeTo)), Convert.ToDouble(txtXTo.Text.Replace(changeFrom, changeTo)), Convert.ToDouble(txtYFrom.Text.Replace(changeFrom, changeTo)), Convert.ToDouble(txtYTo.Text.Replace(changeFrom, changeTo)));
                graph.Clear();
            }
            catch (Exception excep)
            {
                MessageBox.Show(language.GetString("MessageBox_ClearGraph_Exception") + System.Environment.NewLine + excep.Message, language.GetString("MessageBox_Caption_Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //FUNKCJE FORM (opis dostepnych funkcji)
        private void ShowFunctionForm_Handler(object sender, EventArgs e)
        {
            FunctionForm f = new FunctionForm(language, settings);
            f.Show();
        }

        //RownaniaLinioweForm
        private void linearEquationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LinearEquationForm rownaniaForm = new LinearEquationForm(changeFrom, changeTo, language, settings);

            rownaniaForm.Show();
        }

        // RYSOWANIE WYKRESU
        private void btnDraw_Click(object sender, EventArgs e)
        {
            DrawGraph();
        }

        private void DrawGraph(bool rescaling = true)
        {
            try
            {
                //Sprawdzenie czy jakas opcja wykresu jest zacheckowana
                if (rbSpecialFunction.Checked)
                {
                    if (!chkSpecialFunction.Checked)
                        throw new NoneGraphOptionCheckedException();
                }
                else if (rbDifferential.Checked)
                {
                    if (!chkDifferential.Checked)
                        throw new NoneGraphOptionCheckedException();
                }
                else if (rbDifferentialII.Checked)
                {
                    if (!chkDifferentialII.Checked)
                        throw new NoneGraphOptionCheckedException();
                }
                else
                {
                    if (!(chkFunction.Checked || chkFirstDerivative.Checked || chkSecondDerivative.Checked || chkDifferential.Checked || chkDifferentialII.Checked || chkFT.Checked || chkIFT.Checked))
                        throw new NoneGraphOptionCheckedException();
                }

                //Konwersja zmiennych
                string function = txtFunction.Text.Replace(changeFrom, changeTo);

                if (string.IsNullOrEmpty(function) && !chkSpecialFunction.Enabled)
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
                BesselFunctionType bft = BesselFunctionType.Bessel;

                if (chkSpecialFunction.Checked && chkSpecialFunction.Enabled)
                {

                    if (txtFirstCommandArgument.Text == "x")
                        first = double.NaN;
                    else
                        first = GetArgument(ArgumentTypeEnum.BesselFirst);

                    if (txtSecondCommandArgument.Text == "x")
                        second = double.NaN;
                    else
                        second = GetArgument(ArgumentTypeEnum.BesselSecond);

                    if (cmbSpecialFunction.SelectedIndex == 7 || cmbSpecialFunction.SelectedIndex == 8)
                    {
                        if (txtThirdCommandArgument.Text == "x")
                            thrid = double.NaN;
                        else
                            thrid = GetArgument(ArgumentTypeEnum.BesselThird);
                    }

                    if (cmbSpecialFunction.SelectedIndex == 8)
                    {
                        if (txtFourthCommandArgument.Text == "x")
                            fourth = double.NaN;
                        else
                            fourth = GetArgument(ArgumentTypeEnum.BesselFourth);
                    }

                    //znalezienie typu besselowego
                    switch (cmbSpecialFunction.SelectedIndex)
                    {
                        case 0:
                            bft = BesselFunctionType.Bessel;
                            break;
                        case 1:
                            bft = BesselFunctionType.BesselSphere;
                            break;
                        case 2:
                            bft = BesselFunctionType.BesselSphereDerivative;
                            break;
                        case 3:
                            bft = BesselFunctionType.Neumann;
                            break;
                        case 4:
                            bft = BesselFunctionType.NeumannSphere;
                            break;
                        case 5:
                            bft = BesselFunctionType.NeumannSphereDerivative;
                            break;
                        case 6:
                            bft = BesselFunctionType.Hypergeometric01;
                            break;
                        case 7:
                            bft = BesselFunctionType.Hypergeometric11;
                            break;
                        case 8:
                            bft = BesselFunctionType.Hypergeometric21;
                            break;
                        default:
                            break;
                    }
                }

                //Utworzenie klasy
                Graph graph = new Graph(function, picGraph, xFrom, xTo, yFrom, yTo);

                //Reskalling
                if (chkRescaling.Checked && chkRescaling.Enabled && rescaling)
                {
                    //Zbudowanie listy typow funkcji
                    List<FunctionTypeEnum> functionType = new List<FunctionTypeEnum>();

                    if (chkFunction.Checked)
                        functionType.Add(FunctionTypeEnum.Function);

                    if (chkFirstDerivative.Checked)
                        functionType.Add(FunctionTypeEnum.Derivative);

                    if (chkSecondDerivative.Checked)
                        functionType.Add(FunctionTypeEnum.SecondDerivative);

                    double[] reskalling = null;

                    //Obliczenie maxów i minów do reskalingu
                    if (chkSpecialFunction.Checked && chkSpecialFunction.Enabled)
                        reskalling = graph.Reskalling(bft, first, second, thrid, fourth); //bessele
                    else
                        reskalling = graph.Reskalling(functionType.ToArray()); //normlanych

                    xFrom = reskalling[0];
                    xTo = reskalling[1];
                    yFrom = reskalling[2];
                    yTo = reskalling[3];

                    txtXFrom.Text = xFrom.ToString();
                    txtXTo.Text = xTo.ToString();
                    txtYFrom.Text = yFrom.ToString();
                    txtYTo.Text = yTo.ToString();

                    graph = new Graph(function, picGraph, xFrom, xTo, yFrom, yTo);
                }

                //Rysowanie funkcji i pochodnych
                if (chkFunction.Checked && chkFunction.Enabled)
                    graph.Draw(FunctionTypeEnum.Function);

                if (chkFirstDerivative.Checked && chkFirstDerivative.Enabled)
                    graph.Draw(FunctionTypeEnum.Derivative);

                if (chkSecondDerivative.Checked && chkSecondDerivative.Enabled)
                    graph.Draw(FunctionTypeEnum.SecondDerivative);

                //Rysowanie FT
                if ((chkFT.Checked && chkFT.Enabled) || (chkIFT.Checked && chkIFT.Enabled))
                {
                    int sampling = 1000;
                    double cutoff = 0.0;

                    sampling = (int)Math.Round(GetArgument(ArgumentTypeEnum.Sampling), 0);
                    cutoff = GetArgument(ArgumentTypeEnum.Cutoff);

                    if (chkFT.Checked)
                        graph.DrawFT(FunctionTypeEnum.FT, sampling, cutoff);

                    if (chkIFT.Checked)
                        graph.DrawFT(FunctionTypeEnum.IFT, sampling, cutoff);
                }

                //Rysowanie rozniczek
                if (chkDifferential.Checked && chkDifferential.Enabled)
                {
                    double from, to;

                    from = GetArgument(ArgumentTypeEnum.From);
                    to = GetArgument(ArgumentTypeEnum.To);

                    graph.DrawDifferential(FunctionTypeEnum.Differential, from, to);
                }

                if (chkDifferentialII.Checked && chkDifferentialII.Enabled)
                {
                    double from, to, fromII, toII;

                    from = GetArgument(ArgumentTypeEnum.From);
                    to = GetArgument(ArgumentTypeEnum.To);
                    fromII = GetArgument(ArgumentTypeEnum.FromII);
                    toII = GetArgument(ArgumentTypeEnum.ToII);

                    graph.DrawDifferential(FunctionTypeEnum.DifferentialII, from, to, fromII, toII);
                }

                //Rysowanie Bessela
                if (chkSpecialFunction.Checked && chkSpecialFunction.Enabled)
                    graph.DrawBessel(bft, first, second, thrid, fourth);

                //Zakończenie
                IsFunctionDrawn = true;
                picGraph.Refresh();

                //Pobranie punktow wykresu
                graphPoint = graph.GraphPoints;
            }
            catch (Exception excep)
            {
                string type = excep.GetType().Name;

                if (type == "OperatorAtTheBeginningOfTheExpressionException")
                {
                    HandleGraphException("Operator " + (excep as OperatorAtTheBeginningOfTheExpressionException).Operator + " " + language.GetString(type));
                }
                else if (type == "OperatorAtTheEndOfTheExpressionException")
                {
                    HandleGraphException("Operator " + (excep as OperatorAtTheEndOfTheExpressionException).Operator + " " + language.GetString(type));
                }
                else if (type == "ForbiddenSignDetectedException")
                {
                    HandleGraphException(language.GetString(type) + " " + (excep as ForbiddenSignDetectedException).Sign);
                }

                HandleGraphException(language.GetString(type));

                switch (type)
                {
                    case "XFromIsGreaterThenXToException": txtXFrom.Focus(); break;
                    case "YFromIsGreaterThenYToException": txtYFrom.Focus(); break;
                    case "xFromException": txtXFrom.Focus(); break;
                    case "xToException": txtXTo.Focus(); break;
                    case "yFromException": txtYFrom.Focus(); break;
                    case "yToException": txtYTo.Focus(); break;
                    case "CoordinatesXException": txtXFrom.Focus(); break;
                    case "CoordinatesYException": txtYFrom.Focus(); break;
                    case "NaNOccuredException":
                    case "IncorrectEOperatorOccurrenceException":
                    case "OperatorAtTheBeginningOfTheExpressionException":
                    case "OperatorAtTheEndOfTheExpressionException":
                    case "TwoOperatorsOccurredSideBySideException":
                    case "TwoFactorialsOccuredSideBySideException":
                    case "EmptyFunctionStringException":
                    case "LeftAndRightBracketsAmountDoesNotMatchException":
                    case "FunctionNullReferenceException": txtFunction.Focus(); break;
                    case "FromConversionException": txtFrom.Focus(); break;
                    case "FromIIConversionException": txtFromII.Focus(); break;
                    case "ToConversionException": txtTo.Focus(); break;
                    case "ToIIConversionException": txtToII.Focus(); break;
                    case "BesselFirstArgumentException": txtFirstCommandArgument.Focus(); break;
                    case "BesseleSecondArgumentException": txtSecondCommandArgument.Focus(); break;
                    case "BesseleThirdArgumentException": txtThirdCommandArgument.Focus(); break;
                    case "BesseleFourthArgumentException": txtFourthCommandArgument.Focus(); break;
                    case "CutoffValueException": txtCutoff.Focus(); break;
                    case "SamplingValueException": txtSampling.Focus(); break;
                    default:
                        break;
                }
            }
        }

        //AboutBox
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm aboutForm = new AboutForm(language, settings);
            aboutForm.Show();
        }

        //PRZESUWANIE WYKRESU
        bool graphMovingStarted = false;
        Point startingPoint, endPoint;

        private void picGraph_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                startingPoint = e.Location;
                graphMovingStarted = true;
            }
        }

        private void picGraph_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && graphMovingStarted && (!string.IsNullOrEmpty(txtFunction.Text) || rbSpecialFunction.Checked))
            {
                graphMovingStarted = false;
                endPoint = e.Location;
            }
        }

        private void picGraph_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (graphMovingStarted && (!string.IsNullOrEmpty(txtFunction.Text) || rbSpecialFunction.Checked))
                {
                    

                    //Zmienne
                    double xFrom, xTo, yFrom, yTo;

                    xFrom = xTo = yFrom = yTo = 0d;

                    try
                    {
                        xFrom = GetArgument(ArgumentTypeEnum.xFrom);
                        xTo = GetArgument(ArgumentTypeEnum.xTo);
                        yFrom = GetArgument(ArgumentTypeEnum.yFrom);
                        yTo = GetArgument(ArgumentTypeEnum.yTo); ;
                    }
                    catch
                    {
                        MessageBox.Show(language.GetString("picGraph_MouseUp_CoordinatesException"), language.GetString("MessageBox_Caption_Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    double factorX = 0, factorY = 0;
                    endPoint = e.Location;

                    // Obliczanie wspolczynnikow X
                    if (xFrom * xTo <= 0)
                        factorX = picGraph.Width / (-xFrom + xTo);
                    else if (xFrom < 0)
                        factorX = picGraph.Width / (-xFrom + xTo);
                    else
                        factorX = picGraph.Width / (xTo - xFrom);

                    // Obliczanie wspolczynnikow Y
                    if (yFrom * yTo <= 0)
                        factorY = picGraph.Height / (-yFrom + yTo);
                    else if (yFrom < 0 && yTo < 0)
                        factorY = picGraph.Height / (-yFrom + yTo);
                    else
                        factorY = picGraph.Height / (yTo - yFrom);

                    //Ustalenie przesuniecia
                    double differenceX = (endPoint.X - startingPoint.X) / factorX;
                    double differenceY = ((endPoint.Y - picGraph.Width) - (startingPoint.Y - picGraph.Width)) / factorY;

                    //Zapisanie przesuniec
                    xFrom -= differenceX;
                    xTo -= differenceX;
                    yFrom += differenceY;
                    yTo += differenceY;

                    startingPoint = e.Location;

                    if (xFrom > graphMax)
                        xFrom = graphMax;
                    else if (xFrom < graphMin)
                        xFrom = graphMin;

                    if (xTo > graphMax)
                        xTo = graphMax;
                    else if (xTo < graphMin)
                        xTo = graphMin;

                    if (yFrom > graphMax)
                        yFrom = graphMax;
                    else if (yFrom < graphMin)
                        yFrom = graphMin;

                    if (yTo > graphMax)
                        yTo = graphMax;
                    else if (yTo < graphMin)
                        yTo = graphMin;


                    if (chkX.Checked)
                    {
                        txtXFrom.Text = Convert.ToString(xFrom);
                        txtXTo.Text = Convert.ToString(xTo);
                    }

                    if (chkY.Checked)
                    {
                        txtYFrom.Text = Convert.ToString(yFrom);
                        txtYTo.Text = Convert.ToString(yTo);
                    }

                    //Narysowanie nowego wykresu
                    if (IsFunctionDrawn)
                        DrawGraph(false);
                    else
                        ClearGraph();
                }
            }
            catch (Exception ex)
            {
                //TODO: Lepsza obsluga bledow        
                MessageBox.Show(ex.Message, language.GetString("MessageBox_Caption_Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // POWIEKSZANIE/POMNIEJSZANIE WYKRESU
        private void Form1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtFunction.Text) || rbSpecialFunction.Checked)
            {
                try
                {
                    double xFrom, xTo, yFrom, yTo;

                    //Zmienienie nieskończoności w max
                    if (double.IsPositiveInfinity(GetArgument(ArgumentTypeEnum.xFrom)))
                        txtXFrom.Text = graphMax.ToString();
                    if (double.IsPositiveInfinity(GetArgument(ArgumentTypeEnum.xTo)))
                        txtXTo.Text = graphMax.ToString();
                    if (double.IsPositiveInfinity(GetArgument(ArgumentTypeEnum.yFrom)))
                        txtYFrom.Text = graphMax.ToString();
                    if (double.IsPositiveInfinity(GetArgument(ArgumentTypeEnum.yTo)))
                        txtYTo.Text = graphMax.ToString();
                    if (double.IsNegativeInfinity(GetArgument(ArgumentTypeEnum.xFrom)))
                        txtXFrom.Text = graphMin.ToString();
                    if (double.IsNegativeInfinity(GetArgument(ArgumentTypeEnum.xTo)))
                        txtXTo.Text = graphMin.ToString();
                    if (double.IsNegativeInfinity(GetArgument(ArgumentTypeEnum.yFrom)))
                        txtYFrom.Text = graphMin.ToString();
                    if (double.IsNegativeInfinity(GetArgument(ArgumentTypeEnum.yTo)))
                        txtYTo.Text = graphMin.ToString();

                    xFrom = Math.Round(GetArgument(ArgumentTypeEnum.xFrom), 2);
                    xTo = Math.Round(GetArgument(ArgumentTypeEnum.xTo), 2);
                    yFrom = Math.Round(GetArgument(ArgumentTypeEnum.yFrom), 2);
                    yTo = Math.Round(GetArgument(ArgumentTypeEnum.yTo), 2);

                    //Obliczenie nowych wartosci
                    if (e.Delta > 0)
                    {
                        if (chkX.Checked)
                        {
                            double scaleX = Math.Abs(xTo - xFrom);
                            double differenceX = scaleX / 4;

                            xFrom += differenceX;
                            xTo -= differenceX;
                        }

                        if (chkY.Checked)
                        {
                            double scaleY = Math.Abs(yTo - yFrom);
                            double differenceY = scaleY / 4;

                            yFrom += differenceY;
                            yTo -= differenceY;
                        }
                    }
                    else if (e.Delta < 0)
                    {
                        if (chkX.Checked)
                        {
                            double scaleX = Math.Abs(xTo - xFrom);
                            double differenceX = scaleX / 2;

                            xFrom -= differenceX;
                            xTo += differenceX;
                        }

                        if (chkY.Checked)
                        {
                            double scaleY = Math.Abs(yTo - yFrom);
                            double differenceY = scaleY / 2;

                            yFrom -= differenceY;
                            yTo += differenceY;
                        }
                    }

                    //Obsluga błędu, że czasem wylicza takie same wartości :/
                    if (xFrom == xTo)
                    {
                        xFrom -= 0.05;
                        xTo += 0.05;
                    }

                    if (yFrom == yTo)
                    {
                        yFrom -= 0.05;
                        yTo += 0.05;
                    }

                    //Sprawdzenie czy wartosci nie sa zbyt bliskie zeru
                    if (xFrom < 0.1 && xFrom > 0)
                        xFrom = 0.1;
                    if (xFrom > -0.1 && xFrom < 0)
                        xFrom = -0.1;

                    if (xTo < 0.1 && xTo > 0)
                        xTo = 0.1;
                    if (xTo > -0.1 && xTo < 0)
                        xTo = -0.1;

                    if (yFrom < 0.1 && yFrom > 0)
                        yFrom = 0.1;
                    if (yFrom > -0.1 && yFrom < 0)
                        yFrom = -0.1;

                    if (yTo < 0.1 && yTo > 0)
                        yTo = 0.1;
                    if (yTo > -0.1 && yTo < 0)
                        yTo = -0.1;

                    //Sprawdzenie czy wartosci nie sa za duze
                    if (xFrom > graphMax)
                        xFrom = graphMax;
                    else if (xFrom < graphMin)
                        xFrom = graphMin;

                    if (xTo > graphMax)
                        xTo = graphMax;
                    else if (xTo < graphMin)
                        xTo = graphMin;

                    if (yFrom > graphMax)
                        yFrom = graphMax;
                    else if (yFrom < graphMin)
                        yFrom = graphMin;

                    if (yTo > graphMax)
                        yTo = graphMax;
                    else if (yTo < graphMin)
                        yTo = graphMin;

                    txtXFrom.Text = Convert.ToString(xFrom);
                    txtXTo.Text = Convert.ToString(xTo);
                    txtYFrom.Text = Convert.ToString(yFrom);
                    txtYTo.Text = Convert.ToString(yTo);

                    if (IsFunctionDrawn)
                        DrawGraph(false);
                    else
                        ClearGraph();
                }
                catch (Exception excep)
                {
                    string type = excep.GetType().Name;

                    bool defaultException = false;

                    switch (type)
                    {
                        case "XFromIsGreaterThenXToException": txtXFrom.Focus(); break;
                        case "YFromIsGreaterThenYToException": txtYFrom.Focus(); break;
                        case "xFromException": txtXFrom.Focus(); break;
                        case "xToException": txtXTo.Focus(); break;
                        case "yFromException": txtYFrom.Focus(); break;
                        case "yToException": txtYTo.Focus(); break;
                        case "CoordinatesXException": txtXFrom.Focus(); break;
                        case "CoordinatesYException": txtYFrom.Focus(); break;
                        default:
                            MessageBox.Show(language.GetString("Form1_MouseWheel_DefaultException") + Environment.NewLine + excep.Message, language.GetString("MessageBox_Caption_Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                            defaultException = true;
                            break;
                    }

                    if (!defaultException)
                        HandleGraphException(language.GetString(type));
                }
            }
        }

        private void całkaWielokrotnaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //CalkaForm calka = new CalkaForm();
            //calka.Show();
        }

        private void interpolationAproximationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InterpolationForm interpolationForm = new InterpolationForm(changeFrom, changeTo, language, settings);
            interpolationForm.FunctionAccepted += new InterpolationForm.FunctionAcceptedEventHandler(interpolationForm_FunctionAccepted);
            interpolationForm.Show();
        }

        void interpolationForm_FunctionAccepted(string function)
        {
            txtFunction.Text = function;
        }

        //Chowanie pokazywanie wykresu
        Size oldSize = new Size();

        private void graphToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (graphToolStripMenuItem.Checked)
            {
                this.MinimumSize = new Size(1007, 435);
                this.MaximumSize = new Size(0, 0);

                this.MaximizeBox = true;
                this.FormBorderStyle = FormBorderStyle.Sizable;

                this.Size = oldSize;

                this.Resize += new System.EventHandler(this.Form1_Resize);
            }
            else
            {
                this.Resize -= new System.EventHandler(this.Form1_Resize);
                
                oldSize = this.Size;

                this.MinimumSize = new Size(365, 435);
                this.MaximumSize = new Size(365, 435);

                this.MaximizeBox = false;
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
                
                Width = 365;
            }
        }

        private void saveToFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                saveFileDialog.FileName = language.GetString("saveFileDialog_FileName");
                saveFileDialog.Title = language.GetString("ApplicationName");
                DialogResult dr = saveFileDialog.ShowDialog();

                if (dr == DialogResult.OK)
                {
                    Image image = picGraph.Image;
                    image.Save(saveFileDialog.FileName);

                    MessageBox.Show(language.GetString("MessageBox_SaveToFile_Success"), language.GetString("ApplicationName"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception excep)
            {
                MessageBox.Show(language.GetString("MessageBox_SaveToFile_Failure") + Environment.NewLine + excep.Message, language.GetString("MessageBox_Caption_Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Image image = picGraph.Image;
                Clipboard.SetImage(image);

                MessageBox.Show(language.GetString("MessageBox_CopyToClipboard_Success"), language.GetString("ApplicationName"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception excep)
            {
                MessageBox.Show(language.GetString("MessageBox_CopyToClipboard_Failure") + Environment.NewLine + excep.Message, language.GetString("MessageBox_Caption_Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void saveToTXTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (graphPoint != null && graphPoint.Length > 0)
            {
                try
                {
                    DialogResult dr = saveFileDialogTXT.ShowDialog();

                    if (dr == DialogResult.OK)
                    {
                        string fileName = saveFileDialogTXT.FileName;

                        //Otworzenie pliku
                        StreamWriter sw = File.CreateText(fileName);

                        //Zapisanie do pliku
                        foreach (PointF p in graphPoint)
                            sw.WriteLine(p.X + " " + p.Y);

                        //Zamkniecie i komunikat ze ok
                        sw.Flush();
                        sw.Close();

                        MessageBox.Show(language.GetString("MessageBox_SaveToTXT_Success"), language.GetString("ApplicationName"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception excep)
                {
                    MessageBox.Show(language.GetString("MessageBox_SaveToTXT_Failure") + Environment.NewLine + excep.Message, language.GetString("MessageBox_Caption_Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                MessageBox.Show(language.GetString("MessageBox_SaveToTXT_Failure_LackPoints"), language.GetString("MessageBox_Caption_Error"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void chkBoxGraph_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkBox = sender as CheckBox;

            if (chkBox.Checked)
                chkBox.Image = Resources.unlock;
            else
                chkBox.Image = Resources._lock;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (this.Size.Width >= this.MinimumSize.Width && this.Size.Height >= this.MinimumSize.Height) //wiem ze if wyglada bzdurowato, ale jest false jak sie forma minimalizuje ...
            {
                int gainH = Height - 453;
                int gainW = Width - 1007;

                picGraph.Height = 368 + gainH;
                picGraph.Width = 400 + gainW;
                pnlWykres.Height = 368 + gainH;
                pnlWykres.Width = 400 + gainW;
                gbDrawFunction.Left = 769 + gainW;
                gbScale.Left = 769 + gainW;
                btnDraw.Left = 836 + gainW;

                if (IsFunctionDrawn && graphPreviewWhileWindowsScalingToolStripMenuItem.Checked)
                    DrawGraph(false);
                else
                    ClearGraph();
            }            
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            IsFunctionDrawn = false;
        }

        private void gbGraph_Enter(object sender, EventArgs e)
        {
            AcceptButton = btnDraw;
        }

        private void gbGraph_Leave(object sender, EventArgs e)
        {
            AcceptButton = btnCompute;
        }

        private void ChangeSettings(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem)
            {
                ToolStripMenuItem tsmi = sender as ToolStripMenuItem;

                if (tsmi.Name == graphToolStripMenuItem.Name)
                    settings[SettingEnum.GraphMenuChecked] = tsmi.Checked;
                else if (tsmi.Name == graphPreviewWhileWindowsScalingToolStripMenuItem.Name)
                    settings[SettingEnum.GraphPreviewMenuChecked] = tsmi.Checked;
            }

            if (sender is CheckBox)
            {
                CheckBox chk = sender as CheckBox;

                //Nie robie switcha, bo value w case musi być znaną, więc kompilator chkFunction.Name nie akceptuje ...
                if (chk.Name == chkFunction.Name)
                    settings[SettingEnum.FunctionChecked] = chk.Checked;
                else if (chk.Name == chkFirstDerivative.Name)
                    settings[SettingEnum.FirstDerativeChecked] = chk.Checked;
                else if (chk.Name == chkSecondDerivative.Name)
                    settings[SettingEnum.SecondDerativeChecked] = chk.Checked;
                else if (chk.Name == chkSpecialFunction.Name)
                    settings[SettingEnum.SpecialFunctionChecked] = chk.Checked;
                else if (chk.Name == chkRescaling.Name)
                    settings[SettingEnum.AutomaticRescallingChecked] = chk.Checked;
                else if (chk.Name == chkDifferential.Name)
                    settings[SettingEnum.DifferentialChecked] = chk.Checked;
                else if (chk.Name == chkDifferentialII.Name)
                    settings[SettingEnum.DifferentialIIChecked] = chk.Checked;
                else if (chk.Name == chkFT.Name)
                    settings[SettingEnum.FourierTransformChecked] = chk.Checked;
                else if (chk.Name == chkIFT.Name)
                    settings[SettingEnum.InverseFourierTransformChecked] = chk.Checked;
            }
        }

        private void cmbCommand_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = cmbSpecialFunction.SelectedIndex;

            if (index < 6)
            {
                txtThirdCommandArgument.Visible = false;
                txtFourthCommandArgument.Visible = false;

                cmbSpecialFunction.Width = 126;

                txtFirstCommandArgument.Left = 133;
                txtFirstCommandArgument.Width = 90;

                txtSecondCommandArgument.Left = 226;
                txtSecondCommandArgument.Width = 90;
            }
            else if (index == 6)
            {
                txtThirdCommandArgument.Visible = false;
                txtFourthCommandArgument.Visible = false;

                cmbSpecialFunction.Width = 162;

                txtFirstCommandArgument.Left = 169;
                txtFirstCommandArgument.Width = 72;

                txtSecondCommandArgument.Left = 244;
                txtSecondCommandArgument.Width = 72;
            }
            else if (index == 7)
            {
                txtThirdCommandArgument.Visible = true;
                txtFourthCommandArgument.Visible = false;

                cmbSpecialFunction.Width = 99;

                txtFirstCommandArgument.Left = 106;
                txtFirstCommandArgument.Width = 68;

                txtSecondCommandArgument.Left = 177;
                txtSecondCommandArgument.Width = 68;

                txtThirdCommandArgument.Left = 248;
                txtThirdCommandArgument.Width = 68;
            }
            else
            {
                txtThirdCommandArgument.Visible = true;
                txtFourthCommandArgument.Visible = true;

                cmbSpecialFunction.Width = 99;

                txtFirstCommandArgument.Left = 107;
                txtFirstCommandArgument.Width = 50;

                txtSecondCommandArgument.Left = 160;
                txtSecondCommandArgument.Width = 50;

                txtThirdCommandArgument.Left = 213;
                txtThirdCommandArgument.Width = 50;

                txtFourthCommandArgument.Left = 266;
                txtFourthCommandArgument.Width = 50;
            }
        }

        private void btnSpecialFunction_Click(object sender, EventArgs e)
        {
            MessageBox.Show(language.GetString("MessageBox_btnSpecialFunction_Click"), language.GetString("MessageBox_btnSpecialFunction_Click_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void txtFrom_TextChanged(object sender, EventArgs e)
        {
            if ((sender as TextBox).Name == txtFrom.Name)
                txtFromII.Text = txtFrom.Text;
            else
                txtFrom.Text = txtFromII.Text;
        }

        private void languageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Zarzadzanie zaznaczeniami
            if ((sender as ToolStripItem).Name == englishToolStripMenuItem.Name)
            {
                if (englishToolStripMenuItem.Checked)
                    polskiToolStripMenuItem.Checked = false;
                else
                    polskiToolStripMenuItem.Checked = true;
            }
            else
            {
                if (polskiToolStripMenuItem.Checked)
                    englishToolStripMenuItem.Checked = false;
                else
                    englishToolStripMenuItem.Checked = true;
            }

            //Ustawienie settings
            if (englishToolStripMenuItem.Checked)
                settings[SettingEnum.Language] = LanguageEnum.English;
            else
                settings[SettingEnum.Language] = LanguageEnum.Polish;

            //Ustawienie GUI
            LoadLanguage();
            Language.TranslateControl(this, language);
            Language.TranslateControl(contextMenuStrip, language);
        }

        private void proportionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProportionsForm pf = new ProportionsForm(changeFrom, changeTo, language, settings);
            pf.Show();
        }

        private void meanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MeanForm mf = new MeanForm(changeFrom, changeTo, language, settings);
            mf.Show();
        }
    }
}
