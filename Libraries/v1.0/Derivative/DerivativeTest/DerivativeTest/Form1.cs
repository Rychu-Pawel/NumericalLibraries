using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NumericalCalculator;

namespace DerivativeTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnComputeFirst_Click(object sender, EventArgs e)
        {
            try
            {
                Derivative derivative = new Derivative(txtDerivative.Text, Convert.ToDouble(txtPoint.Text));
                txtResultFirst.Text = derivative.ComputeDerivative().ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnComputeSecond_Click(object sender, EventArgs e)
        {
            try
            {
                Derivative derivative = new Derivative(txtDerivative.Text, Convert.ToDouble(txtPoint.Text));
                txtResultSecond.Text = derivative.ComputeDerivativeBis().ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnComputeFunctionAtPoint_Click(object sender, EventArgs e)
        {
            try
            {
                Derivative derivative = new Derivative(txtDerivative.Text, Convert.ToDouble(txtPoint.Text));
                txtResultFunctionAtPoint.Text = derivative.ComputeFunctionAtPoint().ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
