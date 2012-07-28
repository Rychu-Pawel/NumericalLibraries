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
using NumericalCalculator.Exceptions;

namespace InterpolationTest
{
    public partial class InterpolationForm : Form
    {
        public InterpolationForm()
        {
            InitializeComponent();
        }

        private void btnInterpolation_Click(object sender, EventArgs e)
        {
            try
            {
                //Interpolation
                if (rbInterpolation.Checked == true)
                {
                    // VARIABLES
                    Interpolation interpolation = new Interpolation();
                    List<PointD> points = new List<PointD>();

                    // Points reading
                    double x, y;

                    for (int i = 0; i < dgvInterpolation.Rows.Count - 1; i++)
                    {
                        x = Convert.ToDouble(dgvInterpolation[0, i].Value.ToString());
                        y = Convert.ToDouble(dgvInterpolation[1, i].Value.ToString());

                        points.Add(new PointD() { X = x, Y = y });
                    }

                    //Computing
                    interpolation.Points = points;
                    txtFunction.Text = interpolation.Compute();
                }
                //Approximation
                else if (rbApproximation.Checked == true)
                {
                    // VARIABLES
                    Approximation approximation = new Approximation((int)nudLevel.Value);
                    List<PointD> points = new List<PointD>();

                    // Points reading
                    double x, y;

                    for (int i = 0; i < dgvInterpolation.Rows.Count - 1; i++)
                    {
                        x = Convert.ToDouble(dgvInterpolation[0, i].Value.ToString());
                        y = Convert.ToDouble(dgvInterpolation[1, i].Value.ToString());

                        points.Add(new PointD() { X = x, Y = y });
                    }

                    //Computing
                    approximation.Points = points;
                    txtFunction.Text = approximation.Compute();
                }
            }
            catch (WrongApproximationLevelException)
            {
                MessageBox.Show("Wrong approximation level - expected greater than zero!");
                nudLevel.Focus();
            }
            catch (NoPointsProvidedException)
            {
                MessageBox.Show("No points were provided!");
            }
            catch (InconsistentSystemOfEquationsException)
            {
                MessageBox.Show("Can't approximate function. Try other level of approximation.");
                nudLevel.Focus();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Some cells do not contain values");
            }
            catch (FormatException)
            {
                MessageBox.Show("Some provided values are incorrect");
            }
            catch (Exception excep)
            {
                MessageBox.Show(excep.Message);
            }
        }

        private void rbInterpolation_CheckedChanged(object sender, EventArgs e)
        {
            if (rbInterpolation.Checked == true)
                rbApproximation.Checked = false;
        }

        private void rbApproximation_CheckedChanged(object sender, EventArgs e)
        {
            if (rbApproximation.Checked == true)
                rbInterpolation.Checked = false;
        }

        private void InterpolationForm_Resize(object sender, EventArgs e)
        {
            int gain = Height - 440;

            dgvInterpolation.Height = 199 + gain;
            gbInterpolation.Top = 217 + gain;
            gbApproximation.Top = 273 + gain;
            lblResult.Top = 343 + gain;
            txtFunction.Top = 339 + gain;
            btnCompute.Top = 366 + gain;
        }
    }
}
