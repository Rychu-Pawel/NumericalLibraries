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
    public partial class ProportionsForm : Form
    {
        public ProportionsForm()
        {
            InitializeComponent();
        }

        private void btnCompute_Click(object sender, EventArgs e)
        {
            try
            {
                double v1, v2, v3, v4;

                if (string.IsNullOrEmpty(txtFirstValue.Text) || txtFirstValue.Text == "x")
                    v1 = double.NaN;
                else
                {
                    try
                    {
                        v1 = double.Parse(txtFirstValue.Text);
                    }
                    catch
                    {
                        throw new V1FormatException();
                    }
                }

                if (string.IsNullOrEmpty(txtSecondValue.Text) || txtSecondValue.Text == "x")
                    v2 = double.NaN;
                else
                {
                    try
                    {
                        v2 = double.Parse(txtSecondValue.Text);
                    }
                    catch
                    {
                        throw new V2FormatException();
                    }
                }

                if (string.IsNullOrEmpty(txtThirdValue.Text) || txtThirdValue.Text == "x")
                    v3 = double.NaN;
                else
                {
                    try
                    {
                        v3 = double.Parse(txtThirdValue.Text);
                    }
                    catch
                    {
                        throw new V3FormatException();
                    }
                }

                if (string.IsNullOrEmpty(txtFourthValue.Text) || txtFourthValue.Text == "x")
                    v4 = double.NaN;
                else
                {
                    try
                    {
                        v4 = double.Parse(txtFourthValue.Text);
                    }
                    catch
                    {
                        throw new V4FormatException();
                    }
                }

                Proportions prop = new Proportions();
                txtResult.Text = prop.Compute(v1, v2, v3, v4).ToString();
            }
            catch (V1FormatException)
            {
                MessageBox.Show("First value is incorrect");
                txtFirstValue.Focus();
            }
            catch (V2FormatException)
            {
                MessageBox.Show("Second value is incorrect");
                txtFirstValue.Focus();
            }
            catch (V3FormatException)
            {
                MessageBox.Show("Third value is incorrect");
                txtFirstValue.Focus();
            }
            catch (V4FormatException)
            {
                MessageBox.Show("Fourth value is incorrect");
                txtFirstValue.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                txtFirstValue.Focus();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }

    public class V1FormatException : Exception
    { }

    public class V2FormatException : Exception
    { }

    public class V3FormatException : Exception
    { }

    public class V4FormatException : Exception
    { }
}
