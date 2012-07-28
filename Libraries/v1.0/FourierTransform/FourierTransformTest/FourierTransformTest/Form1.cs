using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NumericalCalculator;
using System.IO;

namespace FourierTransformTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnDrawFT_Click(object sender, EventArgs e)
        {
            try
            {
                int sampling = Convert.ToInt32(txtSamplingRate.Text);

                Chart chart = new Chart(txtFunction.Text, pbChart, 0, 6, -1, 150);
                chart.DrawFT(FunctionTypeEnum.FT, sampling);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDraw_Click(object sender, EventArgs e)
        {
            try
            {
                Chart chart = new Chart(txtFunction.Text, pbChart, 0, 6, -1, 3);
                chart.Draw(FunctionTypeEnum.Function);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnExportFT_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialogTXT = new SaveFileDialog();
                saveFileDialogTXT.DefaultExt = "txt";
                saveFileDialogTXT.FileName = "fourier";
                saveFileDialogTXT.Filter = "TXT|*.txt";

                DialogResult dr = saveFileDialogTXT.ShowDialog();

                if (dr == DialogResult.OK)
                {
                    int sampling = Convert.ToInt32(txtSamplingRate.Text);

                    FourierTransform ft = new FourierTransform();
                    List<PointC> list = ft.Compute(txtFunction.Text, sampling, 0, 6);

                    string fileName = saveFileDialogTXT.FileName;

                    //Otworzenie pliku
                    StreamWriter sw = File.CreateText(fileName);

                    //Zapisanie do pliku
                    foreach (PointC p in list)
                        sw.WriteLine(p.X + " " + p.Y);

                    //Zamkniecie i komunikat ze ok
                    sw.Flush();
                    sw.Close();

                    MessageBox.Show("Points exported!" + Environment.NewLine + "Format: (Abscissa.Real, Abscissa.Imaginary) (Ordinate.Real, Ordinate.Imaginary)");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDrawIFT_Click(object sender, EventArgs e)
        {
            try
            {
                int sampling = Convert.ToInt32(txtSamplingRate.Text);
                int cutoff = Convert.ToInt32(txtCutoff.Text);

                Chart chart = new Chart(txtFunction.Text, pbChart, 0, 6, -1, 3);
                chart.DrawFT(FunctionTypeEnum.IFT, sampling, cutoff);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
