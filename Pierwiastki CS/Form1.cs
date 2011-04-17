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
using Pierwiastki_CS.Properties;

namespace Pierwiastki_CS
{
    public partial class Form1 : Form
    {
        bool czyFunkcjaNarysowana = false;
        public string zamienZ, zamienNa; //kropki i przecinki do zamieniania podczas konwersji string na double

        readonly double max = 530000000.0;
        readonly double min = -530000000.0;

        Settings settings;

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

            cmbFunkcjaSpecjalna.SelectedIndex = 0;

            //Ustawienia
            settings = new Settings();

            try
            {
                UstawSettings(settings);
            }
            catch (BrakUstawieniaException)
            {
                settings.PrzywrocUstawieniaDomyslne();
                UstawSettings(settings);
            }

            //Dodatkowe zdarzenia
            this.wykresToolStripMenuItem.Click += new System.EventHandler(this.ZmienUstawinia);
            this.btnPomoc.Click += new EventHandler(PokazFunkcjeForm_Handler);
            this.chkEnergia.CheckedChanged += new EventHandler(radioButton_CheckedChanged);
            this.chkFFT.CheckedChanged += new EventHandler(radioButton_CheckedChanged);
            this.chkRFFT.CheckedChanged += new EventHandler(radioButton_CheckedChanged);

            //Zrobienie gui
            radioButton_CheckedChanged(null, new EventArgs());
        }

        private void UstawSettings(Settings settings)
        {
            wykresToolStripMenuItem.Checked = (bool)settings[Setting.WykresMenuChecked];

            if (!wykresToolStripMenuItem.Checked)
                wykresToolStripMenuItem_Click(null, new EventArgs());

            podgladWykresuPodczasSkalowaniaOnkaToolStripMenuItem.Checked = (bool)settings[Setting.PodgladWykresuMenuChecked];
            chkFunkcja.Checked = (bool)settings[Setting.FunkcjaChecked];
            chkPierwszaPochodna.Checked = (bool)settings[Setting.PierwszaPochodnaChecked];
            chkDrugaPochodna.Checked = (bool)settings[Setting.DrugaPochodnaChecked];
            chkRozniczka.Checked = (bool)settings[Setting.RozniczkaChecked];
            chkRozniczkaII.Checked = (bool)settings[Setting.RozniczkaIIChecked];
            chkEnergia.Checked = (bool)settings[Setting.EnergiaChecked];
            chkFunkcjaSpecjalna.Checked = (bool)settings[Setting.FunkcjaSpecjalnaChecked];
            chkReskalling.Checked = (bool)settings[Setting.AutomatycznyReskallingChecked];
            chkFFT.Checked = (bool)settings[Setting.FFTChecked];
            chkRFFT.Checked = (bool)settings[Setting.RFFTChecked];
        }

        /// <summary>
        /// USTAWIANIE RADIO BUTONOW
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            //Zarzadzanie GUI procz checkboxow
            if (rbRozniczkaII.Checked)
            {
                lblFx.Text = "f''(x) =";

                gbWarunki.Text = "Warunki";
                gbWarunki.Width = 163;

                pnlWarunki.Width = 163;

                lblOd.Text = "f (";
                lblDo.Text = ") =";

                txtOd.Enabled = true;
                txtDo.Enabled = true;
                txtPunkt.Enabled = true;

                txtOd.Width = 44;
                txtDo.Width = 44;

                gbWarunkiII.Enabled = true;
                gbWarunkiII.Visible = true;

                lblOd.Left = 6;
                lblDo.Left = 80;
                txtOd.Left = 30;
                txtDo.Left = 108;

                txtOd.Enabled = !chkEnergia.Checked;

                txtOdII.Enabled = !chkEnergia.Checked;
                txtDoII.Enabled = !chkEnergia.Checked;

                if (chkEnergia.Checked)
                {
                    txtOd.Text = "0";
                    txtDoII.Text = "0";
                }

                chkEnergia.Enabled = true;
            }
            else
            {
                lblFx.Text = "f(x) =";

                lblOd.Text = "od";
                lblDo.Text = "do";

                txtOd.Width = 96;
                txtDo.Width = 96;

                txtOd.Enabled = true;

                gbWarunki.Text = "Warunki";
                gbWarunki.Width = 332;

                pnlWarunki.Width = 332;

                lblOd.Left = 37;
                lblDo.Left = 169;
                txtOd.Left = 67;
                txtDo.Left = 199;

                chkEnergia.Enabled = false;
            }

            if (rbRozniczka.Checked)
            {
                lblFx.Text = "f'(x) =";

                gbWarunki.Text = "Warunki";

                lblOd.Text = "f(";
                lblDo.Text = ") =";

                txtOd.Enabled = true;
                txtDo.Enabled = true;
                txtPunkt.Enabled = true;

                lblOd.Left = 43;
                lblDo.Left = 166;
                txtOd.Left = 64;
                txtDo.Left = 194;
            }
            else if (rbCalka.Checked)
            {
                lblFx.Text = "f(x) =";

                gbWarunki.Text = "Granice";

                lblOd.Text = "dolna";
                lblDo.Text = "górna";

                lblOd.Left = 19;
                lblDo.Left = 169;
                txtOd.Left = 67;
                txtDo.Left = 218;
            }
            else if (!rbRozniczkaII.Checked)
            {
                lblFx.Text = "f(x) =";

                lblOd.Text = "od";
                lblDo.Text = "do";

                gbWarunki.Text = "Warunki";

                lblOd.Left = 37;
                lblDo.Left = 169;
                txtOd.Left = 67;
                txtDo.Left = 199;
            }

            if (rbFunkcjaSpecjalna.Checked)
            {
                gbFunkcja.Text = "Funkcja specjalna";

                pnlFunkcja.Visible = false;
                pnlKomenda.Visible = true;

                txtOd.Enabled = false;
                txtDo.Enabled = false;
                txtPunkt.Enabled = false;                
            }
            else
            {
                gbFunkcja.Text = "Funkcja";

                pnlFunkcja.Visible = true;
                pnlKomenda.Visible = false;

                cmbFunkcjaSpecjalna.Visible = true;
            }

            if (rbHybryda.Checked || rbCalka.Checked)
            {
                txtOd.Enabled = true;
                txtDo.Enabled = true;
                txtPunkt.Enabled = false;
            }
            else if (rbPunkt.Checked || rbPochodnaPunkt.Checked || rbPunktPochodnaBis.Checked)
            {
                txtOd.Enabled = false;
                txtDo.Enabled = false;
                txtPunkt.Enabled = true;
            }
            else if (rbKalkulator.Checked)
            {
                txtOd.Enabled = false;
                txtDo.Enabled = false;
                txtPunkt.Enabled = false;
            }

            if (rbKalkulator.Checked)
            {
                lblFx.Text = string.Empty;

                txtFunkcja.Width = 278;
                txtFunkcja.Left = 9;
            }
            else if (!rbRozniczka.Checked && !rbRozniczkaII.Checked)
            {
                lblFx.Text = "f(x) =";

                txtFunkcja.Width = 243;
                txtFunkcja.Left = 44;
            }
            else
            {
                txtFunkcja.Width = 243;
                txtFunkcja.Left = 44;
            }

            //Checkboxy
            if (rbRozniczka.Checked)
            {
                chkFunkcja.Enabled = false;
                chkPierwszaPochodna.Enabled = false;
                chkDrugaPochodna.Enabled = false;
                chkRozniczka.Enabled = true;
                chkRozniczkaII.Enabled = false;
                chkFunkcjaSpecjalna.Enabled = false;
                chkFFT.Enabled = false;
                chkRFFT.Enabled = false;

                txtProbkowanie.Enabled = false;
                txtOdciecie.Enabled = false;

                lblProbkowanie.Enabled = false;
                lblOdciecie.Enabled = false;

                chkReskalling.Enabled = false;
            }
            else if (rbRozniczkaII.Checked)
            {
                chkFunkcja.Enabled = false;
                chkPierwszaPochodna.Enabled = false;
                chkDrugaPochodna.Enabled = false;
                chkRozniczka.Enabled = false;
                chkRozniczkaII.Enabled = true;
                chkFunkcjaSpecjalna.Enabled = false;
                chkFFT.Enabled = false;
                chkRFFT.Enabled = false;

                txtProbkowanie.Enabled = false;
                txtOdciecie.Enabled = false;

                lblProbkowanie.Enabled = false;
                lblOdciecie.Enabled = false;

                chkReskalling.Enabled = false;
            }
            else if (rbFunkcjaSpecjalna.Checked)
            {
                chkFunkcja.Enabled = false;
                chkPierwszaPochodna.Enabled = false;
                chkDrugaPochodna.Enabled = false;
                chkRozniczka.Enabled = false;
                chkRozniczkaII.Enabled = false;
                chkFunkcjaSpecjalna.Enabled = true;
                chkFFT.Enabled = false;
                chkRFFT.Enabled = false;

                txtProbkowanie.Enabled = false;
                txtOdciecie.Enabled = false;

                lblProbkowanie.Enabled = false;
                lblOdciecie.Enabled = false;

                chkReskalling.Enabled = true;
            }
            else
            {
                chkFunkcja.Enabled = true;
                chkPierwszaPochodna.Enabled = true;
                chkDrugaPochodna.Enabled = true;
                chkRozniczka.Enabled = false;
                chkRozniczkaII.Enabled = false;
                chkFunkcjaSpecjalna.Enabled = false;
                chkFFT.Enabled = true;
                chkRFFT.Enabled = true;

                txtProbkowanie.Enabled = true;
                txtOdciecie.Enabled = true;

                lblProbkowanie.Enabled = true;
                lblOdciecie.Enabled = true;

                chkReskalling.Enabled = true;
            }

            //Wyłączenie reskallingu gdy FFT
            if ((chkFFT.Checked && chkFFT.Enabled) || (chkRFFT.Checked && chkRFFT.Enabled))
                chkReskalling.Enabled = false;

            //Jak FFT to nie RFFT i na odwrot
            if (chkFFT.Checked && chkFFT.Enabled)
            {
                chkFunkcja.Enabled = false;
                chkPierwszaPochodna.Enabled = false;
                chkDrugaPochodna.Enabled = false;
                chkRFFT.Enabled = false;
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

                if (!rbFunkcjaSpecjalna.Checked)
                {
                    funkcja = txtFunkcja.Text.Replace(zamienZ, zamienNa);

                    if (string.IsNullOrEmpty(funkcja))
                    {
                        throw new BrakFunkcjiException();
                    }
                }                

                if (rbKalkulator.Checked)
                {
                    Kalkulator kalkulator = new Kalkulator(funkcja);
                    txtWynik.Text = kalkulator.ObliczWnetrze().ToString();
                }
                else if (rbPunkt.Checked)
                {
                    double x;

                    try
                    {
                        x = Convert.ToDouble(txtPunkt.Text.Replace(zamienZ, zamienNa));
                    }
                    catch (Exception)
                    {
                        //Sprawdzenie może da się oszacować z E jako Euler
                        try
                        {
                            Kalkulator kalkulator = new Kalkulator(txtPunkt.Text.Replace(zamienZ, zamienNa).Replace("E", Math.E.ToString()));
                            x = kalkulator.ObliczWnetrze();

                            if (txtPunkt.Text.Contains('E'))
                                MessageBox.Show("Zinterpretowano E jako liczbę Eulera!", "Uwaga!", MessageBoxButtons.OK, MessageBoxIcon.Information);  
                        }
                        catch
                        {
                            throw new PunktConversionException();
                        }                            
                    }

                    Pochodna punkt = new Pochodna(funkcja, x);
                    txtWynik.Text = punkt.ObliczFunkcjeWPunkcie().ToString();
                }
                else if (rbPochodnaPunkt.Checked)
                {
                    double punkt;

                    try
                    {
                        punkt = Convert.ToDouble(txtPunkt.Text.Replace(zamienZ, zamienNa));
                    }
                    catch (Exception)
                    {
                        //Sprawdzenie może da się oszacować
                        try
                        {
                            Kalkulator kalkulator = new Kalkulator(txtPunkt.Text.Replace(zamienZ, zamienNa).Replace("E", Math.E.ToString()));
                            punkt = kalkulator.ObliczWnetrze();

                            if (txtPunkt.Text.Contains('E'))
                                MessageBox.Show("Zinterpretowano E jako liczbę Eulera!", "Uwaga!", MessageBoxButtons.OK, MessageBoxIcon.Information);  
                        }
                        catch
                        {
                            throw new PunktConversionException();
                        }
                    }

                    Pochodna pochodna = new Pochodna(funkcja, punkt);
                    txtWynik.Text = pochodna.ObliczPochodna().ToString();
                }
                else if (rbPunktPochodnaBis.Checked)
                {
                    double punkt;

                    try
                    {
                        punkt = Convert.ToDouble(txtPunkt.Text.Replace(zamienZ, zamienNa));
                    }
                    catch (Exception)
                    {
                        //Sprawdzenie może da się oszacować
                        try
                        {
                            Kalkulator kalkulator = new Kalkulator(txtPunkt.Text.Replace(zamienZ, zamienNa).Replace("E", Math.E.ToString()));
                            punkt = kalkulator.ObliczWnetrze();

                            if (txtPunkt.Text.Contains('E'))
                                MessageBox.Show("Zinterpretowano E jako liczbę Eulera!", "Uwaga!", MessageBoxButtons.OK, MessageBoxIcon.Information);  
                        }
                        catch
                        {
                            throw new PunktConversionException();
                        }
                    }

                    Pochodna pochodnaBis = new Pochodna(funkcja, punkt);
                    txtWynik.Text = pochodnaBis.ObliczPochodnaBis().ToString();
                }
                else if (rbHybryda.Checked)
                {
                    double from, to;

                    try
                    {
                        from = Convert.ToDouble(txtOd.Text.Replace(zamienZ, zamienNa));
                    }
                    catch (Exception)
                    {
                        //Sprawdzenie może da się oszacować
                        try
                        {
                            Kalkulator kalkulator = new Kalkulator(txtOd.Text.Replace(zamienZ, zamienNa).Replace("E", Math.E.ToString()));
                            from = kalkulator.ObliczWnetrze();

                            if (txtOd.Text.Contains('E'))
                                MessageBox.Show("Zinterpretowano E jako liczbę Eulera!", "Uwaga!", MessageBoxButtons.OK, MessageBoxIcon.Information);  
                        }
                        catch
                        {
                            throw new FromConversionException();
                        }
                    }

                    try
                    {
                        to = Convert.ToDouble(txtDo.Text.Replace(zamienZ, zamienNa));
                    }
                    catch (Exception)
                    {
                        //Sprawdzenie może da się oszacować
                        try
                        {
                            Kalkulator kalkulator = new Kalkulator(txtDo.Text.Replace(zamienZ, zamienNa).Replace("E", Math.E.ToString()));
                            to = kalkulator.ObliczWnetrze();

                            if (txtDo.Text.Contains('E'))
                                MessageBox.Show("Zinterpretowano E jako liczbę Eulera!", "Uwaga!", MessageBoxButtons.OK, MessageBoxIcon.Information);  
                        }
                        catch
                        {
                            throw new ToConversionException();
                        }
                    }

                    Hybryda hybryda = new Hybryda(funkcja, from, to);
                    txtWynik.Text = hybryda.ObliczWnetrze().ToString();
                }
                else if (rbCalka.Checked)
                {
                    double from, to;

                    try
                    {
                        from = Convert.ToDouble(txtOd.Text.Replace(zamienZ, zamienNa));
                    }
                    catch (Exception)
                    {
                        //Sprawdzenie może da się oszacować
                        try
                        {
                            Kalkulator kalkulator = new Kalkulator(txtOd.Text.Replace(zamienZ, zamienNa).Replace("E", Math.E.ToString()));
                            from = kalkulator.ObliczWnetrze();

                            if (txtOd.Text.Contains('E'))
                                MessageBox.Show("Zinterpretowano E jako liczbę Eulera!", "Uwaga!", MessageBoxButtons.OK, MessageBoxIcon.Information);  
                        }
                        catch
                        {
                            throw new FromConversionException();
                        }
                    }

                    try
                    {
                        to = Convert.ToDouble(txtDo.Text.Replace(zamienZ, zamienNa));
                    }
                    catch (Exception)
                    {
                        //Sprawdzenie może da się oszacować
                        try
                        {
                            Kalkulator kalkulator = new Kalkulator(txtDo.Text.Replace(zamienZ, zamienNa).Replace("E", Math.E.ToString()));
                            to = kalkulator.ObliczWnetrze();

                            if (txtDo.Text.Contains('E'))
                                MessageBox.Show("Zinterpretowano E jako liczbę Eulera!", "Uwaga!", MessageBoxButtons.OK, MessageBoxIcon.Information);  
                        }
                        catch
                        {
                            throw new ToConversionException();
                        }
                    }

                    Calka calka = new Calka(funkcja, from, to);
                    txtWynik.Text = calka.ObliczWnetrze().ToString();
                }
                else if (rbRozniczka.Checked)
                {
                    double punkt, from, to;

                    try
                    {
                        punkt = Convert.ToDouble(txtPunkt.Text.Replace(zamienZ, zamienNa));
                    }
                    catch (Exception)
                    {
                        //Sprawdzenie może da się oszacować
                        try
                        {
                            Kalkulator kalkulator = new Kalkulator(txtPunkt.Text.Replace(zamienZ, zamienNa).Replace("E", Math.E.ToString()));
                            punkt = kalkulator.ObliczWnetrze();

                            if (txtPunkt.Text.Contains('E'))
                                MessageBox.Show("Zinterpretowano E jako liczbę Eulera!", "Uwaga!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch
                        {
                            throw new PunktConversionException();
                        }
                    }

                    try
                    {
                        from = Convert.ToDouble(txtOd.Text.Replace(zamienZ, zamienNa));
                    }
                    catch (Exception)
                    {
                        //Sprawdzenie może da się oszacować
                        try
                        {
                            Kalkulator kalkulator = new Kalkulator(txtOd.Text.Replace(zamienZ, zamienNa).Replace("E", Math.E.ToString()));
                            from = kalkulator.ObliczWnetrze();

                            if (txtOd.Text.Contains('E'))
                                MessageBox.Show("Zinterpretowano E jako liczbę Eulera!", "Uwaga!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch
                        {
                            throw new FromConversionException();
                        }
                    }

                    try
                    {
                        to = Convert.ToDouble(txtDo.Text.Replace(zamienZ, zamienNa));
                    }
                    catch (Exception)
                    {
                        //Sprawdzenie może da się oszacować
                        try
                        {
                            Kalkulator kalkulator = new Kalkulator(txtDo.Text.Replace(zamienZ, zamienNa).Replace("E", Math.E.ToString()));
                            to = kalkulator.ObliczWnetrze();

                            if (txtDo.Text.Contains('E'))
                                MessageBox.Show("Zinterpretowano E jako liczbę Eulera!", "Uwaga!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch
                        {
                            throw new ToConversionException();
                        }
                    }

                    Rozniczka rozniczka = new Rozniczka(funkcja);
                    List<PointD> punkty = rozniczka.ObliczRozniczke(punkt, from, to);

                    txtWynik.Text = punkty.Last().Y.ToString();
                }
                else if (rbRozniczkaII.Checked)
                {
                    double punkt, from, to, fromII, toII;

                    try
                    {
                        punkt = Convert.ToDouble(txtPunkt.Text.Replace(zamienZ, zamienNa));
                    }
                    catch (Exception)
                    {
                        //Sprawdzenie może da się oszacować
                        try
                        {
                            Kalkulator kalkulator = new Kalkulator(txtPunkt.Text.Replace(zamienZ, zamienNa).Replace("E", Math.E.ToString()));
                            punkt = kalkulator.ObliczWnetrze();

                            if (txtPunkt.Text.Contains('E'))
                                MessageBox.Show("Zinterpretowano E jako liczbę Eulera!", "Uwaga!", MessageBoxButtons.OK, MessageBoxIcon.Information);  
                        }
                        catch
                        {
                            throw new PunktConversionException();
                        }
                    }

                    try
                    {
                        from = Convert.ToDouble(txtOd.Text.Replace(zamienZ, zamienNa));
                    }
                    catch (Exception)
                    {
                        //Sprawdzenie może da się oszacować
                        try
                        {
                            Kalkulator kalkulator = new Kalkulator(txtOd.Text.Replace(zamienZ, zamienNa).Replace("E", Math.E.ToString()));
                            from = kalkulator.ObliczWnetrze();

                            if (txtOd.Text.Contains('E'))
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

                            if (txtOd.Text.Contains('E'))
                                MessageBox.Show("Zinterpretowano E jako liczbę Eulera!", "Uwaga!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch
                        {
                            throw new FromIIConversionException();
                        }
                    }

                    try
                    {
                        to = Convert.ToDouble(txtDo.Text.Replace(zamienZ, zamienNa));
                    }
                    catch (Exception)
                    {
                        //Sprawdzenie może da się oszacować
                        try
                        {
                            Kalkulator kalkulator = new Kalkulator(txtDo.Text.Replace(zamienZ, zamienNa).Replace("E", Math.E.ToString()));
                            to = kalkulator.ObliczWnetrze();

                            if (txtDo.Text.Contains('E'))
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

                            if (txtDo.Text.Contains('E'))
                                MessageBox.Show("Zinterpretowano E jako liczbę Eulera!", "Uwaga!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch
                        {
                            throw new ToIIConversionException();
                        }
                    }

                    Rozniczka rozniczka = new Rozniczka(funkcja);
                    List<PointD> punkty = rozniczka.ObliczRozniczkeII(punkt, from, to, fromII, toII);

                    txtWynik.Text = punkty.Last().Y.ToString();
                }
                else if (rbFunkcjaSpecjalna.Checked)
                {
                    double pierwszy, drugi, trzeci = 0.0d, czwarty = 0.0d;

                    try
                    {
                        pierwszy = double.Parse(txtArgumentKomendaPierwszy.Text.Replace(zamienZ, zamienNa));
                    }
                    catch (Exception)
                    {
                        //Sprawdzenie może da się oszacować
                        try
                        {
                            Kalkulator kalkulator = new Kalkulator(txtArgumentKomendaPierwszy.Text.Replace(zamienZ, zamienNa).Replace("E", Math.E.ToString()));
                            pierwszy = kalkulator.ObliczWnetrze();

                            if (txtArgumentKomendaPierwszy.Text.Contains('E'))
                                MessageBox.Show("Zinterpretowano E jako liczbę Eulera!", "Uwaga!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch
                        {
                            throw new BesselePierwszyArgumentException();
                        }
                    }

                    try
                    {
                        drugi = double.Parse(txtArgumentKomendaDrugi.Text.Replace(zamienZ, zamienNa));
                    }
                    catch (Exception)
                    {
                        //Sprawdzenie może da się oszacować
                        try
                        {
                            Kalkulator kalkulator = new Kalkulator(txtArgumentKomendaDrugi.Text.Replace(zamienZ, zamienNa).Replace("E", Math.E.ToString()));
                            drugi = kalkulator.ObliczWnetrze();

                            if (txtArgumentKomendaDrugi.Text.Contains('E'))
                                MessageBox.Show("Zinterpretowano E jako liczbę Eulera!", "Uwaga!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch
                        {
                            throw new BesseleDrugiArgumentException();
                        }
                    }

                    if (cmbFunkcjaSpecjalna.SelectedIndex == 7 || cmbFunkcjaSpecjalna.SelectedIndex == 8)
                    {
                        try
                        {
                            trzeci = double.Parse(txtArgumentKomendaTrzeci.Text.Replace(zamienZ, zamienNa));
                        }
                        catch (Exception)
                        {
                            //Sprawdzenie może da się oszacować
                            try
                            {
                                Kalkulator kalkulator = new Kalkulator(txtArgumentKomendaTrzeci.Text.Replace(zamienZ, zamienNa).Replace("E", Math.E.ToString()));
                                trzeci = kalkulator.ObliczWnetrze();

                                if (txtArgumentKomendaTrzeci.Text.Contains('E'))
                                    MessageBox.Show("Zinterpretowano E jako liczbę Eulera!", "Uwaga!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            catch
                            {
                                throw new BesseleTrzeciArgumentException();
                            }
                        }
                    }

                    if (cmbFunkcjaSpecjalna.SelectedIndex == 8)
                    {
                        try
                        {
                            czwarty = double.Parse(txtArgumentKomendaCzwarty.Text.Replace(zamienZ, zamienNa));
                        }
                        catch (Exception)
                        {
                            //Sprawdzenie może da się oszacować
                            try
                            {
                                Kalkulator kalkulator = new Kalkulator(txtArgumentKomendaCzwarty.Text.Replace(zamienZ, zamienNa).Replace("E", Math.E.ToString()));
                                czwarty = kalkulator.ObliczWnetrze();

                                if (txtArgumentKomendaCzwarty.Text.Contains('E'))
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

                    switch (cmbFunkcjaSpecjalna.SelectedIndex)
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

                    txtWynik.Text = wynik.ToString();
                }

                stopWatch.Stop();
                lblCzas.Text = stopWatch.Elapsed.ToString().Substring(3, 13);
            }
            catch (BrakFunkcjiException)
            {
                if (rbKalkulator.Checked)
                    ObsluzException(stopWatch, "Wpisz działanie!");
                else
                    ObsluzException(stopWatch, "Wpisz funkcję!");

                txtFunkcja.Focus();
            }
            catch (WystepujeZmiennaException)
            {
                stopWatch.Stop();
                lblCzas.Text = stopWatch.Elapsed.ToString().Substring(3, 13);

                btnRysuj_Click(btnOblicz, new EventArgs());

                txtWynik.Text = "W wyr. nie może występować zmienna!";
                txtFunkcja.Focus();
            }
            catch (FunkcjaException excep)
            {
                ObsluzException(stopWatch, excep.Message);

                txtFunkcja.Focus();
            }
            catch (PunktConversionException)
            {
                ObsluzException(stopWatch, "Niepoprawny punkt!");

                txtPunkt.Focus();
            }
            catch (FromConversionException)
            {
                if (rbCalka.Checked)
                    ObsluzException(stopWatch, "Niepoprawna dolna granica całkowania!");
                else if (rbRozniczka.Checked)
                    ObsluzException(stopWatch, "Niepoprawny punkt x w pierwszym warunku!");
                else
                    ObsluzException(stopWatch, "Niepoprawny punkt od!");

                txtOd.Focus();
            }
            catch (FromIIConversionException)
            {
                ObsluzException(stopWatch, "Niepoprawny punkt x w drugim warunku!");

                txtOdII.Focus();
            }
            catch (ToConversionException)
            {
                if (rbCalka.Checked)
                    ObsluzException(stopWatch, "Niepoprawna górna granica całkowania!");
                else if (rbRozniczka.Checked)
                    ObsluzException(stopWatch, "Niepoprawna wartość w pierwszym warunku!");
                else
                    ObsluzException(stopWatch, "Niepoprawny punkt do!");

                txtDo.Focus();
            }
            catch (ToIIConversionException)
            {
                ObsluzException(stopWatch, "Niepoprawna wartość w drugim warunku!");

                txtDoII.Focus();
            }
            catch (BesselePierwszyArgumentException excep)
            {
                ObsluzException(stopWatch, excep.Message);

                txtArgumentKomendaPierwszy.Focus();
            }
            catch (BesseleDrugiArgumentException excep)
            {
                ObsluzException(stopWatch, excep.Message);

                txtArgumentKomendaDrugi.Focus();
            }
            catch (BesseleTrzeciArgumentException excep)
            {
                ObsluzException(stopWatch, excep.Message);

                txtArgumentKomendaTrzeci.Focus();
            }
            catch (BesseleCzwartyArgumentException excep)
            {
                ObsluzException(stopWatch, excep.Message);

                txtArgumentKomendaCzwarty.Focus();
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
            lblCzas.Text = stopWatch.Elapsed.ToString().Substring(3, 13);
            txtWynik.Text = "";

            MessageBox.Show(message, "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ObsluzException(string message)
        {
            txtWynik.Text = "";

            MessageBox.Show(message, "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ObsluzExceptionWykres(string message)
        {
            txtWynik.Text = "";
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
            txtFunkcja.Focus();

            WyczyscWykres();
        }

        private void WyczyscWykres()
        {
            try
            {
                Wykres wykres = new Wykres(txtFunkcja.Text, picWykres, Convert.ToDouble(txtXOd.Text.Replace(zamienZ, zamienNa)), Convert.ToDouble(txtXDo.Text.Replace(zamienZ, zamienNa)), Convert.ToDouble(txtYOd.Text.Replace(zamienZ, zamienNa)), Convert.ToDouble(txtYDo.Text.Replace(zamienZ, zamienNa)));
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
            Funkcje f = new Funkcje();
            f.Show();
        }

        //RownaniaLinioweForm
        private void rownaniaLinioweToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RownaniaLinioweForm rownaniaForm = new RownaniaLinioweForm(zamienZ, zamienNa);

            rownaniaForm.Show();
        }

        // RYSOWANIE WYKRESU
        private void btnRysuj_Click(object sender, EventArgs e)
        {
            try
            {
                //Sprawdzenie czy jakas opcja wykresu jest zacheckowana
                if (rbFunkcjaSpecjalna.Checked)
                {
                    if (!chkFunkcjaSpecjalna.Checked)
                        throw new NoneWykresOptionCheckedException();
                }
                else if (rbRozniczka.Checked)
                {
                    if (!chkRozniczka.Checked)
                        throw new NoneWykresOptionCheckedException();
                }
                else if (rbRozniczkaII.Checked)
                {
                    if (!(chkRozniczkaII.Checked || chkEnergia.Checked))
                        throw new NoneWykresOptionCheckedException();
                }
                else
                {
                    if (!(chkFunkcja.Checked || chkPierwszaPochodna.Checked || chkDrugaPochodna.Checked || chkRozniczka.Checked || chkRozniczkaII.Checked || chkFFT.Checked || chkRFFT.Checked))
                        throw new NoneWykresOptionCheckedException();
                }

                //Konwersja zmiennych
                string funkcja = txtFunkcja.Text.Replace(zamienZ, zamienNa);

                if (string.IsNullOrEmpty(funkcja) && !chkFunkcjaSpecjalna.Enabled)
                {
                    throw new BrakFunkcjiException();
                }

                double xOd, xDo, yOd, yDo;

                try
                {
                    xOd = Convert.ToDouble(txtXOd.Text.Replace(zamienZ, zamienNa));
                }
                catch (Exception)
                {
                    throw new xOdException();
                }

                try
                {
                    xDo = Convert.ToDouble(txtXDo.Text.Replace(zamienZ, zamienNa));
                }
                catch (Exception)
                {
                    throw new xDoException();
                }

                try
                {
                    yOd = Convert.ToDouble(txtYOd.Text.Replace(zamienZ, zamienNa));
                }
                catch (Exception)
                {
                    throw new yOdException();
                }

                try
                {
                    yDo = Convert.ToDouble(txtYDo.Text.Replace(zamienZ, zamienNa));
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

                if (chkFunkcjaSpecjalna.Checked && chkFunkcjaSpecjalna.Enabled)
                {
                    try
                    {
                        if (txtArgumentKomendaPierwszy.Text == "x")
                            pierwszy = double.NaN;
                        else
                            pierwszy = double.Parse(txtArgumentKomendaPierwszy.Text.Replace(zamienZ, zamienNa));
                    }
                    catch (Exception)
                    {
                        //Sprawdzenie może da się oszacować
                        try
                        {
                            Kalkulator kalkulator = new Kalkulator(txtArgumentKomendaPierwszy.Text.Replace(zamienZ, zamienNa).Replace("E", Math.E.ToString()));
                            pierwszy = kalkulator.ObliczWnetrze();
                        }
                        catch
                        {
                            throw new BesselePierwszyArgumentException();
                        }
                    }

                    try
                    {
                        if (txtArgumentKomendaDrugi.Text == "x")
                            drugi = double.NaN;
                        else
                            drugi = double.Parse(txtArgumentKomendaDrugi.Text.Replace(zamienZ, zamienNa));
                    }
                    catch (Exception)
                    {
                        //Sprawdzenie może da się oszacować
                        try
                        {
                            Kalkulator kalkulator = new Kalkulator(txtArgumentKomendaDrugi.Text.Replace(zamienZ, zamienNa).Replace("E", Math.E.ToString()));
                            drugi = kalkulator.ObliczWnetrze();
                        }
                        catch
                        {
                            throw new BesseleDrugiArgumentException();
                        }
                    }

                    if (cmbFunkcjaSpecjalna.SelectedIndex == 7 || cmbFunkcjaSpecjalna.SelectedIndex == 8)
                    {
                        try
                        {
                            if (txtArgumentKomendaTrzeci.Text == "x")
                                trzeci = double.NaN;
                            else
                                trzeci = double.Parse(txtArgumentKomendaTrzeci.Text.Replace(zamienZ, zamienNa));
                        }
                        catch (Exception)
                        {
                            //Sprawdzenie może da się oszacować
                            try
                            {
                                Kalkulator kalkulator = new Kalkulator(txtArgumentKomendaTrzeci.Text.Replace(zamienZ, zamienNa).Replace("E", Math.E.ToString()));
                                trzeci = kalkulator.ObliczWnetrze();
                            }
                            catch
                            {
                                throw new BesseleTrzeciArgumentException();
                            }
                        }
                    }

                    if (cmbFunkcjaSpecjalna.SelectedIndex == 8)
                    {
                        try
                        {
                            if (txtArgumentKomendaCzwarty.Text == "x")
                                czwarty = double.NaN;
                            else
                                czwarty = double.Parse(txtArgumentKomendaCzwarty.Text.Replace(zamienZ, zamienNa));
                        }
                        catch (Exception)
                        {
                            //Sprawdzenie może da się oszacować
                            try
                            {
                                Kalkulator kalkulator = new Kalkulator(txtArgumentKomendaCzwarty.Text.Replace(zamienZ, zamienNa).Replace("E", Math.E.ToString()));
                                czwarty = kalkulator.ObliczWnetrze();
                            }
                            catch
                            {
                                throw new BesseleCzwartyArgumentException();
                            }
                        }
                    }

                    //znalezienie typu besselowego
                    switch (cmbFunkcjaSpecjalna.SelectedIndex)
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
                if (chkReskalling.Checked && chkReskalling.Enabled && sender is Button)
                {
                    //Zbudowanie listy typow funkcji
                    List<TypFunkcji> typyFunkcji = new List<TypFunkcji>();

                    if (chkFunkcja.Checked)
                        typyFunkcji.Add(TypFunkcji.Funkcja);

                    if (chkPierwszaPochodna.Checked)
                        typyFunkcji.Add(TypFunkcji.Pochodna);

                    if (chkDrugaPochodna.Checked)
                        typyFunkcji.Add(TypFunkcji.DrugaPochodna);

                    double[] reskalling = null;

                    //Obliczenie maxów i minów do reskalingu
                    if (chkFunkcjaSpecjalna.Checked && chkFunkcjaSpecjalna.Enabled)
                        reskalling = wykres.Reskalling(tfb, pierwszy, drugi, trzeci, czwarty);
                    else
                        reskalling = wykres.Reskalling(typyFunkcji.ToArray()); //normlanych

                    xOd = reskalling[0];
                    xDo = reskalling[1];
                    yOd = reskalling[2];
                    yDo = reskalling[3];

                    txtXOd.Text = xOd.ToString();
                    txtXDo.Text = xDo.ToString();
                    txtYOd.Text = yOd.ToString();
                    txtYDo.Text = yDo.ToString();

                    wykres = new Wykres(funkcja, picWykres, xOd, xDo, yOd, yDo);
                }

                //Rysowanie funkcji i pochodnych
                if (chkFunkcja.Checked && chkFunkcja.Enabled)
                    wykres.Rysuj(TypFunkcji.Funkcja);

                if (chkPierwszaPochodna.Checked && chkPierwszaPochodna.Enabled)
                    wykres.Rysuj(TypFunkcji.Pochodna);

                if (chkDrugaPochodna.Checked && chkDrugaPochodna.Enabled)
                    wykres.Rysuj(TypFunkcji.DrugaPochodna);

                //Rysowanie FFT
                int probkowanie = 1000;
                double odciecie = 0.0;

                //probki
                if ((chkFFT.Checked && chkFFT.Enabled) || (chkRFFT.Checked && chkRFFT.Enabled))
                {
                    string probkowanieString = txtProbkowanie.Text;
                    string odciecieString = txtOdciecie.Text;

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

                if (chkFFT.Checked && chkFFT.Enabled)
                    wykres.RysujFFT(TypFunkcji.FFT, probkowanie, odciecie);

                if (chkRFFT.Checked && chkRFFT.Enabled)
                    wykres.RysujFFT(TypFunkcji.RFFT, probkowanie, odciecie);

                //Rysowanie rozniczek
                if (chkRozniczka.Checked && chkRozniczka.Enabled)
                {
                    double from, to;

                    try
                    {
                        from = Convert.ToDouble(txtOd.Text.Replace(zamienZ, zamienNa));
                    }
                    catch (Exception)
                    {
                        //Sprawdzenie może da się oszacować
                        try
                        {
                            Kalkulator kalkulator = new Kalkulator(txtOd.Text.Replace(zamienZ, zamienNa).Replace("E", Math.E.ToString()));
                            from = kalkulator.ObliczWnetrze();

                            if (txtOd.Text.Contains('E'))
                                MessageBox.Show("Zinterpretowano E jako liczbę Eulera!", "Uwaga!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch
                        {
                            throw new FromConversionException();
                        }
                    }

                    try
                    {
                        to = Convert.ToDouble(txtDo.Text.Replace(zamienZ, zamienNa));
                    }
                    catch (Exception)
                    {
                        //Sprawdzenie może da się oszacować
                        try
                        {
                            Kalkulator kalkulator = new Kalkulator(txtDo.Text.Replace(zamienZ, zamienNa).Replace("E", Math.E.ToString()));
                            to = kalkulator.ObliczWnetrze();

                            if (txtDo.Text.Contains('E'))
                                MessageBox.Show("Zinterpretowano E jako liczbę Eulera!", "Uwaga!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch
                        {
                            throw new ToConversionException();
                        }
                    }

                    wykres.RysujRozniczke(TypFunkcji.Rozniczka, false, from, to);
                }

                if ((chkRozniczkaII.Checked && chkRozniczkaII.Enabled) || (chkEnergia.Checked && chkEnergia.Enabled))
                {
                    double from, to, fromII, toII;

                    try
                    {
                        from = Convert.ToDouble(txtOd.Text.Replace(zamienZ, zamienNa));
                    }
                    catch (Exception)
                    {
                        //Sprawdzenie może da się oszacować
                        try
                        {
                            Kalkulator kalkulator = new Kalkulator(txtOd.Text.Replace(zamienZ, zamienNa).Replace("E", Math.E.ToString()));
                            from = kalkulator.ObliczWnetrze();

                            if (txtOd.Text.Contains('E'))
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

                            if (txtOd.Text.Contains('E'))
                                MessageBox.Show("Zinterpretowano E jako liczbę Eulera!", "Uwaga!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch
                        {
                            throw new FromConversionException();
                        }
                    }

                    try
                    {
                        to = Convert.ToDouble(txtDo.Text.Replace(zamienZ, zamienNa));
                    }
                    catch (Exception)
                    {
                        //Sprawdzenie może da się oszacować
                        try
                        {
                            Kalkulator kalkulator = new Kalkulator(txtDo.Text.Replace(zamienZ, zamienNa).Replace("E", Math.E.ToString()));
                            to = kalkulator.ObliczWnetrze();

                            if (txtDo.Text.Contains('E'))
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

                            if (txtDo.Text.Contains('E'))
                                MessageBox.Show("Zinterpretowano E jako liczbę Eulera!", "Uwaga!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch
                        {
                            throw new ToConversionException();
                        }
                    }

                    if (chkEnergia.Checked && chkEnergia.Enabled)
                    {
                        //Znalezienie bety
                        double beta = 0;
                        int betaIndex = funkcja.IndexOf("*y'");

                        if (betaIndex < 1)
                            beta = 0;
                        else
                        {
                            int plusOrMinusIndex = -1;
                            int poprzedniTempIndex = -1;
                            int tempIndex = 0;

                            //Znalezienie plus lub minus
                            do
                            {
                                tempIndex = funkcja.IndexOfAny(new char[] { '+', '-' }, tempIndex, betaIndex - tempIndex);

                                if (tempIndex >= 0)
                                {
                                    poprzedniTempIndex = tempIndex;
                                    tempIndex++;
                                }
                            }
                            while (tempIndex != -1);

                            if (poprzedniTempIndex >= 0)
                                plusOrMinusIndex = poprzedniTempIndex;
                            else
                                plusOrMinusIndex = 0;

                            //Wyciecie stringa i proba rzutowania
                            string betaString = funkcja.Substring(plusOrMinusIndex, betaIndex - plusOrMinusIndex);

                            double betaTemp;

                            if (double.TryParse(betaString, out betaTemp))
                                beta = betaTemp;
                        }

                        if (chkRozniczkaII.Checked)
                            wykres.RysujRozniczke(TypFunkcji.RozniczkaII, false, from, to, fromII, toII, beta);
                        else
                            wykres.RysujRozniczke(TypFunkcji.RozniczkaII, true, from, to, fromII, toII, beta);
                    }
                    else
                        wykres.RysujRozniczke(TypFunkcji.RozniczkaII, false, from, to, fromII, toII);
                }

                if (chkFunkcjaSpecjalna.Checked && chkFunkcjaSpecjalna.Enabled)
                    wykres.RysujBessele(tfb, pierwszy, drugi, trzeci, czwarty);

                czyFunkcjaNarysowana = true;
            }
            catch (XOdWiekszeNizXDoException)
            {
                ObsluzExceptionWykres("Wartości skali x są niepoprawne. Wartość początkowa skali nie może być większa (lub równa) od wartości końcowej.");

                txtXOd.Focus();
            }
            catch (YOdWiekszeNizYDoException)
            {
                ObsluzExceptionWykres("Wartości skali y są niepoprawne. Wartość początkowa skali nie może być większa (lub równa) od wartości końcowej.");

                txtYOd.Focus();
            }
            catch (OverflowException)
            {
                ObsluzExceptionWykres("Nie można narysować tej funkcji w tym przedziale.");
            }
            catch (xOdException)
            {
                ObsluzExceptionWykres("Niepoprawna wartość od osi x");

                txtXOd.Focus();
            }
            catch (xDoException)
            {
                ObsluzExceptionWykres("Niepoprawna wartość do osi x");

                txtXDo.Focus();
            }
            catch (yOdException)
            {
                ObsluzExceptionWykres("Niepoprawna wartość od osi y");

                txtYOd.Focus();
            }
            catch (yDoException)
            {
                ObsluzExceptionWykres("Niepoprawna wartość do osi y");

                txtYDo.Focus();
            }
            catch (WspolrzedneXException excep)
            {
                ObsluzExceptionWykres(excep.Message);

                txtXOd.Focus();
            }
            catch (WspolrzedneYException excep)
            {
                ObsluzExceptionWykres(excep.Message);

                txtYOd.Focus();
            }
            catch (BrakFunkcjiException)
            {
                ObsluzExceptionWykres("Wpisz funkcję!");

                txtFunkcja.Focus();
            }
            catch (FromConversionException)
            {
                ObsluzExceptionWykres("Niepoprawny punkt x w pierwszym warunku!");

                txtOd.Focus();
            }
            catch (FromIIConversionException)
            {
                ObsluzExceptionWykres("Niepoprawny punkt x w drugim warunku!");

                txtOdII.Focus();
            }
            catch (ToConversionException)
            {
                ObsluzExceptionWykres("Niepoprawna wartość w pierwszym warunku!");

                txtDo.Focus();
            }
            catch (ToIIConversionException)
            {
                ObsluzExceptionWykres("Niepoprawna wartość w drugim warunku!");

                txtDoII.Focus();
            }
            catch (BesselePierwszyArgumentException excep)
            {
                ObsluzExceptionWykres(excep.Message);

                txtArgumentKomendaPierwszy.Focus();
            }
            catch (BesseleDrugiArgumentException excep)
            {
                ObsluzExceptionWykres(excep.Message);

                txtArgumentKomendaDrugi.Focus();
            }
            catch (BesseleTrzeciArgumentException excep)
            {
                ObsluzExceptionWykres(excep.Message);

                txtArgumentKomendaTrzeci.Focus();
            }
            catch (BesseleCzwartyArgumentException excep)
            {
                ObsluzExceptionWykres(excep.Message);

                txtArgumentKomendaCzwarty.Focus();
            }
            catch (NoneWykresOptionCheckedException excep)
            {
                ObsluzExceptionWykres(excep.Message);
            }
            catch (FiltrValueException excep)
            {
                ObsluzExceptionWykres(excep.Message);

                txtOdciecie.Focus();
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
                if (e.Button == MouseButtons.Left && (!string.IsNullOrEmpty(txtFunkcja.Text) || rbFunkcjaSpecjalna.Checked))
                {
                    //Zmienne
                    double xOd, xDo, yOd, yDo;

                    xOd = xDo = yOd = yDo = 0d;

                    try
                    {
                        xOd = Convert.ToDouble(txtXOd.Text.Replace(zamienZ, zamienNa));
                        xDo = Convert.ToDouble(txtXDo.Text.Replace(zamienZ, zamienNa));
                        yOd = Convert.ToDouble(txtYOd.Text.Replace(zamienZ, zamienNa));
                        yDo = Convert.ToDouble(txtYDo.Text.Replace(zamienZ, zamienNa));
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
                        txtXOd.Text = Convert.ToString(xOd);
                        txtXDo.Text = Convert.ToString(xDo);
                    }

                    if (chkY.Checked)
                    {
                        txtYOd.Text = Convert.ToString(yOd);
                        txtYDo.Text = Convert.ToString(yDo);
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
            if (!string.IsNullOrEmpty(txtFunkcja.Text) || rbFunkcjaSpecjalna.Checked)
            {
                try
                {
                    double xOd, xDo, yOd, yDo;

                    //Zmienienie nieskończoności w max
                    if (double.IsPositiveInfinity(Convert.ToDouble(txtXOd.Text.Replace(zamienZ, zamienNa))))
                        txtXOd.Text = max.ToString();
                    if (double.IsPositiveInfinity(Convert.ToDouble(txtXDo.Text.Replace(zamienZ, zamienNa))))
                        txtXDo.Text = max.ToString();
                    if (double.IsPositiveInfinity(Convert.ToDouble(txtYOd.Text.Replace(zamienZ, zamienNa))))
                        txtYOd.Text = max.ToString();
                    if (double.IsPositiveInfinity(Convert.ToDouble(txtYDo.Text.Replace(zamienZ, zamienNa))))
                        txtYDo.Text = max.ToString();
                    if (double.IsNegativeInfinity(Convert.ToDouble(txtXOd.Text.Replace(zamienZ, zamienNa))))
                        txtXOd.Text = min.ToString();
                    if (double.IsNegativeInfinity(Convert.ToDouble(txtXDo.Text.Replace(zamienZ, zamienNa))))
                        txtXDo.Text = min.ToString();
                    if (double.IsNegativeInfinity(Convert.ToDouble(txtYOd.Text.Replace(zamienZ, zamienNa))))
                        txtYOd.Text = min.ToString();
                    if (double.IsNegativeInfinity(Convert.ToDouble(txtYDo.Text.Replace(zamienZ, zamienNa))))
                        txtYDo.Text = min.ToString();

                    try
                    {
                        xOd = Math.Round(Convert.ToDouble(txtXOd.Text.Replace(zamienZ, zamienNa)), 2);
                    }
                    catch (Exception)
                    {
                        throw new xOdException();
                    }

                    try
                    {
                        xDo = Math.Round(Convert.ToDouble(txtXDo.Text.Replace(zamienZ, zamienNa)), 2);
                    }
                    catch (Exception)
                    {
                        throw new xDoException();
                    }

                    try
                    {
                        yOd = Math.Round(Convert.ToDouble(txtYOd.Text.Replace(zamienZ, zamienNa)), 2);
                    }
                    catch (Exception)
                    {
                        throw new yOdException();
                    }

                    try
                    {
                        yDo = Math.Round(Convert.ToDouble(txtYDo.Text.Replace(zamienZ, zamienNa)), 2);
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

                    txtXOd.Text = Convert.ToString(xOd);
                    txtXDo.Text = Convert.ToString(xDo);
                    txtYOd.Text = Convert.ToString(yOd);
                    txtYDo.Text = Convert.ToString(yDo);

                    if (czyFunkcjaNarysowana/* && podgladWykresuPodczasSkalowaniaOnkaToolStripMenuItem.Checked*/)
                        btnRysuj_Click(this, new EventArgs());
                    else
                        WyczyscWykres();
                }
                catch (xOdException)
                {
                    ObsluzException("Niepoprawna wartość od osi x");

                    txtXOd.Focus();
                }
                catch (xDoException)
                {
                    ObsluzException("Niepoprawna wartość do osi x");

                    txtXDo.Focus();
                }
                catch (yOdException)
                {
                    ObsluzException("Niepoprawna wartość od osi y");

                    txtYOd.Focus();
                }
                catch (yDoException)
                {
                    ObsluzException("Niepoprawna wartość do osi y");

                    txtYDo.Focus();
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
            InterpolacjaForm interpolacjaForm = new InterpolacjaForm(this);
            interpolacjaForm.Show();
        }

        //Chowanie pokazywanie wykresu
        Size staryRozmiar = new Size();

        private void wykresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (wykresToolStripMenuItem.Checked)
            {
                this.MinimumSize = new Size(970, 401);
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

                this.MinimumSize = new Size(362, 401);
                this.MaximumSize = new Size(362, 401);

                this.MaximizeBox = false;
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
                
                Width = 362;
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

                    MessageBox.Show("Plik zapisany pomyślnie!", "Pierwiastki", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                MessageBox.Show("Obrazek skopiowany do schowka.", "Pierwiastki", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception excep)
            {
                MessageBox.Show("Nie udało się skopiować obrazka do schowka. " + excep.Message, "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                gbRysujFunkcje.Left = 759 + gainW;
                gbSkala.Left = 759 + gainW;
                btnRysuj.Left = 824 + gainW;

                if (czyFunkcjaNarysowana && podgladWykresuPodczasSkalowaniaOnkaToolStripMenuItem.Checked)
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
            AcceptButton = btnRysuj;
        }

        private void GroupBoxWykresowy_Leave(object sender, EventArgs e)
        {
            AcceptButton = btnOblicz;
        }

        private void ZmienUstawinia(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem)
            {
                ToolStripMenuItem tsmi = sender as ToolStripMenuItem;

                switch (tsmi.Name)
                {
                    case "wykresToolStripMenuItem":
                        settings[Setting.WykresMenuChecked] = tsmi.Checked;
                        break;
                    case "podgladWykresuPodczasSkalowaniaOnkaToolStripMenuItem":
                        settings[Setting.PodgladWykresuMenuChecked] = tsmi.Checked;
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
                        settings[Setting.FunkcjaChecked] = chk.Checked;
                        break;
                    case "chkPierwszaPochodna":
                        settings[Setting.PierwszaPochodnaChecked] = chk.Checked;
                        break;
                    case "chkDrugaPochodna":
                        settings[Setting.DrugaPochodnaChecked] = chk.Checked;
                        break;
                    case "chkFunkcjaSpecjalna":
                        settings[Setting.FunkcjaSpecjalnaChecked] = chk.Checked;
                        break;
                    case "chkWykresAuto":
                        settings[Setting.AutomatycznyReskallingChecked] = chk.Checked;
                        break;
                    case "chkRozniczka":
                        settings[Setting.RozniczkaChecked] = chk.Checked;
                        break;
                    case "chkRozniczkaII":
                        settings[Setting.RozniczkaIIChecked] = chk.Checked;
                        break;
                    case "chkEnergia":
                        settings[Setting.EnergiaChecked] = chk.Checked;
                        break;
                    case "chkFFT":
                        settings[Setting.FFTChecked] = chk.Checked;
                        break;
                    case "chkRFFT":
                        settings[Setting.RFFTChecked] = chk.Checked;
                        break;
                    default:
                        break;
                }
            }
        }

        private void cmbKomenda_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = cmbFunkcjaSpecjalna.SelectedIndex;

            if (index < 6)
            {
                txtArgumentKomendaTrzeci.Visible = false;
                txtArgumentKomendaCzwarty.Visible = false;

                cmbFunkcjaSpecjalna.Width = 126;

                txtArgumentKomendaPierwszy.Left = 133;
                txtArgumentKomendaPierwszy.Width = 90;

                txtArgumentKomendaDrugi.Left = 226;
                txtArgumentKomendaDrugi.Width = 90;
            }
            else if (index == 6)
            {
                txtArgumentKomendaTrzeci.Visible = false;
                txtArgumentKomendaCzwarty.Visible = false;

                cmbFunkcjaSpecjalna.Width = 162;

                txtArgumentKomendaPierwszy.Left = 169;
                txtArgumentKomendaPierwszy.Width = 72;

                txtArgumentKomendaDrugi.Left = 244;
                txtArgumentKomendaDrugi.Width = 72;
            }
            else if (index == 7)
            {
                txtArgumentKomendaTrzeci.Visible = true;
                txtArgumentKomendaCzwarty.Visible = false;

                cmbFunkcjaSpecjalna.Width = 99;

                txtArgumentKomendaPierwszy.Left = 106;
                txtArgumentKomendaPierwszy.Width = 68;

                txtArgumentKomendaDrugi.Left = 177;
                txtArgumentKomendaDrugi.Width = 68;

                txtArgumentKomendaTrzeci.Left = 248;
                txtArgumentKomendaTrzeci.Width = 68;
            }
            else
            {
                txtArgumentKomendaTrzeci.Visible = true;
                txtArgumentKomendaCzwarty.Visible = true;

                cmbFunkcjaSpecjalna.Width = 99;

                txtArgumentKomendaPierwszy.Left = 107;
                txtArgumentKomendaPierwszy.Width = 50;

                txtArgumentKomendaDrugi.Left = 160;
                txtArgumentKomendaDrugi.Width = 50;

                txtArgumentKomendaTrzeci.Left = 213;
                txtArgumentKomendaTrzeci.Width = 50;

                txtArgumentKomendaCzwarty.Left = 266;
                txtArgumentKomendaCzwarty.Width = 50;
            }
        }

        private void btnFunkcjaSpecjalna_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Wystarczy zaznaczyć funkcję specjalną w sekcji wyboru operacji i za jeden z jej argumentów wstawić zmienną x.", "Podpowiedź", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void txtOd_TextChanged(object sender, EventArgs e)
        {
            if ((sender as TextBox).Name == "txtOd")
                txtOdII.Text = txtOd.Text;
            else
                txtOd.Text = txtOdII.Text;
        }
    }
}
