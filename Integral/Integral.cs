﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NumericalLibraries.Calculator.Exceptions;
using NumericalLibraries.Integral.Exceptions;

namespace NumericalLibraries.Integral
{
    public class Integral : Derivative.Derivative
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
            if (function.Length > 1)
            {
                //Przeszukanie funkcji w celu znalezienia 'x'
                for (int i = 0; i < function.Length; i++)
                {
                    //Zamienia x jesli nie jest on czescia 'exp' bo wychodzilo 'e(2*x+3)p
                    if (function[i] == 'x' && ((i > 0) ? (function[i - 1] != 'e') : true))
                    {
                        if (wyrazWolny > 0)
                        {
                            int wielkoscStringPrzed = function.Length;
                            function = function.Substring(0, i) + "(" + Convert.ToString(wspolczynnik) + "*x+" + Convert.ToString(wyrazWolny) + ")" + function.Substring(i + 1, function.Length - i - 1);
                            i += function.Length - wielkoscStringPrzed + 1; // przesuniecie iteracji o dodana funkcje
                        }
                        else if (wyrazWolny < 0)
                        {
                            int wielkoscStringPrzed = function.Length;
                            function = function.Substring(0, i) + "(" + Convert.ToString(wspolczynnik) + "*x" + Convert.ToString(wyrazWolny) + ")" + function.Substring(i + 1, function.Length - i - 1);
                            i += function.Length - wielkoscStringPrzed + 1; // przesuniecie iteracji o dodana funkcje
                        }
                        else // Wyraz wolny == 0
                        {
                            int wielkoscStringPrzed = function.Length;
                            function = function.Substring(0, i) + "(" + Convert.ToString(wspolczynnik) + "*x)" + function.Substring(i + 1, function.Length - i - 1);
                            i += function.Length - wielkoscStringPrzed + 1; // przesuniecie iteracji o dodana funkcje
                        }
                    }
                }
            }
            else if (function == "x") // Funkcja nie jest stała
            {
                if (wyrazWolny > 0)
                    function = Convert.ToString(wspolczynnik) + "*x+" + Convert.ToString(wyrazWolny);
                else if (wyrazWolny < 0)
                    function = Convert.ToString(wspolczynnik) + "*x" + Convert.ToString(wyrazWolny);
            }

            //Dodanie z przodu wspolczynnika - np. 28*x^3+3*x => (32)*(28*x^3+3*x)
            function = "(" + Convert.ToString(wspolczynnik) + ")*(" + function + ")";

            ErrorCheck();
            ConvertToTable();
            ConvertToONP();
        }

        private double ObliczPosrednie()
        {
            string funkcjaWlasciwa = function;

            //Zamienienie granic posrednich np. (-1, -0,9) => (-1, 1);
            ZamienGranice();
            
            double wynikPosredni = 0;

            //Obliczenie całki kwadraturą Gaussa-Legendre'a
            for (int i = 0; i < 5; i++)
                wynikPosredni += kwadratury[0, i] * (ComputeFunctionAtPoint(kwadratury[1, i]) + ComputeFunctionAtPoint(-kwadratury[1, i]));

            function = funkcjaWlasciwa;

            return wynikPosredni;
        }

        public double ComputeIntegral()
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
            ConvertToTable();
            ConvertToONP();

            //Formatowanie wyniku, żeby 4,0000000000001 wypluł jako 4
            if (Math.Abs(wynik - Math.Floor(wynik)) < 0.000000001)
                wynik = Math.Floor(wynik);
            else if (Math.Abs(wynik - Math.Ceiling(wynik)) < 0.000000001)
                wynik = Math.Ceiling(wynik);

            return wynik;
        }

        //KONSTRUKTOR -----------------------------------
        /// <summary>
        /// 
        /// </summary>
        /// <param name="function">Formula</param>
        /// <param name="xFrom">Lower limit</param>
        /// <param name="xTo">Upper limit</param>
        public Integral(string function, double xFrom, double xTo) : base(function)
        {
            this.xOd = xFrom;
            this.xDo = xTo;

            if (double.IsInfinity(xFrom) || double.IsInfinity(xTo))
                throw new IntegralInfinityRangeNotSupportedException();

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

            ErrorCheck();
            ConvertToTable();
            ConvertToONP();
        }
    }
}
