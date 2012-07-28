using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Resources;
using NumericalCalculator;

namespace CalculatorTest
{
    public partial class MeanForm : Form
    {
        public MeanForm()
        {
            InitializeComponent();
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
                    //Read values
                    List<double> values = new List<double>();

                    for (int i = 0; i < dgvValues.Rows.Count - 1; i++)
                        values.Add(Convert.ToDouble(dgvValues[0, i].Value.ToString()));

                    //Compute
                    Mean mean = new Mean();
                    txtResult.Text = mean.ComputeArithmetic(values).ToString();
                }
                else
                {
                    //Read values
                    List<double[]> values = new List<double[]>();

                    for (int i = 0; i < dgvValues.Rows.Count - 1; i++)
                    {
                        double[] value = new double[2];

                        value[0] = Convert.ToDouble(dgvValues[0, i].Value.ToString());
                        value[1] = Convert.ToDouble(dgvValues[1, i].Value.ToString());

                        values.Add(value);
                    }

                    //Compute
                    Mean mean = new Mean();
                    txtResult.Text = mean.ComputeWeighted(values).ToString();
                }
            }
            catch (Exception excep)
            {
                MessageBox.Show(excep.Message);
            }
        }
    }
}
