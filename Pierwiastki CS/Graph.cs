using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace NumericalCalculator
{
    class Graph
    {
    // Zmienne ----------------------------
        double xOd, xDo, yOd, yDo; //Wybrany przedzial
        double zeroX, zeroY; //Ile wynosza miejsca zerowe wykresu w stosunku do wybranego przedzialu
        double wspX, wspY; //Wspolczynniki X i Y, czyli ile jedna jednostka wybranego przez usera przedzialu ma jednostek na moim wykresie
        int picWidth, picHeight;

        readonly double max = 530000000.0;
        readonly double min = -530000000.0;

        PictureBox pWykres;
        Graphics g;

        string funkcja;

        List<PointF> punktyWykresu;

        public PointF[] GraphPoints
        {
            get { return punktyWykresu.ToArray(); }
        }

    // Metody -----------------------------
        public void Clear() //Wyczyszczenie starego wykresu i przygotowanie siatki X i Y
        {
            Font font = new Font("Arial", 7); // Do wypisywania punktow siatki

            g.Clear(Color.White); // Czysci ekran z poprzedniej funkcji

            //Rysowanie siatki X:
            double krok = ((-xOd + xDo) / 6) * wspX;

            if (xOd * xDo <= 0) // Bedzie widoczna os zeroX, wiec rysujemy od zeroX na lewo i od zeroX na prawo
            {
                for (double i = zeroX; i >= zeroX + xOd * wspX; i -= krok)
                {
                    if (i > (xOd * wspX) + zeroX && i < (xDo * wspX) + zeroX)
                    {
                        g.DrawLine(Pens.LightGray, (float)(i), 0, (float)(i), pWykres.Height); // OŚ -X
                        g.DrawString(Convert.ToString(Math.Round(xOd + i / wspX, 3)), font, Brushes.DarkGray, (float)(i), picHeight - 15);
                    }
                }

                for (double i = zeroX; i <= (xDo * wspX) + zeroX; i += krok)
                {
                    if (i > (xOd * wspX) + zeroX && i < (xDo * wspX) + zeroX)
                    {
                        g.DrawLine(Pens.LightGray, (float)(i), 0, (float)(i), pWykres.Height); // OŚ +X
                        g.DrawString(Convert.ToString(Math.Round(xOd + i / wspX, 3)), font, Brushes.DarkGray, (float)(i), picHeight - 15);
                    }
                }
            }
            else if (xOd < 0) // Obie wartosci xOd i xDo sa ujemne
            {
                for (double i = zeroX; i >= zeroX + xOd * wspX; i -= krok)
                {
                    if (i > (xOd * wspX) + zeroX && i < (xDo * wspX) + zeroX)
                    {
                        g.DrawLine(Pens.LightGray, (float)(i), 0, (float)(i), pWykres.Height); // OŚ -X
                        g.DrawString(Convert.ToString(Math.Round(xOd + i / wspX, 3)), font, Brushes.DarkGray, (float)(i), picHeight - 15);
                    }
                }
            }
            else // Obie wartosci sa dodatnie
            {
                for (double i = zeroX; i <= (xDo * wspX) + zeroX; i += krok)
                {
                    if (i > (xOd * wspX) + zeroX && i < (xDo * wspX) + zeroX)
                    {
                        g.DrawLine(Pens.LightGray, (float)(i), 0, (float)(i), pWykres.Height); // OŚ +X
                        g.DrawString(Convert.ToString(Math.Round(xOd + i / wspX, 3)), font, Brushes.DarkGray, (float)(i), picHeight - 15);
                    }
                }
            }

            //Rysowanie siatki Y:
            krok = ((-yOd + yDo) / 6) * wspY;

            if (yOd * yDo <= 0) // Bedzie widoczna os zeroY, wiec rysujemy od zeroY na lewo i od zeroY na prawo
            {
                for (double i = zeroY; i <= zeroY + (-yOd) * wspY; i += krok)
                {
                    if (i < (-yOd * wspY) + zeroY && i > (-yDo * wspY) + zeroY)
                    {
                        g.DrawLine(Pens.LightGray, pWykres.Width, (float)(i), 0, (float)(i)); // OŚ -Y
                        g.DrawString(Convert.ToString(Math.Round(yDo - i / wspY, 3)), font, Brushes.DarkGray, 1, (float)(i));
                    }
                }

                for (double i = zeroY; i >= zeroY + (-yDo) * wspY; i -= krok)
                {
                    if (i < (-yOd * wspY) + zeroY && i > (-yDo * wspY) + zeroY)
                    {
                        g.DrawLine(Pens.LightGray, pWykres.Width, (float)(i), 0, (float)(i)); // OŚ +Y
                        g.DrawString(Convert.ToString(Math.Round(yDo - i / wspY, 3)), font, Brushes.DarkGray, 1, (float)(i));
                    }
                }
            }
            else if (yOd < 0) // Obie wartosci yOd i yDo sa ujemne
            {
                for (double i = zeroY; i <= zeroY + (-yOd) * wspY; i += krok)
                {
                    if (i < (-yOd * wspY) + zeroY && i > (-yDo * wspY) + zeroY)
                    {
                        g.DrawLine(Pens.LightGray, pWykres.Width, (float)(i), 0, (float)(i)); // OŚ -Y
                        g.DrawString(Convert.ToString(Math.Round(yDo - i / wspY, 3)), font, Brushes.DarkGray, 1, (float)(i));
                    }
                }
            }
            else // Obie wartosci sa dodatnie
            {
                for (double i = zeroY; i >= zeroY + (-yDo) * wspY; i -= krok)
                {
                    if (i < (-yOd * wspY) + zeroY && i > (-yDo * wspY) + zeroY)
                    {
                        g.DrawLine(Pens.LightGray, pWykres.Width, (float)(i), 0, (float)(i)); // OŚ +Y
                        g.DrawString(Convert.ToString(Math.Round(yDo - i / wspY, 3)), font, Brushes.DarkGray, 1, (float)(i));
                    }
                }
            }

            //RYSOWANIE OSI OX i OY
            g.DrawLine(Pens.Black, 0, (float)zeroY, pWykres.Width, (float)zeroY); // OŚ X
            g.DrawLine(Pens.Black, (float)zeroX, 0, (float)zeroX, pWykres.Height); // OŚ Y

            font = new Font("Aria", 8);

            //STRZAŁKI I OPIS OSI OX
            g.DrawLine(Pens.Black, (float)pWykres.Width - 12f, (float)zeroY - 5f, (float)pWykres.Width - 2f, (float)zeroY);
            g.DrawLine(Pens.Black, (float)pWykres.Width - 12f, (float)zeroY + 5f, (float)pWykres.Width - 2f, (float)zeroY);
            g.DrawString("x", font, Brushes.Black, (float)pWykres.Width - 13f, (float)zeroY + 3f);

            //STRZAŁKI I OPIS OSI OY
            g.DrawLine(Pens.Black, (float)zeroX - 6f, 10f, (float)zeroX - 1, 2);
            g.DrawLine(Pens.Black, (float)zeroX + 5f, 10f, (float)zeroX + 1, 2);
            g.DrawString("f(x)", font, Brushes.Black, (float)zeroX + 7f, 4f);

            //Wyczyszczenie punktow wykresu
            punktyWykresu = new List<PointF>();
        }

        public void Draw(FunctionTypeEnum functionType)
        {
            punktyWykresu.Clear();

            Derivative funkcjaWPunkcie = new Derivative(funkcja);

            Font font2 = new Font("Arial", 8); // Do wypisywania wzoru funkcji

            List<PointF> punkty = new List<PointF>();

            Pen pen = null;

            for (double i = 0; i < picWidth; i += (double)picWidth / 1000.0)
            {
                float fx = 0;
                double x = (i - zeroX) / wspX;

                switch (functionType)
                {
                    case FunctionTypeEnum.Function:
                        fx = (float)(funkcjaWPunkcie.ComputeFunctionAtPoint(x) * wspY);
                        break;
                    case FunctionTypeEnum.Derivative:
                        fx = (float)(funkcjaWPunkcie.ComputeDerivative(x) * wspY);
                        break;
                    case FunctionTypeEnum.SecondDerivative:
                        fx = (float)(funkcjaWPunkcie.ComputeDerivativeBis(x) * wspY);
                        break;
                    default:
                        throw new NoneGraphOptionCheckedException(); //not reachable code?
                }

                if (fx > max)
                    fx = (float)max;
                else if (fx < min)
                    fx = (float)min;

                punkty.Add(new PointF((float)(i), (float)(zeroY - fx)));
                punktyWykresu.Add(new PointF((float)x, (float)(fx / wspY)));
            }

            //Wypisanie wzoru funkcji
            g.DrawString("f(x) = " + funkcja, font2, Brushes.Black, 3, 3);

            switch (functionType)
            {
                case FunctionTypeEnum.Function:
                    pen = Pens.Blue;
                    break;
                case FunctionTypeEnum.Derivative:
                    pen = Pens.Red;
                    break;
                case FunctionTypeEnum.SecondDerivative:
                    pen = Pens.Green;
                    break;
            }

            //RYSOWANIE WYKRESU
            try
            {
                g.DrawLines(pen, punkty.ToArray());
            }
            catch (Exception)
            {
                //Pozbycie sie NaN - może pomoże
                punkty = punkty.Where(p => !double.IsNaN(p.Y)).ToList();

                try
                {
                    g.DrawLines(pen, punkty.ToArray());
                }
                catch { }
            }           
        }

        public void DrawFT(FunctionTypeEnum functionType, int sampling, double cutoff = 0.0)
        {
            punktyWykresu.Clear();

            Font font2 = new Font("Arial", 8); // Do wypisywania wzoru funkcji

            List<PointF> punkty = new List<PointF>();
            List<PointF> punktyRevers = new List<PointF>();

            List<PointC> punktyC = new List<PointC>();
            List<PointC> punktyReversC = new List<PointC>();

            Pen pen = null;

            FourierTransform fft = new FourierTransform();

            //Obliczenie FFT
            punktyC = fft.Compute(funkcja, sampling, xOd, xDo);

            //Przefiltrowanie
            for (int i = 0; i < punktyC.Count; i++)
            {
                PointC pc = punktyC[i];

                if (Math.Abs(pc.Y.Real) < cutoff)
                    pc.Y = 0;

                punktyC[i] = pc;
            }

            //Obliczenie Reverse FFT
            if (functionType == FunctionTypeEnum.IFT)
                punktyReversC = fft.ComputeInverse(punktyC, sampling, xOd, xDo);

            //Nowe przygotowanie
            if (functionType == FunctionTypeEnum.FT)
                Initialize(funkcja, pWykres, 0, sampling + 3, yOd, yDo);

            //Przeskalowanie pkt-ow
            if (functionType == FunctionTypeEnum.FT)
            {
                for (int i = 0; i < punktyC.Count; i++)
                {
                    PointC pc = punktyC[i];

                    punktyWykresu.Add(pc.ToPointF());

                    pc.X = (pc.X * wspX + zeroX);
                    pc.Y = (pc.Y * wspY);

                    if (pc.Y.Real > max)
                        pc.Y = max;
                    else if (pc.Y.Real < min)
                        pc.Y = min;

                    pc.Y = (zeroY - pc.Y);

                    punkty.Add(pc.ToPointF());
                }
            }

            if (functionType == FunctionTypeEnum.IFT)
            {
                //Przeskalowanie pkt-ow Reverse
                for (int i = 0; i < punktyReversC.Count; i++)
                {
                    PointC pc = punktyReversC[i];

                    punktyWykresu.Add(pc.ToPointF());

                    pc.X = (pc.X * wspX + zeroX);
                    pc.Y = (pc.Y * wspY);

                    if (pc.Y.Real > max)
                        pc.Y = max;
                    else if (pc.Y.Real < min)
                        pc.Y = min;

                    //if (Math.Abs(pc.Y.Real) < odciecie)
                    //    pc.Y = 0;

                    pc.Y = (zeroY - pc.Y);

                    punktyRevers.Add(pc.ToPointF());
                }
            }

            //Wypisanie wzoru funkcji
            if (functionType == FunctionTypeEnum.FT)
                g.DrawString("FFT(" + '\u03C9' + ") = " + funkcja, font2, Brushes.Black, 3, 16);
            else
                g.DrawString("RFFT(x) = " + funkcja, font2, Brushes.Black, 3, 16);

            switch (functionType)
            {
                case FunctionTypeEnum.FT:
                    pen = Pens.PaleVioletRed;
                    break;
                case FunctionTypeEnum.IFT:
                    pen = Pens.Red;
                    break;
                default:
                    pen = Pens.Red;
                    break;
            }

            //RYSOWANIE WYKRESU
            try
            {
                if (functionType == FunctionTypeEnum.FT)
                    g.DrawLines(pen, punkty.ToArray());
                else
                    g.DrawLines(pen, punktyRevers.ToArray());
            }
            catch (Exception)
            {
                //Pozbycie sie NaN - może pomoże
                if (functionType == FunctionTypeEnum.FT)
                    punkty = punkty.Where(p => !double.IsNaN(p.Y)).ToList();
                else
                    punktyRevers = punktyRevers.Where(p => !double.IsNaN(p.Y)).ToList();

                try
                {
                    if (functionType == FunctionTypeEnum.FT)
                        g.DrawLines(pen, punkty.ToArray());
                    else
                        g.DrawLines(pen, punktyRevers.ToArray());
                }
                catch { }
            }
        }

        public void DrawDifferential(FunctionTypeEnum functionType, params double[] parameters)
        {
            punktyWykresu.Clear();

            List<PointF> punkty = new List<PointF>();
            List<PointF> energia = new List<PointF>();
            List<PointF> energia2 = new List<PointF>();

            Font font2 = new Font("Arial", 8); // Do wypisywania wzoru funkcji

            Pen pen = null;

            //Dostosowanie kroku
            double krok = 0.001;
            double wielkoscPrzedzialu = xDo - xOd;

            if (wielkoscPrzedzialu > 20 && wielkoscPrzedzialu < 50)
                krok = 0.0025;
            else if (wielkoscPrzedzialu >= 50 && wielkoscPrzedzialu < 150)
                krok = 0.005;
            else if (wielkoscPrzedzialu >= 150 && wielkoscPrzedzialu < 500)
                krok = 0.01;
            else if (wielkoscPrzedzialu >= 500)
                krok = 0.05;

            //Idziemy od naszego punktu brzegowego w lewo, a potem w prawo
            switch (functionType)
            {
                case FunctionTypeEnum.Differential:

                    Differential rozniczka = new Differential(funkcja);

                    List<PointD> lewo = new List<PointD>();
                    List<PointD> prawo = new List<PointD>();

                    //W lewo
                    if (xOd < parameters[0])
                        lewo = rozniczka.ComputeDifferential(xOd, parameters[0], parameters[1], false, krok);

                    //W prawo
                    rozniczka = new Differential(funkcja);

                    if (xDo >= parameters[0])
                        prawo = rozniczka.ComputeDifferential(xDo, parameters[0], parameters[1], false, krok);

                    //ŁĄCZYMY
                    //odwracamy w lewo
                    lewo.Reverse();

                    //dodajemy lewo
                    foreach (PointD point in lewo)
                    {
                        PointF pf = new PointF();

                        punktyWykresu.Add(point.ToPointF());

                        pf.X = (float)(point.X * wspX + zeroX);
                        pf.Y = (float)(point.Y * wspY);

                        if (pf.Y > max)
                            pf.Y = (float)max;
                        else if (pf.Y < min)
                            pf.Y = (float)min;

                        pf.Y = (float)(zeroY - pf.Y);

                        punkty.Add(pf);
                    }

                    //dodajemy prawo
                    foreach (PointD point in prawo)
                    {
                        PointF pf = new PointF();

                        punktyWykresu.Add(point.ToPointF());

                        pf.X = (float)(point.X * wspX + zeroX);
                        pf.Y = (float)(point.Y * wspY);

                        if (pf.Y > max)
                            pf.Y = (float)max;
                        else if (pf.Y < min)
                            pf.Y = (float)min;

                        pf.Y = (float)(zeroY - pf.Y);

                        punkty.Add(pf);
                    }

                    break;
                case FunctionTypeEnum.DifferentialII:

                    Differential rozniczkaII = new Differential(funkcja);

                    List<PointD> lewoII = new List<PointD>();
                    List<PointD> prawoII = new List<PointD>();

                    //W lewo
                    if (xOd < parameters[0])
                        lewoII = rozniczkaII.ComputeDifferentialII(xOd, parameters[0], parameters[1], parameters[2], parameters[3], false, krok);

                    //W prawo
                    rozniczkaII = new Differential(funkcja);

                    if (xDo >= parameters[0])
                        prawoII = rozniczkaII.ComputeDifferentialII(xDo, parameters[0], parameters[1], parameters[2], parameters[3], false, krok);

                    //ŁĄCZYMY
                    //odwracamy w lewo
                    lewoII.Reverse();

                    foreach (PointD point in lewoII)
                    {
                        //Normalne:
                        PointF pf = new PointF();

                        punktyWykresu.Add(point.ToPointF());

                        pf.X = (float)(point.X * wspX + zeroX);
                        pf.Y = (float)(point.Y * wspY);

                        if (pf.Y > max)
                            pf.Y = (float)max;
                        else if (pf.Y < min)
                            pf.Y = (float)min;

                        pf.Y = (float)(zeroY - pf.Y);

                        punkty.Add(pf);
                    }

                    //dodajemy prawo
                    foreach (PointD point in prawoII)
                    {
                        //Normalne:
                        PointF pf = new PointF();

                        punktyWykresu.Add(point.ToPointF());

                        pf.X = (float)(point.X * wspX + zeroX);
                        pf.Y = (float)(point.Y * wspY);

                        if (pf.Y > max)
                            pf.Y = (float)max;
                        else if (pf.Y < min)
                            pf.Y = (float)min;

                        pf.Y = (float)(zeroY - pf.Y);

                        punkty.Add(pf);
                    }

                    break;
                default:
                    throw new NoneGraphOptionCheckedException(); //not reachable code?
            }

            //Wypisanie wzoru funkcji
            g.DrawString("f(x) = " + funkcja, font2, Brushes.Black, 3, 3);

            switch (functionType)
            {
                case FunctionTypeEnum.Differential:
                    pen = Pens.PaleVioletRed;
                    break;
                case FunctionTypeEnum.DifferentialII:
                    pen = Pens.Maroon;
                    break;
            }

            //RYSOWANIE WYKRESU
            try
            {
                g.DrawLines(pen, punkty.ToArray());
            }
            catch (Exception)
            {
                //Pozbycie sie NaN - może pomoże
                punkty = punkty.Where(p => !double.IsNaN(p.Y)).ToList();
                
                try
                {
                    g.DrawLines(pen, punkty.ToArray());
                }
                catch { }
            }
        }

        public void DrawBessel(BesselFunctionType functionType, params double[] parameters)
        {
            punktyWykresu.Clear();

            int indexZmiennej = ZnajdzIndexZmiennej(functionType, parameters);

            BesselNeumanHyper bnh = new BesselNeumanHyper();
            List<PointF> punkty = new List<PointF>();

            //Obliczenie punktow
            if (indexZmiennej != -1)
            {
                for (double i = 0; i < picWidth; i += (double)picWidth / 1000.0)
                {
                    float fx = 0;
                    parameters[indexZmiennej] = (i - zeroX) / wspX;

                    switch (functionType)
                    {
                        case BesselFunctionType.Bessel:
                            fx = (float)(bnh.Bessel(parameters[0], parameters[1]) * wspY);
                            break;
                        case BesselFunctionType.BesselSphere:
                            fx = (float)(bnh.SphBessel(parameters[0], parameters[1]) * wspY);
                            break;
                        case BesselFunctionType.BesselSphereDerivative:
                            fx = (float)(bnh.SphBesselPrim(parameters[0], parameters[1]) * wspY);
                            break;
                        case BesselFunctionType.Neumann:
                            fx = (float)(bnh.Neumann(parameters[0], parameters[1]) * wspY);
                            break;
                        case BesselFunctionType.NeumannSphere:
                            fx = (float)(bnh.SphNeuman(parameters[0], parameters[1]) * wspY);
                            break;
                        case BesselFunctionType.NeumannSphereDerivative:
                            fx = (float)(bnh.SphNeumanPrim(parameters[0], parameters[1]) * wspY);
                            break;
                        case BesselFunctionType.Hypergeometric01:
                            fx = (float)(bnh.Hyperg_0F_1(parameters[0], parameters[1]) * wspY);
                            break;
                        case BesselFunctionType.Hypergeometric11:
                            fx = (float)(bnh.Hyperg_1F_1(parameters[0], parameters[1], parameters[2]) * wspY);
                            break;
                        case BesselFunctionType.Hypergeometric21:
                            fx = (float)(bnh.Hyperg_2F_1(parameters[0], parameters[1], parameters[2], parameters[3]) * wspY);
                            break;
                        default:
                            throw new Exception("Nie wybrano funkcji!"); //not reachable code?
                    }

                    if (fx > max)
                        fx = (float)max;
                    else if (fx < min)
                        fx = (float)min;

                    punkty.Add(new PointF((float)(i), (float)(zeroY - fx)));
                    punktyWykresu.Add(new PointF((float)parameters[indexZmiennej], (float)(fx / wspY)));
                }
            }
            else //Dodanie dwóch punktów - funkcja stała
            {
                float fx = 0;

                switch (functionType)
                {
                    case BesselFunctionType.Bessel:
                        fx = (float)(bnh.Bessel(parameters[0], parameters[1]) * wspY);
                        break;
                    case BesselFunctionType.BesselSphere:
                        fx = (float)(bnh.SphBessel(parameters[0], parameters[1]) * wspY);
                        break;
                    case BesselFunctionType.BesselSphereDerivative:
                        fx = (float)(bnh.SphBesselPrim(parameters[0], parameters[1]) * wspY);
                        break;
                    case BesselFunctionType.Neumann:
                        fx = (float)(bnh.Neumann(parameters[0], parameters[1]) * wspY);
                        break;
                    case BesselFunctionType.NeumannSphere:
                        fx = (float)(bnh.SphNeuman(parameters[0], parameters[1]) * wspY);
                        break;
                    case BesselFunctionType.NeumannSphereDerivative:
                        fx = (float)(bnh.SphNeumanPrim(parameters[0], parameters[1]) * wspY);
                        break;
                    case BesselFunctionType.Hypergeometric01:
                        fx = (float)(bnh.Hyperg_0F_1(parameters[0], parameters[1]) * wspY);
                        break;
                    case BesselFunctionType.Hypergeometric11:
                        fx = (float)(bnh.Hyperg_1F_1(parameters[0], parameters[1], parameters[2]) * wspY);
                        break;
                    case BesselFunctionType.Hypergeometric21:
                        fx = (float)(bnh.Hyperg_2F_1(parameters[0], parameters[1], parameters[2], parameters[3]) * wspY);
                        break;
                    default:
                        throw new Exception("Nie wybrano funkcji!"); //not reachable code?
                }

                if (fx > max)
                    fx = (float)max;
                else if (fx < min)
                    fx = (float)min;

                punkty.Add(new PointF((float)(0), (float)(zeroY - fx)));
                punkty.Add(new PointF((float)(picWidth), (float)(zeroY - fx)));
            }         
            
            //RYSOWANIE WYKRESU
            try
            {
                g.DrawLines(Pens.Orange, punkty.ToArray());
            }
            catch (Exception)
            {
                //Pozbycie sie NaN - może pomoże
                punkty = punkty.Where(p => !double.IsNaN(p.Y)).ToList();

                try
                {
                    g.DrawLines(Pens.Orange, punkty.ToArray());
                }
                catch { }
            }            
        }

        public double[] Reskalling(params FunctionTypeEnum[] functionType)
        {
            Derivative funkcjaWPunkcie = new Derivative(funkcja); // Do obliczania wartosci funkcji w punkcie
            List<PointF> punkty = new List<PointF>();

            //Obliczenie punktow
            for (double i = 0; i < picWidth; i += (double)picWidth / 100.0)
            {
                float fx = 0;
                double punkt = (i - zeroX) / wspX;

                if (functionType.Contains(FunctionTypeEnum.Function))
                {
                    fx = (float)funkcjaWPunkcie.ComputeFunctionAtPoint(punkt);

                    if (fx > max)
                        fx = (float)max;
                    else if (fx < min)
                        fx = (float)min;

                    punkty.Add(new PointF((float)punkt, fx));
                }

                if (functionType.Contains(FunctionTypeEnum.Derivative))
                {
                    fx = (float)funkcjaWPunkcie.ComputeDerivative(punkt);

                    if (fx > max)
                        fx = (float)max;
                    else if (fx < min)
                        fx = (float)min;

                    punkty.Add(new PointF((float)punkt, fx));
                }

                if (functionType.Contains(FunctionTypeEnum.SecondDerivative))
                {
                    fx = (float)funkcjaWPunkcie.ComputeDerivativeBis(punkt);

                    if (fx > max)
                        fx = (float)max;
                    else if (fx < min)
                        fx = (float)min;

                    punkty.Add(new PointF((float)punkt, fx));
                }
            }

            //Wyciagniecie punktow bez NaN
            List<PointF> nowePunkty = punkty.Where(pkt => (!double.IsNaN(pkt.Y) && !double.IsInfinity(pkt.Y))).ToList();

            if (nowePunkty.Count == 0)
                return new double[] { xOd, xDo, yOd, yDo };

            double minX, maxX;

            //Jak byly jakies NaN to zmieniamy X i powtarzamy reskalling
            if (punkty.Count() > nowePunkty.Count())
            {
                minX = nowePunkty.Select(p => p.X).Min();
                maxX = nowePunkty.Select(p => p.X).Max();

                //Obliczenie +-1%
                double jedenProcentX = Math.Abs(maxX - minX) * 0.01;

                minX += jedenProcentX;
                maxX -= jedenProcentX;

                minX = Math.Round(minX, 2);
                maxX = Math.Round(maxX, 2);

                Graph wykres = new Graph(funkcja, pWykres, minX, maxX, yOd, yDo);

                return wykres.Reskalling(functionType);
            }

            double maxY = nowePunkty.Select(p => p.Y).Max();
            double minY = nowePunkty.Select(p => p.Y).Min();

            //Obliczenie +-5%
            double piecProcentY = Math.Abs(maxY - minY) * 0.05;

            maxY += piecProcentY;
            minY -= piecProcentY;

            maxY = Math.Round(maxY, 2);
            minY = Math.Round(minY, 2);

            if (maxY > max)
                maxY = (float)max;
            else if (maxY < min)
                maxY = (float)min;

            if (minY > max)
                minY = (float)max;
            else if (minY < min)
                minY = (float)min;

            if (minY == maxY)
            {
                minY -= 0.5;
                maxY += 0.5;
            }

            double[] f = new double[] { xOd, xDo, minY, maxY };

            return f;
        }

        public double[] Reskalling(BesselFunctionType functionType, params double[] parameters)
        {
            int indexZmiennej = ZnajdzIndexZmiennej(functionType, parameters);

            BesselNeumanHyper bnh = new BesselNeumanHyper();
            List<PointF> punkty = new List<PointF>();

            //Obliczenie punktow
            if (indexZmiennej != -1)
            {
                for (double i = 0; i < picWidth; i += (double)picWidth / 100.0)
                {
                    float fx = 0;
                    parameters[indexZmiennej] = (i - zeroX) / wspX;

                    switch (functionType)
                    {
                        case BesselFunctionType.Bessel:
                            fx = (float)bnh.Bessel(parameters[0], parameters[1]);
                            break;
                        case BesselFunctionType.BesselSphere:
                            fx = (float)bnh.SphBessel(parameters[0], parameters[1]);
                            break;
                        case BesselFunctionType.BesselSphereDerivative:
                            fx = (float)bnh.SphBesselPrim(parameters[0], parameters[1]);
                            break;
                        case BesselFunctionType.Neumann:
                            fx = (float)bnh.Neumann(parameters[0], parameters[1]);
                            break;
                        case BesselFunctionType.NeumannSphere:
                            fx = (float)bnh.SphNeuman(parameters[0], parameters[1]);
                            break;
                        case BesselFunctionType.NeumannSphereDerivative:
                            fx = (float)bnh.SphNeumanPrim(parameters[0], parameters[1]);
                            break;
                        case BesselFunctionType.Hypergeometric01:
                            fx = (float)bnh.Hyperg_0F_1(parameters[0], parameters[1]);
                            break;
                        case BesselFunctionType.Hypergeometric11:
                            fx = (float)bnh.Hyperg_1F_1(parameters[0], parameters[1], parameters[2]);
                            break;
                        case BesselFunctionType.Hypergeometric21:
                            fx = (float)bnh.Hyperg_2F_1(parameters[0], parameters[1], parameters[2], parameters[3]);
                            break;
                        default:
                            throw new Exception("Nie wybrano funkcji!"); //not reachable code?
                    }

                    if (fx > max)
                        fx = (float)max;
                    else if (fx < min)
                        fx = (float)min;

                    punkty.Add(new PointF((float)(parameters[indexZmiennej]), fx));
                }
            }
            else //Dodanie dwóch punktów - funkcja stała
            {
                float fx = 0;

                switch (functionType)
                {
                    case BesselFunctionType.Bessel:
                        fx = (float)bnh.Bessel(parameters[0], parameters[1]);
                        break;
                    case BesselFunctionType.BesselSphere:
                        fx = (float)bnh.SphBessel(parameters[0], parameters[1]);
                        break;
                    case BesselFunctionType.BesselSphereDerivative:
                        fx = (float)bnh.SphBesselPrim(parameters[0], parameters[1]);
                        break;
                    case BesselFunctionType.Neumann:
                        fx = (float)bnh.Neumann(parameters[0], parameters[1]);
                        break;
                    case BesselFunctionType.NeumannSphere:
                        fx = (float)bnh.SphNeuman(parameters[0], parameters[1]);
                        break;
                    case BesselFunctionType.NeumannSphereDerivative:
                        fx = (float)bnh.SphNeumanPrim(parameters[0], parameters[1]);
                        break;
                    case BesselFunctionType.Hypergeometric01:
                        fx = (float)bnh.Hyperg_0F_1(parameters[0], parameters[1]);
                        break;
                    case BesselFunctionType.Hypergeometric11:
                        fx = (float)bnh.Hyperg_1F_1(parameters[0], parameters[1], parameters[2]);
                        break;
                    case BesselFunctionType.Hypergeometric21:
                        fx = (float)bnh.Hyperg_2F_1(parameters[0], parameters[1], parameters[2], parameters[3]);
                        break;
                    default:
                        throw new Exception("Nie wybrano funkcji!"); //not reachable code?
                }

                if (fx > max)
                    fx = (float)max;
                else if (fx < min)
                    fx = (float)min;

                punkty.Add(new PointF((float)((0 - zeroX) / wspX), fx));
                punkty.Add(new PointF((float)((picWidth - zeroX) / wspX), fx));
            }

            //Wyciagniecie punktow bez NaN
            List<PointF> nowePunkty = punkty.Where(pkt => (!double.IsNaN(pkt.Y) && !double.IsInfinity(pkt.Y))).ToList();

            if (nowePunkty.Count == 0)
                return new double[] { xOd, xDo, yOd, yDo};

            double minX, maxX;

            //Jak byly jakies NaN to zmieniamy X i powtarzamy reskalling
            if (punkty.Count() > nowePunkty.Count())
            {
                minX = nowePunkty.Select(p => p.X).Min();
                maxX = nowePunkty.Select(p => p.X).Max();

                //Obliczenie +-1%
                double jedenProcentX = Math.Abs(maxX - minX) * 0.01;

                minX += jedenProcentX;
                maxX -= jedenProcentX;

                minX = Math.Round(minX, 2);
                maxX = Math.Round(maxX, 2);

                Graph wykres = new Graph(funkcja, pWykres, minX, maxX, yOd, yDo);

                parameters[indexZmiennej] = double.NaN;

                return wykres.Reskalling(functionType, parameters);
            }

            double maxY = nowePunkty.Select(p => p.Y).Max();
            double minY = nowePunkty.Select(p => p.Y).Min();

            //Obliczenie +-5%
            double piecProcentY = Math.Abs(maxY - minY) * 0.05;

            maxY += piecProcentY;
            minY -= piecProcentY;

            maxY = Math.Round(maxY, 2);
            minY = Math.Round(minY, 2);

            if (maxY > max)
                maxY = (float)max;
            else if (maxY < min)
                maxY = (float)min;

            if (minY > max)
                minY = (float)max;
            else if (minY < min)
                minY = (float)min;

            if (minY == maxY)
            {
                minY -= 0.5;
                maxY += 0.5;
            }

            double[] f = new double[] { xOd, xDo, minY, maxY };            

            return f;
        }

        private void NapiszWzorFunkcjiBesselowej(BesselFunctionType typFunkcji, double[] parametry, int indexZmiennej)
        {
            Font font = new Font("Arial", 8); // Do wypisywania wzoru funkcji

            string parametryString = string.Empty;

            //Zbudowanie stringu parametrow
            if (typFunkcji == BesselFunctionType.Hypergeometric21)
            {
                parametryString += (indexZmiennej != 0) ? parametry[0].ToString() : "x";
                parametryString += ", ";
                parametryString += (indexZmiennej != 1) ? parametry[1].ToString() : "x";
                parametryString += ", ";
                parametryString += (indexZmiennej != 2) ? parametry[2].ToString() : "x";
                parametryString += ", ";
                parametryString += (indexZmiennej != 3) ? parametry[3].ToString() : "x";
            }
            else if (typFunkcji == BesselFunctionType.Hypergeometric11)
            {
                parametryString += (indexZmiennej != 0) ? parametry[0].ToString() : "x";
                parametryString += ", ";
                parametryString += (indexZmiennej != 1) ? parametry[1].ToString() : "x";
                parametryString += ", ";
                parametryString += (indexZmiennej != 2) ? parametry[2].ToString() : "x";
            }
            else
            {
                parametryString += (indexZmiennej != 0) ? parametry[0].ToString() : "x";
                parametryString += ", ";
                parametryString += (indexZmiennej != 1) ? parametry[1].ToString() : "x";
            }

            //Wypisanie nazw
            switch (typFunkcji)
            {
                case BesselFunctionType.Bessel:
                    g.DrawString("Bessel(" + parametryString + ")", font, Brushes.Orange, 3, 3);
                    break;
                case BesselFunctionType.BesselSphere:
                    g.DrawString("Sferyczna f. Bessela(" + parametryString + ")", font, Brushes.Orange, 3, 3);
                    break;
                case BesselFunctionType.BesselSphereDerivative:
                    g.DrawString("Poch. sfer. f. Bessela(" + parametryString + ")", font, Brushes.Orange, 3, 3);
                    break;
                case BesselFunctionType.Neumann:
                    g.DrawString("Neumann(" + parametryString + ")", font, Brushes.Orange, 3, 3);
                    break;
                case BesselFunctionType.NeumannSphere:
                    g.DrawString("Sferyczna f. Neumanna(" + parametryString + ")", font, Brushes.Orange, 3, 3);
                    break;
                case BesselFunctionType.NeumannSphereDerivative:
                    g.DrawString("Poch. sfer. f. Neumanna(" + parametryString + ")", font, Brushes.Orange, 3, 3);
                    break;
                case BesselFunctionType.Hypergeometric01:
                    g.DrawString("Hipergeometryczna0F1(" + parametryString + ")", font, Brushes.Orange, 3, 3);
                    break;
                case BesselFunctionType.Hypergeometric11:
                    g.DrawString("Hipergeometryczna1F1(" + parametryString + ")", font, Brushes.Orange, 3, 3);
                    break;
                case BesselFunctionType.Hypergeometric21:
                    g.DrawString("Hipergeometryczna2F1(" + parametryString + ")", font, Brushes.Orange, 3, 3);
                    break;
                default:
                    throw new Exception("Nie wybrano funkcji!"); //not reachable code?
            }
        }

        //private List<PointF> ObliczPunktyBesselow(TypFunkcjiBessela typFunkcji, double[] parametry, int indexZmiennej)
        //{
        //    BesselNeumanHyper bnh = new BesselNeumanHyper();
        //    List<PointF> punkty = new List<PointF>();

        //    //Obliczenie punktow
        //    if (indexZmiennej != -1)
        //    {
        //        for (double i = 0; i < picWidth; i += (double)picWidth / 1000.0)
        //        {
        //            float fx = 0;
        //            parametry[indexZmiennej] = (i - zeroX) / wspX;

        //            switch (typFunkcji)
        //            {
        //                case TypFunkcjiBessela.Bessel:
        //                    fx = (float)(bnh.Bessel(parametry[0], parametry[1]) * wspY);
        //                    break;
        //                case TypFunkcjiBessela.BesselSferyczny:
        //                    fx = (float)(bnh.SphBessel(parametry[0], parametry[1]) * wspY);
        //                    break;
        //                case TypFunkcjiBessela.BesselPochodnaSferycznej:
        //                    fx = (float)(bnh.SphBesselPrim(parametry[0], parametry[1]) * wspY);
        //                    break;
        //                case TypFunkcjiBessela.Neumann:
        //                    fx = (float)(bnh.Neumann(parametry[0], parametry[1]) * wspY);
        //                    break;
        //                case TypFunkcjiBessela.NeumannSferyczny:
        //                    fx = (float)(bnh.SphNeuman(parametry[0], parametry[1]) * wspY);
        //                    break;
        //                case TypFunkcjiBessela.NeumannPochodnaSferycznej:
        //                    fx = (float)(bnh.SphNeumanPrim(parametry[0], parametry[1]) * wspY);
        //                    break;
        //                case TypFunkcjiBessela.Hipergeometryczna01:
        //                    fx = (float)(bnh.Hyperg_0F_1(parametry[0], parametry[1]) * wspY);
        //                    break;
        //                case TypFunkcjiBessela.Hipergeometryczna11:
        //                    fx = (float)(bnh.Hyperg_1F_1(parametry[0], parametry[1], parametry[2]) * wspY);
        //                    break;
        //                case TypFunkcjiBessela.Hipergeometryczna21:
        //                    fx = (float)(bnh.Hyperg_2F_1(parametry[0], parametry[1], parametry[2], parametry[3]) * wspY);
        //                    break;
        //                default:
        //                    throw new Exception("Nie wybrano funkcji!"); //not reachable code?
        //            }

        //            if (fx > max)
        //                fx = (float)max;
        //            else if (fx < min)
        //                fx = (float)min;

        //            punkty.Add(new PointF((float)(i), (float)(zeroY - fx)));
        //        }
        //    }
        //    else //Dodanie dwóch punktów - funkcja stała
        //    {
        //        float fx = 0;

        //        switch (typFunkcji)
        //        {
        //            case TypFunkcjiBessela.Bessel:
        //                fx = (float)(bnh.Bessel(parametry[0], parametry[1]) * wspY);
        //                break;
        //            case TypFunkcjiBessela.BesselSferyczny:
        //                fx = (float)(bnh.SphBessel(parametry[0], parametry[1]) * wspY);
        //                break;
        //            case TypFunkcjiBessela.BesselPochodnaSferycznej:
        //                fx = (float)(bnh.SphBesselPrim(parametry[0], parametry[1]) * wspY);
        //                break;
        //            case TypFunkcjiBessela.Neumann:
        //                fx = (float)(bnh.Neumann(parametry[0], parametry[1]) * wspY);
        //                break;
        //            case TypFunkcjiBessela.NeumannSferyczny:
        //                fx = (float)(bnh.SphNeuman(parametry[0], parametry[1]) * wspY);
        //                break;
        //            case TypFunkcjiBessela.NeumannPochodnaSferycznej:
        //                fx = (float)(bnh.SphNeumanPrim(parametry[0], parametry[1]) * wspY);
        //                break;
        //            case TypFunkcjiBessela.Hipergeometryczna01:
        //                fx = (float)(bnh.Hyperg_0F_1(parametry[0], parametry[1]) * wspY);
        //                break;
        //            case TypFunkcjiBessela.Hipergeometryczna11:
        //                fx = (float)(bnh.Hyperg_1F_1(parametry[0], parametry[1], parametry[2]) * wspY);
        //                break;
        //            case TypFunkcjiBessela.Hipergeometryczna21:
        //                fx = (float)(bnh.Hyperg_2F_1(parametry[0], parametry[1], parametry[2], parametry[3]) * wspY);
        //                break;
        //            default:
        //                throw new Exception("Nie wybrano funkcji!"); //not reachable code?
        //        }

        //        if (fx > max)
        //            fx = (float)max;
        //        else if (fx < min)
        //            fx = (float)min;

        //        punkty.Add(new PointF((float)(0), (float)(zeroY - fx)));
        //        punkty.Add(new PointF((float)(picWidth), (float)(zeroY - fx)));
        //    }

        //    return punkty;
        //}

        private int ZnajdzIndexZmiennej(BesselFunctionType typFunkcji, double[] parametry)
        {
            int indexZmiennej = -1;

            //Znalezienie indexu zmiennej
            if (typFunkcji == BesselFunctionType.Bessel || typFunkcji == BesselFunctionType.BesselSphere || typFunkcji == BesselFunctionType.BesselSphereDerivative || typFunkcji == BesselFunctionType.Neumann || typFunkcji == BesselFunctionType.NeumannSphere || typFunkcji == BesselFunctionType.NeumannSphereDerivative || typFunkcji == BesselFunctionType.Hypergeometric01)
            {
                if (double.IsNaN(parametry[0]))
                    indexZmiennej = 0;

                if (double.IsNaN(parametry[1]))
                {
                    if (indexZmiennej == -1)
                        indexZmiennej = 1;
                    else
                        throw new BesseleSecondArgumentException("Zmienna x może występować tylko jako jeden argument!");
                }
            }
            else if (typFunkcji == BesselFunctionType.Hypergeometric11)
            {
                if (double.IsNaN(parametry[0]))
                    indexZmiennej = 0;

                if (double.IsNaN(parametry[1]))
                {
                    if (indexZmiennej == -1)
                        indexZmiennej = 1;
                    else
                        throw new BesseleSecondArgumentException("Zmienna x może występować tylko jako jeden argument!");
                }

                if (double.IsNaN(parametry[2]))
                {
                    if (indexZmiennej == -1)
                        indexZmiennej = 2;
                    else
                        throw new BesseleThirdArgumentException("Zmienna x może występować tylko jako jeden argument!");
                }
            }
            else if (typFunkcji == BesselFunctionType.Hypergeometric21)
            {
                if (double.IsNaN(parametry[0]))
                    indexZmiennej = 0;

                if (double.IsNaN(parametry[1]))
                {
                    if (indexZmiennej == -1)
                        indexZmiennej = 1;
                    else
                        throw new BesseleSecondArgumentException("Zmienna x może występować tylko jako jeden argument!");
                }

                if (double.IsNaN(parametry[2]))
                {
                    if (indexZmiennej == -1)
                        indexZmiennej = 2;
                    else
                        throw new BesseleThirdArgumentException("Zmienna x może występować tylko jako jeden argument!");
                }

                if (double.IsNaN(parametry[3]))
                {
                    if (indexZmiennej == -1)
                        indexZmiennej = 3;
                    else
                        throw new BesseleFourthArgumentException("Zmienna x może występować tylko jako jeden argument!");
                }
            }

            return indexZmiennej;
        }



    // Konstruktor ------------------------
        public Graph(string function, PictureBox picGraph, double xFrom, double xTo, double yFrom, double yTo)
        {
            Initialize(function, picGraph, xFrom, xTo, yFrom, yTo);
        }

        private void Initialize(string funk, PictureBox picWykres, double xOd, double xDo, double yOd, double yDo)
        {
            if (xDo == xOd)
                throw new CoordinatesXException();

            if (yDo == yOd)
                throw new CoordinatesYException();

            funkcja = funk;
            this.pWykres = picWykres;
            picWidth = picWykres.Width;
            picHeight = picWykres.Height;

            pWykres.Image = new Bitmap(picWidth, picHeight);
            g = Graphics.FromImage(pWykres.Image);

            //Zabezpieczenie przed za duzymi granicami wykresu
            if (xOd > max)
                xOd = max;
            else if (xOd < min)
                xOd = min;

            if (xDo > max)
                xDo = max;
            else if (xDo < min)
                xDo = min;

            if (yOd > max)
                yOd = max;
            else if (yOd < min)
                yOd = min;

            if (yDo > max)
                yDo = max;
            else if (yDo < min)
                yDo = min;

            //Przypisanie granic
            this.xOd = xOd;
            this.xDo = xDo;
            this.yOd = yOd;
            this.yDo = yDo;

            // Obliczanie miejsc zerowych i wspolczynnikow X
            if (xOd * xDo <= 0) //Czy kreska zerowa X jest widoczna na wykresie
            {
                wspX = picWidth / (-xOd + xDo);
                zeroX = -xOd * wspX;
            }
            else if (xOd < 0) // Miejsce zerowe nie widoczne, obydwa sa ujemne
            {
                wspX = picWidth / (-xOd + xDo);
                zeroX = picWidth + (-xDo * wspX);
            }
            else // Miejsce zerowe nie widoczne, obydwa sa dodatnie
            {
                wspX = picWidth / (xDo - xOd);
                zeroX = (-xOd * wspX);
            }

            // Obliczanie miejsc zerowych i wspolczynnikow Y
            if (yOd * yDo <= 0)
            {
                wspY = picHeight / (-yOd + yDo);
                zeroY = yDo * wspY;
            }
            else if (yOd < 0 && yDo < 0) // Miejsce zerowe nie widoczne, obydwa sa ujemne
            {
                wspY = picHeight / (-yOd + yDo);
                zeroY = yDo * wspY;
            }
            else // Miejsce zerowe nie widoczne, obydwa sa dodatnie
            {
                wspY = picHeight / (yDo - yOd);
                zeroY = picHeight + (yOd * wspY);
            }

            //Czyszczenie starego wykresu
            Clear();
        }
    }
}
