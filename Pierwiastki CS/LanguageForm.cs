using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Resources;
using System.Reflection;

namespace NumericalCalculator
{
    public class LanguageForm : Form
    {
        protected ResourceManager language;
        protected Settings settings;

        protected string changeFrom, changeTo; //kropki i przecinki do zamieniania podczas konwersji string na double

        protected void TranslateControl(ResourceManager language, Settings settings)
        {
            this.language = language;
            this.settings = settings;

            //Translacja
            Language.TranslateControl(this, language, this.Name + "_");
        }
    }
}
