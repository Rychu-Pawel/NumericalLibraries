using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NumericalCalculator.Exceptions;
using System.Windows.Forms;

namespace NumericalCalculator
{
    public class Approximation
    {
    //ZMIENNE ------------------------
        private int iloscPunktow, stopien;
        private List<PointD> points;
        private double[] Eyx, Ex, x;
        private double[,] wspolczynniki;

        //Zmienne dla ekstrapolacji liniowej i kwadratowej
        private double Ex1, Ey, Ex2, Ex1y;
        private double Ex3, Ex4, Ex2y;
        private double x1, x2; //Wspolczynniki stajace przy x-ach
        private double x3;

        /// <summary>
        /// Points list for approximation
        /// </summary>
        public List<PointD> Points
        {
            get { return points; }
            set
            {
                points = value;

                if (points != null)
                    iloscPunktow = points.Count;
                else
                    iloscPunktow = 0;
            }
        }

    //METODY -------------------------
        private void Sprawdzenie()
        {
            // Sprawdzenie czy punkty x sie nie powtarzaja
            for (int i = 0; i < iloscPunktow; i++)
                for (int j = i + 1; j < iloscPunktow; j++)
                    if (points[i].X == points[j].X)
                        throw new SystemException("Wartosci x = " + Convert.ToString(points[i].X) + " wystepuja co najmniej dwukrotnie");
        }

        private void ObliczSumyLiniowe()
        {
            Ex1 = Ey = Ex2 = Ex1y = 0;

            for (int i = 0; i < iloscPunktow; i++)
            {
                Ex1 += points[i].X;
                Ey += points[i].Y;
                Ex2 += Math.Pow(points[i].X, 2.0);
                Ex1y += points[i].X * points[i].Y;
            }
        }

        private void ObliczSumyKwadratowe()
        {
            Ex1 = Ey = Ex2 = Ex1y = Ex3 = Ex4 = Ex2y = 0;

            for (int i = 0; i < iloscPunktow; i++)
            {
                Ex1 += points[i].X;
                Ey += points[i].Y;
                Ex2 += Math.Pow(points[i].X, 2.0);
                Ex1y += points[i].X * points[i].Y;
                Ex3 += Math.Pow(points[i].X, 3.0);
                Ex4 += Math.Pow(points[i].X, 4.0);
                Ex2y += Math.Pow(points[i].X, 2.0) * points[i].Y;
            }
        }

        private void ObliczSumy()
        {
            //Ex[0]
            Ex[0] = iloscPunktow;
            
            //Eyx[0]
            for (int i = 0; i < iloscPunktow; i++)
                Eyx[0] += points[i].Y;

            for (int i = 0; i < iloscPunktow; i++)
            {
                //Ex
                for (int j = 1; j < stopien * 2 + 1; j++)
                        Ex[j] += Math.Pow(points[i].X, j);

                //Eyx
                for (int j = 1; j < stopien + 1; j++)
                        Eyx[j] += points[i].Y * Math.Pow(points[i].X, j);
            }
        }

        private void ObliczWspolczynnikiLiniowe()
        {
            x1 = x2 = 0;

            double mianownik = 0;

            mianownik = iloscPunktow * Ex2 - Math.Pow(Ex1, 2.0);

            x2 = (iloscPunktow * Ex1y - Ex1 * Ey) / mianownik;

            x1 = (Ey * Ex2 - Ex1 * Ex1y) / mianownik;
        }

        private void ObliczWspolczynnikiKwadratowe()
        {
            x1 = x2 = x3 = 0;

            double czescPierwsza = 0, czescDruga = 0; //Powtarzajace sie elementy we wzorze

            czescPierwsza = iloscPunktow * Ex2 - Math.Pow(Ex1, 2.0);
            czescDruga = iloscPunktow * Ex3 - Ex2 * Ex1;

            x3 = (czescPierwsza * (iloscPunktow * Ex2y - Ex2 * Ey) - (iloscPunktow * Ex1y - Ey * Ex1) * (iloscPunktow * Ex3 - Ex2 * Ex1)) / (czescPierwsza * (iloscPunktow * Ex4 - Ex2 * Ex2) - Math.Pow((iloscPunktow * Ex3 - Ex2 * Ex1), 2.0));

            x2 = (iloscPunktow * Ex1y - Ex1 * Ey - (iloscPunktow * Ex3 - Ex1 * Ex2) * x3) / (iloscPunktow * Ex2 - Ex1 * Ex1);

            x1 = (Ey - Ex2 * x3 - Ex1 * x2) / iloscPunktow;
        }

        private void ObliczWspolczynniki()
        {
            //Uzupelnienie macierzy dla gaussa
            for (int i = 0; i < stopien + 1; i++)
                for (int j = 0; j < stopien + 1; j++)
                        wspolczynniki[j, i] = Ex[i + j];

            //Rozszerzenie macierzy wspolczynnikow o wyniki
            for (int i = 0; i < stopien + 1; i++)
                wspolczynniki[i, stopien + 1] = Eyx[i];

            //Wyliczenie wspolczynnikow
            LinearEquation Gauss = new LinearEquation(wspolczynniki);
            x = Gauss.Compute();
        }

        private string ObliczRegresjeLiniowa()
        {
            Sprawdzenie();

            ObliczSumyLiniowe();

            ObliczWspolczynnikiLiniowe();

            //Utworzeniu stringu funkcja
            string funkcja = string.Empty;

            if (x2 == -1)
                funkcja = "-x";
            else if (x2 == 1)
                funkcja = "x";
            else if (x2 != 0)
                funkcja = Convert.ToString(x2) + "*x";

            if (x1 < 0)
                funkcja += Convert.ToString(x1);
            else if (x1 > 0)
            {
                if (x2 != 0) // Jak jest funkcja stala to zeby nie bylo np. "+2" tylko samo "2"
                    funkcja += "+" + Convert.ToString(x1);
                else
                    funkcja += Convert.ToString(x1);
            }

            if (funkcja == string.Empty) // To znaczy ze funkcja jest stala = 0
                funkcja = "0";

            //Zwrocenie stringu
            return funkcja;
        }

        private string ObliczRegresjeKwadratowa()
        {
            Sprawdzenie();

            ObliczSumyKwadratowe();

            ObliczWspolczynnikiKwadratowe();

            //Utworzeniu stringu funkcja
            string funkcja = string.Empty;

            if (x3 == -1)
                funkcja = "-x^2";
            else if (x3 == 1)
                funkcja = "x^2";
            else if (x3 != 0)
                funkcja = Convert.ToString(x3) + "*x^2";

            if (x2 == -1)
                funkcja += "-x";
            else if (x2 == 1)
                funkcja += "+x";
            else if (x2 < 0)
                funkcja += Convert.ToString(x2) + "*x";
            else if (x2 > 0)
                funkcja += "+" + Convert.ToString(x2) + "*x";

            if (x1 < 0)
                funkcja += Convert.ToString(x1);
            else if (x1 > 0)
            {
                if (x2 != 0 || x3 != 0) // Jak jest funkcja stala to zeby nie bylo np. "+2" tylko samo "2"
                    funkcja += "+" + Convert.ToString(x1);
                else
                    funkcja += Convert.ToString(x1);
            }

            if (funkcja == string.Empty) // To znaczy ze funkcja jest stala = 0
                funkcja = "0";

            //Usuwanie "+" z przodu
            if (funkcja.StartsWith("+"))
                funkcja = funkcja.Substring(1);

            //Usuwanie "1*" z przodu
            if (funkcja.StartsWith("1*"))
                funkcja = funkcja.Substring(2);

            //Zwrocenie funkcji
            return funkcja;
        }

        private string FormatujWynik()
        //UWAGA!!!!! WYŁĄCZONE (wykomentowane) JEST POMIJANIE MAŁYCH WSPÓŁCZYNNIKÓW!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!  ---
        {                          //                                                                                  \
            int znak = stopien;          //                                                                                   \   
            string funkcja = string.Empty;                         //                                                    \
                                                                   //                                                     \
            //Formatowanie wyjsciowego stringa                                                                             \
            for (int i = stopien; i >= 0; i--)                     //                                                       \
            {                                                      //                                                       /
                if (znak++ % 2 == 0) // ZNAK DODATNI PRZY X                                                                /
                {                                                  //                                                     /
                    //if (!(Math.Abs(x[i]) < 0.000000001)) // JAK WSPOLCZYNNIK PRZY X jest bardzo maly to pomija   <------
                    //{
                        if (x[i] < 0 && x[i] != 1 && x[i] != -1) // + i - daje -
                        {
                            if (i > 1) // Jak 1, to pisze sam x, jak 0 w ogole pomija x;
                                funkcja += (Convert.ToString(x[i]) + "*x^" + Convert.ToString(i));
                            else if (i == 1)
                                funkcja += (Convert.ToString(x[i]) + "*x");
                            else if (i == 0)
                                funkcja += (Convert.ToString(x[i]));
                        }
                        else if (x[i] > 0 && x[i] != 1 && x[i] != -1) // Po prostu liczba dodatnia
                        {
                            if (i > 1) // Jak 1, to pisze sam x, jak 0 w ogole pomija x;
                                funkcja += ("+" + Convert.ToString(x[i]) + "*x^" + Convert.ToString(i));
                            else if (i == 1)
                                funkcja += ("+" + Convert.ToString(x[i]) + "*x");
                            else if (i == 0)
                                funkcja += ("+" + Convert.ToString(x[i]));
                        }
                        else if (x[i] == 1) // Pomijamy 1* przy x
                        {
                            if (i > 1) // Jak 1, to pisze sam x, jak 0 w ogole pomija x;
                                funkcja += ("+x^" + Convert.ToString(i));
                            else if (i == 1)
                                funkcja += ("+x");
                            else if (i == 0)
                                funkcja += ("+1");
                        }
                        else if (x[i] == -1) // Pomijamy 1* przy x, dodajac -
                            if (i > 1) // Jak 1, to pisze sam x, jak 0 w ogole pomija x;
                                funkcja += ("-x^" + Convert.ToString(i));
                            else if (i == 1)
                                funkcja += ("-x");
                            else if (i == 0)
                                funkcja += ("-1");
                    //}
                }
                else // ZNAK UJEMNY PRZY X
                {
                    //if (!(Math.Abs(x[i]) < 0.000000001)) // JAK WSPOLCZYNNIK PRZY X jest bardzo maly to pomija
                    //{
                        if (x[i] < 0 && x[i] != 1 && x[i] != -1) // + i - daje -
                        {
                            if (i > 1) // Jak 1, to pisze sam x, jak 0 w ogole pomija x;
                                funkcja += ("+" + Convert.ToString(Math.Abs(x[i])) + "*x^" + Convert.ToString(i));
                            else if (i == 1)
                                funkcja += ("+" + Convert.ToString(Math.Abs(x[i])) + "*x");
                            else if (i == 0)
                                funkcja += ("+" + Convert.ToString(Math.Abs(x[i])));
                        }
                        else if (x[i] > 0 && x[i] != 1 && x[i] != -1) // Po prostu liczba dodatnia
                        {
                            if (i > 1) // Jak 1, to pisze sam x, jak 0 w ogole pomija x;
                                funkcja += ("-" + Convert.ToString(Math.Abs(x[i])) + "*x^" + Convert.ToString(i));
                            else if (i == 1)
                                funkcja += ("-" + Convert.ToString(Math.Abs(x[i])) + "*x");
                            else if (i == 0)
                                funkcja += ("-" + Convert.ToString(Math.Abs(x[i])));
                        }
                        else if (x[i] == 1) // Pomijamy 1* przy x
                        {
                            if (i > 1) // Jak 1, to pisze sam x, jak 0 w ogole pomija x;
                                funkcja += ("-x^" + Convert.ToString(i));
                            else if (i == 1)
                                funkcja += ("-x");
                            else if (i == 0)
                                funkcja += ("-1");
                        }
                        else if (x[i] == -1) // Pomijamy 1* przy x, dodajac -
                            if (i > 1) // Jak 1, to pisze sam x, jak 0 w ogole pomija x;
                                funkcja += ("+x^" + Convert.ToString(i));
                            else if (i == 1)
                                funkcja += ("+x");
                            else if (i == 0)
                                funkcja += ("+1");
                    //}
                }
            }

            //Usuwanie "+" z przodu
            if (funkcja.StartsWith("+"))
                funkcja = funkcja.Substring(1);

            //Usuwanie "1*" z przodu
            if (funkcja.StartsWith("1*"))
                funkcja = funkcja.Substring(2);

            return funkcja;
        }

        /// <summary>
        /// Approximates function from given points
        /// </summary>
        /// <returns>Approximated function</returns>
        public string Compute()
        {
            if (stopien == 1)
                return ObliczRegresjeLiniowa();
            else if (stopien == 2)
                return  ObliczRegresjeKwadratowa();
            else if (stopien < 1)
                throw new WrongApproximationLevelException();
            else
            {
                Sprawdzenie();

                ObliczSumy();

                ObliczWspolczynniki();

                //Utworzeniu stringu funkcja
                string funkcja = FormatujWynik();

                return funkcja;
            }
        }

    //KONSTRUKTOR --------------------
        /// <summary>
        /// 
        /// </summary>
        /// <param name="level">Approximation level</param>
        public Approximation(int level)
        {
            iloscPunktow = 0;
            this.stopien = level;

            //Dla Ekstrapolacji liniowej i kwadratowej
            Ex1 = Ey = Ex2 = Ex1y = Ex3 = Ex4 = Ex2y = x1 = x2 = x3 = 0;

            //Dla ekstrapolacji dowolnego stopnia
            Eyx = new double[level + 1];
            Ex = new double[2 * level + 1];
            x = new double[level + 1];

            wspolczynniki = new double[level + 1, level + 2];
        }
    }
}
