using System.Drawing;

namespace Rychusoft.NumericalLibraries.Common
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
            x = 0.0;
            y = 0.0;

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
