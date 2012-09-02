using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace NumericalCalculator.Logic
{
    class MeanWindowLogic
    {
        MeanWindow window;

        public Property Result { get; set; }
        public Property Arithmetic { get; set; }
        public Property Weighted { get; set; }

        public List<MeanData> MeanDataList { get; set; }

        public MeanWindowLogic(MeanWindow mw)
        {
            this.window = mw;

            Result = new Property();
            Arithmetic = new Property();
            Weighted = new Property();

            MeanDataList = new List<Logic.MeanData>();

            //Zdarzenie do pokazywania/ukrywania weighteowanej kolumny w gridzie
            Weighted.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(Weighted_PropertyChanged);

            //Ustawienie arythmetycznej jako domyślnej
            Arithmetic.Bool = true;
            Weighted.Bool = false;
        }

        private void Weighted_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            //Pokazanie ukrycie kolumny
            window.dgValues.Columns[1].Visibility = Weighted.Bool ? Visibility.Visible : Visibility.Collapsed;
        }
    }

    class MeanData
    {
        public double Value { get; set; }
        public double Weight { get; set; }
    }
}
