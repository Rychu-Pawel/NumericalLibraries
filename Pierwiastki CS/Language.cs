using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO.IsolatedStorage;
using System.IO;
using System.Globalization;
using System.Windows.Forms;
using System.Resources;

namespace NumericalCalculator
{
    public static class Language
    {
        /// <summary>
        /// Translates Control With Its All Subcontrols and Subitems
        /// </summary>
        /// <param name="control">Control to translate</param>
        /// <param name="language">Resources file containing translation strings</param>
        /// <param name="prefix">Prefix to add to control stranslation strings. For example for AboutForm it is "AboutForm_".</param>
        public static void TranslateControl(Control control, ResourceManager language, string prefix = "")
        {
            string text = language.GetString(prefix + control.Name);

            if (!string.IsNullOrEmpty(text))
                control.Text = text;

            //Przetlumaczenie podkontrolek
            foreach (Control c in control.Controls)
            {
                TranslateControl(c, language, prefix);
            }

            //Przetlumaczenie itemow dla ToolStrip
            if (control is ToolStrip)
            {                
                foreach (ToolStripItem item in (control as ToolStrip).Items)
                    TranslateToolStripItems(item, language, control.Name + "_" + prefix);
            }
        }

        public static void TranslateToolStripItems(ToolStripItem item, ResourceManager language, string prefix)
        {
            string text = language.GetString(prefix + item.Name);

            if (!string.IsNullOrEmpty(text))
                item.Text = text;

            if (item is ToolStripDropDownItem && (item as ToolStripDropDownItem).HasDropDownItems)
            {
                foreach (ToolStripItem subItem in (item as ToolStripDropDownItem).DropDownItems)
                {
                    TranslateToolStripItems(subItem as ToolStripDropDownItem, language, prefix);
                }
            }
        }
    }

    
    public enum LanguageEnum
    {
        Polish,
        English
    }
}
