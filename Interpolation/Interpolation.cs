using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using NumericalCalculator.Exceptions;

namespace NumericalCalculator
{
    public class Interpolation
    {
//ZMIENNE ------------------------------------
        private int iloscPunktow;
        private double[,] bazy;
        private double[] bazyMianownikow;
        private List<PointD> points;

        private double[] wynik; // Wynik jako tablica współczynników

        private string strResult; //Wynik jakos f(x)

        /// <summary>
        /// Points list for interpolation
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

//METODY -------------------------------------

        private void ErrorCheck()
        {
            // Sprawdzenie czy punkty x sie nie powtarzaja
            for (int i = 0; i < iloscPunktow; i++)
                for (int j = i + 1; j < iloscPunktow; j++)
                    if (points[i].X == points[j].X)
                        throw new SystemException("x = " + Convert.ToString(points[i].X) + " appears at least twice");
        }

        private void StworzBaze(int j, int iteracja) // j - pomijany index we wzorze lagrange'a
        {
            // wszedzie j - 1, bo tablice sa od 0, a nie od 1;
            if (iteracja == -1)
            {
                //Stwórz pierwsza iteracje
                if (j == 1) // tworzymy l(j,2)
                    bazy[j - 1, 0] = points[1].X;
                else
                    bazy[j - 1, 0] = points[0].X;

                bazy[j - 1, 1] = 1;

                //Jesli iloscPunktow > 2
                    //stworzBaze(baza, iteracja + 1)
                if (iloscPunktow > 2)
                    StworzBaze(j, iteracja + 1);
            }
            else
            {
                //Stworz nowa potege
                bazy[j - 1, iteracja + 2] = 1;

                //Stworz (n-i)-te potegi (oznaczenie n - zgodnie ze wzorem lagrange'a)
                int mnoznik = iteracja + 1; // mnoznik - przez ktory punkt (x) ma pomnozyc
                if (j - 1 <= mnoznik) // pominiecie elementu pomijanego we wzorze lagrange'a
                    mnoznik++;

                for (int i = iteracja + 1; i > 0; i--)
                        bazy[j - 1, i] = bazy[j - 1, i]*points[mnoznik].X + bazy[j - 1, i - 1];

                //Stworz nowy ostatni (wolny) element
                bazy[j - 1, 0] *= points[mnoznik].X;

                // Rekurencja //Jesli (iteracja < wielkosc bazy) => stworzBaze(baza, iteracja + 1)
                if (iteracja < iloscPunktow - 3)
                    StworzBaze(j, iteracja + 1);
            }
        }

        private void StworzBazeMianownikow(int j)
        {
            bazyMianownikow[j - 1] = 1;

            for (int i = 1; i <= iloscPunktow; i++)
                if (i != j)
                    bazyMianownikow[j - 1] *= (points[j - 1].X - points[i - 1].X);

            bazyMianownikow[j - 1] = points[j - 1].Y/bazyMianownikow[j - 1];
        }

        private void Interpoluj()
        {
            //Zmienne
            bazy = new double[iloscPunktow, iloscPunktow];
            bazyMianownikow = new double[iloscPunktow];
            wynik = new double[iloscPunktow];

            //Wykonujemy
            for (int i = 1; i <= iloscPunktow; i++)
                StworzBaze(i, -1);

            for (int i = 1; i <= iloscPunktow; i++)
                StworzBazeMianownikow(i);

            //Wykonanie interpolacji
            for (int i = 0; i < iloscPunktow; i++)
                for (int j = 0; j < iloscPunktow; j++)
                    wynik[i] += bazy[j, i] * bazyMianownikow[j];
        }

        private void FormatujWynik()
        {
            int znak = 2;

            //Formatowanie wyjsciowego stringa
            for (int i = iloscPunktow - 1; i >= 0; i--)
            {
                if (znak++ % 2 == 0) // ZNAK DODATNI PRZY X
                {
                    if (!(Math.Abs(wynik[i]) < 0.000000001)) // JAK WSPOLCZYNNIK PRZY X jest bardzo maly to pomija
                    {
                        if (wynik[i] < 0 && wynik[i] != 1 && wynik[i] != -1) // + i - daje -
                        {
                            if (i > 1) // Jak 1, to pisze sam x, jak 0 w ogole pomija x;
                                strResult += (Convert.ToString(wynik[i]) + "*x^" + Convert.ToString(i));
                            else if (i == 1)
                                strResult += (Convert.ToString(wynik[i]) + "*x");
                            else if (i == 0)
                                strResult += (Convert.ToString(wynik[i]));
                        }
                        else if (wynik[i] > 0 && wynik[i] != 1 && wynik[i] != -1) // Po prostu liczba dodatnia
                        {
                            if (i > 1) // Jak 1, to pisze sam x, jak 0 w ogole pomija x;
                                strResult += ("+" + Convert.ToString(wynik[i]) + "*x^" + Convert.ToString(i));
                            else if (i == 1)
                                strResult += ("+" + Convert.ToString(wynik[i]) + "*x");
                            else if (i == 0)
                                strResult += ("+" + Convert.ToString(wynik[i]));
                        }
                        else if (wynik[i] == 1) // Pomijamy 1* przy x
                        {
                            if (i > 1) // Jak 1, to pisze sam x, jak 0 w ogole pomija x;
                                strResult += ("+x^" + Convert.ToString(i));
                            else if (i == 1)
                                strResult += ("+x");
                            else if (i == 0)
                                strResult += ("+1");
                        }
                        else if (wynik[i] == -1) // Pomijamy 1* przy x, dodajac -
                            if (i > 1) // Jak 1, to pisze sam x, jak 0 w ogole pomija x;
                                strResult += ("-x^" + Convert.ToString(i));
                            else if (i == 1)
                                strResult += ("-x");
                            else if (i == 0)
                                strResult += ("-1");
                    }
                }
                else // ZNAK UJEMNY PRZY X
                {
                    if (!(Math.Abs(wynik[i]) < 0.000000001)) // JAK WSPOLCZYNNIK PRZY X jest bardzo maly to pomija
                    {
                        if (wynik[i] < 0 && wynik[i] != 1 && wynik[i] != -1) // + i - daje -
                        {
                            if (i > 1) // Jak 1, to pisze sam x, jak 0 w ogole pomija x;
                                strResult += ("+" + Convert.ToString(Math.Abs(wynik[i])) + "*x^" + Convert.ToString(i));
                            else if (i == 1)
                                strResult += ("+" + Convert.ToString(Math.Abs(wynik[i])) + "*x");
                            else if (i == 0)
                                strResult += ("+" + Convert.ToString(Math.Abs(wynik[i])));
                        }
                        else if (wynik[i] > 0 && wynik[i] != 1 && wynik[i] != -1) // Po prostu liczba dodatnia
                        {
                            if (i > 1) // Jak 1, to pisze sam x, jak 0 w ogole pomija x;
                                strResult += ("-" + Convert.ToString(Math.Abs(wynik[i])) + "*x^" + Convert.ToString(i));
                            else if (i == 1)
                                strResult += ("-" + Convert.ToString(Math.Abs(wynik[i])) + "*x");
                            else if (i == 0)
                                strResult += ("-" + Convert.ToString(Math.Abs(wynik[i])));
                        }
                        else if (wynik[i] == 1) // Pomijamy 1* przy x
                        {
                            if (i > 1) // Jak 1, to pisze sam x, jak 0 w ogole pomija x;
                                strResult += ("-x^" + Convert.ToString(i));
                            else if (i == 1)
                                strResult += ("-x");
                            else if (i == 0)
                                strResult += ("-1");
                        }
                        else if (wynik[i] == -1) // Pomijamy 1* przy x, dodajac -
                            if (i > 1) // Jak 1, to pisze sam x, jak 0 w ogole pomija x;
                                strResult += ("+x^" + Convert.ToString(i));
                            else if (i == 1)
                                strResult += ("+x");
                            else if (i == 0)
                                strResult += ("+1");
                    }
                }
            }

            //Usuwanie "+" z przodu
            if (strResult.StartsWith("+"))
                strResult = strResult.Substring(1);

            //Usuwanie "1*" z przodu
            if (strResult.StartsWith("1*"))
                strResult = strResult.Substring(2);
        }

        /// <summary>
        /// Interpolate function from given points
        /// </summary>
        /// <returns>Interpolated function</returns>
        public string Compute()
        {
            ErrorCheck(); //Wykonanie sprawdzenia

            strResult = string.Empty;

            if (iloscPunktow == 0) // Jezeli nie ma punktow, to blad, jak tylko 1, to zwraca funkcje stala, jak 2 lub wiecej to liczy
                throw new NoPointsProvidedException();
            else if (iloscPunktow == 1)
                return Convert.ToString(points[0].Y);
            else if (iloscPunktow >= 2) //INTERPOLACJA !!!
            {
                Interpoluj();

                FormatujWynik();
            }

            if (strResult == string.Empty) // To znaczy ze funkcja jest stala = 0
                strResult = "0";

            return strResult;
        }
    }
}
