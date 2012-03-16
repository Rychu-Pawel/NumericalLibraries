using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NumericalCalculator
{
    public partial class LinearEquationForm : Form
    {
        string zamienZ, zamienNa; //kropki i przecinki do zamieniania podczas konwersji string na double

        public LinearEquationForm(string zamienZ, string zamienNa)
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
                int iloscZmiennych = Convert.ToInt32(nudNumberOfVariables.Value);

                //zwiekszanie
                if (iloscZmiennych > dgvEquations.Columns.Count - 1)
                {
                    //Przemienienie kolumny "r" na "xN"
                    dgvEquations.Columns["r"].HeaderText = "x" + Convert.ToString(dgvEquations.Columns.Count);
                    dgvEquations.Columns["r"].Name = "x" + Convert.ToString(dgvEquations.Columns.Count);
                    
                    //Dodadanie dodatkowych kolumn do rownan
                    for (int i = dgvEquations.Columns.Count + 1; i <= iloscZmiennych; i++)
                        dgvEquations.Columns.Add("x" + Convert.ToString(i), "x" + Convert.ToString(i));

                    //Dodadnie kolumny wyniki
                    dgvEquations.Columns.Add("r", "r");

                    //Dodanie dodatkowych rowkow do rownan
                    for (int i = dgvEquations.Rows.Count; i < iloscZmiennych; i++)
                        dgvEquations.Rows.Add();

                    //Dodawanie dodatkowych kolumn do wynikow
                    for(int i = dgvResults.Columns.Count + 1; i <= iloscZmiennych; i++)
                        dgvResults.Columns.Add("x" + Convert.ToString(i), "x" + Convert.ToString(i));
                }
                //zmniejszanie
                else
                {
                    //Odejmowanie column od rownan
                    for(int i = dgvEquations.Columns.Count; i > iloscZmiennych + 1; i--)
                        dgvEquations.Columns.Remove(dgvEquations.Columns[dgvEquations.Columns.Count - 1]);

                    dgvEquations.Columns[dgvEquations.Columns.Count - 1].HeaderText = "r";
                    dgvEquations.Columns[dgvEquations.Columns.Count - 1].Name = "r";

                    //odejmowanie rowkow od rownan
                    for (int i = dgvEquations.Rows.Count; i > iloscZmiennych; i--)
                        dgvEquations.Rows.Remove(dgvEquations.Rows[dgvEquations.Rows.Count - 1]);

                    //odejmowanie kolumn od wynikow
                    for (int i = dgvResults.Columns.Count; i > iloscZmiennych; i--)
                        dgvResults.Columns.Remove(dgvResults.Columns[dgvResults.Columns.Count - 1]);
                }
            }
            catch (SystemException excep)
            {
                MessageBox.Show(excep.Message, "Blad!", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void RownaniaLinioweForm_Load(object sender, EventArgs e)
        {
            dgvEquations.Rows.Add(2);
            dgvResults.Rows.Add();
            dgvResults.ClearSelection();
        }

        private void btnOblicz_Click(object sender, EventArgs e)
        {
            try
            {
                RownaniaLiniowe Gauss = new RownaniaLiniowe(dgvEquations, zamienZ, zamienNa);

                double[] niewiadome = Gauss.Oblicz();

                for (int i = 0; i < dgvResults.Columns.Count; i++)
                    dgvResults[i, 0].Value = niewiadome[i];
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Nie wszystkie komorki zostaly wypelnione!", "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                for (int i = 0; i < dgvResults.Columns.Count; i++)
                    dgvResults[i, 0].Value = string.Empty;
            }
            catch (System.FormatException)
            {
                MessageBox.Show("Błędne wartosci w komórkach!", "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                for (int i = 0; i < dgvResults.Columns.Count; i++)
                    dgvResults[i, 0].Value = string.Empty;
            }
            catch (SystemException excep)
            {
                MessageBox.Show(excep.Message, "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                for (int i = 0; i < dgvResults.Columns.Count; i++)
                    dgvResults[i, 0].Value = string.Empty;
            }
        }

        private void RownaniaLinioweForm_Resize(object sender, EventArgs e)
        {
            int gainX = Width - 598;
            int gainY = Height - 404; //Height not found :O

            dgvEquations.Width = 558 + gainX;
            dgvEquations.Height = 178 + gainY;

            lblResults.Top = 214 + gainY;

            dgvResults.Top = 230 + gainY;
            dgvResults.Width = 558 + gainX;

            btnCompute.Top = 331 + gainY;
            btnCompute.Left = 254 + gainX / 2;
        }
    }
}
