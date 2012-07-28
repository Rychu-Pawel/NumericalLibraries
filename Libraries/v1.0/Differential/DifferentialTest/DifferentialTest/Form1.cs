using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NumericalCalculator;

namespace DifferentialTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnComputeFirstOrder_Click(object sender, EventArgs e)
        {
            try
            {
                double lookingPoint = Convert.ToDouble(txtLookingPointFirstOrder.Text);
                double startingPoint = Convert.ToDouble(txtPointFirstOrder.Text);
                double startingPointFunctionValue = Convert.ToDouble(txtPointValueFirstOrder.Text);

                Differential diff = new Differential(txtDifferentialFirstOrder.Text);
                txtResultFirstOrder.Text = diff.ComputeDifferential(lookingPoint, startingPoint, startingPointFunctionValue).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnComputeSecondOrder_Click(object sender, EventArgs e)
        {
            try
            {
                double lookingPoint = Convert.ToDouble(txtLookingPointSecondOrder.Text);
                double startingPoint = Convert.ToDouble(txtPointSecondOrder.Text);
                double startingPointFunctionValue = Convert.ToDouble(txtPointValueSecondOrder.Text);
                double startingPointFunctionValueII = Convert.ToDouble(txtPointValueSecondOrderII.Text);

                Differential diff = new Differential(txtDifferentialSecondOrder.Text);
                txtResultSecondOrder.Text = diff.ComputeDifferentialII(lookingPoint, startingPoint, startingPointFunctionValue, startingPointFunctionValueII).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtPointSecondOrder_TextChanged(object sender, EventArgs e)
        {
            txtPointSecondOrderII.Text = txtPointSecondOrder.Text;
        }
    }
}
