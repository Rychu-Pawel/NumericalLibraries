using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NumericalCalculator;

namespace HybridTest
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
                double xFrom = Convert.ToDouble(txtFrom.Text);
                double xTo = Convert.ToDouble(txtTo.Text);

                Hybrid hybrid = new Hybrid(txtFunction.Text, xFrom, xTo);
                txtResult.Text = hybrid.ComputeHybrid().ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
