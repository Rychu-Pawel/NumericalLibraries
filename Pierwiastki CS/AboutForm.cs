using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Resources;

namespace NumericalCalculator
{
    public partial class AboutForm : Form
    {
        ResourceManager language;
        Settings settings;

        public AboutForm(ResourceManager language, Settings settings)
        {
            InitializeComponent();

            this.language = language;
            this.settings = settings;

            //Translacja
            Language.TranslateControl(this, language, "AboutForm_");
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AboutForm_Load(object sender, EventArgs e)
        {
            lblVersionValue.Text = Application.ProductVersion;
        }

        private void lnkPawelRychlicki_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://pawelrychlicki.pl");
        }
    }
}
