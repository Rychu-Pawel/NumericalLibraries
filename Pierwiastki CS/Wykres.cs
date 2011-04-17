using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace Pierwiastki_CS
{
    class Wykres
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

    // Metody -----------------------------
        public void Wyczysc() //Wyczyszczenie starego wykresu i przygotowanie siatki X i Y
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
        }

        public void Rysuj(TypFunkcji typFunkcji)
        {
            Pochodna funkcjaWPunkcie = new Pochodna(funkcja);

            Font font2 = new Font("Arial", 8); // Do wypisywania wzoru funkcji

            List<PointF> punkty = new List<PointF>();

            Pen pen = null;

            for (double i = 0; i < picWidth; i += (double)picWidth / 1000.0)
            {
                float fx = 0;

                switch (typFunkcji)
                {
                    case TypFunkcji.Funkcja:
                        fx = (float)(funkcjaWPunkcie.ObliczFunkcjeWPunkcie((i - zeroX) / wspX) * wspY);
                        break;
                    case TypFunkcji.Pochodna:
                        fx = (float)(funkcjaWPunkcie.ObliczPochodna((i - zeroX) / wspX) * wspY);
                        break;
                    case TypFunkcji.DrugaPochodna:
                        fx = (float)(funkcjaWPunkcie.ObliczPochodnaBis((i - zeroX) / wspX) * wspY);
                        break;
                    default:
                        throw new NoneWykresOptionCheckedException(); //not reachable code?
                }

                if (fx > max)
                    fx = (float)max;
                else if (fx < min)
                    fx = (float)min;

                punkty.Add(new PointF((float)(i), (float)(zeroY - fx)));
            }

            //Wypisanie wzoru funkcji
            g.DrawString("f(x) = " + funkcja, font2, Brushes.Black, 3, 3);

            switch (typFunkcji)
            {
                case TypFunkcji.Funkcja:
                    pen = Pens.Blue;
                    break;
                case TypFunkcji.Pochodna:
                    pen = Pens.Red;
                    break;
                case TypFunkcji.DrugaPochodna:
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

        public void RysujFFT(TypFunkcji typFunkcji, int probkowanie, double odciecie = 0.0)
        {
            Font font2 = new Font("Arial", 8); // Do wypisywania wzoru funkcji

            List<PointF> punkty = new List<PointF>();
            List<PointF> punktyRevers = new List<PointF>();

            List<PointC> punktyC = new List<PointC>();
            List<PointC> punktyReversC = new List<PointC>();

            Pen pen = null;

            FastFourierTransform fft = new FastFourierTransform();

            //Obliczenie FFT
            punktyC = fft.Oblicz(funkcja, probkowanie, xOd, xDo);

            //Przefiltrowanie
            for (int i = 0; i < punktyC.Count; i++)
            {
                PointC pc = punktyC[i];

                if (Math.Abs(pc.Y.Real) < odciecie)
                    pc.Y = 0;

                punktyC[i] = pc;
            }

            //Obliczenie Reverse FFT
            if (typFunkcji == TypFunkcji.RFFT)
                punktyReversC = fft.ObliczOdwrocona(punktyC, probkowanie, xOd, xDo);

            //Nowe przygotowanie
            if (typFunkcji == TypFunkcji.FFT)
                Przygotowanie(funkcja, pWykres, 0, probkowanie + 3, yOd, yDo);

            //Przeskalowanie pkt-ow
            if (typFunkcji == TypFunkcji.FFT)
            {
                for (int i = 0; i < punktyC.Count; i++)
                {
                    PointC pc = punktyC[i];

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

            if (typFunkcji == TypFunkcji.RFFT)
            {
                //Przeskalowanie pkt-ow Reverse
                for (int i = 0; i < punktyReversC.Count; i++)
                {
                    PointC pc = punktyReversC[i];

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
            if (typFunkcji == TypFunkcji.FFT)
                g.DrawString("FFT(" + '\u03C9' + ") = " + funkcja, font2, Brushes.Black, 3, 16);
            else
                g.DrawString("RFFT(x) = " + funkcja, font2, Brushes.Black, 3, 16);

            switch (typFunkcji)
            {
                case TypFunkcji.FFT:
                    pen = Pens.PaleVioletRed;
                    break;
                case TypFunkcji.RFFT:
                    pen = Pens.Red;
                    break;
                default:
                    pen = Pens.Red;
                    break;
            }

            //RYSOWANIE WYKRESU
            try
            {
                if (typFunkcji == TypFunkcji.FFT)
                    g.DrawLines(pen, punkty.ToArray());
                else
                    g.DrawLines(pen, punktyRevers.ToArray());
            }
            catch (Exception)
            {
                //Pozbycie sie NaN - może pomoże
                if (typFunkcji == TypFunkcji.FFT)
                    punkty = punkty.Where(p => !double.IsNaN(p.Y)).ToList();
                else
                    punktyRevers = punktyRevers.Where(p => !double.IsNaN(p.Y)).ToList();

                try
                {
                    if (typFunkcji == TypFunkcji.FFT)
                        g.DrawLines(pen, punkty.ToArray());
                    else
                        g.DrawLines(pen, punktyRevers.ToArray());
                }
                catch { }
            }
        }

        public void RysujRozniczke(TypFunkcji typFunkcji, bool czyRysowacTylkoEnergie, params double[] parametry)
        {
            List<PointF> punkty = new List<PointF>();
            List<PointF> energia = new List<PointF>();
            List<PointF> energia2 = new List<PointF>();

            bool czyRysowacEnergie = parametry.Length > 4;

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
            switch (typFunkcji)
            {
                case TypFunkcji.Rozniczka:

                    Rozniczka rozniczka = new Rozniczka(funkcja);

                    List<PointD> lewo = new List<PointD>();
                    List<PointD> prawo = new List<PointD>();

                    //W lewo
                    if (xOd < parametry[0])
                        lewo = rozniczka.ObliczRozniczke(xOd, parametry[0], parametry[1], false, krok);

                    //W prawo
                    rozniczka = new Rozniczka(funkcja);

                    if (xDo >= parametry[0])
                        prawo = rozniczka.ObliczRozniczke(xDo, parametry[0], parametry[1], false, krok);

                    //ŁĄCZYMY
                    //odwracamy w lewo
                    lewo.Reverse();

                    //dodajemy lewo
                    foreach (PointD point in lewo)
                    {
                        PointF pf = new PointF();

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
                case TypFunkcji.RozniczkaII:

                    Rozniczka rozniczkaII = new Rozniczka(funkcja);

                    List<PointD> lewoII = new List<PointD>();
                    List<PointD> prawoII = new List<PointD>();

                    //W lewo
                    if (xOd < parametry[0])
                        lewoII = rozniczkaII.ObliczRozniczkeII(xOd, parametry[0], parametry[1], parametry[2], parametry[3], false, krok);

                    //W prawo
                    rozniczkaII = new Rozniczka(funkcja);

                    if (xDo >= parametry[0])
                        prawoII = rozniczkaII.ObliczRozniczkeII(xDo, parametry[0], parametry[1], parametry[2], parametry[3], false, krok);

                    //ŁĄCZYMY
                    //odwracamy w lewo
                    lewoII.Reverse();

                    //dodajemy lewo
                    double dzielnik = 2.0; //dzielnik dla energii - jak jest 1.91 - 1.92 to moim zdaniem ładniej wychodzi

                    foreach (PointD point in lewoII)
                    {
                        if (czyRysowacEnergie)
                        {
                            //Energia:
                            PointF energiaPoint = new PointF();
                            energiaPoint.X = (float)(point.X * wspX + zeroX);
                            energiaPoint.Y = (float)((parametry[1] * Math.Exp(parametry[4] / dzielnik * point.X)) * wspY);

                            if (energiaPoint.Y > max)
                                energiaPoint.Y = (float)max;
                            else if (energiaPoint.Y < min)
                                energiaPoint.Y = (float)min;

                            energiaPoint.Y = (float)(zeroY - energiaPoint.Y);

                            energia.Add(energiaPoint);

                            //Energia2:
                            PointF energiaPoint2 = new PointF();
                            energiaPoint2.X = (float)(point.X * wspX + zeroX);
                            energiaPoint2.Y = (float)((-parametry[1] * Math.Exp(parametry[4] / dzielnik * point.X)) * wspY);

                            if (energiaPoint2.Y > max)
                                energiaPoint2.Y = (float)max;
                            else if (energiaPoint2.Y < min)
                                energiaPoint2.Y = (float)min;

                            energiaPoint2.Y = (float)(zeroY - energiaPoint2.Y);

                            energia2.Add(energiaPoint2);
                        }

                        //Normalne:
                        PointF pf = new PointF();

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
                        if (czyRysowacEnergie)
                        {
                            //Energia:
                            PointF energiaPoint = new PointF();
                            energiaPoint.X = (float)(point.X * wspX + zeroX);
                            energiaPoint.Y = (float)((parametry[1] * Math.Exp(parametry[4] / dzielnik * point.X)) * wspY);

                            if (energiaPoint.Y > max)
                                energiaPoint.Y = (float)max;
                            else if (energiaPoint.Y < min)
                                energiaPoint.Y = (float)min;

                            energiaPoint.Y = (float)(zeroY - energiaPoint.Y);

                            energia.Add(energiaPoint);

                            //Energia2:
                            PointF energiaPoint2 = new PointF();
                            energiaPoint2.X = (float)(point.X * wspX + zeroX);
                            energiaPoint2.Y = (float)((-parametry[1] * Math.Exp(parametry[4] / dzielnik * point.X)) * wspY);

                            if (energiaPoint2.Y > max)
                                energiaPoint2.Y = (float)max;
                            else if (energiaPoint2.Y < min)
                                energiaPoint2.Y = (float)min;

                            energiaPoint2.Y = (float)(zeroY - energiaPoint2.Y);

                            energia2.Add(energiaPoint2);
                        }

                        //Normalne:
                        PointF pf = new PointF();

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
                    throw new NoneWykresOptionCheckedException(); //not reachable code?
            }

            //Wypisanie wzoru funkcji
            g.DrawString("f(x) = " + funkcja, font2, Brushes.Black, 3, 3);

            switch (typFunkcji)
            {
                case TypFunkcji.Rozniczka:
                    pen = Pens.PaleVioletRed;
                    break;
                case TypFunkcji.RozniczkaII:
                    pen = Pens.Maroon;
                    break;
            }

            //RYSOWANIE WYKRESU
            try
            {
                if (!czyRysowacTylkoEnergie)
                    g.DrawLines(pen, punkty.ToArray());

                if (czyRysowacEnergie)
                {
                    //Energia:
                    g.DrawLines(Pens.DarkSeaGreen, energia.ToArray());
                    g.DrawLines(Pens.DarkSeaGreen, energia2.ToArray());
                }
            }
            catch (Exception)
            {
                //Pozbycie sie NaN - może pomoże
                if (!czyRysowacTylkoEnergie)
                    punkty = punkty.Where(p => !double.IsNaN(p.Y)).ToList();

                if (czyRysowacEnergie)
                {
                    //W energii
                    energia = energia.Where(p => !double.IsNaN(p.Y)).ToList();
                    energia2 = energia2.Where(p => !double.IsNaN(p.Y)).ToList();
                }

                try
                {
                    if (!czyRysowacTylkoEnergie)
                        g.DrawLines(pen, punkty.ToArray());

                    if (czyRysowacEnergie)
                    {
                        //Energia:
                        g.DrawLines(Pens.DarkSeaGreen, energia.ToArray());
                        g.DrawLines(Pens.DarkSeaGreen, energia2.ToArray());
                    }
                }
                catch { }
            }
        }

        public void RysujBessele(TypFunkcjiBessela typFunkcji, params double[] parametry)
        {
            int indexZmiennej = ZnajdzIndexZmiennej(typFunkcji, parametry);

            BesselNeumanHyper bnh = new BesselNeumanHyper();
            List<PointF> punkty = new List<PointF>();

            //Obliczenie punktow
            if (indexZmiennej != -1)
            {
                for (double i = 0; i < picWidth; i += (double)picWidth / 1000.0)
                {
                    float fx = 0;
                    parametry[indexZmiennej] = (i - zeroX) / wspX;

                    switch (typFunkcji)
                    {
                        case TypFunkcjiBessela.Bessel:
                            fx = (float)(bnh.Bessel(parametry[0], parametry[1]) * wspY);
                            break;
                        case TypFunkcjiBessela.BesselSferyczny:
                            fx = (float)(bnh.SphBessel(parametry[0], parametry[1]) * wspY);
                            break;
                        case TypFunkcjiBessela.BesselPochodnaSferycznej:
                            fx = (float)(bnh.SphBesselPrim(parametry[0], parametry[1]) * wspY);
                            break;
                        case TypFunkcjiBessela.Neumann:
                            fx = (float)(bnh.Neumann(parametry[0], parametry[1]) * wspY);
                            break;
                        case TypFunkcjiBessela.NeumannSferyczny:
                            fx = (float)(bnh.SphNeuman(parametry[0], parametry[1]) * wspY);
                            break;
                        case TypFunkcjiBessela.NeumannPochodnaSferycznej:
                            fx = (float)(bnh.SphNeumanPrim(parametry[0], parametry[1]) * wspY);
                            break;
                        case TypFunkcjiBessela.Hipergeometryczna01:
                            fx = (float)(bnh.Hyperg_0F_1(parametry[0], parametry[1]) * wspY);
                            break;
                        case TypFunkcjiBessela.Hipergeometryczna11:
                            fx = (float)(bnh.Hyperg_1F_1(parametry[0], parametry[1], parametry[2]) * wspY);
                            break;
                        case TypFunkcjiBessela.Hipergeometryczna21:
                            fx = (float)(bnh.Hyperg_2F_1(parametry[0], parametry[1], parametry[2], parametry[3]) * wspY);
                            break;
                        default:
                            throw new Exception("Nie wybrano funkcji!"); //not reachable code?
                    }

                    if (fx > max)
                        fx = (float)max;
                    else if (fx < min)
                        fx = (float)min;

                    punkty.Add(new PointF((float)(i), (float)(zeroY - fx)));
                }
            }
            else //Dodanie dwóch punktów - funkcja stała
            {
                float fx = 0;

                switch (typFunkcji)
                {
                    case TypFunkcjiBessela.Bessel:
                        fx = (float)(bnh.Bessel(parametry[0], parametry[1]) * wspY);
                        break;
                    case TypFunkcjiBessela.BesselSferyczny:
                        fx = (float)(bnh.SphBessel(parametry[0], parametry[1]) * wspY);
                        break;
                    case TypFunkcjiBessela.BesselPochodnaSferycznej:
                        fx = (float)(bnh.SphBesselPrim(parametry[0], parametry[1]) * wspY);
                        break;
                    case TypFunkcjiBessela.Neumann:
                        fx = (float)(bnh.Neumann(parametry[0], parametry[1]) * wspY);
                        break;
                    case TypFunkcjiBessela.NeumannSferyczny:
                        fx = (float)(bnh.SphNeuman(parametry[0], parametry[1]) * wspY);
                        break;
                    case TypFunkcjiBessela.NeumannPochodnaSferycznej:
                        fx = (float)(bnh.SphNeumanPrim(parametry[0], parametry[1]) * wspY);
                        break;
                    case TypFunkcjiBessela.Hipergeometryczna01:
                        fx = (float)(bnh.Hyperg_0F_1(parametry[0], parametry[1]) * wspY);
                        break;
                    case TypFunkcjiBessela.Hipergeometryczna11:
                        fx = (float)(bnh.Hyperg_1F_1(parametry[0], parametry[1], parametry[2]) * wspY);
                        break;
                    case TypFunkcjiBessela.Hipergeometryczna21:
                        fx = (float)(bnh.Hyperg_2F_1(parametry[0], parametry[1], parametry[2], parametry[3]) * wspY);
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

        public double[] Reskalling(params TypFunkcji[] typFunkcji)
        {
            Pochodna funkcjaWPunkcie = new Pochodna(funkcja); // Do obliczania wartosci funkcji w punkcie
            List<PointF> punkty = new List<PointF>();

            //Obliczenie punktow
            for (double i = 0; i < picWidth; i += (double)picWidth / 100.0)
            {
                float fx = 0;
                double punkt = (i - zeroX) / wspX;

                if (typFunkcji.Contains(TypFunkcji.Funkcja))
                {
                    fx = (float)funkcjaWPunkcie.ObliczFunkcjeWPunkcie(punkt);

                    if (fx > max)
                        fx = (float)max;
                    else if (fx < min)
                        fx = (float)min;

                    punkty.Add(new PointF((float)punkt, fx));
                }

                if (typFunkcji.Contains(TypFunkcji.Pochodna))
                {
                    fx = (float)funkcjaWPunkcie.ObliczPochodna(punkt);

                    if (fx > max)
                        fx = (float)max;
                    else if (fx < min)
                        fx = (float)min;

                    punkty.Add(new PointF((float)punkt, fx));
                }

                if (typFunkcji.Contains(TypFunkcji.DrugaPochodna))
                {
                    fx = (float)funkcjaWPunkcie.ObliczPochodnaBis(punkt);

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

                Wykres wykres = new Wykres(funkcja, pWykres, minX, maxX, yOd, yDo);

                return wykres.Reskalling(typFunkcji);
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

        public double[] Reskalling(TypFunkcjiBessela typFunkcji, params double[] parametry)
        {
            int indexZmiennej = ZnajdzIndexZmiennej(typFunkcji, parametry);

            BesselNeumanHyper bnh = new BesselNeumanHyper();
            List<PointF> punkty = new List<PointF>();

            //Obliczenie punktow
            if (indexZmiennej != -1)
            {
                for (double i = 0; i < picWidth; i += (double)picWidth / 100.0)
                {
                    float fx = 0;
                    parametry[indexZmiennej] = (i - zeroX) / wspX;

                    switch (typFunkcji)
                    {
                        case TypFunkcjiBessela.Bessel:
                            fx = (float)bnh.Bessel(parametry[0], parametry[1]);
                            break;
                        case TypFunkcjiBessela.BesselSferyczny:
                            fx = (float)bnh.SphBessel(parametry[0], parametry[1]);
                            break;
                        case TypFunkcjiBessela.BesselPochodnaSferycznej:
                            fx = (float)bnh.SphBesselPrim(parametry[0], parametry[1]);
                            break;
                        case TypFunkcjiBessela.Neumann:
                            fx = (float)bnh.Neumann(parametry[0], parametry[1]);
                            break;
                        case TypFunkcjiBessela.NeumannSferyczny:
                            fx = (float)bnh.SphNeuman(parametry[0], parametry[1]);
                            break;
                        case TypFunkcjiBessela.NeumannPochodnaSferycznej:
                            fx = (float)bnh.SphNeumanPrim(parametry[0], parametry[1]);
                            break;
                        case TypFunkcjiBessela.Hipergeometryczna01:
                            fx = (float)bnh.Hyperg_0F_1(parametry[0], parametry[1]);
                            break;
                        case TypFunkcjiBessela.Hipergeometryczna11:
                            fx = (float)bnh.Hyperg_1F_1(parametry[0], parametry[1], parametry[2]);
                            break;
                        case TypFunkcjiBessela.Hipergeometryczna21:
                            fx = (float)bnh.Hyperg_2F_1(parametry[0], parametry[1], parametry[2], parametry[3]);
                            break;
                        default:
                            throw new Exception("Nie wybrano funkcji!"); //not reachable code?
                    }

                    if (fx > max)
                        fx = (float)max;
                    else if (fx < min)
                        fx = (float)min;

                    punkty.Add(new PointF((float)(parametry[indexZmiennej]), fx));
                }
            }
            else //Dodanie dwóch punktów - funkcja stała
            {
                float fx = 0;

                switch (typFunkcji)
                {
                    case TypFunkcjiBessela.Bessel:
                        fx = (float)bnh.Bessel(parametry[0], parametry[1]);
                        break;
                    case TypFunkcjiBessela.BesselSferyczny:
                        fx = (float)bnh.SphBessel(parametry[0], parametry[1]);
                        break;
                    case TypFunkcjiBessela.BesselPochodnaSferycznej:
                        fx = (float)bnh.SphBesselPrim(parametry[0], parametry[1]);
                        break;
                    case TypFunkcjiBessela.Neumann:
                        fx = (float)bnh.Neumann(parametry[0], parametry[1]);
                        break;
                    case TypFunkcjiBessela.NeumannSferyczny:
                        fx = (float)bnh.SphNeuman(parametry[0], parametry[1]);
                        break;
                    case TypFunkcjiBessela.NeumannPochodnaSferycznej:
                        fx = (float)bnh.SphNeumanPrim(parametry[0], parametry[1]);
                        break;
                    case TypFunkcjiBessela.Hipergeometryczna01:
                        fx = (float)bnh.Hyperg_0F_1(parametry[0], parametry[1]);
                        break;
                    case TypFunkcjiBessela.Hipergeometryczna11:
                        fx = (float)bnh.Hyperg_1F_1(parametry[0], parametry[1], parametry[2]);
                        break;
                    case TypFunkcjiBessela.Hipergeometryczna21:
                        fx = (float)bnh.Hyperg_2F_1(parametry[0], parametry[1], parametry[2], parametry[3]);
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

                Wykres wykres = new Wykres(funkcja, pWykres, minX, maxX, yOd, yDo);

                parametry[indexZmiennej] = double.NaN;

                return wykres.Reskalling(typFunkcji, parametry);
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

        private void NapiszWzorFunkcjiBesselowej(TypFunkcjiBessela typFunkcji, double[] parametry, int indexZmiennej)
        {
            Font font = new Font("Arial", 8); // Do wypisywania wzoru funkcji

            string parametryString = string.Empty;

            //Zbudowanie stringu parametrow
            if (typFunkcji == TypFunkcjiBessela.Hipergeometryczna21)
            {
                parametryString += (indexZmiennej != 0) ? parametry[0].ToString() : "x";
                parametryString += ", ";
                parametryString += (indexZmiennej != 1) ? parametry[1].ToString() : "x";
                parametryString += ", ";
                parametryString += (indexZmiennej != 2) ? parametry[2].ToString() : "x";
                parametryString += ", ";
                parametryString += (indexZmiennej != 3) ? parametry[3].ToString() : "x";
            }
            else if (typFunkcji == TypFunkcjiBessela.Hipergeometryczna11)
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
                case TypFunkcjiBessela.Bessel:
                    g.DrawString("Bessel(" + parametryString + ")", font, Brushes.Orange, 3, 3);
                    break;
                case TypFunkcjiBessela.BesselSferyczny:
                    g.DrawString("Sferyczna f. Bessela(" + parametryString + ")", font, Brushes.Orange, 3, 3);
                    break;
                case TypFunkcjiBessela.BesselPochodnaSferycznej:
                    g.DrawString("Poch. sfer. f. Bessela(" + parametryString + ")", font, Brushes.Orange, 3, 3);
                    break;
                case TypFunkcjiBessela.Neumann:
                    g.DrawString("Neumann(" + parametryString + ")", font, Brushes.Orange, 3, 3);
                    break;
                case TypFunkcjiBessela.NeumannSferyczny:
                    g.DrawString("Sferyczna f. Neumanna(" + parametryString + ")", font, Brushes.Orange, 3, 3);
                    break;
                case TypFunkcjiBessela.NeumannPochodnaSferycznej:
                    g.DrawString("Poch. sfer. f. Neumanna(" + parametryString + ")", font, Brushes.Orange, 3, 3);
                    break;
                case TypFunkcjiBessela.Hipergeometryczna01:
                    g.DrawString("Hipergeometryczna0F1(" + parametryString + ")", font, Brushes.Orange, 3, 3);
                    break;
                case TypFunkcjiBessela.Hipergeometryczna11:
                    g.DrawString("Hipergeometryczna1F1(" + parametryString + ")", font, Brushes.Orange, 3, 3);
                    break;
                case TypFunkcjiBessela.Hipergeometryczna21:
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

        private int ZnajdzIndexZmiennej(TypFunkcjiBessela typFunkcji, double[] parametry)
        {
            int indexZmiennej = -1;

            //Znalezienie indexu zmiennej
            if (typFunkcji == TypFunkcjiBessela.Bessel || typFunkcji == TypFunkcjiBessela.BesselSferyczny || typFunkcji == TypFunkcjiBessela.BesselPochodnaSferycznej || typFunkcji == TypFunkcjiBessela.Neumann || typFunkcji == TypFunkcjiBessela.NeumannSferyczny || typFunkcji == TypFunkcjiBessela.NeumannPochodnaSferycznej || typFunkcji == TypFunkcjiBessela.Hipergeometryczna01)
            {
                if (double.IsNaN(parametry[0]))
                    indexZmiennej = 0;

                if (double.IsNaN(parametry[1]))
                {
                    if (indexZmiennej == -1)
                        indexZmiennej = 1;
                    else
                        throw new BesseleDrugiArgumentException("Zmienna x może występować tylko jako jeden argument!");
                }
            }
            else if (typFunkcji == TypFunkcjiBessela.Hipergeometryczna11)
            {
                if (double.IsNaN(parametry[0]))
                    indexZmiennej = 0;

                if (double.IsNaN(parametry[1]))
                {
                    if (indexZmiennej == -1)
                        indexZmiennej = 1;
                    else
                        throw new BesseleDrugiArgumentException("Zmienna x może występować tylko jako jeden argument!");
                }

                if (double.IsNaN(parametry[2]))
                {
                    if (indexZmiennej == -1)
                        indexZmiennej = 2;
                    else
                        throw new BesseleTrzeciArgumentException("Zmienna x może występować tylko jako jeden argument!");
                }
            }
            else if (typFunkcji == TypFunkcjiBessela.Hipergeometryczna21)
            {
                if (double.IsNaN(parametry[0]))
                    indexZmiennej = 0;

                if (double.IsNaN(parametry[1]))
                {
                    if (indexZmiennej == -1)
                        indexZmiennej = 1;
                    else
                        throw new BesseleDrugiArgumentException("Zmienna x może występować tylko jako jeden argument!");
                }

                if (double.IsNaN(parametry[2]))
                {
                    if (indexZmiennej == -1)
                        indexZmiennej = 2;
                    else
                        throw new BesseleTrzeciArgumentException("Zmienna x może występować tylko jako jeden argument!");
                }

                if (double.IsNaN(parametry[3]))
                {
                    if (indexZmiennej == -1)
                        indexZmiennej = 3;
                    else
                        throw new BesseleCzwartyArgumentException("Zmienna x może występować tylko jako jeden argument!");
                }
            }

            return indexZmiennej;
        }



    // Konstruktor ------------------------
        public Wykres(string funk, PictureBox picWykres, double xOd, double xDo, double yOd, double yDo)
        {
            Przygotowanie(funk, picWykres, xOd, xDo, yOd, yDo);
        }

        private void Przygotowanie(string funk, PictureBox picWykres, double xOd, double xDo, double yOd, double yDo)
        {
            if (xDo == xOd)
                throw new WspolrzedneXException("Wspolrzedne x od i do nie mogą być takie same!");

            if (yDo == yOd)
                throw new WspolrzedneYException("Wspolrzedne y od i do nie mogą być takie same!");

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
            Wyczysc();
        }
    }
}
