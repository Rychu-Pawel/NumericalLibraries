using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumericalCalculator.Logic
{
    class ProportionWindowLogic
    {
        public Property First { get; set; }
        public Property Second { get; set; }
        public Property Third { get; set; }
        public Property Fourth { get; set; }
        public Property Result { get; set; }

        public ProportionWindowLogic()
        {
            First = new Property();
            Second = new Property();
            Third = new Property();
            Fourth = new Property();
            Result = new Property();
        }
    }
}
