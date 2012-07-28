using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NumericalCalculator;

namespace IntegralTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCompute_Click(object sender, EventArgs e)
        {
            double lowerLimit = Convert.ToDouble(txtLowerLimit.Text);
            double upperLimit = Convert.ToDouble(txtUpperLimit.Text);

            Integral integral = new Integral(txtIntegral.Text, lowerLimit, upperLimit);
            double result = integral.ComputeIntegral();

            txtResult.Text = result.ToString();
        }
    }
}
