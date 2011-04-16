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
    public partial class RownaniaLinioweForm : Form
    {
        string zamienZ, zamienNa; //kropki i przecinki do zamieniania podczas konwersji string na double

        public RownaniaLinioweForm(string zamienZ, string zamienNa)
        {
            InitializeComponent();

            this.zamienZ = zamienZ;
            this.zamienNa = zamienNa;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            //Zmiana liczby kolumn w dgvRownania i Wyniki
            try
            {
                int iloscZmiennych = Convert.ToInt32(nudIloscZmiennych.Value);

                //zwiekszanie
                if (iloscZmiennych > dgvRownania.Columns.Count - 1)
                {
                    //Przemienienie kolumny "r" na "xN"
                    dgvRownania.Columns["r"].HeaderText = "x" + Convert.ToString(dgvRownania.Columns.Count);
                    dgvRownania.Columns["r"].Name = "x" + Convert.ToString(dgvRownania.Columns.Count);
                    
                    //Dodadanie dodatkowych kolumn do rownan
                    for (int i = dgvRownania.Columns.Count + 1; i <= iloscZmiennych; i++)
                        dgvRownania.Columns.Add("x" + Convert.ToString(i), "x" + Convert.ToString(i));

                    //Dodadnie kolumny wyniki
                    dgvRownania.Columns.Add("r", "r");

                    //Dodanie dodatkowych rowkow do rownan
                    for (int i = dgvRownania.Rows.Count; i < iloscZmiennych; i++)
                        dgvRownania.Rows.Add();

                    //Dodawanie dodatkowych kolumn do wynikow
                    for(int i = dgvWyniki.Columns.Count + 1; i <= iloscZmiennych; i++)
                        dgvWyniki.Columns.Add("x" + Convert.ToString(i), "x" + Convert.ToString(i));
                }
                //zmniejszanie
                else
                {
                    //Odejmowanie column od rownan
                    for(int i = dgvRownania.Columns.Count; i > iloscZmiennych + 1; i--)
                        dgvRownania.Columns.Remove(dgvRownania.Columns[dgvRownania.Columns.Count - 1]);

                    dgvRownania.Columns[dgvRownania.Columns.Count - 1].HeaderText = "r";
                    dgvRownania.Columns[dgvRownania.Columns.Count - 1].Name = "r";

                    //odejmowanie rowkow od rownan
                    for (int i = dgvRownania.Rows.Count; i > iloscZmiennych; i--)
                        dgvRownania.Rows.Remove(dgvRownania.Rows[dgvRownania.Rows.Count - 1]);

                    //odejmowanie kolumn od wynikow
                    for (int i = dgvWyniki.Columns.Count; i > iloscZmiennych; i--)
                        dgvWyniki.Columns.Remove(dgvWyniki.Columns[dgvWyniki.Columns.Count - 1]);
                }
            }
            catch (SystemException excep)
            {
                MessageBox.Show(excep.Message, "Blad!", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void RownaniaLinioweForm_Load(object sender, EventArgs e)
        {
            dgvRownania.Rows.Add(2);
            dgvWyniki.Rows.Add();
            dgvWyniki.ClearSelection();
        }

        private void btnOblicz_Click(object sender, EventArgs e)
        {
            try
            {
                RownaniaLiniowe Gauss = new RownaniaLiniowe(dgvRownania, zamienZ, zamienNa);

                double[] niewiadome = Gauss.Oblicz();

                for (int i = 0; i < dgvWyniki.Columns.Count; i++)
                    dgvWyniki[i, 0].Value = niewiadome[i];
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Nie wszystkie komorki zostaly wypelnione!", "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                for (int i = 0; i < dgvWyniki.Columns.Count; i++)
                    dgvWyniki[i, 0].Value = string.Empty;
            }
            catch (System.FormatException)
            {
                MessageBox.Show("Błędne wartosci w komórkach!", "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                for (int i = 0; i < dgvWyniki.Columns.Count; i++)
                    dgvWyniki[i, 0].Value = string.Empty;
            }
            catch (SystemException excep)
            {
                MessageBox.Show(excep.Message, "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                for (int i = 0; i < dgvWyniki.Columns.Count; i++)
                    dgvWyniki[i, 0].Value = string.Empty;
            }
        }

        private void RownaniaLinioweForm_Resize(object sender, EventArgs e)
        {
            int gainX = Width - 598;
            int gainY = Height - 404; //Height not found :O

            dgvRownania.Width = 558 + gainX;
            dgvRownania.Height = 178 + gainY;

            lblWyniki.Top = 214 + gainY;

            dgvWyniki.Top = 230 + gainY;
            dgvWyniki.Width = 558 + gainX;

            btnOblicz.Top = 331 + gainY;
            btnOblicz.Left = 254 + gainX / 2;
        }
    }
}
