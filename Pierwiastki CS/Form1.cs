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
    public partial class Form1 : Form
    {
        bool IsFunctionDrawn = false;
        public string changeFrom, changeTo; //kropki i przecinki do zamieniania podczas konwersji string na double

        readonly double max = 530000000.0;
        readonly double min = -530000000.0;

        Settings settings;
        ResourceManager language;

        PointF[] graphPoint;

        public Form1()
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
            }
            catch (SettingNullReferenceException)
            {
                try
                {
                    settings.RestoreDefaults();

                    LoadLanguage();
                    SetSettings();
                    Language.TranslateControl(this, language);
                }
                catch
                {
                    MessageBox.Show(language.GetString("MessageBox_InstallationCorrupted"), language.GetString("MessageBox_InstallationCorrupted_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            //Dodatkowe zdarzenia
            this.graphToolStripMenuItem.Click += new System.EventHandler(this.ZmienUstawinia);
            this.btnHelp.Click += new EventHandler(ShowFunctionForm_Handler);
            this.chkFT.CheckedChanged += new EventHandler(radioButton_CheckedChanged);
            this.chkIFT.CheckedChanged += new EventHandler(radioButton_CheckedChanged);

            //Zrobienie gui
            SetRadioButtons();
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
                wykresToolStripMenuItem_Click(null, new EventArgs());

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

                lblFrom.Left = 37;
                lblTo.Left = 169;
                txtFrom.Left = 67;
                txtTo.Left = 199;
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

                lblFrom.Left = 37;
                lblTo.Left = 169;
                txtFrom.Left = 67;
                txtTo.Left = 199;
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

        private void btnOblicz_Click(object sender, EventArgs e)
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
                    try
                    {
                        point = Convert.ToDouble(txtPoint.Text.Replace(changeFrom, changeTo));
                    }
                    catch (Exception)
                    {
                        //Sprawdzenie może da się oszacować
                        try
                        {
                            Calculator kalkulator = new Calculator(txtPoint.Text.Replace(changeFrom, changeTo).Replace("E", Math.E.ToString()));
                            point = kalkulator.ComputeInterior();

                            if (txtPoint.Text.Contains('E'))
                                MessageBox.Show(language.GetString("MessageBox_EulerWarning"), language.GetString("MessageBox_Caption_Warning"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch
                        {
                            throw new PointConversionException();
                        }
                    }
                }
            
                if (rbRoot.Checked || rbIntegral.Checked || rbDifferential.Checked || rbDifferentialII.Checked)
                {
                    //Pobranie from i to
                    try
                    {
                        from = Convert.ToDouble(txtFrom.Text.Replace(changeFrom, changeTo));
                    }
                    catch (Exception)
                    {
                        //Sprawdzenie może da się oszacować
                        try
                        {
                            Calculator kalkulator = new Calculator(txtFrom.Text.Replace(changeFrom, changeTo).Replace("E", Math.E.ToString()));
                            from = kalkulator.ComputeInterior();

                            if (txtFrom.Text.Contains('E'))
                                MessageBox.Show(language.GetString("MessageBox_EulerWarning"), language.GetString("MessageBox_Caption_Warning"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch
                        {
                            throw new FromConversionException();
                        }
                    }

                    try
                    {
                        to = Convert.ToDouble(txtTo.Text.Replace(changeFrom, changeTo));
                    }
                    catch (Exception)
                    {
                        //Sprawdzenie może da się oszacować
                        try
                        {
                            Calculator kalkulator = new Calculator(txtTo.Text.Replace(changeFrom, changeTo).Replace("E", Math.E.ToString()));
                            to = kalkulator.ComputeInterior();

                            if (txtTo.Text.Contains('E'))
                                MessageBox.Show(language.GetString("MessageBox_EulerWarning"), language.GetString("MessageBox_Caption_Warning"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch
                        {
                            throw new ToConversionException();
                        }
                    }
                }

                if (rbDifferentialII.Checked)
                {
                    //Pobranie fromII i toII
                    try
                    {
                        fromII = Convert.ToDouble(txtFromII.Text.Replace(changeFrom, changeTo));
                    }
                    catch (Exception)
                    {
                        //Sprawdzenie może da się oszacować
                        try
                        {
                            Calculator kalkulator = new Calculator(txtFromII.Text.Replace(changeFrom, changeTo).Replace("E", Math.E.ToString()));
                            fromII = kalkulator.ComputeInterior();

                            if (txtFrom.Text.Contains('E'))
                                MessageBox.Show(language.GetString("MessageBox_EulerWarning"), language.GetString("MessageBox_Caption_Warning"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch
                        {
                            throw new FromIIConversionException();
                        }
                    }

                    try
                    {
                        toII = Convert.ToDouble(txtToII.Text.Replace(changeFrom, changeTo));
                    }
                    catch (Exception)
                    {
                        //Sprawdzenie może da się oszacować
                        try
                        {
                            Calculator kalkulator = new Calculator(txtToII.Text.Replace(changeFrom, changeTo).Replace("E", Math.E.ToString()));
                            toII = kalkulator.ComputeInterior();

                            if (txtTo.Text.Contains('E'))
                                MessageBox.Show(language.GetString("MessageBox_EulerWarning"), language.GetString("MessageBox_Caption_Warning"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch
                        {
                            throw new ToIIConversionException();
                        }
                    }
                }

                if (rbSpecialFunction.Checked)
                {
                    //Pobranie argumentow besselowych
                    try
                    {
                        firstBesselArgument = double.Parse(txtFirstCommandArgument.Text.Replace(changeFrom, changeTo));
                    }
                    catch (Exception)
                    {
                        //Sprawdzenie może da się oszacować
                        try
                        {
                            Calculator kalkulator = new Calculator(txtFirstCommandArgument.Text.Replace(changeFrom, changeTo).Replace("E", Math.E.ToString()));
                            firstBesselArgument = kalkulator.ComputeInterior();

                            if (txtFirstCommandArgument.Text.Contains('E'))
                                MessageBox.Show(language.GetString("MessageBox_EulerWarning"), language.GetString("MessageBox_Caption_Warning"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch
                        {
                            throw new BesselFirstArgumentException();
                        }
                    }

                    try
                    {
                        secondBesselArgument = double.Parse(txtSecondCommandArgument.Text.Replace(changeFrom, changeTo));
                    }
                    catch (Exception)
                    {
                        //Sprawdzenie może da się oszacować
                        try
                        {
                            Calculator kalkulator = new Calculator(txtSecondCommandArgument.Text.Replace(changeFrom, changeTo).Replace("E", Math.E.ToString()));
                            secondBesselArgument = kalkulator.ComputeInterior();

                            if (txtSecondCommandArgument.Text.Contains('E'))
                                MessageBox.Show(language.GetString("MessageBox_EulerWarning"), language.GetString("MessageBox_Caption_Warning"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch
                        {
                            throw new BesseleSecondArgumentException();
                        }
                    }

                    if (cmbSpecialFunction.SelectedIndex == 7 || cmbSpecialFunction.SelectedIndex == 8)
                    {
                        try
                        {
                            thirdBesselArgument = double.Parse(txtThirdCommandArgument.Text.Replace(changeFrom, changeTo));
                        }
                        catch (Exception)
                        {
                            //Sprawdzenie może da się oszacować
                            try
                            {
                                Calculator kalkulator = new Calculator(txtThirdCommandArgument.Text.Replace(changeFrom, changeTo).Replace("E", Math.E.ToString()));
                                thirdBesselArgument = kalkulator.ComputeInterior();

                                if (txtThirdCommandArgument.Text.Contains('E'))
                                    MessageBox.Show(language.GetString("MessageBox_EulerWarning"), language.GetString("MessageBox_Caption_Warning"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            catch
                            {
                                throw new BesseleThirdArgumentException();
                            }
                        }
                    }

                    if (cmbSpecialFunction.SelectedIndex == 8)
                    {
                        try
                        {
                            fourthBesselArgument = double.Parse(txtFourthCommandArgument.Text.Replace(changeFrom, changeTo));
                        }
                        catch (Exception)
                        {
                            //Sprawdzenie może da się oszacować
                            try
                            {
                                Calculator kalkulator = new Calculator(txtFourthCommandArgument.Text.Replace(changeFrom, changeTo).Replace("E", Math.E.ToString()));
                                fourthBesselArgument = kalkulator.ComputeInterior();

                                if (txtFourthCommandArgument.Text.Contains('E'))
                                    MessageBox.Show(language.GetString("MessageBox_EulerWarning"), language.GetString("MessageBox_Caption_Warning"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            catch
                            {
                                throw new BesseleFourthArgumentException();
                            }
                        }
                    }
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
                    List<PointD> points = derivative.ObliczRozniczkeII(point, from, to, fromII, toII);

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
            catch (FunctionNullReferenceException)
            {
                if (rbCalculator.Checked)
                    HandleException(stopWatch, language.GetString("FunctionNullReferenceException_Calculator"));
                else
                    HandleException(stopWatch, language.GetString("FunctionNullReferenceException"));

                txtFunction.Focus();
            }
            catch (VariableFoundException)
            {
                stopWatch.Stop();
                lblTime.Text = stopWatch.Elapsed.ToString().Substring(3, 13);

                btnDraw_Click(btnCompute, new EventArgs());

                txtResult.Text = language.GetString("VariableFoundException");
                txtFunction.Focus();
            }
            catch (FunctionException excep)
            {
                HandleException(stopWatch, excep.Message);

                txtFunction.Focus();
            }
            catch (PointConversionException)
            {
                HandleException(stopWatch, language.GetString("PointConversionException"));

                txtPoint.Focus();
            }
            catch (FromConversionException)
            {
                if (rbIntegral.Checked)
                    HandleException(stopWatch, language.GetString("FromConversionException_Integral"));
                else if (rbDifferential.Checked || rbDifferentialII.Checked)
                    HandleException(stopWatch, language.GetString("FromConversionException_Differential"));
                else
                    HandleException(stopWatch, language.GetString("FromConversionException"));

                txtFrom.Focus();
            }
            catch (FromIIConversionException)
            {
                HandleException(stopWatch, language.GetString("FromIIConversionException"));

                txtFromII.Focus();
            }
            catch (ToConversionException)
            {
                if (rbIntegral.Checked)
                    HandleException(stopWatch, language.GetString("ToConversionException_Integral"));
                else if (rbDifferential.Checked || rbDifferentialII.Checked)
                    HandleException(stopWatch, language.GetString("ToConversionException_Differential"));
                else
                    HandleException(stopWatch, language.GetString("ToConversionException"));

                txtTo.Focus();
            }
            catch (ToIIConversionException)
            {
                HandleException(stopWatch, language.GetString("ToIIConversionException"));

                txtToII.Focus();
            }
            catch (BesselFirstArgumentException)
            {
                HandleException(stopWatch, language.GetString("BesselFirstArgumentException"));

                txtFirstCommandArgument.Focus();
            }
            catch (BesseleSecondArgumentException)
            {
                HandleException(stopWatch, language.GetString("BesseleSecondArgumentException"));

                txtSecondCommandArgument.Focus();
            }
            catch (BesseleThirdArgumentException)
            {
                HandleException(stopWatch, language.GetString("BesseleThirdArgumentException"));

                txtThirdCommandArgument.Focus();
            }
            catch (BesseleFourthArgumentException)
            {
                HandleException(stopWatch, language.GetString("BesseleFourthArgumentException"));

                txtFourthCommandArgument.Focus();
            }
            catch (SystemException)
            {
                HandleException(stopWatch, language.GetString("SystemException"));
            }
            catch (Exception)
            {
                HandleException(stopWatch, language.GetString("Exception"));
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
            FunctionForm f = new FunctionForm();
            f.Show();
        }

        //RownaniaLinioweForm
        private void linearEquationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LinearEquationForm rownaniaForm = new LinearEquationForm(changeFrom, changeTo);

            rownaniaForm.Show();
        }

        // RYSOWANIE WYKRESU
        private void btnDraw_Click(object sender, EventArgs e)
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
                string funkcja = txtFunction.Text.Replace(changeFrom, changeTo);

                if (string.IsNullOrEmpty(funkcja) && !chkSpecialFunction.Enabled)
                {
                    throw new FunctionNullReferenceException();
                }

                double xFrom, xTo, yFrom, yTo;

                try
                {
                    xFrom = Convert.ToDouble(txtXFrom.Text.Replace(changeFrom, changeTo));
                }
                catch (Exception)
                {
                    //Sprawdzenie może da się oszacować
                    try
                    {
                        Calculator calculator = new Calculator(txtXFrom.Text.Replace(changeFrom, changeTo).Replace("E", Math.E.ToString()));
                        xFrom = calculator.ComputeInterior();

                        if (txtXFrom.Text.Contains('E'))
                            MessageBox.Show(language.GetString("MessageBox_EulerWarning"), language.GetString("MessageBox_Caption_Warning"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch
                    {
                        throw new xFromException();
                    }                    
                }

                try
                {
                    xTo = Convert.ToDouble(txtXTo.Text.Replace(changeFrom, changeTo));
                }
                catch (Exception)
                {
                    //Sprawdzenie może da się oszacować
                    try
                    {
                        Calculator calculator = new Calculator(txtXTo.Text.Replace(changeFrom, changeTo).Replace("E", Math.E.ToString()));
                        xTo = calculator.ComputeInterior();

                        if (txtXTo.Text.Contains('E'))
                            MessageBox.Show(language.GetString("MessageBox_EulerWarning"), language.GetString("MessageBox_Caption_Warning"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch
                    {
                        throw new xToException();
                    }                    
                }

                try
                {
                    yFrom = Convert.ToDouble(txtYFrom.Text.Replace(changeFrom, changeTo));
                }
                catch (Exception)
                {
                    //Sprawdzenie może da się oszacować
                    try
                    {
                        Calculator calculator = new Calculator(txtYFrom.Text.Replace(changeFrom, changeTo).Replace("E", Math.E.ToString()));
                        yFrom = calculator.ComputeInterior();

                        if (txtYFrom.Text.Contains('E'))
                            MessageBox.Show(language.GetString("MessageBox_EulerWarning"), language.GetString("MessageBox_Caption_Warning"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch
                    {
                        throw new yFromException();
                    }                      
                }

                try
                {
                    yTo = Convert.ToDouble(txtYTo.Text.Replace(changeFrom, changeTo));
                }
                catch (Exception)
                {
                    //Sprawdzenie może da się oszacować
                    try
                    {
                        Calculator calculator = new Calculator(txtYTo.Text.Replace(changeFrom, changeTo).Replace("E", Math.E.ToString()));
                        yTo = calculator.ComputeInterior();

                        if (txtYTo.Text.Contains('E'))
                            MessageBox.Show(language.GetString("MessageBox_EulerWarning"), language.GetString("MessageBox_Caption_Warning"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch
                    {
                        throw new yToException();
                    }                      
                }

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
                TypFunkcjiBessela tfb = TypFunkcjiBessela.Bessel;

                if (chkSpecialFunction.Checked && chkSpecialFunction.Enabled)
                {
                    try
                    {
                        if (txtFirstCommandArgument.Text == "x")
                            first = double.NaN;
                        else
                            first = double.Parse(txtFirstCommandArgument.Text.Replace(changeFrom, changeTo));
                    }
                    catch (Exception)
                    {
                        //Sprawdzenie może da się oszacować
                        try
                        {
                            Calculator kalkulator = new Calculator(txtFirstCommandArgument.Text.Replace(changeFrom, changeTo).Replace("E", Math.E.ToString()));
                            first = kalkulator.ComputeInterior();
                        }
                        catch
                        {
                            throw new BesselFirstArgumentException();
                        }
                    }

                    try
                    {
                        if (txtSecondCommandArgument.Text == "x")
                            second = double.NaN;
                        else
                            second = double.Parse(txtSecondCommandArgument.Text.Replace(changeFrom, changeTo));
                    }
                    catch (Exception)
                    {
                        //Sprawdzenie może da się oszacować
                        try
                        {
                            Calculator kalkulator = new Calculator(txtSecondCommandArgument.Text.Replace(changeFrom, changeTo).Replace("E", Math.E.ToString()));
                            second = kalkulator.ComputeInterior();
                        }
                        catch
                        {
                            throw new BesseleSecondArgumentException();
                        }
                    }

                    if (cmbSpecialFunction.SelectedIndex == 7 || cmbSpecialFunction.SelectedIndex == 8)
                    {
                        try
                        {
                            if (txtThirdCommandArgument.Text == "x")
                                thrid = double.NaN;
                            else
                                thrid = double.Parse(txtThirdCommandArgument.Text.Replace(changeFrom, changeTo));
                        }
                        catch (Exception)
                        {
                            //Sprawdzenie może da się oszacować
                            try
                            {
                                Calculator kalkulator = new Calculator(txtThirdCommandArgument.Text.Replace(changeFrom, changeTo).Replace("E", Math.E.ToString()));
                                thrid = kalkulator.ComputeInterior();
                            }
                            catch
                            {
                                throw new BesseleThirdArgumentException();
                            }
                        }
                    }

                    if (cmbSpecialFunction.SelectedIndex == 8)
                    {
                        try
                        {
                            if (txtFourthCommandArgument.Text == "x")
                                fourth = double.NaN;
                            else
                                fourth = double.Parse(txtFourthCommandArgument.Text.Replace(changeFrom, changeTo));
                        }
                        catch (Exception)
                        {
                            //Sprawdzenie może da się oszacować
                            try
                            {
                                Calculator kalkulator = new Calculator(txtFourthCommandArgument.Text.Replace(changeFrom, changeTo).Replace("E", Math.E.ToString()));
                                fourth = kalkulator.ComputeInterior();
                            }
                            catch
                            {
                                throw new BesseleFourthArgumentException();
                            }
                        }
                    }

                    //znalezienie typu besselowego
                    switch (cmbSpecialFunction.SelectedIndex)
                    {
                        case 0:
                            tfb = TypFunkcjiBessela.Bessel;
                            break;
                        case 1:
                            tfb = TypFunkcjiBessela.BesselSphere;
                            break;
                        case 2:
                            tfb = TypFunkcjiBessela.BesselSphereDerivative;
                            break;
                        case 3:
                            tfb = TypFunkcjiBessela.Neumann;
                            break;
                        case 4:
                            tfb = TypFunkcjiBessela.NeumannSphere;
                            break;
                        case 5:
                            tfb = TypFunkcjiBessela.NeumannSphereDerivative;
                            break;
                        case 6:
                            tfb = TypFunkcjiBessela.Hypergeometric01;
                            break;
                        case 7:
                            tfb = TypFunkcjiBessela.Hypergeometric11;
                            break;
                        case 8:
                            tfb = TypFunkcjiBessela.Hypergeometric21;
                            break;
                        default:
                            break;
                    }
                }

                //Utworzenie klasy
                Graph wykres = new Graph(funkcja, picGraph, xFrom, xTo, yFrom, yTo);

                //Reskalling
                if (chkRescaling.Checked && chkRescaling.Enabled && sender is Button)
                {
                    //Zbudowanie listy typow funkcji
                    List<FunctionTypeEnum> typyFunkcji = new List<FunctionTypeEnum>();

                    if (chkFunction.Checked)
                        typyFunkcji.Add(FunctionTypeEnum.Function);

                    if (chkFirstDerivative.Checked)
                        typyFunkcji.Add(FunctionTypeEnum.Derivative);

                    if (chkSecondDerivative.Checked)
                        typyFunkcji.Add(FunctionTypeEnum.SecondDerivative);

                    double[] reskalling = null;

                    //Obliczenie maxów i minów do reskalingu
                    if (chkSpecialFunction.Checked && chkSpecialFunction.Enabled)
                        reskalling = wykres.Reskalling(tfb, first, second, thrid, fourth);
                    else
                        reskalling = wykres.Reskalling(typyFunkcji.ToArray()); //normlanych

                    xFrom = reskalling[0];
                    xTo = reskalling[1];
                    yFrom = reskalling[2];
                    yTo = reskalling[3];

                    txtXFrom.Text = xFrom.ToString();
                    txtXTo.Text = xTo.ToString();
                    txtYFrom.Text = yFrom.ToString();
                    txtYTo.Text = yTo.ToString();

                    wykres = new Graph(funkcja, picGraph, xFrom, xTo, yFrom, yTo);
                }

                //Rysowanie funkcji i pochodnych
                if (chkFunction.Checked && chkFunction.Enabled)
                    wykres.Rysuj(FunctionTypeEnum.Function);

                if (chkFirstDerivative.Checked && chkFirstDerivative.Enabled)
                    wykres.Rysuj(FunctionTypeEnum.Derivative);

                if (chkSecondDerivative.Checked && chkSecondDerivative.Enabled)
                    wykres.Rysuj(FunctionTypeEnum.SecondDerivative);

                //Rysowanie FFT
                int probkowanie = 1000;
                double odciecie = 0.0;

                //probki
                if ((chkFT.Checked && chkFT.Enabled) || (chkIFT.Checked && chkIFT.Enabled))
                {
                    string probkowanieString = txtSampling.Text;
                    string odciecieString = txtCutoff.Text;

                    if (!int.TryParse(probkowanieString, out probkowanie))
                        throw new SamplingValueException();

                    double temp;

                    if (string.IsNullOrEmpty(odciecieString))
                        odciecie = 0.0;
                    else
                    {
                        if (!double.TryParse(odciecieString, out temp))
                            throw new FilterValueException();
                        else
                            odciecie = temp;
                    }
                }

                if (chkFT.Checked && chkFT.Enabled)
                    wykres.RysujFFT(FunctionTypeEnum.FT, probkowanie, odciecie);

                if (chkIFT.Checked && chkIFT.Enabled)
                    wykres.RysujFFT(FunctionTypeEnum.IFT, probkowanie, odciecie);

                //Rysowanie rozniczek
                if (chkDifferential.Checked && chkDifferential.Enabled)
                {
                    double from, to;

                    try
                    {
                        from = Convert.ToDouble(txtFrom.Text.Replace(changeFrom, changeTo));
                    }
                    catch (Exception)
                    {
                        //Sprawdzenie może da się oszacować
                        try
                        {
                            Calculator kalkulator = new Calculator(txtFrom.Text.Replace(changeFrom, changeTo).Replace("E", Math.E.ToString()));
                            from = kalkulator.ComputeInterior();

                            if (txtFrom.Text.Contains('E'))
                                MessageBox.Show("Zinterpretowano E jako liczbę Eulera!", "Uwaga!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch
                        {
                            throw new FromConversionException();
                        }
                    }

                    try
                    {
                        to = Convert.ToDouble(txtTo.Text.Replace(changeFrom, changeTo));
                    }
                    catch (Exception)
                    {
                        //Sprawdzenie może da się oszacować
                        try
                        {
                            Calculator kalkulator = new Calculator(txtTo.Text.Replace(changeFrom, changeTo).Replace("E", Math.E.ToString()));
                            to = kalkulator.ComputeInterior();

                            if (txtTo.Text.Contains('E'))
                                MessageBox.Show("Zinterpretowano E jako liczbę Eulera!", "Uwaga!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch
                        {
                            throw new ToConversionException();
                        }
                    }

                    wykres.RysujRozniczke(FunctionTypeEnum.Differential, from, to);
                }

                if (chkDifferentialII.Checked && chkDifferentialII.Enabled)
                {
                    double from, to, fromII, toII;

                    try
                    {
                        from = Convert.ToDouble(txtFrom.Text.Replace(changeFrom, changeTo));
                    }
                    catch (Exception)
                    {
                        //Sprawdzenie może da się oszacować
                        try
                        {
                            Calculator kalkulator = new Calculator(txtFrom.Text.Replace(changeFrom, changeTo).Replace("E", Math.E.ToString()));
                            from = kalkulator.ComputeInterior();

                            if (txtFrom.Text.Contains('E'))
                                MessageBox.Show("Zinterpretowano E jako liczbę Eulera!", "Uwaga!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch
                        {
                            throw new FromConversionException();
                        }
                    }

                    try
                    {
                        fromII = Convert.ToDouble(txtFromII.Text.Replace(changeFrom, changeTo));
                    }
                    catch (Exception)
                    {
                        //Sprawdzenie może da się oszacować
                        try
                        {
                            Calculator kalkulator = new Calculator(txtFromII.Text.Replace(changeFrom, changeTo).Replace("E", Math.E.ToString()));
                            fromII = kalkulator.ComputeInterior();

                            if (txtFrom.Text.Contains('E'))
                                MessageBox.Show("Zinterpretowano E jako liczbę Eulera!", "Uwaga!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch
                        {
                            throw new FromConversionException();
                        }
                    }

                    try
                    {
                        to = Convert.ToDouble(txtTo.Text.Replace(changeFrom, changeTo));
                    }
                    catch (Exception)
                    {
                        //Sprawdzenie może da się oszacować
                        try
                        {
                            Calculator kalkulator = new Calculator(txtTo.Text.Replace(changeFrom, changeTo).Replace("E", Math.E.ToString()));
                            to = kalkulator.ComputeInterior();

                            if (txtTo.Text.Contains('E'))
                                MessageBox.Show("Zinterpretowano E jako liczbę Eulera!", "Uwaga!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch
                        {
                            throw new ToConversionException();
                        }
                    }

                    try
                    {
                        toII = Convert.ToDouble(txtToII.Text.Replace(changeFrom, changeTo));
                    }
                    catch (Exception)
                    {
                        //Sprawdzenie może da się oszacować
                        try
                        {
                            Calculator kalkulator = new Calculator(txtToII.Text.Replace(changeFrom, changeTo).Replace("E", Math.E.ToString()));
                            toII = kalkulator.ComputeInterior();

                            if (txtTo.Text.Contains('E'))
                                MessageBox.Show("Zinterpretowano E jako liczbę Eulera!", "Uwaga!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch
                        {
                            throw new ToConversionException();
                        }
                    }

                    wykres.RysujRozniczke(FunctionTypeEnum.DifferentialII, from, to, fromII, toII);
                }

                if (chkSpecialFunction.Checked && chkSpecialFunction.Enabled)
                    wykres.RysujBessele(tfb, first, second, thrid, fourth);

                //Zakończenie
                IsFunctionDrawn = true;

                //Pobranie punktow wykresu
                graphPoint = wykres.PunktyWykresu;
            }
            catch (XFromIsGreaterThenXToException)
            {
                HandleGraphException("Wartości skali x są niepoprawne. Wartość początkowa skali nie może być większa (lub równa) od wartości końcowej.");

                txtXFrom.Focus();
            }
            catch (YFromIsGreaterThenYToException)
            {
                HandleGraphException("Wartości skali y są niepoprawne. Wartość początkowa skali nie może być większa (lub równa) od wartości końcowej.");

                txtYFrom.Focus();
            }
            catch (OverflowException)
            {
                HandleGraphException("Nie można narysować tej funkcji w tym przedziale.");
            }
            catch (xFromException)
            {
                HandleGraphException("Niepoprawna wartość od osi x");

                txtXFrom.Focus();
            }
            catch (xToException)
            {
                HandleGraphException("Niepoprawna wartość do osi x");

                txtXTo.Focus();
            }
            catch (yFromException)
            {
                HandleGraphException("Niepoprawna wartość od osi y");

                txtYFrom.Focus();
            }
            catch (yToException)
            {
                HandleGraphException("Niepoprawna wartość do osi y");

                txtYTo.Focus();
            }
            catch (CoordinatesXException excep)
            {
                HandleGraphException(excep.Message);

                txtXFrom.Focus();
            }
            catch (CoordinatesYException excep)
            {
                HandleGraphException(excep.Message);

                txtYFrom.Focus();
            }
            catch (FunctionNullReferenceException)
            {
                HandleGraphException("Wpisz funkcję!");

                txtFunction.Focus();
            }
            catch (FromConversionException)
            {
                HandleGraphException("Niepoprawny punkt x w pierwszym warunku!");

                txtFrom.Focus();
            }
            catch (FromIIConversionException)
            {
                HandleGraphException("Niepoprawny punkt x w drugim warunku!");

                txtFromII.Focus();
            }
            catch (ToConversionException)
            {
                HandleGraphException("Niepoprawna wartość w pierwszym warunku!");

                txtTo.Focus();
            }
            catch (ToIIConversionException)
            {
                HandleGraphException("Niepoprawna wartość w drugim warunku!");

                txtToII.Focus();
            }
            catch (BesselFirstArgumentException excep)
            {
                HandleGraphException(excep.Message);

                txtFirstCommandArgument.Focus();
            }
            catch (BesseleSecondArgumentException excep)
            {
                HandleGraphException(excep.Message);

                txtSecondCommandArgument.Focus();
            }
            catch (BesseleThirdArgumentException excep)
            {
                HandleGraphException(excep.Message);

                txtThirdCommandArgument.Focus();
            }
            catch (BesseleFourthArgumentException excep)
            {
                HandleGraphException(excep.Message);

                txtFourthCommandArgument.Focus();
            }
            catch (NoneGraphOptionCheckedException excep)
            {
                HandleGraphException(excep.Message);
            }
            catch (FilterValueException excep)
            {
                HandleGraphException(excep.Message);

                txtCutoff.Focus();
            }
            catch (SystemException)
            {
                HandleGraphException("Wystąpił nieoczekiwany wyjątek. Sprawdź poprawność wprowadzonej formuły!");
            }
            catch (Exception)
            {
                HandleGraphException("Wystąpił nieoczekiwany wyjątek. Sprawdź poprawność wprowadzonej formuły!");
            }
        }

        //AboutBox
        private void oProgramieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm aboutForm = new AboutForm();
            aboutForm.Show();
        }

        //PRZESUWANIE WYKRESU
        Point punktPoczatkowy, punktKoncowy;

        private void picWykres_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                punktPoczatkowy = e.Location;
        }

        private void picWykres_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left && (!string.IsNullOrEmpty(txtFunction.Text) || rbSpecialFunction.Checked))
                {
                    //Zmienne
                    double xOd, xDo, yOd, yDo;

                    xOd = xDo = yOd = yDo = 0d;

                    try
                    {
                        xOd = Convert.ToDouble(txtXFrom.Text.Replace(changeFrom, changeTo));
                        xDo = Convert.ToDouble(txtXTo.Text.Replace(changeFrom, changeTo));
                        yOd = Convert.ToDouble(txtYFrom.Text.Replace(changeFrom, changeTo));
                        yDo = Convert.ToDouble(txtYTo.Text.Replace(changeFrom, changeTo));
                    }
                    catch (SystemException)
                    {
                        MessageBox.Show("Bledne wartosci X i Y", "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    double wspX = 0, wspY = 0;
                    punktKoncowy = e.Location;

                    // Obliczanie wspolczynnikow X
                    if (xOd * xDo <= 0)
                        wspX = picGraph.Width / (-xOd + xDo);
                    else if (xOd < 0)
                        wspX = picGraph.Width / (-xOd + xDo);
                    else
                        wspX = picGraph.Width / (xDo - xOd);

                    // Obliczanie wspolczynnikow Y
                    if (yOd * yDo <= 0)
                        wspY = picGraph.Height / (-yOd + yDo);
                    else if (yOd < 0 && yDo < 0)
                        wspY = picGraph.Height / (-yOd + yDo);
                    else
                        wspY = picGraph.Height / (yDo - yOd);

                    //Ustalenie przesuniecia
                    double roznicaX = (punktKoncowy.X - punktPoczatkowy.X) / wspX;
                    double roznicaY = ((punktKoncowy.Y - picGraph.Width) - (punktPoczatkowy.Y - picGraph.Width)) / wspY;

                    //Zapisanie przesuniec
                    xOd -= roznicaX;
                    xDo -= roznicaX;
                    yOd += roznicaY;
                    yDo += roznicaY;

                    if (xOd > max)
                        xOd = max;
                    else if (xOd < min)
                        xOd = min;

                    if (xDo > max)
                        xDo = max;
                    else if (xDo < min)
                        xDo = min;

                    if (yOd > max)
                        yOd = max;
                    else if (yOd < min)
                        yOd = min;

                    if (yDo > max)
                        yDo = max;
                    else if (yDo < min)
                        yDo = min;


                    if (chkX.Checked)
                    {
                        txtXFrom.Text = Convert.ToString(xOd);
                        txtXTo.Text = Convert.ToString(xDo);
                    }

                    if (chkY.Checked)
                    {
                        txtYFrom.Text = Convert.ToString(yOd);
                        txtYTo.Text = Convert.ToString(yDo);
                    }

                    //Narysowanie nowego wykresu
                    if (IsFunctionDrawn/* && podgladWykresuPodczasSkalowaniaOnkaToolStripMenuItem.Checked*/)
                        btnDraw_Click(this, new EventArgs());
                    else
                        ClearGraph();
                }
            }
            catch
            {
                //TODO: JAKAS OBSLUGA BLEDOW            
            }
        }

        // POWIEKSZANIE/POMNIEJSZANIE WYKRESU
        private void Form1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtFunction.Text) || rbSpecialFunction.Checked)
            {
                try
                {
                    double xOd, xDo, yOd, yDo;

                    //Zmienienie nieskończoności w max
                    if (double.IsPositiveInfinity(Convert.ToDouble(txtXFrom.Text.Replace(changeFrom, changeTo))))
                        txtXFrom.Text = max.ToString();
                    if (double.IsPositiveInfinity(Convert.ToDouble(txtXTo.Text.Replace(changeFrom, changeTo))))
                        txtXTo.Text = max.ToString();
                    if (double.IsPositiveInfinity(Convert.ToDouble(txtYFrom.Text.Replace(changeFrom, changeTo))))
                        txtYFrom.Text = max.ToString();
                    if (double.IsPositiveInfinity(Convert.ToDouble(txtYTo.Text.Replace(changeFrom, changeTo))))
                        txtYTo.Text = max.ToString();
                    if (double.IsNegativeInfinity(Convert.ToDouble(txtXFrom.Text.Replace(changeFrom, changeTo))))
                        txtXFrom.Text = min.ToString();
                    if (double.IsNegativeInfinity(Convert.ToDouble(txtXTo.Text.Replace(changeFrom, changeTo))))
                        txtXTo.Text = min.ToString();
                    if (double.IsNegativeInfinity(Convert.ToDouble(txtYFrom.Text.Replace(changeFrom, changeTo))))
                        txtYFrom.Text = min.ToString();
                    if (double.IsNegativeInfinity(Convert.ToDouble(txtYTo.Text.Replace(changeFrom, changeTo))))
                        txtYTo.Text = min.ToString();

                    try
                    {
                        xOd = Math.Round(Convert.ToDouble(txtXFrom.Text.Replace(changeFrom, changeTo)), 2);
                    }
                    catch (Exception)
                    {
                        throw new xFromException();
                    }

                    try
                    {
                        xDo = Math.Round(Convert.ToDouble(txtXTo.Text.Replace(changeFrom, changeTo)), 2);
                    }
                    catch (Exception)
                    {
                        throw new xToException();
                    }

                    try
                    {
                        yOd = Math.Round(Convert.ToDouble(txtYFrom.Text.Replace(changeFrom, changeTo)), 2);
                    }
                    catch (Exception)
                    {
                        throw new yFromException();
                    }

                    try
                    {
                        yDo = Math.Round(Convert.ToDouble(txtYTo.Text.Replace(changeFrom, changeTo)), 2);
                    }
                    catch (Exception)
                    {
                        throw new yToException();
                    }

                    //Obliczenie nowych wartosci
                    if (e.Delta > 0)
                    {
                        if (chkX.Checked)
                        {
                            double skalaX = Math.Abs(xDo - xOd);
                            double zmianaX = skalaX / 4;

                            xOd += zmianaX;
                            xDo -= zmianaX;
                                
                            //Obsluga błędu, że czasem wylicza takie same wartości :/
                            if (xOd == xDo)
                            {
                                xOd -= 0.05;
                                xDo += 0.05;
                            }
                        }

                        if (chkY.Checked)
                        {
                            double skalaY = Math.Abs(yDo - yOd);
                            double zmianaY = skalaY / 4;

                            yOd += zmianaY;
                            yDo -= zmianaY;

                            //Obsluga błędu, że czasem wylicza takie same wartości :/
                            if (yOd == yDo)
                            {
                                yOd -= 0.05;
                                yDo += 0.05;
                            }
                        }
                    }
                    else if (e.Delta < 0)
                    {
                        if (chkX.Checked)
                        {
                            double skalaX = Math.Abs(xDo - xOd);
                            double zmianaX = skalaX / 2;

                            xOd -= zmianaX;
                            xDo += zmianaX;

                            //Obsluga błędu, że czasem wylicza takie same wartości :/
                            if (xOd == xDo)
                            {
                                xOd -= 0.05;
                                xDo += 0.05;
                            }
                        }

                        if (chkY.Checked)
                        {
                            double skalaY = Math.Abs(yDo - yOd);
                            double zmianaY = skalaY / 2;

                            yOd -= zmianaY;
                            yDo += zmianaY;

                            //Obsluga błędu, że czasem wylicza takie same wartości :/
                            if (yOd == yDo)
                            {
                                yOd -= 0.05;
                                yDo += 0.05;
                            }
                        }
                    }

                    //Sprawdzenie czy wartosci nie sa zbyt bliskie zeru
                    if (xOd < 0.1 && xOd > 0)
                        xOd = 0.1;
                    if (xOd > -0.1 && xOd < 0)
                        xOd = -0.1;

                    if (xDo < 0.1 && xDo > 0)
                        xDo = 0.1;
                    if (xDo > -0.1 && xDo < 0)
                        xDo = -0.1;

                    if (yOd < 0.1 && yOd > 0)
                        yOd = 0.1;
                    if (yOd > -0.1 && yOd < 0)
                        yOd = -0.1;

                    if (yDo < 0.1 && yDo > 0)
                        yDo = 0.1;
                    if (yDo > -0.1 && yDo < 0)
                        yDo = -0.1;

                    //Sprawdzenie czy wartosci nie sa za duze
                    if (xOd > max)
                        xOd = max;
                    else if (xOd < min)
                        xOd = min;

                    if (xDo > max)
                        xDo = max;
                    else if (xDo < min)
                        xDo = min;

                    if (yOd > max)
                        yOd = max;
                    else if (yOd < min)
                        yOd = min;

                    if (yDo > max)
                        yDo = max;
                    else if (yDo < min)
                        yDo = min;

                    txtXFrom.Text = Convert.ToString(xOd);
                    txtXTo.Text = Convert.ToString(xDo);
                    txtYFrom.Text = Convert.ToString(yOd);
                    txtYTo.Text = Convert.ToString(yDo);

                    if (IsFunctionDrawn/* && podgladWykresuPodczasSkalowaniaOnkaToolStripMenuItem.Checked*/)
                        btnDraw_Click(this, new EventArgs());
                    else
                        ClearGraph();
                }
                catch (xFromException)
                {
                    HandleException("Niepoprawna wartość od osi x");

                    txtXFrom.Focus();
                }
                catch (xToException)
                {
                    HandleException("Niepoprawna wartość do osi x");

                    txtXTo.Focus();
                }
                catch (yFromException)
                {
                    HandleException("Niepoprawna wartość od osi y");

                    txtYFrom.Focus();
                }
                catch (yToException)
                {
                    HandleException("Niepoprawna wartość do osi y");

                    txtYTo.Focus();
                }
                catch (Exception excep)
                {
                    MessageBox.Show("Błąd zmiany skali. " + excep.Message, "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void całkaWielokrotnaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //CalkaForm calka = new CalkaForm();
            //calka.Show();
        }

        private void interpolacjaIAproksymacjaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InterpolationForm interpolacjaForm = new InterpolationForm(this);
            interpolacjaForm.Show();
        }

        //Chowanie pokazywanie wykresu
        Size staryRozmiar = new Size();

        private void wykresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (graphToolStripMenuItem.Checked)
            {
                this.MinimumSize = new Size(1007, 435);
                this.MaximumSize = new Size(0, 0);

                this.MaximizeBox = true;
                this.FormBorderStyle = FormBorderStyle.Sizable;

                this.Size = staryRozmiar;

                this.Resize += new System.EventHandler(this.Form1_Resize);
            }
            else
            {
                this.Resize -= new System.EventHandler(this.Form1_Resize);
                
                staryRozmiar = this.Size;

                this.MinimumSize = new Size(365, 435);
                this.MaximumSize = new Size(365, 435);

                this.MaximizeBox = false;
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
                
                Width = 365;
            }
        }

        private void zapiszDoPlikuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = saveFileDialog.ShowDialog();

                if (dr == DialogResult.OK)
                {
                    Image image = picGraph.Image;
                    image.Save(saveFileDialog.FileName);

                    MessageBox.Show("Plik zapisany pomyślnie!", "PierwiastkiCS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception excep)
            {
                MessageBox.Show("Nie udało się zapisać pliku. " + excep.Message, "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void kopiujToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Image image = picGraph.Image;
                Clipboard.SetImage(image);

                MessageBox.Show("Obrazek skopiowany do schowka.", "PierwiastkiCS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception excep)
            {
                MessageBox.Show("Nie udało się skopiować obrazka do schowka. " + excep.Message, "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void zapiszDoTXTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (graphPoint != null && graphPoint.Length > 0)
            {
                try
                {
                    DialogResult dr = saveFileDialogTXT.ShowDialog();

                    if (dr == DialogResult.OK)
                    {
                        string sciezkaPliku = saveFileDialogTXT.FileName;

                        //Otworzenie pliku
                        StreamWriter sw = File.CreateText(sciezkaPliku);

                        //Zapisanie do pliku
                        foreach (PointF p in graphPoint)
                            sw.WriteLine(p.X + " " + p.Y);

                        //Zamkniecie i komunikat ze ok
                        sw.Flush();
                        sw.Close();

                        MessageBox.Show("Plik zapisany pomyślnie!", "PierwiastkiCS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception excep)
                {
                    MessageBox.Show("Nie udało się zapisać pliku. " + excep.Message, "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                MessageBox.Show("Brak punktów wykresu!", "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void chkBoxWykres_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkBox = sender as CheckBox;

            if (chkBox.Checked)
                chkBox.Image = Resources.unlock;
            else
                chkBox.Image = Resources._lock;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (this.Size.Width >= this.MinimumSize.Width && this.Size.Height >= this.MinimumSize.Height) //wiem ze wyglada bzdurowato, ale jest false jak sie forma minimalizuje ...
            {
                int gainH = Height - 435;
                int gainW = Width - 998;

                picGraph.Height = 350 + gainH;
                picGraph.Width = 400 + gainW;
                pnlWykres.Height = 350 + gainH;
                pnlWykres.Width = 400 + gainW;
                gbDrawFunction.Left = 759 + gainW;
                gbScale.Left = 759 + gainW;
                btnDraw.Left = 824 + gainW;

                if (IsFunctionDrawn && graphPreviewWhileWindowsScalingToolStripMenuItem.Checked)
                    btnDraw_Click(this, new EventArgs());
                else
                    ClearGraph();
            }            
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            IsFunctionDrawn = false;
        }

        private void GroupBoxWykresowy_Enter(object sender, EventArgs e)
        {
            AcceptButton = btnDraw;
        }

        private void GroupBoxWykresowy_Leave(object sender, EventArgs e)
        {
            AcceptButton = btnCompute;
        }

        private void ZmienUstawinia(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem)
            {
                ToolStripMenuItem tsmi = sender as ToolStripMenuItem;

                switch (tsmi.Name)
                {
                    case "wykresToolStripMenuItem":
                        settings[SettingEnum.GraphMenuChecked] = tsmi.Checked;
                        break;
                    case "podgladWykresuPodczasSkalowaniaOnkaToolStripMenuItem":
                        settings[SettingEnum.GraphPreviewMenuChecked] = tsmi.Checked;
                        break;
                    default:
                        break;
                }
            }
            if (sender is CheckBox)
            {
                CheckBox chk = sender as CheckBox;

                switch (chk.Name)
                {
                    case "chkFunkcja":
                        settings[SettingEnum.FunctionChecked] = chk.Checked;
                        break;
                    case "chkPierwszaPochodna":
                        settings[SettingEnum.FirstDerativeChecked] = chk.Checked;
                        break;
                    case "chkDrugaPochodna":
                        settings[SettingEnum.SecondDerativeChecked] = chk.Checked;
                        break;
                    case "chkFunkcjaSpecjalna":
                        settings[SettingEnum.SpecialFunctionChecked] = chk.Checked;
                        break;
                    case "chkWykresAuto":
                        settings[SettingEnum.AutomaticRescallingChecked] = chk.Checked;
                        break;
                    case "chkRozniczka":
                        settings[SettingEnum.DifferentialChecked] = chk.Checked;
                        break;
                    case "chkRozniczkaII":
                        settings[SettingEnum.DifferentialIIChecked] = chk.Checked;
                        break;
                    case "chkFFT":
                        settings[SettingEnum.FourierTransformChecked] = chk.Checked;
                        break;
                    case "chkRFFT":
                        settings[SettingEnum.InverseFourierTransformChecked] = chk.Checked;
                        break;
                    default:
                        break;
                }
            }
        }

        private void cmbKomenda_SelectedIndexChanged(object sender, EventArgs e)
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

        private void btnFunkcjaSpecjalna_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Wystarczy zaznaczyć funkcję specjalną w sekcji wyboru operacji i za jeden z jej argumentów wstawić zmienną x.", "Podpowiedź", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void txtOd_TextChanged(object sender, EventArgs e)
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
        }
    }
}
