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
    public partial class FunctionForm : LanguageForm
    {
        public FunctionForm(ResourceManager language, Settings settings)
        {
            InitializeComponent();
            TranslateControl(language, settings);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Hide();
        }
    }
}
