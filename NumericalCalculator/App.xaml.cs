using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using NumericalCalculator.Translations;
using System.Globalization;

namespace NumericalCalculator
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            //Jeśli pierwsze uruchomienie to wykrycie języka
            if (NumericalCalculator.Properties.Settings.Default.FirstRun)
            {
                NumericalCalculator.Properties.Settings.Default.Culture = Translation.DefaultCulture();
                NumericalCalculator.Properties.Settings.Default.FirstRun = false;

                NumericalCalculator.Properties.Settings.Default.Save();
            }

            //Nadanie języka
            Translation.SetLanguage(NumericalCalculator.Properties.Settings.Default.Culture);

            //Wykonanie base'a
            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            NumericalCalculator.Properties.Settings.Default.Save();

            base.OnExit(e);
        }
    }
}
