using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace NumericalCalculator
{
    class Rozniczka : Pochodna
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
        public List<PointD> ObliczRozniczke(double punktWKtorymSzukamyWartosciFunkcji, double punktPoczatkowy, double wartoscFunkcjiWPunkciePoczatkowym, bool czyFormatowacWynik = true, double krok = 0.001)
        {
            this.krok = krok;

            f0 = f1 = f2 = f3 = 0;
            wynik = 0;

            List<PointD> punkty = new List<PointD>();

            //Przygotowanie
            szukanyPunkt = punktWKtorymSzukamyWartosciFunkcji;
            pktPoczatkowy = punktPoczatkowy;
            wartoscPoczatkowa = wartoscFunkcjiWPunkciePoczatkowym;

            //sprawdzenie czy mam sie posuwać do przodu czy do tyłu
            if (szukanyPunkt > pktPoczatkowy)
            {
                //Posuwam się do przodu
                for (double i = pktPoczatkowy + krok; i < szukanyPunkt; i += krok)
                {
                    ObliczFy();

                    wynik = wartoscPoczatkowa + (krok / 6) * (f0 + 2 * f1 + 2 * f2 + f3);

                    //Ustawienie nowych wartosci startowych dla kolejnej iteracji
                    wartoscPoczatkowa = wynik;
                    pktPoczatkowy = i;

                    punkty.Add(new PointD(i, wynik));
                }

                //Doliczenie do żądanej wartosci (bo jak szukam np. x = 3,4567 to teraz doliczyłem do 3,456)
                krok = szukanyPunkt - pktPoczatkowy;
                ObliczFy();
                wynik = wartoscPoczatkowa + (krok / 6) * (f0 + 2 * f1 + 2 * f2 + f3);

                if (czyFormatowacWynik)
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
                krok = -krok;

                //Posuwam się do tyłu
                for (double i = pktPoczatkowy + krok; i > szukanyPunkt; i += krok)
                {
                    ObliczFy();

                    wynik = wartoscPoczatkowa + (krok / 6) * (f0 + 2 * f1 + 2 * f2 + f3);

                    //Ustawienie nowych wartosci startowych dla kolejnej iteracji
                    wartoscPoczatkowa = wynik;
                    pktPoczatkowy = i;

                    punkty.Add(new PointD(i, y));
                }

                //Doliczenie do żądanej wartosci (bo jak szukam np. x = 3,4567 to teraz doliczyłem do 3,456)
                krok = -(pktPoczatkowy - szukanyPunkt);
                ObliczFy();
                wynik = wartoscPoczatkowa + (krok / 6) * (f0 + 2 * f1 + 2 * f2 + f3);

                if (czyFormatowacWynik)
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
                return new List<PointD>() { new PointD(szukanyPunkt, wartoscFunkcjiWPunkciePoczatkowym) };
        }

        internal List<PointD> ObliczRozniczkeII(double punktWKtorymSzukamyWartosciFunkcji, double punktPoczatkowy, double wartoscFunkcjiWPunkciePoczatkowym, double punktPoczatkowyII, double wartoscFunkcjiWPunkciePoczatkowymII, bool czyFormatowacWynik = true, double krok = 0.001)
        {
            this.krok = krok;

            //Podstawienie
            funkcjaII = funkcja.Replace("y'", "u");
            funkcja = "u";

            //Obliczenie ONP dla I
            KonwertujNaTablice();
            KonwertujNaONP();

            tablicaONPfunkcjiI = funkcjaONP;

            //Obliczenie ONP dla II
            funkcja = funkcjaII;

            KonwertujNaTablice();
            KonwertujNaONP();

            tablicaONPfunkcjiII = funkcjaONP;

            //Przywrocenie poprzedniej funkcji i ONP
            funkcja = "u";
            funkcjaONP = tablicaONPfunkcjiI;

            //Właściwe obliczenia
            f0 = f1 = f2 = f3 = f0II = f1II = f2II = f3II = 0;
            wynik = wynikII = 0;

            List<PointD> punkty = new List<PointD>();
            List<PointD> punktyII = new List<PointD>();

            //Przygotowanie
            szukanyPunktII = punktWKtorymSzukamyWartosciFunkcji;
            pktPoczatkowy = punktPoczatkowy;
            pktPoczatkowyII = punktPoczatkowyII;
            wartoscPoczatkowa = wartoscFunkcjiWPunkciePoczatkowym;
            wartoscPoczatkowaII = wartoscFunkcjiWPunkciePoczatkowymII;

            //sprawdzenie czy mam sie posuwać do przodu czy do tyłu
            if (szukanyPunktII > pktPoczatkowyII)
            {
                //Posuwam się do przodu
                for (double i = pktPoczatkowyII + krok; i < szukanyPunktII; i += krok)
                {
                    ObliczFyII();

                    wynik = wartoscPoczatkowa + (krok / 6) * (f0 + 2 * f1 + 2 * f2 + f3);
                    wynikII = wartoscPoczatkowaII + (krok / 6) * (f0II + 2 * f1II + 2 * f2II + f3II);

                    //Ustawienie nowych wartosci startowych dla kolejnej iteracji
                    wartoscPoczatkowa = wynik;
                    wartoscPoczatkowaII = wynikII;
                    pktPoczatkowy = i;
                    pktPoczatkowyII = i;

                    punkty.Add(new PointD(i, wynik));
                    punktyII.Add(new PointD(i, wynikII));
                }

                //Doliczenie do żądanej wartosci (bo jak szukam np. x = 3,4567 to teraz doliczyłem do 3,456)
                krok = szukanyPunktII - pktPoczatkowyII;
                ObliczFyII();
                wynik = wartoscPoczatkowa + (krok / 6) * (f0 + 2 * f1 + 2 * f2 + f3);

                if (czyFormatowacWynik)
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
                krok = -krok;

                //Posuwam się do tyłu
                //Posuwam się do przodu
                for (double i = pktPoczatkowyII + krok; i > szukanyPunktII; i += krok)
                {
                    ObliczFyII();

                    wynik = wartoscPoczatkowa + (krok / 6) * (f0 + 2 * f1 + 2 * f2 + f3);
                    wynikII = wartoscPoczatkowaII + (krok / 6) * (f0II + 2 * f1II + 2 * f2II + f3II);

                    //Ustawienie nowych wartosci startowych dla kolejnej iteracji
                    wartoscPoczatkowa = wynik;
                    wartoscPoczatkowaII = wynikII;
                    pktPoczatkowy = i;
                    pktPoczatkowyII = i;

                    punkty.Add(new PointD(i, wynik));
                    punktyII.Add(new PointD(i, wynikII));
                }

                //Doliczenie do żądanej wartosci (bo jak szukam np. x = 3,4567 to teraz doliczyłem do 3,456)
                krok = szukanyPunktII - pktPoczatkowyII;
                ObliczFyII();
                wynik = wartoscPoczatkowa + (krok / 6) * (f0 + 2 * f1 + 2 * f2 + f3);

                if (czyFormatowacWynik)
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
                return new List<PointD>() { new PointD(szukanyPunkt, wartoscFunkcjiWPunkciePoczatkowym) };
        }

        private void ObliczFy()
        {
            y = wartoscPoczatkowa;
            x = pktPoczatkowy;

            //Obliczenie f0
            if (double.IsNaN(y))
                throw new FunkcjaException("Brak rozwiazan w zbiorze liczb rzeczywistych!");

            f0 = ObliczFunkcjeWPunkcie();

            //Obliczenie f1
            x += krok / 2;
            y = wartoscPoczatkowa + ((krok / 2) * f0);

            if (double.IsNaN(y))
                throw new FunkcjaException("Brak rozwiazan w zbiorze liczb rzeczywistych!");

            f1 = ObliczFunkcjeWPunkcie();

            //Obliczenie f2
            y = wartoscPoczatkowa + ((krok / 2) * f1);

            if (double.IsNaN(y))
                throw new FunkcjaException("Brak rozwiazan w zbiorze liczb rzeczywistych!");

            f2 = ObliczFunkcjeWPunkcie();

            //Obliczenie f3
            x += krok / 2;
            y = wartoscPoczatkowa + (krok * f2);

            if (double.IsNaN(y))
                throw new FunkcjaException("Brak rozwiazan w zbiorze liczb rzeczywistych!");

            f3 = ObliczFunkcjeWPunkcie();
        }

        private void ObliczFyII()
        {
            u = wartoscPoczatkowaII;
            y = wartoscPoczatkowa;
            x = pktPoczatkowy;

            //Obliczenie f0
            if (double.IsNaN(y) || double.IsNaN(u))
                throw new FunkcjaException("Brak rozwiazan w zbiorze liczb rzeczywistych!");

            f0 = ObliczFunkcjeWPunkcie();

            //Obliczenie f0II
            string funkcjaTemp = funkcja;
            funkcja = funkcjaII;

            funkcjaONP = tablicaONPfunkcjiII;

            f0II = ObliczFunkcjeWPunkcie();

            //Obliczenie f1
            funkcja = funkcjaTemp;

            funkcjaONP = tablicaONPfunkcjiI;

            x += krok / 2;
            y = wartoscPoczatkowa + ((krok / 2) * f0);
            u = wartoscPoczatkowaII + ((krok / 2) * f0II);

            if (double.IsNaN(y) || double.IsNaN(u))
                throw new FunkcjaException("Brak rozwiazan w zbiorze liczb rzeczywistych!");

            f1 = ObliczFunkcjeWPunkcie();

            //Obliczenie f1II
            funkcjaTemp = funkcja;
            funkcja = funkcjaII;

            funkcjaONP = tablicaONPfunkcjiII;

            f1II = ObliczFunkcjeWPunkcie();

            //Obliczenie f2
            funkcja = funkcjaTemp;

            funkcjaONP = tablicaONPfunkcjiI;

            y = wartoscPoczatkowa + ((krok / 2) * f1);
            u = wartoscPoczatkowaII + ((krok / 2) * f1II);

            if (double.IsNaN(y) || double.IsNaN(u))
                throw new FunkcjaException("Brak rozwiazan w zbiorze liczb rzeczywistych!");

            f2 = ObliczFunkcjeWPunkcie();

            //Obliczenie f2II
            funkcjaTemp = funkcja;
            funkcja = funkcjaII;

            funkcjaONP = tablicaONPfunkcjiII;

            f2II = ObliczFunkcjeWPunkcie();

            //Oblicenie f3
            funkcja = funkcjaTemp;

            funkcjaONP = tablicaONPfunkcjiI;

            x += krok / 2;
            y = wartoscPoczatkowa + (krok * f2);
            u = wartoscPoczatkowaII + ((krok) * f2II);

            if (double.IsNaN(y) || double.IsNaN(u))
                throw new FunkcjaException("Brak rozwiazan w zbiorze liczb rzeczywistych!");

            f3 = ObliczFunkcjeWPunkcie();

            //Obliczenie f3II
            funkcjaTemp = funkcja;
            funkcja = funkcjaII;

            funkcjaONP = tablicaONPfunkcjiII;

            f3II = ObliczFunkcjeWPunkcie();

            //Przywrocenie funkcji i ONP
            funkcja = funkcjaTemp;

            funkcjaONP = tablicaONPfunkcjiI;
        }

        //KONSTUKTOR --------------------------------
        public Rozniczka(string funkcja)
            : base(funkcja)
        { }
    }
}
