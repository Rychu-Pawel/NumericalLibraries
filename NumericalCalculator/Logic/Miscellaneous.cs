using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace NumericalCalculator
{
    //public enum ArgumentTypeEnum
    //{
    //    Point,
    //    From,
    //    To,
    //    FromII,
    //    ToII,
    //    BesselFirst,
    //    BesselThird,
    //    BesselSecond,
    //    BesselFourth,
    //    Sampling,
    //    Cutoff,
    //    xFrom,
    //    xTo,
    //    yFrom,
    //    yTo,
    //}

    class Property : INotifyPropertyChanged
    {
        string changeFrom, changeTo;

        string text = string.Empty;
        double value = double.NaN;

        public string Text
        {
            get { return text; }
            set
            {
                //Przypisanie tekstu
                text = value;

                //Zrzutowanie na double
                double d;

                if (double.TryParse(text.Replace(changeFrom, changeTo), out d))
                    this.value = d;
                else
                    this.value = double.NaN;

                //Powiadomienie
                Notify();
            }
        }

        public double Value
        {
            get { return this.value; }
            set 
            { 
                //Przypisanie wartosci
                this.value = value;

                //Zrzutowanie na string
                text = value.ToString();

                //Powiadomienie
                Notify();
            }
        }

        public Property()
        {
            //Sprawdzenie czy system przyjmuje kropkę czy przecinek
            string test = "1.1";
            double testDouble;

            if (double.TryParse(test, out testDouble))
            {
                changeFrom = ",";
                changeTo = ".";
            }
            else
            {
                changeFrom = ".";
                changeTo = ",";
            }
        }

        //INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private void Notify()
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("Value"));
                PropertyChanged(this, new PropertyChangedEventArgs("Text"));
            }
        }
    }
}
