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

namespace LinearEquationTest
{
    public partial class LinearEquationForm : Form
    {
        public LinearEquationForm()
        {
            InitializeComponent();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                int variablesNumber = Convert.ToInt32(nudNumberOfVariables.Value);

                if (variablesNumber > dgvEquations.Columns.Count - 1)
                {
                    dgvEquations.Columns["r"].HeaderText = "x" + Convert.ToString(dgvEquations.Columns.Count);
                    dgvEquations.Columns["r"].Name = "x" + Convert.ToString(dgvEquations.Columns.Count);
                    
                    for (int i = dgvEquations.Columns.Count + 1; i <= variablesNumber; i++)
                        dgvEquations.Columns.Add("x" + Convert.ToString(i), "x" + Convert.ToString(i));

                    dgvEquations.Columns.Add("r", "r");

                    for (int i = dgvEquations.Rows.Count; i < variablesNumber; i++)
                        dgvEquations.Rows.Add();

                    for(int i = dgvResults.Columns.Count + 1; i <= variablesNumber; i++)
                        dgvResults.Columns.Add("x" + Convert.ToString(i), "x" + Convert.ToString(i));
                }
                else
                {
                    for(int i = dgvEquations.Columns.Count; i > variablesNumber + 1; i--)
                        dgvEquations.Columns.Remove(dgvEquations.Columns[dgvEquations.Columns.Count - 1]);

                    dgvEquations.Columns[dgvEquations.Columns.Count - 1].HeaderText = "r";
                    dgvEquations.Columns[dgvEquations.Columns.Count - 1].Name = "r";

                    for (int i = dgvEquations.Rows.Count; i > variablesNumber; i--)
                        dgvEquations.Rows.Remove(dgvEquations.Rows[dgvEquations.Rows.Count - 1]);

                    for (int i = dgvResults.Columns.Count; i > variablesNumber; i--)
                        dgvResults.Columns.Remove(dgvResults.Columns[dgvResults.Columns.Count - 1]);
                }
            }
            catch (Exception excep)
            {
                MessageBox.Show(excep.Message);
            }
        }

        private void LinearEquationForm_Load(object sender, EventArgs e)
        {
            dgvEquations.Rows.Add(2);
            dgvResults.Rows.Add();
            dgvResults.ClearSelection();
        }

        private void btnCompute_Click(object sender, EventArgs e)
        {
            try
            {
                LinearEquation gauss = new LinearEquation(dgvEquations);

                double[] niewiadome = gauss.Compute();

                for (int i = 0; i < dgvResults.Columns.Count; i++)
                    dgvResults[i, 0].Value = niewiadome[i];
            }
            catch (NullReferenceException)
            {
                HandleException("Provide value for all cells!");
            }
            catch (FormatException)
            {
                HandleException("Incorrect values in some cells!");
            }
            catch (Exception excep)
            {
                HandleException(excep.Message);
            }
        }

        private void HandleException(string message)
        {
            MessageBox.Show(message);

            ClearResults();
        }

        private void ClearResults()
        {
            for (int i = 0; i < dgvResults.Columns.Count; i++)
                dgvResults[i, 0].Value = string.Empty;
        }

        private void LinearEquationForm_Resize(object sender, EventArgs e)
        {
            int gainX = Width - 598;
            int gainY = Height - 404; //Height404 not found :O

            dgvEquations.Width = 558 + gainX;
            dgvEquations.Height = 178 + gainY;

            lblResults.Top = 214 + gainY;

            dgvResults.Top = 230 + gainY;
            dgvResults.Width = 558 + gainX;

            btnCompute.Top = 331 + gainY;
            btnCompute.Left = 254 + gainX / 2;
        }
    }
}
