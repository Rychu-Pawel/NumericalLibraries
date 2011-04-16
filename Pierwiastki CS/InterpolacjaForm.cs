using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Pierwiastki_CS
{
    public partial class InterpolacjaForm : Form
    {
        private Form1 f1;
        public InterpolacjaForm(Form1 f)
        {
            InitializeComponent();
            f1 = f;
        }

//btnOblicz

        private void btnInterpoluj_Click(object sender, EventArgs e)
        {
            //Interpolacja
            if (rbInterpolacja.Checked == true)
            {
                try
                {
                    // ZMIENNE
                    Interpolacja interpolacja = new Interpolacja();
                    interpolacja.iloscPunktow = dgvInterpolacja.Rows.Count - 1;
                    interpolacja.punkty = new double[2, interpolacja.iloscPunktow];

                    // WCZYTANIE PUNKTOW I SPRAWDZENIE BLEDOW
                
                    // Wczytanie punktow do pamieci
                    for (int i = 0; i < interpolacja.iloscPunktow; i++)
                    {
                        interpolacja.punkty[0, i] = Convert.ToDouble(dgvInterpolacja[0, i].Value.ToString().Replace(f1.zamienZ, f1.zamienNa));
                        interpolacja.punkty[1, i] = Convert.ToDouble(dgvInterpolacja[1, i].Value.ToString().Replace(f1.zamienZ, f1.zamienNa));
                    }

                    txtFunkcja.Text = interpolacja.Oblicz();
                }
                catch (FunkcjaException excep)
                {
                    MessageBox.Show(excep.Message, "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtFunkcja.Focus();
                }
                catch (NullReferenceException)
                {
                    MessageBox.Show("Nie wszystkie komórki zostały wypełnione!", "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (System.FormatException)
                {
                    MessageBox.Show("Błędne wartości w komórkach!", "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception excep)
                {
                    MessageBox.Show(excep.Message, "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            //Regresja
            else if (rbAproksymacja.Checked == true)
            {
                try
                {
                    Aproksymacja aproksymacja = new Aproksymacja(dgvInterpolacja, (int)nudStopien.Value, f1.zamienZ, f1.zamienNa);

                    txtFunkcja.Text = aproksymacja.Oblicz();

                    //Sprawdzenie czy wynik jest sensowny
                    
                }
                catch (FunkcjaException excep)
                {
                    MessageBox.Show(excep.Message, "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtFunkcja.Focus();
                }
                catch (NullReferenceException)
                {
                    MessageBox.Show("Nie wszystkie komórki zostały wypełnione!", "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (System.FormatException)
                {
                    MessageBox.Show("Błędne wartości w komórkach!", "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception excep)
                {
                    if (excep.Message == "Układ sprzeczny!")
                        MessageBox.Show("Nie mogę zaproksymować funkcji. Spróbuj aproksymacji innego stopnia.", "Bład!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show(excep.Message, "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

//btnZatwierdz

        private void btnZatwierdz_Click(object sender, EventArgs e)
        {
            if (txtFunkcja.Text != string.Empty)
            {
                f1.txtFunkcja.Text = txtFunkcja.Text;
                Close();
            }
            else
                MessageBox.Show("Najpierw zinterpoluj lub zaaproksymuj funkcje.", "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

//Radio Buttony

        private void rbInterpolacja_CheckedChanged(object sender, EventArgs e)
        {
            if (rbInterpolacja.Checked == true)
                rbAproksymacja.Checked = false;
        }

        private void rbRegresjaLiniowa_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAproksymacja.Checked == true)
                rbInterpolacja.Checked = false;
        }

        private void InterpolacjaForm_Resize(object sender, EventArgs e)
        {
            int gain = Height - 440;

            dgvInterpolacja.Height = 199 + gain;
            gbInterpolacja.Top = 217 + gain;
            gbEkstrapolacja.Top = 273 + gain;
            lblWynik.Top = 343 + gain;
            txtFunkcja.Top = 339 + gain;
            btnOblicz.Top = 366 + gain;
            btnZatwierdz.Top = 366 + gain;
        }
    }
}
