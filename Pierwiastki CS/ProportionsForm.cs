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
    public partial class ProportionsForm : LanguageForm
    {
        string changeFrom, changeTo; //kropki i przecinki do zamieniania podczas konwersji string na double

        public ProportionsForm(string changeFrom, string changeTo, ResourceManager language, Settings settings)
        {
            InitializeComponent();

            this.changeFrom = changeFrom;
            this.changeTo = changeTo;

            TranslateControl(language, settings);
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
                        v1 = double.Parse(txtFirstValue.Text.Replace(changeFrom, changeTo));
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
                        v2 = double.Parse(txtSecondValue.Text.Replace(changeFrom, changeTo));
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
                        v3 = double.Parse(txtThirdValue.Text.Replace(changeFrom, changeTo));
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
                        v4 = double.Parse(txtFourthValue.Text.Replace(changeFrom, changeTo));
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
                MessageBox.Show(language.GetString("ProportionsForm_V1FormatException"), language.GetString("MessageBox_Caption_Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtFirstValue.Focus();
            }
            catch (V2FormatException)
            {
                MessageBox.Show(language.GetString("ProportionsForm_V2FormatException"), language.GetString("MessageBox_Caption_Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtFirstValue.Focus();
            }
            catch (V3FormatException)
            {
                MessageBox.Show(language.GetString("ProportionsForm_V3FormatException"), language.GetString("MessageBox_Caption_Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtFirstValue.Focus();
            }
            catch (V4FormatException)
            {
                MessageBox.Show(language.GetString("ProportionsForm_V4FormatException"), language.GetString("MessageBox_Caption_Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtFirstValue.Focus();
            }
            catch (Exception ex)
            {
                string message = language.GetString(ex.GetType().Name);

                if (string.IsNullOrEmpty(message))
                    message = ex.Message;

                MessageBox.Show(message, language.GetString("MessageBox_Caption_Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
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
