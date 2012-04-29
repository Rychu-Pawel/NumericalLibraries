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
    public partial class LinearEquationForm : LanguageForm
    {
        public LinearEquationForm(string changeFrom, string changeTo, ResourceManager language, Settings settings)
        {
            InitializeComponent();

            this.changeFrom = changeFrom;
            this.changeTo = changeTo;

            TranslateControl(language, settings);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            //Zmiana liczby kolumn w dgvRownania i Wyniki
            try
            {
                int variablesNumber = Convert.ToInt32(nudNumberOfVariables.Value);

                //zwiekszanie
                if (variablesNumber > dgvEquations.Columns.Count - 1)
                {
                    //Przemienienie kolumny "r" na "xN"
                    dgvEquations.Columns["r"].HeaderText = "x" + Convert.ToString(dgvEquations.Columns.Count);
                    dgvEquations.Columns["r"].Name = "x" + Convert.ToString(dgvEquations.Columns.Count);
                    
                    //Dodadanie dodatkowych kolumn do rownan
                    for (int i = dgvEquations.Columns.Count + 1; i <= variablesNumber; i++)
                        dgvEquations.Columns.Add("x" + Convert.ToString(i), "x" + Convert.ToString(i));

                    //Dodadnie kolumny wyniki
                    dgvEquations.Columns.Add("r", "r");

                    //Dodanie dodatkowych rowkow do rownan
                    for (int i = dgvEquations.Rows.Count; i < variablesNumber; i++)
                        dgvEquations.Rows.Add();

                    //Dodawanie dodatkowych kolumn do wynikow
                    for(int i = dgvResults.Columns.Count + 1; i <= variablesNumber; i++)
                        dgvResults.Columns.Add("x" + Convert.ToString(i), "x" + Convert.ToString(i));
                }
                //zmniejszanie
                else
                {
                    //Odejmowanie column od rownan
                    for(int i = dgvEquations.Columns.Count; i > variablesNumber + 1; i--)
                        dgvEquations.Columns.Remove(dgvEquations.Columns[dgvEquations.Columns.Count - 1]);

                    dgvEquations.Columns[dgvEquations.Columns.Count - 1].HeaderText = "r";
                    dgvEquations.Columns[dgvEquations.Columns.Count - 1].Name = "r";

                    //odejmowanie rowkow od rownan
                    for (int i = dgvEquations.Rows.Count; i > variablesNumber; i--)
                        dgvEquations.Rows.Remove(dgvEquations.Rows[dgvEquations.Rows.Count - 1]);

                    //odejmowanie kolumn od wynikow
                    for (int i = dgvResults.Columns.Count; i > variablesNumber; i--)
                        dgvResults.Columns.Remove(dgvResults.Columns[dgvResults.Columns.Count - 1]);
                }
            }
            catch (Exception excep)
            {
                MessageBox.Show(excep.Message, language.GetString("MessageBox_Caption_Error"), MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
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
                LinearEquation gauss = new LinearEquation(dgvEquations, changeFrom, changeTo);

                double[] niewiadome = gauss.Oblicz();

                for (int i = 0; i < dgvResults.Columns.Count; i++)
                    dgvResults[i, 0].Value = niewiadome[i];
            }
            catch (NullReferenceException)
            {
                HandleException(language.GetString("LinearEquation_NullReferenceException"));
            }
            catch (FormatException)
            {
                HandleException(language.GetString("LinearEquation_FormatException"));
            }
            catch (Exception excep)
            {
                string message = language.GetString(excep.GetType().Name);

                if (string.IsNullOrEmpty(message))
                    message = excep.Message;

                HandleException(message);
            }
        }

        private void HandleException(string message)
        {
            MessageBox.Show(message, language.GetString("MessageBox_Caption_Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);

            //Czyszczenie resultow
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
            int gainY = Height - 404; //Height not found :O

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
