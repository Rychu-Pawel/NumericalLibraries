using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Resources;
using NumericalCalculator.Exceptions;

namespace NumericalCalculator
{
    public partial class InterpolationForm : LanguageForm
    {
        //Event
        public delegate void FunctionAcceptedEventHandler(string function);
        public event FunctionAcceptedEventHandler FunctionAccepted;

        public InterpolationForm(string changeFrom, string changeTo, ResourceManager language, Settings settings)
        {
            InitializeComponent();

            this.changeFrom = changeFrom;
            this.changeTo = changeTo;

            TranslateControl(language, settings);
        }

        private void btnInterpoluj_Click(object sender, EventArgs e)
        {
            try
            {
                //Interpolacja
                if (rbInterpolation.Checked == true)
                {
                    // ZMIENNE
                    Interpolation interpolation = new Interpolation();
                    List<PointD> points = new List<PointD>();

                    // WCZYTANIE PUNKTOW I SPRAWDZENIE BLEDOW

                    // Wczytanie punktow do pamieci
                    double x, y;

                    for (int i = 0; i < dgvInterpolation.Rows.Count - 1; i++)
                    {
                        x = Convert.ToDouble(dgvInterpolation[0, i].Value.ToString().Replace(changeFrom, changeTo));
                        y = Convert.ToDouble(dgvInterpolation[1, i].Value.ToString().Replace(changeFrom, changeTo));

                        points.Add(new PointD() { X = x, Y = y });
                    }

                    interpolation.Points = points;
                    txtFunction.Text = interpolation.Compute();
                }
                //Aproksymacja
                else if (rbApproximation.Checked == true)
                {
                    Approximation approximation = new Approximation(dgvInterpolation, (int)nudLevel.Value, changeFrom, changeTo);

                    txtFunction.Text = approximation.Oblicz();

                    //TODO: Sprawdzenie czy wynik jest sensowny - komentarz po latach: nie wiem jak zamierzalem to zrobic (i po co)?
                }
            }
            catch (WrongApproximationLevelException)
            {
                MessageBox.Show(language.GetString("InterpolationForm_WrongApproximationLevelException"), language.GetString("MessageBox_Caption_Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                nudLevel.Focus();
            }
            catch (NoPointsProvidedException)
            {
                MessageBox.Show(language.GetString("InterpolationForm_NoPointsProvidedException"), language.GetString("MessageBox_Caption_Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (InconsistentSystemOfEquationsException)
            {
                MessageBox.Show(language.GetString("InterpolationForm_InconsistentSystemOfEquationsException"), language.GetString("MessageBox_Caption_Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                nudLevel.Focus();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show(language.GetString("InterpolationForm_NullReferenceException"), language.GetString("MessageBox_Caption_Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (FormatException)
            {
                MessageBox.Show(language.GetString("InterpolationForm_FormatException"), language.GetString("MessageBox_Caption_Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception excep)
            {
                string message = language.GetString(excep.GetType().Name);

                if (string.IsNullOrEmpty(message))
                    message = excep.Message;

                MessageBox.Show(message, language.GetString("MessageBox_Caption_Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            if (txtFunction.Text != string.Empty)
            {
                FunctionAccepted(txtFunction.Text);
                Close();
            }
            else
                MessageBox.Show(language.GetString("InterpolationForm_MessageBox_btnApply"), language.GetString("MessageBox_Caption_Error"), MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            btnApply.Top = 366 + gain;
        }
    }
}
