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
        bool czyFunkcjaNarysowana = false;
        public string zamienZ, zamienNa; //kropki i przecinki do zamieniania podczas konwersji string na double

        readonly double max = 530000000.0;
        readonly double min = -530000000.0;

        Settings settings;
        ResourceManager language;

        PointF[] punktyWykresu;

        public Form1()
        {
            InitializeComponent();

            //Sprawdzenie czy system przyjmuje kropkę czy przecinek
            string test = "1.1";
            double testDouble;

            if (double.TryParse(test, out testDouble))
            {
                zamienZ = ",";
                zamienNa = ".";
            }
            else
            {
                zamienZ = ".";
                zamienNa = ",";
            }

            cmbSpecialFunction.SelectedIndex = 0;

            //Ustawienia
            settings = new Settings();

            try
            {
                LoadLanguage(settings);
                UstawSettings(settings);
            }
            catch (SettingNullReferenceException)
            {
                try
                {
                    settings.PrzywrocUstawieniaDomyslne();

                    LoadLanguage(settings);
                    UstawSettings(settings);
                }
                catch
                {
                    MessageBox.Show("Instalacja programu jest niepoprawna. Należy odinstalować i zainstalować program ponownie!", "Niezidentyfikowany problem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                }
            }

            //Dodatkowe zdarzenia
            this.graphToolStripMenuItem.Click += new System.EventHandler(this.ZmienUstawinia);
            this.btnHelp.Click += new EventHandler(PokazFunkcjeForm_Handler);
            this.chkFT.CheckedChanged += new EventHandler(radioButton_CheckedChanged);
            this.chkIFT.CheckedChanged += new EventHandler(radioButton_CheckedChanged);

            //Zrobienie gui
            radioButton_CheckedChanged(null, new EventArgs());
        }

        private void LoadLanguage(Settings settings)
        {
            if ((LanguageEnum)settings[SettingEnum.Language] == LanguageEnum.Polish)
                language = new ResourceManager("NumericalCalculator.Translations.LanguagePolish", GetType().Assembly);
            else
                language = new ResourceManager("NumericalCalculator.Translations.LanguageEnglish", GetType().Assembly);
        }

        private void UstawSettings(Settings settings)
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
        }

        /// <summary>
        /// USTAWIANIE RADIO BUTONOW
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            //Zarzadzanie GUI procz checkboxow
            if (rbDifferentialII.Checked)
            {
                lblFx.Text = "f''(x) =";

                gbConditions.Text = "Warunki";
                gbConditions.Width = 163;

                pnlWarunki.Width = 163;

                lblFrom.Text = "f (";
                lblTo.Text = ") =";

                txtFrom.Enabled = true;
                txtTo.Enabled = true;
                txtPoint.Enabled = true;

                txtFrom.Width = 44;
                txtTo.Width = 44;

                gbWarunkiII.Enabled = true;
                gbWarunkiII.Visible = true;

                lblFrom.Left = 6;
                lblTo.Left = 80;
                txtFrom.Left = 30;
                txtTo.Left = 108;
            }
            else
            {
                lblFx.Text = "f(x) =";

                lblFrom.Text = "od";
                lblTo.Text = "do";

                txtFrom.Width = 96;
                txtTo.Width = 96;

                txtFrom.Enabled = true;

                gbConditions.Text = "Warunki";
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

                gbConditions.Text = "Warunki";

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

                gbConditions.Text = "Granice";

                lblFrom.Text = "dolna";
                lblTo.Text = "górna";

                lblFrom.Left = 19;
                lblTo.Left = 169;
                txtFrom.Left = 67;
                txtTo.Left = 218;
            }
            else if (!rbDifferentialII.Checked)
            {
                lblFx.Text = "f(x) =";

                lblFrom.Text = "od";
                lblTo.Text = "do";

                gbConditions.Text = "Warunki";

                lblFrom.Left = 37;
                lblTo.Left = 169;
                txtFrom.Left = 67;
                txtTo.Left = 199;
            }

            if (rbSpecialFunction.Checked)
            {
                gbFunction.Text = "Funkcja specjalna";

                pnlFunkcja.Visible = false;
                pnlKomenda.Visible = true;

                txtFrom.Enabled = false;
                txtTo.Enabled = false;
                txtPoint.Enabled = false;                
            }
            else
            {
                gbFunction.Text = "Funkcja";

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
                string funkcja = string.Empty;

                if (!rbSpecialFunction.Checked)
                {
                    funkcja = txtFunction.Text.Replace(zamienZ, zamienNa);

                    if (string.IsNullOrEmpty(funkcja))
                    {
                        throw new BrakFunkcjiException();
                    }
                }                

                if (rbCalculator.Checked)
                {
                    Kalkulator kalkulator = new Kalkulator(funkcja);
                    txtResult.Text = kalkulator.ObliczWnetrze().ToString();
                }
                else if (rbPoint.Checked)
                {
                    double x;

                    try
                    {
                        x = Convert.ToDouble(txtPoint.Text.Replace(zamienZ, zamienNa));
                    }
                    catch (Exception)
                    {
                        //Sprawdzenie może da się oszacować z E jako Euler
                        try
                        {
                            Kalkulator kalkulator = new Kalkulator(txtPoint.Text.Replace(zamienZ, zamienNa).Replace("E", Math.E.ToString()));
                            x = kalkulator.ObliczWnetrze();

                            if (txtPoint.Text.Contains('E'))
                                MessageBox.Show("Zinterpretowano E jako liczbę Eulera!", "Uwaga!", MessageBoxButtons.OK, MessageBoxIcon.Information);  
                        }
                        catch
                        {
                            throw new PunktConversionException();
                        }                            
                    }

                    Pochodna punkt = new Pochodna(funkcja, x);
                    txtResult.Text = punkt.ObliczFunkcjeWPunkcie().ToString();
                }
                else if (rbDerivativePoint.Checked)
                {
                    double punkt;

                    try
                    {
                        punkt = Convert.ToDouble(txtPoint.Text.Replace(zamienZ, zamienNa));
                    }
                    catch (Exception)
                    {
                        //Sprawdzenie może da się oszacować
                        try
                        {
                            Kalkulator kalkulator = new Kalkulator(txtPoint.Text.Replace(zamienZ, zamienNa).Replace("E", Math.E.ToString()));
                            punkt = kalkulator.ObliczWnetrze();

                            if (txtPoint.Text.Contains('E'))
                                MessageBox.Show("Zinterpretowano E jako liczbę Eulera!", "Uwaga!", MessageBoxButtons.OK, MessageBoxIcon.Information);  
                        }
                        catch
                        {
                            throw new PunktConversionException();
                        }
                    }

                    Pochodna pochodna = new Pochodna(funkcja, punkt);
                    txtResult.Text = pochodna.ObliczPochodna().ToString();
                }
                else if (rbDerivativePointBis.Checked)
                {
                    double punkt;

                    try
                    {
                        punkt = Convert.ToDouble(txtPoint.Text.Replace(zamienZ, zamienNa));
                    }
                    catch (Exception)
                    {
                        //Sprawdzenie może da się oszacować
                        try
                        {
                            Kalkulator kalkulator = new Kalkulator(txtPoint.Text.Replace(zamienZ, zamienNa).Replace("E", Math.E.ToString()));
                            punkt = kalkulator.ObliczWnetrze();

                            if (txtPoint.Text.Contains('E'))
                                MessageBox.Show("Zinterpretowano E jako liczbę Eulera!", "Uwaga!", MessageBoxButtons.OK, MessageBoxIcon.Information);  
                        }
                        catch
                        {
                            throw new PunktConversionException();
                        }
                    }

                    Pochodna pochodnaBis = new Pochodna(funkcja, punkt);
                    txtResult.Text = pochodnaBis.ObliczPochodnaBis().ToString();
                }
                else if (rbRoot.Checked)
                {
                    double from, to;

                    try
                    {
                        from = Convert.ToDouble(txtFrom.Text.Replace(zamienZ, zamienNa));
                    }
                    catch (Exception)
                    {
                        //Sprawdzenie może da się oszacować
                        try
                        {
                            Kalkulator kalkulator = new Kalkulator(txtFrom.Text.Replace(zamienZ, zamienNa).Replace("E", Math.E.ToString()));
                            from = kalkulator.ObliczWnetrze();

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
                        to = Convert.ToDouble(txtTo.Text.Replace(zamienZ, zamienNa));
                    }
                    catch (Exception)
                    {
                        //Sprawdzenie może da się oszacować
                        try
                        {
                            Kalkulator kalkulator = new Kalkulator(txtTo.Text.Replace(zamienZ, zamienNa).Replace("E", Math.E.ToString()));
                            to = kalkulator.ObliczWnetrze();

                            if (txtTo.Text.Contains('E'))
                                MessageBox.Show("Zinterpretowano E jako liczbę Eulera!", "Uwaga!", MessageBoxButtons.OK, MessageBoxIcon.Information);  
                        }
                        catch
                        {
                            throw new ToConversionException();
                        }
                    }

                    Hybryda hybryda = new Hybryda(funkcja, from, to);
                    txtResult.Text = hybryda.ObliczWnetrze().ToString();
                }
                else if (rbIntegral.Checked)
                {
                    double from, to;

                    try
                    {
                        from = Convert.ToDouble(txtFrom.Text.Replace(zamienZ, zamienNa));
                    }
                    catch (Exception)
                    {
                        //Sprawdzenie może da się oszacować
                        try
                        {
                            Kalkulator kalkulator = new Kalkulator(txtFrom.Text.Replace(zamienZ, zamienNa).Replace("E", Math.E.ToString()));
                            from = kalkulator.ObliczWnetrze();

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
                        to = Convert.ToDouble(txtTo.Text.Replace(zamienZ, zamienNa));
                    }
                    catch (Exception)
                    {
                        //Sprawdzenie może da się oszacować
                        try
                        {
                            Kalkulator kalkulator = new Kalkulator(txtTo.Text.Replace(zamienZ, zamienNa).Replace("E", Math.E.ToString()));
                            to = kalkulator.ObliczWnetrze();

                            if (txtTo.Text.Contains('E'))
                                MessageBox.Show("Zinterpretowano E jako liczbę Eulera!", "Uwaga!", MessageBoxButtons.OK, MessageBoxIcon.Information);  
                        }
                        catch
                        {
                            throw new ToConversionException();
                        }
                    }

                    Calka calka = new Calka(funkcja, from, to);
                    txtResult.Text = calka.ObliczWnetrze().ToString();
                }
                else if (rbDifferential.Checked)
                {
                    double punkt, from, to;

                    try
                    {
                        punkt = Convert.ToDouble(txtPoint.Text.Replace(zamienZ, zamienNa));
                    }
                    catch (Exception)
                    {
                        //Sprawdzenie może da się oszacować
                        try
                        {
                            Kalkulator kalkulator = new Kalkulator(txtPoint.Text.Replace(zamienZ, zamienNa).Replace("E", Math.E.ToString()));
                            punkt = kalkulator.ObliczWnetrze();

                            if (txtPoint.Text.Contains('E'))
                                MessageBox.Show("Zinterpretowano E jako liczbę Eulera!", "Uwaga!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch
                        {
                            throw new PunktConversionException();
                        }
                    }

                    try
                    {
                        from = Convert.ToDouble(txtFrom.Text.Replace(zamienZ, zamienNa));
                    }
                    catch (Exception)
                    {
                        //Sprawdzenie może da się oszacować
                        try
                        {
                            Kalkulator kalkulator = new Kalkulator(txtFrom.Text.Replace(zamienZ, zamienNa).Replace("E", Math.E.ToString()));
                            from = kalkulator.ObliczWnetrze();

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
                        to = Convert.ToDouble(txtTo.Text.Replace(zamienZ, zamienNa));
                    }
                    catch (Exception)
                    {
                        //Sprawdzenie może da się oszacować
                        try
                        {
                            Kalkulator kalkulator = new Kalkulator(txtTo.Text.Replace(zamienZ, zamienNa).Replace("E", Math.E.ToString()));
                            to = kalkulator.ObliczWnetrze();

                            if (txtTo.Text.Contains('E'))
                                MessageBox.Show("Zinterpretowano E jako liczbę Eulera!", "Uwaga!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch
                        {
                            throw new ToConversionException();
                        }
                    }

                    Rozniczka rozniczka = new Rozniczka(funkcja);
                    List<PointD> punkty = rozniczka.ObliczRozniczke(punkt, from, to);

                    txtResult.Text = punkty.Last().Y.ToString();
                }
                else if (rbDifferentialII.Checked)
                {
                    double punkt, from, to, fromII, toII;

                    try
                    {
                        punkt = Convert.ToDouble(txtPoint.Text.Replace(zamienZ, zamienNa));
                    }
                    catch (Exception)
                    {
                        //Sprawdzenie może da się oszacować
                        try
                        {
                            Kalkulator kalkulator = new Kalkulator(txtPoint.Text.Replace(zamienZ, zamienNa).Replace("E", Math.E.ToString()));
                            punkt = kalkulator.ObliczWnetrze();

                            if (txtPoint.Text.Contains('E'))
                                MessageBox.Show("Zinterpretowano E jako liczbę Eulera!", "Uwaga!", MessageBoxButtons.OK, MessageBoxIcon.Information);  
                        }
                        catch
                        {
                            throw new PunktConversionException();
                        }
                    }

                    try
                    {
                        from = Convert.ToDouble(txtFrom.Text.Replace(zamienZ, zamienNa));
                    }
                    catch (Exception)
                    {
                        //Sprawdzenie może da się oszacować
                        try
                        {
                            Kalkulator kalkulator = new Kalkulator(txtFrom.Text.Replace(zamienZ, zamienNa).Replace("E", Math.E.ToString()));
                            from = kalkulator.ObliczWnetrze();

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
                        fromII = Convert.ToDouble(txtOdII.Text.Replace(zamienZ, zamienNa));
                    }
                    catch (Exception)
                    {
                        //Sprawdzenie może da się oszacować
                        try
                        {
                            Kalkulator kalkulator = new Kalkulator(txtOdII.Text.Replace(zamienZ, zamienNa).Replace("E", Math.E.ToString()));
                            fromII = kalkulator.ObliczWnetrze();

                            if (txtFrom.Text.Contains('E'))
                                MessageBox.Show("Zinterpretowano E jako liczbę Eulera!", "Uwaga!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch
                        {
                            throw new FromIIConversionException();
                        }
                    }

                    try
                    {
                        to = Convert.ToDouble(txtTo.Text.Replace(zamienZ, zamienNa));
                    }
                    catch (Exception)
                    {
                        //Sprawdzenie może da się oszacować
                        try
                        {
                            Kalkulator kalkulator = new Kalkulator(txtTo.Text.Replace(zamienZ, zamienNa).Replace("E", Math.E.ToString()));
                            to = kalkulator.ObliczWnetrze();

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
                        toII = Convert.ToDouble(txtDoII.Text.Replace(zamienZ, zamienNa));
                    }
                    catch (Exception)
                    {
                        //Sprawdzenie może da się oszacować
                        try
                        {
                            Kalkulator kalkulator = new Kalkulator(txtDoII.Text.Replace(zamienZ, zamienNa).Replace("E", Math.E.ToString()));
                            toII = kalkulator.ObliczWnetrze();

                            if (txtTo.Text.Contains('E'))
                                MessageBox.Show("Zinterpretowano E jako liczbę Eulera!", "Uwaga!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch
                        {
                            throw new ToIIConversionException();
                        }
                    }

                    Rozniczka rozniczka = new Rozniczka(funkcja);
                    List<PointD> punkty = rozniczka.ObliczRozniczkeII(punkt, from, to, fromII, toII);

                    txtResult.Text = punkty.Last().Y.ToString();
                }
                else if (rbSpecialFunction.Checked)
                {
                    double pierwszy, drugi, trzeci = 0.0d, czwarty = 0.0d;

                    try
                    {
                        pierwszy = double.Parse(txtFirstCommandArgument.Text.Replace(zamienZ, zamienNa));
                    }
                    catch (Exception)
                    {
                        //Sprawdzenie może da się oszacować
                        try
                        {
                            Kalkulator kalkulator = new Kalkulator(txtFirstCommandArgument.Text.Replace(zamienZ, zamienNa).Replace("E", Math.E.ToString()));
                            pierwszy = kalkulator.ObliczWnetrze();

                            if (txtFirstCommandArgument.Text.Contains('E'))
                                MessageBox.Show("Zinterpretowano E jako liczbę Eulera!", "Uwaga!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch
                        {
                            throw new BesselePierwszyArgumentException();
                        }
                    }

                    try
                    {
                        drugi = double.Parse(txtSecondCommandArgument.Text.Replace(zamienZ, zamienNa));
                    }
                    catch (Exception)
                    {
                        //Sprawdzenie może da się oszacować
                        try
                        {
                            Kalkulator kalkulator = new Kalkulator(txtSecondCommandArgument.Text.Replace(zamienZ, zamienNa).Replace("E", Math.E.ToString()));
                            drugi = kalkulator.ObliczWnetrze();

                            if (txtSecondCommandArgument.Text.Contains('E'))
                                MessageBox.Show("Zinterpretowano E jako liczbę Eulera!", "Uwaga!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch
                        {
                            throw new BesseleDrugiArgumentException();
                        }
                    }

                    if (cmbSpecialFunction.SelectedIndex == 7 || cmbSpecialFunction.SelectedIndex == 8)
                    {
                        try
                        {
                            trzeci = double.Parse(txtThirdCommandArgument.Text.Replace(zamienZ, zamienNa));
                        }
                        catch (Exception)
                        {
                            //Sprawdzenie może da się oszacować
                            try
                            {
                                Kalkulator kalkulator = new Kalkulator(txtThirdCommandArgument.Text.Replace(zamienZ, zamienNa).Replace("E", Math.E.ToString()));
                                trzeci = kalkulator.ObliczWnetrze();

                                if (txtThirdCommandArgument.Text.Contains('E'))
                                    MessageBox.Show("Zinterpretowano E jako liczbę Eulera!", "Uwaga!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            catch
                            {
                                throw new BesseleTrzeciArgumentException();
                            }
                        }
                    }

                    if (cmbSpecialFunction.SelectedIndex == 8)
                    {
                        try
                        {
                            czwarty = double.Parse(thtFourthCommandArgument.Text.Replace(zamienZ, zamienNa));
                        }
                        catch (Exception)
                        {
                            //Sprawdzenie może da się oszacować
                            try
                            {
                                Kalkulator kalkulator = new Kalkulator(thtFourthCommandArgument.Text.Replace(zamienZ, zamienNa).Replace("E", Math.E.ToString()));
                                czwarty = kalkulator.ObliczWnetrze();

                                if (thtFourthCommandArgument.Text.Contains('E'))
                                    MessageBox.Show("Zinterpretowano E jako liczbę Eulera!", "Uwaga!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            catch
                            {
                                throw new BesseleCzwartyArgumentException();
                            }
                        }
                    }

                    BesselNeumanHyper bessel = new BesselNeumanHyper();

                    double wynik = 0.0d;

                    switch (cmbSpecialFunction.SelectedIndex)
                    {
                        case 0:
                            wynik = bessel.Bessel(pierwszy, drugi);
                            break;
                        case 1:
                            wynik = bessel.SphBessel(pierwszy, drugi);
                            break;
                        case 2:
                            wynik = bessel.SphBesselPrim(pierwszy, drugi);
                            break;
                        case 3:
                            wynik = bessel.Neumann(pierwszy, drugi);
                            break;
                        case 4:
                            wynik = bessel.SphNeuman(pierwszy, drugi);
                            break;
                        case 5:
                            wynik = bessel.SphNeumanPrim(pierwszy, drugi);
                            break;
                        case 6:
                            wynik = bessel.Hyperg_0F_1(pierwszy, drugi);
                            break;
                        case 7:
                            wynik = bessel.Hyperg_1F_1(pierwszy, drugi, trzeci);
                            break;
                        case 8:
                            wynik = bessel.Hyperg_2F_1(pierwszy, drugi, trzeci, czwarty);
                            break;
                        default:
                            break;
                    }

                    txtResult.Text = wynik.ToString();
                }

                stopWatch.Stop();
                lblTime.Text = stopWatch.Elapsed.ToString().Substring(3, 13);
            }
            catch (BrakFunkcjiException)
            {
                if (rbCalculator.Checked)
                    ObsluzException(stopWatch, "Wpisz działanie!");
                else
                    ObsluzException(stopWatch, "Wpisz funkcję!");

                txtFunction.Focus();
            }
            catch (WystepujeZmiennaException)
            {
                stopWatch.Stop();
                lblTime.Text = stopWatch.Elapsed.ToString().Substring(3, 13);

                btnRysuj_Click(btnCompute, new EventArgs());

                txtResult.Text = "W wyr. nie może występować zmienna!";
                txtFunction.Focus();
            }
            catch (FunkcjaException excep)
            {
                ObsluzException(stopWatch, excep.Message);

                txtFunction.Focus();
            }
            catch (PunktConversionException)
            {
                ObsluzException(stopWatch, "Niepoprawny punkt!");

                txtPoint.Focus();
            }
            catch (FromConversionException)
            {
                if (rbIntegral.Checked)
                    ObsluzException(stopWatch, "Niepoprawna dolna granica całkowania!");
                else if (rbDifferential.Checked)
                    ObsluzException(stopWatch, "Niepoprawny punkt x w pierwszym warunku!");
                else
                    ObsluzException(stopWatch, "Niepoprawny punkt od!");

                txtFrom.Focus();
            }
            catch (FromIIConversionException)
            {
                ObsluzException(stopWatch, "Niepoprawny punkt x w drugim warunku!");

                txtOdII.Focus();
            }
            catch (ToConversionException)
            {
                if (rbIntegral.Checked)
                    ObsluzException(stopWatch, "Niepoprawna górna granica całkowania!");
                else if (rbDifferential.Checked)
                    ObsluzException(stopWatch, "Niepoprawna wartość w pierwszym warunku!");
                else
                    ObsluzException(stopWatch, "Niepoprawny punkt do!");

                txtTo.Focus();
            }
            catch (ToIIConversionException)
            {
                ObsluzException(stopWatch, "Niepoprawna wartość w drugim warunku!");

                txtDoII.Focus();
            }
            catch (BesselePierwszyArgumentException excep)
            {
                ObsluzException(stopWatch, excep.Message);

                txtFirstCommandArgument.Focus();
            }
            catch (BesseleDrugiArgumentException excep)
            {
                ObsluzException(stopWatch, excep.Message);

                txtSecondCommandArgument.Focus();
            }
            catch (BesseleTrzeciArgumentException excep)
            {
                ObsluzException(stopWatch, excep.Message);

                txtThirdCommandArgument.Focus();
            }
            catch (BesseleCzwartyArgumentException excep)
            {
                ObsluzException(stopWatch, excep.Message);

                thtFourthCommandArgument.Focus();
            }
            catch (SystemException)
            {
                ObsluzException(stopWatch, "Wystąpił nieoczekiwany wyjątek. Sprawdź poprawność wprowadzonej formuły!");
            }
            catch (Exception)
            {
                ObsluzException(stopWatch, "Wystąpił nieoczekiwany wyjątek. Sprawdź poprawność wprowadzonej formuły!");
            }
        }

        private void ObsluzException(Stopwatch stopWatch, string message)
        {
            stopWatch.Stop();
            lblTime.Text = stopWatch.Elapsed.ToString().Substring(3, 13);
            txtResult.Text = "";

            MessageBox.Show(message, "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ObsluzException(string message)
        {
            txtResult.Text = "";

            MessageBox.Show(message, "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ObsluzExceptionWykres(string message)
        {
            txtResult.Text = "";
            czyFunkcjaNarysowana = false;

            MessageBox.Show(message, "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Error);

            try
            {
                WyczyscWykres();
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

            WyczyscWykres();
        }

        private void WyczyscWykres()
        {
            try
            {
                Wykres wykres = new Wykres(txtFunction.Text, picWykres, Convert.ToDouble(txtXFrom.Text.Replace(zamienZ, zamienNa)), Convert.ToDouble(txtXTo.Text.Replace(zamienZ, zamienNa)), Convert.ToDouble(txtYFrom.Text.Replace(zamienZ, zamienNa)), Convert.ToDouble(txtYTo.Text.Replace(zamienZ, zamienNa)));
                wykres.Wyczysc();
            }
            catch (SystemException excep)
            {
                MessageBox.Show("Błąd rysowania siatki wykresu! " + excep.Message, "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //FUNKCJE FORM (opis dostepnych funkcji)
        private void PokazFunkcjeForm_Handler(object sender, EventArgs e)
        {
            FunctionForm f = new FunctionForm();
            f.Show();
        }

        //RownaniaLinioweForm
        private void rownaniaLinioweToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LinearEquationForm rownaniaForm = new LinearEquationForm(zamienZ, zamienNa);

            rownaniaForm.Show();
        }

        // RYSOWANIE WYKRESU
        private void btnRysuj_Click(object sender, EventArgs e)
        {
            try
            {
                //Sprawdzenie czy jakas opcja wykresu jest zacheckowana
                if (rbSpecialFunction.Checked)
                {
                    if (!chkSpecialFunction.Checked)
                        throw new NoneWykresOptionCheckedException();
                }
                else if (rbDifferential.Checked)
                {
                    if (!chkDifferential.Checked)
                        throw new NoneWykresOptionCheckedException();
                }
                else if (rbDifferentialII.Checked)
                {
                    if (!chkDifferentialII.Checked)
                        throw new NoneWykresOptionCheckedException();
                }
                else
                {
                    if (!(chkFunction.Checked || chkFirstDerivative.Checked || chkSecondDerivative.Checked || chkDifferential.Checked || chkDifferentialII.Checked || chkFT.Checked || chkIFT.Checked))
                        throw new NoneWykresOptionCheckedException();
                }

                //Konwersja zmiennych
                string funkcja = txtFunction.Text.Replace(zamienZ, zamienNa);

                if (string.IsNullOrEmpty(funkcja) && !chkSpecialFunction.Enabled)
                {
                    throw new BrakFunkcjiException();
                }

                double xOd, xDo, yOd, yDo;

                try
                {
                    xOd = Convert.ToDouble(txtXFrom.Text.Replace(zamienZ, zamienNa));
                }
                catch (Exception)
                {
                    throw new xOdException();
                }

                try
                {
                    xDo = Convert.ToDouble(txtXTo.Text.Replace(zamienZ, zamienNa));
                }
                catch (Exception)
                {
                    throw new xDoException();
                }

                try
                {
                    yOd = Convert.ToDouble(txtYFrom.Text.Replace(zamienZ, zamienNa));
                }
                catch (Exception)
                {
                    throw new yOdException();
                }

                try
                {
                    yDo = Convert.ToDouble(txtYTo.Text.Replace(zamienZ, zamienNa));
                }
                catch (Exception)
                {
                    throw new yDoException();
                }

                //Sprawdzenie czy zakres jest OK
                if (xOd >= xDo)
                {
                    throw new XOdWiekszeNizXDoException();
                }

                if (yOd >= yDo)
                {
                    throw new YOdWiekszeNizYDoException();
                }

                //Pobranie Besselowych wartosci i znalezienie typu besselowego
                double pierwszy = 0.0, drugi = 0.0, trzeci = 0.0, czwarty = 0.0;
                TypFunkcjiBessela tfb = TypFunkcjiBessela.Bessel;

                if (chkSpecialFunction.Checked && chkSpecialFunction.Enabled)
                {
                    try
                    {
                        if (txtFirstCommandArgument.Text == "x")
                            pierwszy = double.NaN;
                        else
                            pierwszy = double.Parse(txtFirstCommandArgument.Text.Replace(zamienZ, zamienNa));
                    }
                    catch (Exception)
                    {
                        //Sprawdzenie może da się oszacować
                        try
                        {
                            Kalkulator kalkulator = new Kalkulator(txtFirstCommandArgument.Text.Replace(zamienZ, zamienNa).Replace("E", Math.E.ToString()));
                            pierwszy = kalkulator.ObliczWnetrze();
                        }
                        catch
                        {
                            throw new BesselePierwszyArgumentException();
                        }
                    }

                    try
                    {
                        if (txtSecondCommandArgument.Text == "x")
                            drugi = double.NaN;
                        else
                            drugi = double.Parse(txtSecondCommandArgument.Text.Replace(zamienZ, zamienNa));
                    }
                    catch (Exception)
                    {
                        //Sprawdzenie może da się oszacować
                        try
                        {
                            Kalkulator kalkulator = new Kalkulator(txtSecondCommandArgument.Text.Replace(zamienZ, zamienNa).Replace("E", Math.E.ToString()));
                            drugi = kalkulator.ObliczWnetrze();
                        }
                        catch
                        {
                            throw new BesseleDrugiArgumentException();
                        }
                    }

                    if (cmbSpecialFunction.SelectedIndex == 7 || cmbSpecialFunction.SelectedIndex == 8)
                    {
                        try
                        {
                            if (txtThirdCommandArgument.Text == "x")
                                trzeci = double.NaN;
                            else
                                trzeci = double.Parse(txtThirdCommandArgument.Text.Replace(zamienZ, zamienNa));
                        }
                        catch (Exception)
                        {
                            //Sprawdzenie może da się oszacować
                            try
                            {
                                Kalkulator kalkulator = new Kalkulator(txtThirdCommandArgument.Text.Replace(zamienZ, zamienNa).Replace("E", Math.E.ToString()));
                                trzeci = kalkulator.ObliczWnetrze();
                            }
                            catch
                            {
                                throw new BesseleTrzeciArgumentException();
                            }
                        }
                    }

                    if (cmbSpecialFunction.SelectedIndex == 8)
                    {
                        try
                        {
                            if (thtFourthCommandArgument.Text == "x")
                                czwarty = double.NaN;
                            else
                                czwarty = double.Parse(thtFourthCommandArgument.Text.Replace(zamienZ, zamienNa));
                        }
                        catch (Exception)
                        {
                            //Sprawdzenie może da się oszacować
                            try
                            {
                                Kalkulator kalkulator = new Kalkulator(thtFourthCommandArgument.Text.Replace(zamienZ, zamienNa).Replace("E", Math.E.ToString()));
                                czwarty = kalkulator.ObliczWnetrze();
                            }
                            catch
                            {
                                throw new BesseleCzwartyArgumentException();
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
                            tfb = TypFunkcjiBessela.BesselSferyczny;
                            break;
                        case 2:
                            tfb = TypFunkcjiBessela.BesselPochodnaSferycznej;
                            break;
                        case 3:
                            tfb = TypFunkcjiBessela.Neumann;
                            break;
                        case 4:
                            tfb = TypFunkcjiBessela.NeumannSferyczny;
                            break;
                        case 5:
                            tfb = TypFunkcjiBessela.NeumannPochodnaSferycznej;
                            break;
                        case 6:
                            tfb = TypFunkcjiBessela.Hipergeometryczna01;
                            break;
                        case 7:
                            tfb = TypFunkcjiBessela.Hipergeometryczna11;
                            break;
                        case 8:
                            tfb = TypFunkcjiBessela.Hipergeometryczna21;
                            break;
                        default:
                            break;
                    }
                }

                //Utworzenie klasy
                Wykres wykres = new Wykres(funkcja, picWykres, xOd, xDo, yOd, yDo);

                //Reskalling
                if (chkRescaling.Checked && chkRescaling.Enabled && sender is Button)
                {
                    //Zbudowanie listy typow funkcji
                    List<TypFunkcji> typyFunkcji = new List<TypFunkcji>();

                    if (chkFunction.Checked)
                        typyFunkcji.Add(TypFunkcji.Funkcja);

                    if (chkFirstDerivative.Checked)
                        typyFunkcji.Add(TypFunkcji.Pochodna);

                    if (chkSecondDerivative.Checked)
                        typyFunkcji.Add(TypFunkcji.DrugaPochodna);

                    double[] reskalling = null;

                    //Obliczenie maxów i minów do reskalingu
                    if (chkSpecialFunction.Checked && chkSpecialFunction.Enabled)
                        reskalling = wykres.Reskalling(tfb, pierwszy, drugi, trzeci, czwarty);
                    else
                        reskalling = wykres.Reskalling(typyFunkcji.ToArray()); //normlanych

                    xOd = reskalling[0];
                    xDo = reskalling[1];
                    yOd = reskalling[2];
                    yDo = reskalling[3];

                    txtXFrom.Text = xOd.ToString();
                    txtXTo.Text = xDo.ToString();
                    txtYFrom.Text = yOd.ToString();
                    txtYTo.Text = yDo.ToString();

                    wykres = new Wykres(funkcja, picWykres, xOd, xDo, yOd, yDo);
                }

                //Rysowanie funkcji i pochodnych
                if (chkFunction.Checked && chkFunction.Enabled)
                    wykres.Rysuj(TypFunkcji.Funkcja);

                if (chkFirstDerivative.Checked && chkFirstDerivative.Enabled)
                    wykres.Rysuj(TypFunkcji.Pochodna);

                if (chkSecondDerivative.Checked && chkSecondDerivative.Enabled)
                    wykres.Rysuj(TypFunkcji.DrugaPochodna);

                //Rysowanie FFT
                int probkowanie = 1000;
                double odciecie = 0.0;

                //probki
                if ((chkFT.Checked && chkFT.Enabled) || (chkIFT.Checked && chkIFT.Enabled))
                {
                    string probkowanieString = txtSampling.Text;
                    string odciecieString = txtCutoff.Text;

                    if (!int.TryParse(probkowanieString, out probkowanie))
                        throw new ProbkowanieValueException();

                    double temp;

                    if (string.IsNullOrEmpty(odciecieString))
                        odciecie = 0.0;
                    else
                    {
                        if (!double.TryParse(odciecieString, out temp))
                            throw new FiltrValueException();
                        else
                            odciecie = temp;
                    }
                }

                if (chkFT.Checked && chkFT.Enabled)
                    wykres.RysujFFT(TypFunkcji.FFT, probkowanie, odciecie);

                if (chkIFT.Checked && chkIFT.Enabled)
                    wykres.RysujFFT(TypFunkcji.RFFT, probkowanie, odciecie);

                //Rysowanie rozniczek
                if (chkDifferential.Checked && chkDifferential.Enabled)
                {
                    double from, to;

                    try
                    {
                        from = Convert.ToDouble(txtFrom.Text.Replace(zamienZ, zamienNa));
                    }
                    catch (Exception)
                    {
                        //Sprawdzenie może da się oszacować
                        try
                        {
                            Kalkulator kalkulator = new Kalkulator(txtFrom.Text.Replace(zamienZ, zamienNa).Replace("E", Math.E.ToString()));
                            from = kalkulator.ObliczWnetrze();

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
                        to = Convert.ToDouble(txtTo.Text.Replace(zamienZ, zamienNa));
                    }
                    catch (Exception)
                    {
                        //Sprawdzenie może da się oszacować
                        try
                        {
                            Kalkulator kalkulator = new Kalkulator(txtTo.Text.Replace(zamienZ, zamienNa).Replace("E", Math.E.ToString()));
                            to = kalkulator.ObliczWnetrze();

                            if (txtTo.Text.Contains('E'))
                                MessageBox.Show("Zinterpretowano E jako liczbę Eulera!", "Uwaga!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch
                        {
                            throw new ToConversionException();
                        }
                    }

                    wykres.RysujRozniczke(TypFunkcji.Rozniczka, from, to);
                }

                if (chkDifferentialII.Checked && chkDifferentialII.Enabled)
                {
                    double from, to, fromII, toII;

                    try
                    {
                        from = Convert.ToDouble(txtFrom.Text.Replace(zamienZ, zamienNa));
                    }
                    catch (Exception)
                    {
                        //Sprawdzenie może da się oszacować
                        try
                        {
                            Kalkulator kalkulator = new Kalkulator(txtFrom.Text.Replace(zamienZ, zamienNa).Replace("E", Math.E.ToString()));
                            from = kalkulator.ObliczWnetrze();

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
                        fromII = Convert.ToDouble(txtOdII.Text.Replace(zamienZ, zamienNa));
                    }
                    catch (Exception)
                    {
                        //Sprawdzenie może da się oszacować
                        try
                        {
                            Kalkulator kalkulator = new Kalkulator(txtOdII.Text.Replace(zamienZ, zamienNa).Replace("E", Math.E.ToString()));
                            fromII = kalkulator.ObliczWnetrze();

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
                        to = Convert.ToDouble(txtTo.Text.Replace(zamienZ, zamienNa));
                    }
                    catch (Exception)
                    {
                        //Sprawdzenie może da się oszacować
                        try
                        {
                            Kalkulator kalkulator = new Kalkulator(txtTo.Text.Replace(zamienZ, zamienNa).Replace("E", Math.E.ToString()));
                            to = kalkulator.ObliczWnetrze();

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
                        toII = Convert.ToDouble(txtDoII.Text.Replace(zamienZ, zamienNa));
                    }
                    catch (Exception)
                    {
                        //Sprawdzenie może da się oszacować
                        try
                        {
                            Kalkulator kalkulator = new Kalkulator(txtDoII.Text.Replace(zamienZ, zamienNa).Replace("E", Math.E.ToString()));
                            toII = kalkulator.ObliczWnetrze();

                            if (txtTo.Text.Contains('E'))
                                MessageBox.Show("Zinterpretowano E jako liczbę Eulera!", "Uwaga!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch
                        {
                            throw new ToConversionException();
                        }
                    }

                    wykres.RysujRozniczke(TypFunkcji.RozniczkaII, from, to, fromII, toII);
                }

                if (chkSpecialFunction.Checked && chkSpecialFunction.Enabled)
                    wykres.RysujBessele(tfb, pierwszy, drugi, trzeci, czwarty);

                //Zakończenie
                czyFunkcjaNarysowana = true;

                //Pobranie punktow wykresu
                punktyWykresu = wykres.PunktyWykresu;
            }
            catch (XOdWiekszeNizXDoException)
            {
                ObsluzExceptionWykres("Wartości skali x są niepoprawne. Wartość początkowa skali nie może być większa (lub równa) od wartości końcowej.");

                txtXFrom.Focus();
            }
            catch (YOdWiekszeNizYDoException)
            {
                ObsluzExceptionWykres("Wartości skali y są niepoprawne. Wartość początkowa skali nie może być większa (lub równa) od wartości końcowej.");

                txtYFrom.Focus();
            }
            catch (OverflowException)
            {
                ObsluzExceptionWykres("Nie można narysować tej funkcji w tym przedziale.");
            }
            catch (xOdException)
            {
                ObsluzExceptionWykres("Niepoprawna wartość od osi x");

                txtXFrom.Focus();
            }
            catch (xDoException)
            {
                ObsluzExceptionWykres("Niepoprawna wartość do osi x");

                txtXTo.Focus();
            }
            catch (yOdException)
            {
                ObsluzExceptionWykres("Niepoprawna wartość od osi y");

                txtYFrom.Focus();
            }
            catch (yDoException)
            {
                ObsluzExceptionWykres("Niepoprawna wartość do osi y");

                txtYTo.Focus();
            }
            catch (WspolrzedneXException excep)
            {
                ObsluzExceptionWykres(excep.Message);

                txtXFrom.Focus();
            }
            catch (WspolrzedneYException excep)
            {
                ObsluzExceptionWykres(excep.Message);

                txtYFrom.Focus();
            }
            catch (BrakFunkcjiException)
            {
                ObsluzExceptionWykres("Wpisz funkcję!");

                txtFunction.Focus();
            }
            catch (FromConversionException)
            {
                ObsluzExceptionWykres("Niepoprawny punkt x w pierwszym warunku!");

                txtFrom.Focus();
            }
            catch (FromIIConversionException)
            {
                ObsluzExceptionWykres("Niepoprawny punkt x w drugim warunku!");

                txtOdII.Focus();
            }
            catch (ToConversionException)
            {
                ObsluzExceptionWykres("Niepoprawna wartość w pierwszym warunku!");

                txtTo.Focus();
            }
            catch (ToIIConversionException)
            {
                ObsluzExceptionWykres("Niepoprawna wartość w drugim warunku!");

                txtDoII.Focus();
            }
            catch (BesselePierwszyArgumentException excep)
            {
                ObsluzExceptionWykres(excep.Message);

                txtFirstCommandArgument.Focus();
            }
            catch (BesseleDrugiArgumentException excep)
            {
                ObsluzExceptionWykres(excep.Message);

                txtSecondCommandArgument.Focus();
            }
            catch (BesseleTrzeciArgumentException excep)
            {
                ObsluzExceptionWykres(excep.Message);

                txtThirdCommandArgument.Focus();
            }
            catch (BesseleCzwartyArgumentException excep)
            {
                ObsluzExceptionWykres(excep.Message);

                thtFourthCommandArgument.Focus();
            }
            catch (NoneWykresOptionCheckedException excep)
            {
                ObsluzExceptionWykres(excep.Message);
            }
            catch (FiltrValueException excep)
            {
                ObsluzExceptionWykres(excep.Message);

                txtCutoff.Focus();
            }
            catch (SystemException)
            {
                ObsluzExceptionWykres("Wystąpił nieoczekiwany wyjątek. Sprawdź poprawność wprowadzonej formuły!");
            }
            catch (Exception)
            {
                ObsluzExceptionWykres("Wystąpił nieoczekiwany wyjątek. Sprawdź poprawność wprowadzonej formuły!");
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
                        xOd = Convert.ToDouble(txtXFrom.Text.Replace(zamienZ, zamienNa));
                        xDo = Convert.ToDouble(txtXTo.Text.Replace(zamienZ, zamienNa));
                        yOd = Convert.ToDouble(txtYFrom.Text.Replace(zamienZ, zamienNa));
                        yDo = Convert.ToDouble(txtYTo.Text.Replace(zamienZ, zamienNa));
                    }
                    catch (SystemException)
                    {
                        MessageBox.Show("Bledne wartosci X i Y", "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    double wspX = 0, wspY = 0;
                    punktKoncowy = e.Location;

                    // Obliczanie wspolczynnikow X
                    if (xOd * xDo <= 0)
                        wspX = picWykres.Width / (-xOd + xDo);
                    else if (xOd < 0)
                        wspX = picWykres.Width / (-xOd + xDo);
                    else
                        wspX = picWykres.Width / (xDo - xOd);

                    // Obliczanie wspolczynnikow Y
                    if (yOd * yDo <= 0)
                        wspY = picWykres.Height / (-yOd + yDo);
                    else if (yOd < 0 && yDo < 0)
                        wspY = picWykres.Height / (-yOd + yDo);
                    else
                        wspY = picWykres.Height / (yDo - yOd);

                    //Ustalenie przesuniecia
                    double roznicaX = (punktKoncowy.X - punktPoczatkowy.X) / wspX;
                    double roznicaY = ((punktKoncowy.Y - picWykres.Width) - (punktPoczatkowy.Y - picWykres.Width)) / wspY;

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
                    if (czyFunkcjaNarysowana/* && podgladWykresuPodczasSkalowaniaOnkaToolStripMenuItem.Checked*/)
                        btnRysuj_Click(this, new EventArgs());
                    else
                        WyczyscWykres();
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
                    if (double.IsPositiveInfinity(Convert.ToDouble(txtXFrom.Text.Replace(zamienZ, zamienNa))))
                        txtXFrom.Text = max.ToString();
                    if (double.IsPositiveInfinity(Convert.ToDouble(txtXTo.Text.Replace(zamienZ, zamienNa))))
                        txtXTo.Text = max.ToString();
                    if (double.IsPositiveInfinity(Convert.ToDouble(txtYFrom.Text.Replace(zamienZ, zamienNa))))
                        txtYFrom.Text = max.ToString();
                    if (double.IsPositiveInfinity(Convert.ToDouble(txtYTo.Text.Replace(zamienZ, zamienNa))))
                        txtYTo.Text = max.ToString();
                    if (double.IsNegativeInfinity(Convert.ToDouble(txtXFrom.Text.Replace(zamienZ, zamienNa))))
                        txtXFrom.Text = min.ToString();
                    if (double.IsNegativeInfinity(Convert.ToDouble(txtXTo.Text.Replace(zamienZ, zamienNa))))
                        txtXTo.Text = min.ToString();
                    if (double.IsNegativeInfinity(Convert.ToDouble(txtYFrom.Text.Replace(zamienZ, zamienNa))))
                        txtYFrom.Text = min.ToString();
                    if (double.IsNegativeInfinity(Convert.ToDouble(txtYTo.Text.Replace(zamienZ, zamienNa))))
                        txtYTo.Text = min.ToString();

                    try
                    {
                        xOd = Math.Round(Convert.ToDouble(txtXFrom.Text.Replace(zamienZ, zamienNa)), 2);
                    }
                    catch (Exception)
                    {
                        throw new xOdException();
                    }

                    try
                    {
                        xDo = Math.Round(Convert.ToDouble(txtXTo.Text.Replace(zamienZ, zamienNa)), 2);
                    }
                    catch (Exception)
                    {
                        throw new xDoException();
                    }

                    try
                    {
                        yOd = Math.Round(Convert.ToDouble(txtYFrom.Text.Replace(zamienZ, zamienNa)), 2);
                    }
                    catch (Exception)
                    {
                        throw new yOdException();
                    }

                    try
                    {
                        yDo = Math.Round(Convert.ToDouble(txtYTo.Text.Replace(zamienZ, zamienNa)), 2);
                    }
                    catch (Exception)
                    {
                        throw new yDoException();
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

                    if (czyFunkcjaNarysowana/* && podgladWykresuPodczasSkalowaniaOnkaToolStripMenuItem.Checked*/)
                        btnRysuj_Click(this, new EventArgs());
                    else
                        WyczyscWykres();
                }
                catch (xOdException)
                {
                    ObsluzException("Niepoprawna wartość od osi x");

                    txtXFrom.Focus();
                }
                catch (xDoException)
                {
                    ObsluzException("Niepoprawna wartość do osi x");

                    txtXTo.Focus();
                }
                catch (yOdException)
                {
                    ObsluzException("Niepoprawna wartość od osi y");

                    txtYFrom.Focus();
                }
                catch (yDoException)
                {
                    ObsluzException("Niepoprawna wartość do osi y");

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
                    Image image = picWykres.Image;
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
                Image image = picWykres.Image;
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
            if (punktyWykresu != null && punktyWykresu.Length > 0)
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
                        foreach (PointF p in punktyWykresu)
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

                picWykres.Height = 350 + gainH;
                picWykres.Width = 400 + gainW;
                pnlWykres.Height = 350 + gainH;
                pnlWykres.Width = 400 + gainW;
                gbDrawFunction.Left = 759 + gainW;
                gbScale.Left = 759 + gainW;
                btnDraw.Left = 824 + gainW;

                if (czyFunkcjaNarysowana && graphPreviewWhileWindowsScalingToolStripMenuItem.Checked)
                    btnRysuj_Click(this, new EventArgs());
                else
                    WyczyscWykres();
            }            
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            czyFunkcjaNarysowana = false;
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
                thtFourthCommandArgument.Visible = false;

                cmbSpecialFunction.Width = 126;

                txtFirstCommandArgument.Left = 133;
                txtFirstCommandArgument.Width = 90;

                txtSecondCommandArgument.Left = 226;
                txtSecondCommandArgument.Width = 90;
            }
            else if (index == 6)
            {
                txtThirdCommandArgument.Visible = false;
                thtFourthCommandArgument.Visible = false;

                cmbSpecialFunction.Width = 162;

                txtFirstCommandArgument.Left = 169;
                txtFirstCommandArgument.Width = 72;

                txtSecondCommandArgument.Left = 244;
                txtSecondCommandArgument.Width = 72;
            }
            else if (index == 7)
            {
                txtThirdCommandArgument.Visible = true;
                thtFourthCommandArgument.Visible = false;

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
                thtFourthCommandArgument.Visible = true;

                cmbSpecialFunction.Width = 99;

                txtFirstCommandArgument.Left = 107;
                txtFirstCommandArgument.Width = 50;

                txtSecondCommandArgument.Left = 160;
                txtSecondCommandArgument.Width = 50;

                txtThirdCommandArgument.Left = 213;
                txtThirdCommandArgument.Width = 50;

                thtFourthCommandArgument.Left = 266;
                thtFourthCommandArgument.Width = 50;
            }
        }

        private void btnFunkcjaSpecjalna_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Wystarczy zaznaczyć funkcję specjalną w sekcji wyboru operacji i za jeden z jej argumentów wstawić zmienną x.", "Podpowiedź", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void txtOd_TextChanged(object sender, EventArgs e)
        {
            if ((sender as TextBox).Name == "txtOd")
                txtOdII.Text = txtFrom.Text;
            else
                txtFrom.Text = txtOdII.Text;
        }
    }
}
