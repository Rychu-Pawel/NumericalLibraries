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
    public partial class MeanForm : LanguageForm
    {
        public MeanForm(string changeFrom, string changeTo, ResourceManager language, Settings settings)
        {
            InitializeComponent();

            this.changeFrom = changeFrom;
            this.changeTo = changeTo;

            TranslateControl(language, settings);
        }

        private void rbArithmetic_CheckedChanged(object sender, EventArgs e)
        {
            dgvValues.Columns["Weight"].Visible = !rbArithmetic.Checked;
        }


    }
}
