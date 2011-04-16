using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pierwiastki_CS
{
    class Calka : Pochodna
    {
    //ZMIENNE ---------------------------------------
        double[,] kwadratury;
        double xOd, xDo;

        double wynik;

        //METODY ----------------------------------------
        private void ZamienGranice()
        {
            double wspolczynnik, wyrazWolny;

            wspolczynnik = (xDo - xOd) / 2;

            wyrazWolny = (xOd + xDo) / 2;

            //Zamiana x na np. (2*x+3)
            if (funkcja.Length > 1)
            {
                //Przeszukanie funkcji w celu znalezienia 'x'
                for (int i = 0; i < funkcja.Length; i++)
                {
                    //Zamienia x jesli nie jest on czescia 'exp' bo wychodzilo 'e(2*x+3)p
                    if (funkcja[i] == 'x' && ((i > 0) ? (funkcja[i - 1] != 'e') : true))
                    {
                        if (wyrazWolny > 0)
                        {
                            int wielkoscStringPrzed = funkcja.Length;
                            funkcja = funkcja.Substring(0, i) + "(" + Convert.ToString(wspolczynnik) + "*x+" + Convert.ToString(wyrazWolny) + ")" + funkcja.Substring(i + 1, funkcja.Length - i - 1);
                            i += funkcja.Length - wielkoscStringPrzed + 1; // przesuniecie iteracji o dodana funkcje
                        }
                        else if (wyrazWolny < 0)
                        {
                            int wielkoscStringPrzed = funkcja.Length;
                            funkcja = funkcja.Substring(0, i) + "(" + Convert.ToString(wspolczynnik) + "*x" + Convert.ToString(wyrazWolny) + ")" + funkcja.Substring(i + 1, funkcja.Length - i - 1);
                            i += funkcja.Length - wielkoscStringPrzed + 1; // przesuniecie iteracji o dodana funkcje
                        }
                        else // Wyraz wolny == 0
                        {
                            int wielkoscStringPrzed = funkcja.Length;
                            funkcja = funkcja.Substring(0, i) + "(" + Convert.ToString(wspolczynnik) + "*x)" + funkcja.Substring(i + 1, funkcja.Length - i - 1);
                            i += funkcja.Length - wielkoscStringPrzed + 1; // przesuniecie iteracji o dodana funkcje
                        }
                    }
                }
            }
            else if (funkcja == "x") // Funkcja nie jest stała
            {
                if (wyrazWolny > 0)
                    funkcja = Convert.ToString(wspolczynnik) + "*x+" + Convert.ToString(wyrazWolny);
                else if (wyrazWolny < 0)
                    funkcja = Convert.ToString(wspolczynnik) + "*x" + Convert.ToString(wyrazWolny);
            }

            //Dodanie z przodu wspolczynnika - np. 28*x^3+3*x => (32)*(28*x^3+3*x)
            funkcja = "(" + Convert.ToString(wspolczynnik) + ")*(" + funkcja + ")";

            SprawdzenieOdBledow();
            KonwertujNaTablice();
            KonwertujNaONP();
        }

        private double ObliczPosrednie()
        {
            string funkcjaWlasciwa = funkcja;

            //Zamienienie granic posrednich np. (-1, -0,9) => (-1, 1);
            ZamienGranice();
            
            double wynikPosredni = 0;

            //Obliczenie całki kwadraturą Gaussa-Legendre'a
            for (int i = 0; i < 5; i++)
                wynikPosredni += kwadratury[0, i] * (ObliczFunkcjeWPunkcie(kwadratury[1, i]) + ObliczFunkcjeWPunkcie(-kwadratury[1, i]));

            funkcja = funkcjaWlasciwa;

            return wynikPosredni;
        }

        public override double ObliczWnetrze()
        {
            //Zamiana granic
            if (xOd != -1 || xDo != 1)
                ZamienGranice();

            //Obliczenie całki od -1 do 1 jako sumy 100 całek
            for (double i = -1; i <= 1; i += 0.01)
            {
                xOd = i;
                xDo = i + 0.01;
                wynik += ObliczPosrednie();
            }

            //Przywrocenie z powrotem ustawien dla oryginalnej funkcji
            KonwertujNaTablice();
            KonwertujNaONP();

            //Formatowanie wyniku, żeby 4,0000000000001 wypluł jako 4
            if (Math.Abs(wynik - Math.Floor(wynik)) < 0.000000001)
                wynik = Math.Floor(wynik);
            else if (Math.Abs(wynik - Math.Ceiling(wynik)) < 0.000000001)
                wynik = Math.Ceiling(wynik);

            return wynik;
        }

        //KONSTRUKTOR -----------------------------------
        public Calka(string funkcja, double xOd, double xDo) : base(funkcja)
        {
            this.xOd = xOd;
            this.xDo = xDo;

            if (double.IsInfinity(xOd) || double.IsInfinity(xDo))
                throw new FunkcjaException("Niestety nieskończoność nie jest jeszcze obsługiwana");

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

            wynik = 0;

            SprawdzenieOdBledow();
            KonwertujNaTablice();
            KonwertujNaONP();
        }
    }
}
