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
    public partial class CalkaForm : Form
    {
        public CalkaForm()
        {
            InitializeComponent();
            dgvDane.Rows.Add(2);
        }

        private void btnOblicz_Click(object sender, EventArgs e)
        {
            //try
            //{
                CalkaWielokrotna calkaWielokrotna = new CalkaWielokrotna(txtFunkcja.Text, dgvDane);

                double wynik = calkaWielokrotna.Oblicz();

                if (double.IsNaN(wynik))
                    txtWynik.Text = "Brak rozw. w zbiorze liczb rzeczywistych";
                else
                    txtWynik.Text = Convert.ToString(wynik);
            //}
            //catch (SystemException excep)
            //{
            //    MessageBox.Show(excep.Message, "Blad!", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            //}
        }

        private void nudStopien_ValueChanged(object sender, EventArgs e)
        {
            if (dgvDane.Rows.Count < nudStopien.Value)
            {
                dgvDane.Rows.Add((int)(nudStopien.Value - dgvDane.Rows.Count));
            }
            else
            {
                for (int i = dgvDane.Rows.Count - 1; i > nudStopien.Value - 1; i--)
                    dgvDane.Rows.Remove(dgvDane.Rows[i]);
            }
        }
    }
}
