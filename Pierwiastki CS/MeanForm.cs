using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Resources;

namespace NumericalCalculator
{
    public partial class MeanForm : LanguageForm
    {
        public MeanForm(string changeFrom, string changeTo, ResourceManager language, Settings settings)
        {
            InitializeComponent();

            this.changeFrom = changeFrom;
            this.changeTo = changeTo;

            TranslateControl(language, settings);
        }

        private void rbArithmetic_CheckedChanged(object sender, EventArgs e)
        {
            dgvValues.Columns["Weight"].Visible = !rbArithmetic.Checked;
        }

        private void btnCompute_Click(object sender, EventArgs e)
        {
            try
            {
                if (rbArithmetic.Checked)
                {
                    //Wczytanie punktow
                    List<double> values = new List<double>();

                    for (int i = 0; i < dgvValues.Rows.Count - 1; i++)
                        values.Add(Convert.ToDouble(dgvValues[0, i].Value.ToString().Replace(changeFrom, changeTo)));

                    Mean mean = new Mean();
                    txtResult.Text = mean.ComputeArithmetic(values).ToString();
                }
                else
                {
                    //Wczytanie punktow
                    List<double[]> values = new List<double[]>();

                    for (int i = 0; i < dgvValues.Rows.Count - 1; i++)
                    {
                        double[] value = new double[2];

                        value[0] = Convert.ToDouble(dgvValues[0, i].Value.ToString().Replace(changeFrom, changeTo));
                        value[1] = Convert.ToDouble(dgvValues[1, i].Value.ToString().Replace(changeFrom, changeTo));

                        values.Add(value);
                    }

                    Mean mean = new Mean();
                    txtResult.Text = mean.ComputeWeighted(values).ToString();
                }
            }
            catch (Exception excep)
            {
                //Próba wyciągnięcia messageu dla exceptiona
                string message = language.GetString(this.Name + "_" + excep.GetType().Name);

                if (string.IsNullOrEmpty(message))
                {
                    message = language.GetString(excep.GetType().Name);

                    if (string.IsNullOrEmpty(message))
                        message = excep.Message;
                }

                //Komunikat
                MessageBox.Show(message, language.GetString("MessageBox_Caption_Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
