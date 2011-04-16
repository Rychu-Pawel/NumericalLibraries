using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pierwiastki_CS
{
    class CalkaWielokrotna
    {
    //ZMIENNE ============================
        double wynik;
        double iloczynWspolczynnikow;
        double[] wyrazWolny;

        string funkcja;
        string funkcjaCache;
        string funkcjaCacheUjemna;

        int stopien;

        double[] wspolczynniki;
        string[] zmienne;
        double[,] granice;

        double[,] kwadratury;

    //METODY =============================
        private void SprawdzenieOdBledow() // SPRAWDZA POPRAWNOSC WPROWADZONYCH DANYCH
        {
            string cache;

            //Drobne zmiany
            for (int i = 0; i < funkcja.Length; i++)
            {
                char c = funkcja[i];
                if (c == ' ')  // Usuwanie spacji
                {
                    cache = funkcja.Substring(i + 1, funkcja.Length - (i + 1));
                    funkcja = funkcja.Substring(0, i);
                    funkcja += cache;
                    i--;
                }

                //Zamiana np. E-05 na 10^(-5)
                if (c == 'E') // Zliczanie licznika
                {
                    try
                    {
                        int licznikNawiasow = 1, licznik = 1; //licznik = znakow po E trzeba objac w nawias

                        if (funkcja[i + licznik] == '+' || funkcja[i + licznik] == '-')
                            licznik++;

                        if (funkcja[i + licznik] == '(')
                        {
                            licznik++;

                            while (licznikNawiasow != 0)
                            {
                                if (funkcja[i + licznik] == '(')
                                    licznikNawiasow++;
                                else if (funkcja[i + licznik] == ')')
                                    licznikNawiasow--;

                                licznik++;
                            }
                        }

                        while (i + licznik < funkcja.Length && (char.IsDigit(funkcja[i + licznik]) || funkcja[i + licznik] == ','))
                            licznik++;

                        //Podmiana E na *10^(
                        funkcja = funkcja.Substring(0, i) + "*10^(" + funkcja.Substring(i + 1, licznik - 1) + ")" + funkcja.Substring(i + licznik, funkcja.Length - i - licznik);
                    }
                    catch (SystemException)
                    {
                        throw new SystemException("Niepoprawne wystapienie operatora \"E\"");
                    }
                }

                //Sprawdzenie czy ktoras zmienna nie jest funkcja
                foreach (string zmienna in zmienne)
                {
                    if (zmienna == "asin" || zmienna == "acos" || zmienna == "atg" || zmienna == "actg" || zmienna == "exp" || zmienna == "E" || zmienna == "ln" || zmienna == "lg" || zmienna == "log" || zmienna == "sqrt" || zmienna == "sin" || zmienna == "cos" || zmienna == "tg" || zmienna == "ctg" || zmienna == "*" || zmienna == "+" || zmienna == "/" || zmienna == "-" || zmienna == ")" || zmienna == "(" || zmienna == "," || zmienna == "." || zmienna == "^" || zmienna == "si" || zmienna == "co" || zmienna == "in" || zmienna == "os" || zmienna == "ct" || zmienna == "tg" || zmienna == "as" || zmienna == "asi" || zmienna == "ac" || zmienna == "aco" || zmienna == "at" || zmienna == "act" || zmienna == "ex" || zmienna == "xp" || zmienna == "lo" || zmienna == "og" || zmienna == "sq" || zmienna == "rt" || zmienna == "qr" || zmienna == "E" || zmienna == " " || zmienna == "s" || zmienna == "i" || zmienna == "n" || zmienna == "c" || zmienna == "o" || zmienna == "t" || zmienna == "g" || zmienna == "q" || zmienna == "r" || zmienna == "e" || zmienna == "p" || zmienna == "a")
                        throw new SystemException("Zmienna nie moze nazywac sie \'" + zmienna + "\'!");
                }

                //Operator nie moze stac na poczatku wyrazenia (za wyjatkiem + i -), i na koncu (tutaj juz wszystkie)
                if (i == 0 && (c == '*' || c == '/' || c == '^'))
                    throw new SystemException("Operator \"" + c + "\" nie moze stac na poczatku wyrazenia!");

                if (i == funkcja.Length - 1 && (c == '+' || c == '-' || c == '*' || c == '/' || c == '^'))
                    throw new SystemException("Operator nie moze stac na koncu wyrazenia!");

                //Czy nie stoja kolo siebie dwa operatory
                if (i != 0)
                    if ((c == '+' || c == '-' || c == '*' || c == '/' || c == '^') && (funkcja[i - 1] == '+' || funkcja[i - 1] == '-' || funkcja[i - 1] == '*' || funkcja[i - 1] == '/' || funkcja[i - 1] == '^' || funkcja[i - 1] == ','))
                        throw new SystemException("Dwa operatory wystepuja obok siebie!");

                //Przepuszcza tylko dozwolone znaki - Wylaczone bo kazdy inny znak moze byc tu zmienna
                //if (!(c == 'E' || c == ' ' || c == ',' || c == 's' || c == 'i' || c == 'n' || c == 'c' || c == 'o' || c == 't' || c == 'g' || c == 'q' || c == 'r' || c == 'e' || c == 'x' || c == 'p' || c == 'a' || char.IsDigit(c) || c == '(' || c == ')' || c == '+' || c == '-' || c == '*' || c == '/' || c == '^'))
                //    throw new SystemException("Wystepuje niedozwolony znak!");

                // TO DO: Sprawdzenie czy funkcje np. exp(x) sa dobrze wpisane, np. nie epx, albo exp, exp())
            }

            if (funkcja == string.Empty)
                throw new SystemException("Wpisz funkcje!");
        }

        private void PodmienX(string zamiennik, int miejsce)
        {
            if ((miejsce > 0) ? (funkcja[miejsce - 1] != 'e') : true) //Zamienia x jesli nie jest on czescia 'exp' bo wychodzilo 'e(2*x+3)p
            {
                //if (wyrazWolny[iteracja] > 0)
                //{
                //    funkcja = funkcja.Substring(0, miejsce) + "(" + Convert.ToString(wspolczynniki[iteracja]) + "*x+" + Convert.ToString(wyrazWolny[iteracja]) + ")" + funkcja.Substring(miejsce + 1, funkcja.Length - miejsce - 1);
                //}
                //else if (wyrazWolny[iteracja] < 0)
                //{
                //    funkcja = funkcja.Substring(0, miejsce) + "(" + Convert.ToString(wspolczynniki[iteracja]) + "*x" + Convert.ToString(wyrazWolny[iteracja]) + ")" + funkcja.Substring(miejsce + 1, funkcja.Length - miejsce - 1);
                //}
                //else // Wyraz wolny == 0
                //{
                //    funkcja = funkcja.Substring(0, miejsce) + "(" + Convert.ToString(wspolczynniki[iteracja]) + "*x)" + funkcja.Substring(miejsce + 1, funkcja.Length - miejsce - 1);
                //}
                funkcja = funkcja.Substring(0, miejsce) + zamiennik + funkcja.Substring(miejsce + 1, funkcja.Length - miejsce - 1);
            }
        }

        private void ZamienGranice() // Zmienia granice calkowania po wszystkich zmiennych na <-1, 1>
        {
            //Obliczenie iloczynu wspolczynnikow i wyrazow wolnych
            for (int i = 0; i < stopien; i++)
            {
                wyrazWolny[i] = (granice[i, 0] + granice[i, 1]) / 2;
            }

            //Wywolanie podmienienia
            for (int i = 0; i < stopien; i++)
            {
                string zamiennik;

                //utworzenie stringu do podmiany
                if (wyrazWolny[i] > 0)
                    zamiennik = "(" + Convert.ToString(wspolczynniki[i]) + "*" + zmienne[i] + "+" + Convert.ToString(wyrazWolny[i]) + ")";
                else if (wyrazWolny[i] < 0)
                    zamiennik = "(" + Convert.ToString(wspolczynniki[i]) + "*" + zmienne[i] + Convert.ToString(wyrazWolny[i]) + ")";
                else // Wyraz wolny == 0
                    zamiennik = "(" + Convert.ToString(wspolczynniki[i]) + "*" + zmienne[i] + ")";

                //Jak x
                if (zmienne[i] == "x")
                {
                    //Znalezienie gdzie wystepuje x
                    int sprawdzenie = 0;

                    for (int j = 0; j <= funkcja.Length - zmienne[i].Length; j++)
                    {
                        while ((sprawdzenie < zmienne[i].Length) && (zmienne[i][sprawdzenie] == funkcja[j + sprawdzenie]))
                            sprawdzenie++;

                        if (sprawdzenie == zmienne[i].Length)
                        {
                            string funkcjaPrzed = funkcja;
                            PodmienX(zamiennik, j);
                            j += funkcja.Length - funkcjaPrzed.Length;
                        }

                        sprawdzenie = 0;
                    }
                }
                //A jak nie to zwykla podmiana
                else
                    funkcja = funkcja.Replace(zmienne[i], zamiennik);
            }
        }

        private void PodmienZmienne(int iteracja) //Iteracja mowi nam na ktory punkt legendre'a mamy zamienic zmienne
        {
            funkcjaCache = funkcja;
            funkcjaCacheUjemna = funkcja;
            string punktLegendrea = "(" + Convert.ToString(kwadratury[1, iteracja]) + ")";
            string punktLegendreaUjemny = "(" + Convert.ToString(-kwadratury[1, iteracja]) + ")";
            
            //Podmiana zwyklej (DODATNIEJ)
            for (int i = 0; i < stopien; i++)
            {
                //Podmiana X
                if (zmienne[i] == "x")
                {
                    //Znalezienie gdzie wystepuje x
                    int sprawdzenie = 0;

                    for (int j = 0; j <= funkcjaCache.Length - zmienne[i].Length; j++)
                    {
                        while ((sprawdzenie < zmienne[i].Length) && (zmienne[i][sprawdzenie] == funkcjaCache[j + sprawdzenie]))
                            sprawdzenie++;

                        if (sprawdzenie == zmienne[i].Length)
                        {
                            string funkcjaPrzed = funkcjaCache;

                            //Podmiana
                            if ((j > 0) ? (funkcjaCache[j - 1] != 'e') : true) //Zamienia x jesli nie jest on czescia 'exp' bo wychodzilo 'e(2*x+3)p
                            {
                                funkcjaCache = funkcjaCache.Substring(0, j) + punktLegendrea + funkcjaCache.Substring(j + 1, funkcjaCache.Length - j - 1);
                            }

                            j += funkcjaCache.Length - funkcjaPrzed.Length;
                        }

                        sprawdzenie = 0;
                    }
                }
                else //Zwykla podmiana
                {
                    funkcjaCache = funkcjaCache.Replace(zmienne[i], punktLegendrea);
                }
            }

            //Podmiana UJEMNEJ
            for (int i = 0; i < stopien; i++)
            {
                //Podmiana X
                if (zmienne[i] == "x")
                {
                    //Znalezienie gdzie wystepuje x
                    int sprawdzenie = 0;

                    for (int j = 0; j <= funkcjaCacheUjemna.Length - zmienne[i].Length; j++)
                    {
                        while ((sprawdzenie < zmienne[i].Length) && (zmienne[i][sprawdzenie] == funkcjaCacheUjemna[j + sprawdzenie]))
                            sprawdzenie++;

                        if (sprawdzenie == zmienne[i].Length)
                        {
                            string funkcjaPrzed = funkcjaCacheUjemna;

                            //Podmiana
                            if ((j > 0) ? (funkcjaCacheUjemna[j - 1] != 'e') : true) //Zamienia x jesli nie jest on czescia 'exp' bo wychodzilo 'e(2*x+3)p
                            {
                                funkcjaCacheUjemna = funkcjaCacheUjemna.Substring(0, j) + punktLegendreaUjemny + funkcjaCacheUjemna.Substring(j + 1, funkcjaCacheUjemna.Length - j - 1);
                            }

                            j += funkcjaCacheUjemna.Length - funkcjaPrzed.Length;
                        }

                        sprawdzenie = 0;
                    }
                }
                else //Zwykla podmiana
                {
                    funkcjaCacheUjemna = funkcjaCacheUjemna.Replace(zmienne[i], punktLegendreaUjemny);
                }
            }
        }

        public double Oblicz()
        {
            SprawdzenieOdBledow();

            ZamienGranice();

            for (int i = 0; i < 5; i++)
            {
                PodmienZmienne(i); //Zamienia zmienne na punkty legendre'a
                
                Kalkulator kalkulator1 = new Kalkulator(funkcjaCache);
                Kalkulator kalkulator2 = new Kalkulator(funkcjaCacheUjemna);

                wynik += iloczynWspolczynnikow * kwadratury[0, i] * (kalkulator1.Oblicz() + kalkulator2.Oblicz());
            }

            //Formatowanie wyniku, żeby 4,0000000000001 wypluł jako 4
            if (Math.Abs(wynik - Math.Floor(wynik)) < 0.000000001)
                wynik = Math.Floor(wynik);
            else if (Math.Abs(wynik - Math.Ceiling(wynik)) < 0.000000001)
                wynik = Math.Ceiling(wynik);

            return wynik;
        }

    //KONSTRUKTOR ========================
        public CalkaWielokrotna(string funk, System.Windows.Forms.DataGridView dgvDane)
        {
            stopien = dgvDane.Rows.Count;
            funkcja = funk;
            wynik = 0;
            iloczynWspolczynnikow = 1;
            wyrazWolny = new double[stopien];
            wspolczynniki = new double[stopien];

            granice = new double[stopien, 2];
            zmienne = new string[stopien];

            //Uzupelnienie granic i zmiennych
            for (int i = 0; i < stopien; i++)
            {
                zmienne[i] = dgvDane[0, i].Value.ToString();

                //Sprawdza czy czasem zmienna nie jest liczba
                double ble = 0;
                if (Double.TryParse(zmienne[i], out ble))
                    throw new SystemException("Wiersz " + Convert.ToString(i) + " zawiera wartosc, ktora nie moze byc zmienna.");

                granice[i, 0] = Convert.ToDouble(dgvDane[1, i].Value);
                granice[i, 1] = Convert.ToDouble(dgvDane[2, i].Value);
                wspolczynniki[i] = (granice[i, 1] - granice[i, 0]) / 2;
                iloczynWspolczynnikow *= wspolczynniki[i];
            }

            kwadratury = new double[2, 5]; //calka dla 10 kwadratur

            //Wpisanie kwadratur [0,2] 0 <- waga, 2 <- wartosc funkcji w pkcie 2 i -2 dla wagi 0;
            kwadratury[0, 0] = 0.0666713443;
            kwadratury[1, 0] = 0.9739065285;
            kwadratury[0, 1] = 0.1494513492;
            kwadratury[1, 1] = 0.8650633667;
            kwadratury[0, 2] = 0.2190863625;
            kwadratury[1, 2] = 0.6794095683;
            kwadratury[0, 3] = 0.2692667193;
            kwadratury[1, 3] = 0.4333953941;
            kwadratury[0, 4] = 0.2955242247;
            kwadratury[1, 4] = 0.1488743390;

        }
    }
}
