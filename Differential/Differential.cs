using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using NumericalLibraries.Calculator.Exceptions;
using NumericalLibraries.Common;
using NumericalLibraries.Differential.Exceptions;

namespace NumericalLibraries.Differential
{
    public class Differential : Derivative.Derivative
    {
        //ZMIENNE -----------------------------------
        string funkcjaII;
        double szukanyPunkt;
        double szukanyPunktII;
        double pktPoczatkowy, wartoscPoczatkowa;
        double pktPoczatkowyII, wartoscPoczatkowaII;
        double krok;

        string[] tablicaONPfunkcjiI;
        string[] tablicaONPfunkcjiII;

        double f0, f1, f2, f3;
        double f0II, f1II, f2II, f3II;

        double wynik;
        double wynikII;

        //METODY ------------------------------------

        /// <summary>
        /// Compute differential
        /// </summary>
        /// <param name="valueLookingPoint">Point at which return the value</param>
        /// <param name="startingPoint">Starting point</param>
        /// <param name="startingPointFunctionValue">Function value at starting point</param>
        /// <param name="applyResultFormatting">Makes output better look. For example outputs 4.0 instead 4,000000000000001 or 3,999999999999999</param>
        /// <param name="step"></param>
        /// <returns></returns>
        public double ComputeDifferential(double valueLookingPoint, double startingPoint, double startingPointFunctionValue, bool applyResultFormatting = true, double step = 0.001)
        {
            List<PointD> points = ComputeDifferentialPointsList(valueLookingPoint, startingPoint, startingPointFunctionValue, applyResultFormatting, step);

            return points.Last().Y;
        }

        /// <summary>
        /// Compute all function values from starting point to valueLookingPoint
        /// </summary>
        /// <param name="valueLookingPoint">Ending point</param>
        /// <param name="startingPoint">Starting point</param>
        /// <param name="startingPointFunctionValue">Function value at starting point</param>
        /// <param name="applyResultFormatting">Makes output better look. For example outputs 4.0 instead 4,000000000000001 or 3,999999999999999</param>
        /// <param name="step"></param>
        /// <returns></returns>
        public List<PointD> ComputeDifferentialPointsList(double valueLookingPoint, double startingPoint, double startingPointFunctionValue, bool applyResultFormatting = true, double step = 0.001)
        {
            this.krok = step;

            f0 = f1 = f2 = f3 = 0;
            wynik = 0;

            List<PointD> punkty = new List<PointD>();

            //Przygotowanie
            szukanyPunkt = valueLookingPoint;
            pktPoczatkowy = startingPoint;
            wartoscPoczatkowa = startingPointFunctionValue;

            //sprawdzenie czy mam sie posuwać do przodu czy do tyłu
            if (szukanyPunkt > pktPoczatkowy)
            {
                //Posuwam się do przodu
                for (double i = pktPoczatkowy + step; i < szukanyPunkt; i += step)
                {
                    ComputeFy();

                    wynik = wartoscPoczatkowa + (step / 6) * (f0 + 2 * f1 + 2 * f2 + f3);

                    //Ustawienie nowych wartosci startowych dla kolejnej iteracji
                    wartoscPoczatkowa = wynik;
                    pktPoczatkowy = i;

                    punkty.Add(new PointD(i, wynik));
                }

                //Doliczenie do żądanej wartosci (bo jak szukam np. x = 3,4567 to teraz doliczyłem do 3,456)
                step = szukanyPunkt - pktPoczatkowy;
                ComputeFy();
                wynik = wartoscPoczatkowa + (step / 6) * (f0 + 2 * f1 + 2 * f2 + f3);

                if (applyResultFormatting)
                {
                    //Formatowanie wyniku, żeby 4,0000000000001 wypluł jako 4
                    if (Math.Abs(wynik - Math.Floor(wynik)) < 0.000000001)
                        wynik = Math.Floor(wynik);
                    else if (Math.Abs(wynik - Math.Ceiling(wynik)) < 0.000000001)
                        wynik = Math.Ceiling(wynik);
                }

                punkty.Add(new PointD(szukanyPunkt, wynik));

                return punkty;
            }
            else if (szukanyPunkt < pktPoczatkowy)
            {
                step = -step;

                //Posuwam się do tyłu
                for (double i = pktPoczatkowy + step; i > szukanyPunkt; i += step)
                {
                    ComputeFy();

                    wynik = wartoscPoczatkowa + (step / 6) * (f0 + 2 * f1 + 2 * f2 + f3);

                    //Ustawienie nowych wartosci startowych dla kolejnej iteracji
                    wartoscPoczatkowa = wynik;
                    pktPoczatkowy = i;

                    punkty.Add(new PointD(i, y));
                }

                //Doliczenie do żądanej wartosci (bo jak szukam np. x = 3,4567 to teraz doliczyłem do 3,456)
                step = -(pktPoczatkowy - szukanyPunkt);
                ComputeFy();
                wynik = wartoscPoczatkowa + (step / 6) * (f0 + 2 * f1 + 2 * f2 + f3);

                if (applyResultFormatting)
                {
                    //Formatowanie wyniku, żeby 4,0000000000001 wypluł jako 4
                    if (Math.Abs(wynik - Math.Floor(wynik)) < 0.000000001)
                        wynik = Math.Floor(wynik);
                    else if (Math.Abs(wynik - Math.Ceiling(wynik)) < 0.000000001)
                        wynik = Math.Ceiling(wynik);
                }

                punkty.Add(new PointD(szukanyPunkt, wynik));

                return punkty;
            }
            else //Szukany pkt jest == zadanemu punktowi
                return new List<PointD>() { new PointD(szukanyPunkt, startingPointFunctionValue) };
        }

        /// <summary>
        /// Compute second order differential
        /// </summary>
        /// <param name="valueLookingPoint">Ending point</param>
        /// <param name="startingPoint">Starting point</param>
        /// <param name="startingPointFunctionValue">Function value at starting point</param>
        /// <param name="startingPointFunctionValueII">Derivative value at starting point</param>
        /// <param name="applyResultFormatting">Makes output better look. For example outputs 4.0 instead 4,000000000000001 or 3,999999999999999</param>
        /// <param name="step"></param>
        /// <returns></returns>
        public double ComputeDifferentialII(double valueLookingPoint, double startingPoint, double startingPointFunctionValue, double startingPointFunctionValueII, bool applyResultFormatting = true, double step = 0.001)
        {
            List<PointD> points = ComputeDifferentialIIPointsList(valueLookingPoint, startingPoint, startingPointFunctionValue, startingPointFunctionValueII, applyResultFormatting, step);

            return points.Last().Y;
        }

        /// <summary>
        /// Compute all function values from starting point to valueLookingPoint
        /// </summary>
        /// <param name="valueLookingPoint">Ending point</param>
        /// <param name="startingPoint">Starting point</param>
        /// <param name="startingPointFunctionValue">Function value at starting point</param>
        /// <param name="startingPointFunctionValueII">Derivative value at starting point</param>
        /// <param name="applyResultFormatting">Makes output better look. For example outputs 4.0 instead 4,000000000000001 or 3,999999999999999</param>
        /// <param name="step"></param>
        /// <returns></returns>
        public List<PointD> ComputeDifferentialIIPointsList(double valueLookingPoint, double startingPoint, double startingPointFunctionValue, double startingPointFunctionValueII, bool applyResultFormatting = true, double step = 0.001)
        {
            this.krok = step;

            //Podstawienie
            funkcjaII = function.Replace("y'", "u");
            function = "u";

            //Obliczenie ONP dla I
            ConvertToTable();
            ConvertToONP();

            tablicaONPfunkcjiI = functionONP;

            //Obliczenie ONP dla II
            function = funkcjaII;

            ConvertToTable();
            ConvertToONP();

            tablicaONPfunkcjiII = functionONP;

            //Przywrocenie poprzedniej funkcji i ONP
            function = "u";
            functionONP = tablicaONPfunkcjiI;

            //Właściwe obliczenia
            f0 = f1 = f2 = f3 = f0II = f1II = f2II = f3II = 0;
            wynik = wynikII = 0;

            List<PointD> punkty = new List<PointD>();
            List<PointD> punktyII = new List<PointD>();

            //Przygotowanie
            szukanyPunktII = valueLookingPoint;
            pktPoczatkowy = startingPoint;
            pktPoczatkowyII = startingPoint; //pkt poczatkowy II jest chyba do wyrzucenia, ale trzeba to sprawdzic w algorytmie czy moze sie zdazyc ze pktPocztkowy != pktPoczatkowyII
            wartoscPoczatkowa = startingPointFunctionValue;
            wartoscPoczatkowaII = startingPointFunctionValueII;

            //sprawdzenie czy mam sie posuwać do przodu czy do tyłu
            if (szukanyPunktII > pktPoczatkowyII)
            {
                //Posuwam się do przodu
                for (double i = pktPoczatkowyII + step; i < szukanyPunktII; i += step)
                {
                    ComputeFyII();

                    wynik = wartoscPoczatkowa + (step / 6) * (f0 + 2 * f1 + 2 * f2 + f3);
                    wynikII = wartoscPoczatkowaII + (step / 6) * (f0II + 2 * f1II + 2 * f2II + f3II);

                    //Ustawienie nowych wartosci startowych dla kolejnej iteracji
                    wartoscPoczatkowa = wynik;
                    wartoscPoczatkowaII = wynikII;
                    pktPoczatkowy = i;
                    pktPoczatkowyII = i;

                    punkty.Add(new PointD(i, wynik));
                    punktyII.Add(new PointD(i, wynikII));
                }

                //Doliczenie do żądanej wartosci (bo jak szukam np. x = 3,4567 to teraz doliczyłem do 3,456)
                step = szukanyPunktII - pktPoczatkowyII;
                ComputeFyII();
                wynik = wartoscPoczatkowa + (step / 6) * (f0 + 2 * f1 + 2 * f2 + f3);

                if (applyResultFormatting)
                {
                    //Formatowanie wyniku, żeby 4,0000000000001 wypluł jako 4
                    if (Math.Abs(wynik - Math.Floor(wynik)) < 0.000000001)
                        wynik = Math.Floor(wynik);
                    else if (Math.Abs(wynik - Math.Ceiling(wynik)) < 0.000000001)
                        wynik = Math.Ceiling(wynik);
                }

                punkty.Add(new PointD(szukanyPunktII, wynik));

                return punkty;
            }
            else if (szukanyPunktII < pktPoczatkowyII)
            {
                step = -step;

                //Posuwam się do tyłu
                //Posuwam się do przodu
                for (double i = pktPoczatkowyII + step; i > szukanyPunktII; i += step)
                {
                    ComputeFyII();

                    wynik = wartoscPoczatkowa + (step / 6) * (f0 + 2 * f1 + 2 * f2 + f3);
                    wynikII = wartoscPoczatkowaII + (step / 6) * (f0II + 2 * f1II + 2 * f2II + f3II);

                    //Ustawienie nowych wartosci startowych dla kolejnej iteracji
                    wartoscPoczatkowa = wynik;
                    wartoscPoczatkowaII = wynikII;
                    pktPoczatkowy = i;
                    pktPoczatkowyII = i;

                    punkty.Add(new PointD(i, wynik));
                    punktyII.Add(new PointD(i, wynikII));
                }

                //Doliczenie do żądanej wartosci (bo jak szukam np. x = 3,4567 to teraz doliczyłem do 3,456)
                step = szukanyPunktII - pktPoczatkowyII;
                ComputeFyII();
                wynik = wartoscPoczatkowa + (step / 6) * (f0 + 2 * f1 + 2 * f2 + f3);

                if (applyResultFormatting)
                {
                    //Formatowanie wyniku, żeby 4,0000000000001 wypluł jako 4
                    if (Math.Abs(wynik - Math.Floor(wynik)) < 0.000000001)
                        wynik = Math.Floor(wynik);
                    else if (Math.Abs(wynik - Math.Ceiling(wynik)) < 0.000000001)
                        wynik = Math.Ceiling(wynik);
                }

                punkty.Add(new PointD(szukanyPunktII, wynik));

                return punkty;
            }
            else //Szukany pkt jest == zadanemu punktowi
                return new List<PointD>() { new PointD(szukanyPunkt, startingPointFunctionValue) };
        }

        private void ComputeFy()
        {
            y = wartoscPoczatkowa;
            x = pktPoczatkowy;

            //Obliczenie f0
            if (double.IsNaN(y))
                throw new NaNOccuredException();

            f0 = ComputeFunctionAtPoint();

            //Obliczenie f1
            x += krok / 2;
            y = wartoscPoczatkowa + ((krok / 2) * f0);

            if (double.IsNaN(y))
                throw new NaNOccuredException();

            f1 = ComputeFunctionAtPoint();

            //Obliczenie f2
            y = wartoscPoczatkowa + ((krok / 2) * f1);

            if (double.IsNaN(y))
                throw new NaNOccuredException();

            f2 = ComputeFunctionAtPoint();

            //Obliczenie f3
            x += krok / 2;
            y = wartoscPoczatkowa + (krok * f2);

            if (double.IsNaN(y))
                throw new NaNOccuredException();

            f3 = ComputeFunctionAtPoint();
        }

        private void ComputeFyII()
        {
            u = wartoscPoczatkowaII;
            y = wartoscPoczatkowa;
            x = pktPoczatkowy;

            //Obliczenie f0
            if (double.IsNaN(y) || double.IsNaN(u))
                throw new NaNOccuredException();

            f0 = ComputeFunctionAtPoint();

            //Obliczenie f0II
            string funkcjaTemp = function;
            function = funkcjaII;

            functionONP = tablicaONPfunkcjiII;

            f0II = ComputeFunctionAtPoint();

            //Obliczenie f1
            function = funkcjaTemp;

            functionONP = tablicaONPfunkcjiI;

            x += krok / 2;
            y = wartoscPoczatkowa + ((krok / 2) * f0);
            u = wartoscPoczatkowaII + ((krok / 2) * f0II);

            if (double.IsNaN(y) || double.IsNaN(u))
                throw new NaNOccuredException();

            f1 = ComputeFunctionAtPoint();

            //Obliczenie f1II
            funkcjaTemp = function;
            function = funkcjaII;

            functionONP = tablicaONPfunkcjiII;

            f1II = ComputeFunctionAtPoint();

            //Obliczenie f2
            function = funkcjaTemp;

            functionONP = tablicaONPfunkcjiI;

            y = wartoscPoczatkowa + ((krok / 2) * f1);
            u = wartoscPoczatkowaII + ((krok / 2) * f1II);

            if (double.IsNaN(y) || double.IsNaN(u))
                throw new NaNOccuredException();

            f2 = ComputeFunctionAtPoint();

            //Obliczenie f2II
            funkcjaTemp = function;
            function = funkcjaII;

            functionONP = tablicaONPfunkcjiII;

            f2II = ComputeFunctionAtPoint();

            //Oblicenie f3
            function = funkcjaTemp;

            functionONP = tablicaONPfunkcjiI;

            x += krok / 2;
            y = wartoscPoczatkowa + (krok * f2);
            u = wartoscPoczatkowaII + ((krok) * f2II);

            if (double.IsNaN(y) || double.IsNaN(u))
                throw new NaNOccuredException();

            f3 = ComputeFunctionAtPoint();

            //Obliczenie f3II
            funkcjaTemp = function;
            function = funkcjaII;

            functionONP = tablicaONPfunkcjiII;

            f3II = ComputeFunctionAtPoint();

            //Przywrocenie funkcji i ONP
            function = funkcjaTemp;

            functionONP = tablicaONPfunkcjiI;
        }

        //KONSTUKTOR --------------------------------
        public Differential(string function)
            : base(function)
        { }
    }
}
