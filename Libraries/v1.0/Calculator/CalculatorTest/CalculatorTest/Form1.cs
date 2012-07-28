using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NumericalCalculator;

namespace CalculatorTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCompute_Click(object sender, EventArgs e)
        {
            try
            {
                Calculator calc = new Calculator(txtFunction.Text);
                txtResult.Text = calc.Compute().ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                txtFunction.Focus();
            }
        }

        private void btnMean_Click(object sender, EventArgs e)
        {
            MeanForm mf = new MeanForm();
            mf.ShowDialog();
        }

        private void btnProportions_Click(object sender, EventArgs e)
        {
            ProportionsForm prop = new ProportionsForm();
            prop.ShowDialog();
        }
    }
}
