using System;
using System.Collections.Generic;
////using System.Linq;
using System.Text;

namespace Pierwiastki_CS
{
    class Interpolacja
    {
//ZMIENNE ------------------------------------
        public int iloscPunktow;
        private double[,] bazy;
        private double[] bazyMianownikow;
        public double[,] punkty;
        private double[] wynik; // Wynik jako tablica współczynników

        private string strWynik; //Wynik jakos f(x)

//METODY -------------------------------------

        private void Sprawdzenie()
        {
            // Sprawdzenie czy punkty x sie nie powtarzaja
            for (int i = 0; i < iloscPunktow; i++)
                for (int j = i + 1; j < iloscPunktow; j++)
                    if (punkty[0, i] == punkty[0, j])
                        throw new SystemException("Wartosci x = " + Convert.ToString(punkty[0, i]) + " wystepuja co najmniej dwukrotnie");
        }

        private void StworzBaze(int j, int iteracja) // j - pomijany index we wzorze lagrange'a
        {
            // wszedzie j - 1, bo tablice sa od 0, a nie od 1;
            if (iteracja == -1)
            {
                //Stwórz pierwsza iteracje
                if (j == 1) // tworzymy l(j,2)
                    bazy[j - 1, 0] = punkty[0, 1];
                else
                    bazy[j - 1, 0] = punkty[0, 0];

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
                        bazy[j - 1, i] = bazy[j - 1, i]*punkty[0, mnoznik] + bazy[j - 1, i - 1];

                //Stworz nowy ostatni (wolny) element
                bazy[j - 1, 0] *= punkty[0, mnoznik];

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
                    bazyMianownikow[j - 1] *= (punkty[0, j - 1] - punkty[0, i - 1]);

            bazyMianownikow[j - 1] = punkty[1, j - 1]/bazyMianownikow[j - 1];
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
                                strWynik += (Convert.ToString(wynik[i]) + "*x^" + Convert.ToString(i));
                            else if (i == 1)
                                strWynik += (Convert.ToString(wynik[i]) + "*x");
                            else if (i == 0)
                                strWynik += (Convert.ToString(wynik[i]));
                        }
                        else if (wynik[i] > 0 && wynik[i] != 1 && wynik[i] != -1) // Po prostu liczba dodatnia
                        {
                            if (i > 1) // Jak 1, to pisze sam x, jak 0 w ogole pomija x;
                                strWynik += ("+" + Convert.ToString(wynik[i]) + "*x^" + Convert.ToString(i));
                            else if (i == 1)
                                strWynik += ("+" + Convert.ToString(wynik[i]) + "*x");
                            else if (i == 0)
                                strWynik += ("+" + Convert.ToString(wynik[i]));
                        }
                        else if (wynik[i] == 1) // Pomijamy 1* przy x
                        {
                            if (i > 1) // Jak 1, to pisze sam x, jak 0 w ogole pomija x;
                                strWynik += ("+x^" + Convert.ToString(i));
                            else if (i == 1)
                                strWynik += ("+x");
                            else if (i == 0)
                                strWynik += ("+1");
                        }
                        else if (wynik[i] == -1) // Pomijamy 1* przy x, dodajac -
                            if (i > 1) // Jak 1, to pisze sam x, jak 0 w ogole pomija x;
                                strWynik += ("-x^" + Convert.ToString(i));
                            else if (i == 1)
                                strWynik += ("-x");
                            else if (i == 0)
                                strWynik += ("-1");
                    }
                }
                else // ZNAK UJEMNY PRZY X
                {
                    if (!(Math.Abs(wynik[i]) < 0.000000001)) // JAK WSPOLCZYNNIK PRZY X jest bardzo maly to pomija
                    {
                        if (wynik[i] < 0 && wynik[i] != 1 && wynik[i] != -1) // + i - daje -
                        {
                            if (i > 1) // Jak 1, to pisze sam x, jak 0 w ogole pomija x;
                                strWynik += ("+" + Convert.ToString(Math.Abs(wynik[i])) + "*x^" + Convert.ToString(i));
                            else if (i == 1)
                                strWynik += ("+" + Convert.ToString(Math.Abs(wynik[i])) + "*x");
                            else if (i == 0)
                                strWynik += ("+" + Convert.ToString(Math.Abs(wynik[i])));
                        }
                        else if (wynik[i] > 0 && wynik[i] != 1 && wynik[i] != -1) // Po prostu liczba dodatnia
                        {
                            if (i > 1) // Jak 1, to pisze sam x, jak 0 w ogole pomija x;
                                strWynik += ("-" + Convert.ToString(Math.Abs(wynik[i])) + "*x^" + Convert.ToString(i));
                            else if (i == 1)
                                strWynik += ("-" + Convert.ToString(Math.Abs(wynik[i])) + "*x");
                            else if (i == 0)
                                strWynik += ("-" + Convert.ToString(Math.Abs(wynik[i])));
                        }
                        else if (wynik[i] == 1) // Pomijamy 1* przy x
                        {
                            if (i > 1) // Jak 1, to pisze sam x, jak 0 w ogole pomija x;
                                strWynik += ("-x^" + Convert.ToString(i));
                            else if (i == 1)
                                strWynik += ("-x");
                            else if (i == 0)
                                strWynik += ("-1");
                        }
                        else if (wynik[i] == -1) // Pomijamy 1* przy x, dodajac -
                            if (i > 1) // Jak 1, to pisze sam x, jak 0 w ogole pomija x;
                                strWynik += ("+x^" + Convert.ToString(i));
                            else if (i == 1)
                                strWynik += ("+x");
                            else if (i == 0)
                                strWynik += ("+1");
                    }
                }
            }

            //Usuwanie "+" z przodu
            if (strWynik.StartsWith("+"))
                strWynik = strWynik.Substring(1);

            //Usuwanie "1*" z przodu
            if (strWynik.StartsWith("1*"))
                strWynik = strWynik.Substring(2);
        }

        public string Oblicz()
        {
            Sprawdzenie(); //Wykonanie sprawdzenia

            strWynik = string.Empty;

            if (iloscPunktow == 0) // Jezeli nie ma punktow, to blad, jak tylko 1, to zwraca funkcje stala, jak 2 lub wiecej to liczy
                throw new SystemException("Nie podano zadnych punktow");
            else if (iloscPunktow == 1)
                return Convert.ToString(punkty[1, 0]);
            else if (iloscPunktow >= 2) //INTERPOLACJA !!!
            {
                Interpoluj();

                FormatujWynik();
            }

            if (strWynik == string.Empty) // To znaczy ze funkcja jest stala = 0
                strWynik = "0";

            return strWynik;

        }
    }
}
