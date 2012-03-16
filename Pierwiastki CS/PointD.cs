using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace NumericalCalculator
{
    public class PointD
    {
        bool isEmpty;

        double x;
        double y;

        public double X
        {
            get { return x; }
            set
            {
                isEmpty = false;
                x = value;
            }
        }

        public double Y
        {
            get { return y; }
            set
            {
                isEmpty = false;
                y = value;
            }
        }

        public bool IsEmpty
        {
            get { return isEmpty; }
        }

        public PointF ToPointF()
        {
            if (isEmpty)
                return new PointF();

            PointF pointF = new PointF((float)x, (float)y);
            return pointF;
        }

        public PointD()
        {
            x = double.NaN;
            y = double.NaN;

            isEmpty = true;
        }

        public PointD(double x, double y)
        {
            this.x = x;
            this.y = y;

            isEmpty = false;
        }
    }
}
