using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Numerics;

namespace NumericalLibraries.Common
{
    public class PointC
    {
        bool isEmpty;

        Complex x;
        Complex y;

        public Complex X
        {
            get { return x; }
            set
            {
                isEmpty = false;
                x = value;
            }
        }

        public Complex Y
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

            PointF pointF = new PointF((float)x.Real, (float)y.Real);
            return pointF;
        }

        public PointC()
        {
            x = Complex.Zero;
            y = Complex.Zero;

            isEmpty = true;
        }

        public PointC(double x, double y)
        {
            this.x = x;
            this.y = y;

            isEmpty = true;

        }

        public PointC(Complex x, Complex y)
        {
            this.x = x;
            this.y = y;

            isEmpty = false;
        }
    }
}