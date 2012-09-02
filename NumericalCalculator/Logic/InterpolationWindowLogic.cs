using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumericalCalculator.Logic
{
    class InterpolationWindowLogic
    {
        public Property Result { get; set; }
        public Property Level { get; set; }
        public Property Interpolation { get; set; }
        public Property Approximation { get; set; }

        public List<PointD> InterpolationDataList { get; set; }

        public InterpolationWindowLogic()
        {
            Result = new Property();
            Level = new Property();
            Interpolation = new Property();
            Approximation = new Property();

            InterpolationDataList = new List<PointD>();

            Level.Text = "2";

            //Ustawienie arythmetycznej jako domyślnej
            Interpolation.Bool = true;
            Approximation.Bool = false;
        }
    }
}
