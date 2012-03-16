using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumericalCalculator
{
    class Funkcja
    {
    // ZMIENNE --------------------------------
        protected string funkcja;
        protected string[] funkcjaTablica;
        protected string[] funkcjaONP;

        protected Stos stos = new Stos();

        protected HashSet<char> dozwoloneZnaki = new HashSet<char>() { 'E', ' ', ',', '.', 'l', 's', 'i', 'n', 'c', 'o', 't', 'g', 'q', 'r', 'e', 'x', 'p', 'a', '(', ')', '+', '-', '*', '/', '^', '!', 'y', 'h', 'u', 'y', '\'' };
        protected HashSet<char> operatory = new HashSet<char>() { '+', '-', '*', '/', '^', '!' };
        protected HashSet<char> operatoryBezSilni = new HashSet<char>() { '+', '-', '*', '/', '^' };
        protected HashSet<char> operatoryBezSilniAleZKropkaPrzecinek = new HashSet<char>() { '+', '-', '*', '/', '^', ',', '.' };
        protected HashSet<char> operatoryBezPlusMinus = new HashSet<char>() { '*', '/', '^', '!' };

        protected HashSet<string> operatoryBezSilniString = new HashSet<string>() { "+", "-", "*", "/", "^" };

    // METODY ---------------------------------
        protected bool CzyLiczba(string mayBeANum) // SPRAWDZA CZY STRING JEST LICZBA
        {
            double number;
            return double.TryParse(mayBeANum, out number);
        }

        protected virtual void SprawdzenieOdBledow() // SPRAWDZA POPRAWNOSC WPROWADZONYCH DANYCH
        {
            string cache;

            //Tak na szybko zamiana PI na wartość
            funkcja = funkcja.Replace("PI", Math.PI.ToString());

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

                        while (i + licznik < funkcja.Length && (char.IsDigit(funkcja[i + licznik]) || funkcja[i + licznik] == ',' || funkcja[i + licznik] == '.'))
                            licznik++;

                        //Podmiana E na *10^(
                        funkcja = funkcja.Substring(0, i) + "*10^(" + funkcja.Substring(i + 1, licznik - 1) + ")" + funkcja.Substring(i + licznik, funkcja.Length - i - licznik);
                    }
                    catch (SystemException)
                    {
                        throw new FunkcjaException("Niepoprawne wystąpienie operatora \"E\"");
                    }
                }

                //Operator nie moze stac na poczatku wyrazenia (za wyjatkiem + i -), i na koncu (za wyjątkiem silni)
                if (i == 0 && operatoryBezPlusMinus.Contains(c))
                    throw new FunkcjaException("Operator \"" + c + "\" nie może stać na początku wyrażenia!");

                if (i == funkcja.Length - 1 && operatoryBezSilni.Contains(c))
                    throw new FunkcjaException("Operator nie może stać na koncu wyrażenia!");

                //Czy nie stoja kolo siebie dwa operatory (za wyjątkiem silni jako pierwszej z dwóch operatorów)
                if (i != 0)
                    if (operatory.Contains(c) && operatoryBezSilniAleZKropkaPrzecinek.Contains(funkcja[i - 1]))
                        throw new FunkcjaException("Dwa operatory występują obok siebie!");

                //Sprawdzenie czy nie ma dwóch silni obok siebie
                if (i != 0)
                    if (c == '!' && funkcja[i - 1] == '!')
                        throw new FunkcjaException("Dwie silnie występują obok siebie!");

                //Przepuszcza tylko dozwolone znaki
                if (!(dozwoloneZnaki.Contains(c) || char.IsDigit(c)))                    
                    throw new FunkcjaException("Występuje niedozwolony znak \'" + c.ToString() + "\' !");

                // TO DO: Sprawdzenie czy funkcje np. exp(x) sa dobrze wpisane, np. nie epx, albo exp)
            }

            if (funkcja == string.Empty)
                throw new FunkcjaException("Wpisz funkcję!");
        }

        protected void KonwertujNaTablice()// Konwertuje string funkcja na string[] funkcja
        {
            // DEKLARACJA funkcjaTablica
            int i, cache = 0; // CACHE - ILE TRZEBA UCIAC funkcja.Length ŻEBY ZADEKLAROWAC DOBRA WIELKOSC TABLICY

            for (i = 0; i < funkcja.Length - 1; i++) // SPRAWDZENIE ILE JEST LICZB, a nie CYFR, oraz ile jest funkcji typu cos, sin, exp...
            {
                if ((char.IsDigit(funkcja[i]) || funkcja[i] == ',' || funkcja[i] == '.') && (char.IsDigit(funkcja[i + 1]) || funkcja[i + 1] == ',' || funkcja[i + 1] == '.'))
                    cache++;

                // IF FUNKCJA DWULITEROWA
                if ((i + 1 <= funkcja.Length - 1) && ((funkcja[i] == 't' && funkcja[i + 1] == 'g') || (funkcja[i] == 'l' && funkcja[i + 1] == 'n') || (funkcja[i] == 'l' && funkcja[i + 1] == 'g')))
                {
                    int n = -1;
                    cache++;
                    i += 2;

                    do
                    {
                        if ((i > funkcja.Length - 1) || (n == -1 && operatory.Contains(funkcja[i]))) break;
                        if (funkcja[i] == '(')
                            if (n != -1) n++;
                            else n = 1;
                        else if (funkcja[i] == ')')
                            if (n == -1)  //Obsluga przypadku '...cos30)'
                                break;
                            else
                                n--;

                        cache++;
                        i++;
                    }
                    while (n != 0);
                }
                // IF FUNKCJA TRZYLITEROWA
                else if ((i + 2 <= funkcja.Length - 1) && ((funkcja[i] == 's' && funkcja[i + 1] == 'i' && funkcja[i + 2] == 'n') || (funkcja[i] == 'c' && funkcja[i + 1] == 'o' && funkcja[i + 2] == 's') || (funkcja[i] == 'c' && funkcja[i + 1] == 't' && funkcja[i + 2] == 'g') || (funkcja[i] == 'e' && funkcja[i + 1] == 'x' && funkcja[i + 2] == 'p') || (funkcja[i] == 'a' && funkcja[i + 1] == 't' && funkcja[i + 2] == 'g') || (funkcja[i] == 'l' && funkcja[i + 1] == 'o' && funkcja[i + 2] == 'g') || (funkcja[i] == 't' && funkcja[i + 1] == 'g' && funkcja[i + 2] == 'h') || (funkcja[i] == 't' && funkcja[i + 1] == 'a' && funkcja[i + 2] == 'n') || (funkcja[i] == 'c' && funkcja[i + 1] == 't' && funkcja[i + 2] == 'n') || (funkcja[i] == 'c' && funkcja[i + 1] == 'o' && funkcja[i + 2] == 't') || (funkcja[i] == 's' && funkcja[i + 1] == 'e' && funkcja[i + 2] == 'c') || (funkcja[i] == 'c' && funkcja[i + 1] == 's' && funkcja[i + 2] == 'c')))
                {
                    int n = -1; //n - zlicza nawiasy
                    cache += 2;
                    i += 3; //przesuniecie indeksu petli for o np. "cos"

                    do
                    {
                        if ((i > funkcja.Length - 1) || (n == -1 && operatory.Contains(funkcja[i])))
                            break;
                        if (funkcja[i] == '(')
                            if (n != -1)
                                n++;
                            else
                                n = 1;
                        else if (funkcja[i] == ')')
                            if (n == -1) //Obsluga przypadku '...cos30)'
                                break;
                            else
                                n--;

                        cache++;
                        i++;
                    }
                    while (n != 0);
                }
                // IF FUNKCJA 4-ro LITEROWA
                else if ((i + 3 <= funkcja.Length - 1) && ((funkcja[i] == 's' && funkcja[i + 1] == 'q' && funkcja[i + 2] == 'r' && funkcja[i + 3] == 't') || (funkcja[i] == 'a' && ((funkcja[i + 1] == 's' && funkcja[i + 2] == 'i' && funkcja[i + 3] == 'n') || (funkcja[i + 1] == 'c' && funkcja[i + 2] == 'o' && funkcja[i + 3] == 's') || (funkcja[i + 1] == 'c' && funkcja[i + 2] == 't' && funkcja[i + 3] == 'g'))) || (funkcja[i] == 's' && funkcja[i + 1] == 'i' && funkcja[i + 2] == 'n' && funkcja[i + 3] == 'h') || (funkcja[i] == 'c' && funkcja[i + 1] == 'o' && funkcja[i + 2] == 's' && funkcja[i + 3] == 'h') || (funkcja[i] == 'c' && funkcja[i + 1] == 't' && funkcja[i + 2] == 'g' && funkcja[i + 3] == 'h') || (funkcja[i] == 't' && funkcja[i + 1] == 'a' && funkcja[i + 2] == 'n' && funkcja[i + 3] == 'h') || (funkcja[i] == 'c' && funkcja[i + 1] == 't' && funkcja[i + 2] == 'n' && funkcja[i + 3] == 'h') || (funkcja[i] == 'c' && funkcja[i + 1] == 'o' && funkcja[i + 2] == 't' && funkcja[i + 3] == 'h')))
                {
                    int n = -1;
                    cache += 3;
                    i += 4;

                    do
                    {
                        if ((i > funkcja.Length - 1) || (n == -1 && operatory.Contains(funkcja[i]))) break;
                        if (funkcja[i] == '(')
                            if (n != -1) n++;
                            else n = 1;
                        else if (funkcja[i] == ')')
                            if (n == -1)  //Obsluga przypadku '...cos30)'
                                break;
                            else
                                n--;

                        cache++;
                        i++;
                    }
                    while (n != 0);
                }
            }

            funkcjaTablica = new string[funkcja.Length - cache];

            // KONWERSJA
            int j = 0;

            for (i = 0; i < funkcjaTablica.Length; i++)
            {
                string zwrot;

                if (char.IsDigit(funkcja[j]) || funkcja[j] == ',' || funkcja[j] == '.' || funkcja[j] == 'x' || funkcja[j] == 'u' || funkcja[j] == 'y') // JAK CYFRA
                {
                    zwrot = char.ToString(funkcja[j]);

                    while ((j + 1 < funkcja.Length) && (char.IsDigit(funkcja[j + 1]) || (funkcja[j + 1] == ',') || (funkcja[j + 1] == '.'))) // JEŚLI LICZBA
                        zwrot += funkcja[j++ + 1];

                    j++;
                }
                // FUNKCJA 4 ZNAKOWA
                else if ((funkcja[j] == 's' && funkcja[j + 1] == 'q' && funkcja[j + 2] == 'r' && funkcja[j + 3] == 't') || (funkcja[j] == 'a' && ((funkcja[j + 1] == 's' && funkcja[j + 2] == 'i' && funkcja[j + 3] == 'n') || (funkcja[j + 1] == 'c' && funkcja[j + 2] == 'o' && funkcja[j + 3] == 's') || (funkcja[j + 1] == 'c' && funkcja[j + 2] == 't' && funkcja[j + 3] == 'g'))) || (funkcja[j] == 's' && funkcja[j + 1] == 'j' && funkcja[j + 2] == 'n' && funkcja[j + 3] == 'h') || (funkcja[j] == 'c' && funkcja[j + 1] == 'o' && funkcja[j + 2] == 's' && funkcja[j + 3] == 'h') || (funkcja[j] == 'c' && funkcja[j + 1] == 't' && funkcja[j + 2] == 'g' && funkcja[j + 3] == 'h') || (funkcja[j] == 't' && funkcja[j + 1] == 'a' && funkcja[j + 2] == 'n' && funkcja[j + 3] == 'h') || (funkcja[j] == 'c' && funkcja[j + 1] == 't' && funkcja[j + 2] == 'n' && funkcja[j + 3] == 'h') || (funkcja[j] == 'c' && funkcja[j + 1] == 'o' && funkcja[j + 2] == 't' && funkcja[j + 3] == 'h'))
                {
                    string funkcjaPodstawowa;
                    funkcjaPodstawowa = (char.ToString(funkcja[j]) + char.ToString(funkcja[j + 1]) + char.ToString(funkcja[j + 2]) + char.ToString(funkcja[j + 3]));
                    j += 4;
                    int n = -1;

                    do
                    {
                        if ((j > funkcja.Length - 1) || (n == -1 && operatory.Contains(funkcja[j]))) break; // obsluga np cos30 zamiast cos(30)
                        if (funkcja[j] == '(')
                            if (n != -1) n++;
                            else n = 1;
                        else if (funkcja[j] == ')')
                            if (n == -1)  //Obsluga przypadku '...cos30)'
                                break;
                            else
                                n--;

                        funkcjaPodstawowa += funkcja[j++];
                    }
                    while (n != 0);

                    zwrot = funkcjaPodstawowa;
                }
                // FUNKCJA 3 ZNAKOWA
                else if ((funkcja[j] == 's' && funkcja[j + 1] == 'i' && funkcja[j + 2] == 'n') || (funkcja[j] == 'c' && funkcja[j + 1] == 'o' && funkcja[j + 2] == 's') || (funkcja[j] == 'c' && funkcja[j + 1] == 't' && funkcja[j + 2] == 'g') || (funkcja[j] == 'e' && funkcja[j + 1] == 'x' && funkcja[j + 2] == 'p') || (funkcja[j] == 'a' && funkcja[j + 1] == 't' && funkcja[j + 2] == 'g') || (funkcja[j] == 'l' && funkcja[j + 1] == 'o' && funkcja[j + 2] == 'g') || (funkcja[j] == 't' && funkcja[j + 1] == 'g' && funkcja[j + 2] == 'h') || (funkcja[j] == 't' && funkcja[j + 1] == 'a' && funkcja[j + 2] == 'n') || (funkcja[j] == 'c' && funkcja[j + 1] == 't' && funkcja[j + 2] == 'n') || (funkcja[j] == 'c' && funkcja[j + 1] == 'o' && funkcja[j + 2] == 't') || (funkcja[j] == 's' && funkcja[j + 1] == 'e' && funkcja[j + 2] == 'c') || (funkcja[j] == 'c' && funkcja[j + 1] == 's' && funkcja[j + 2] == 'c'))
                {
                    string funkcjaPodstawowa;
                    funkcjaPodstawowa = (char.ToString(funkcja[j]) + char.ToString(funkcja[j + 1]) + char.ToString(funkcja[j + 2]));
                    j += 3;
                    int n = -1;

                    do
                    {
                        if ((j > funkcja.Length - 1) || (n == -1 && operatory.Contains(funkcja[j])))
                            break; // obsluga np cos30 zamiast cos(30)
                        if (funkcja[j] == '(')
                            if (n != -1)
                                n++;
                            else
                                n = 1;
                        else if (funkcja[j] == ')')
                            if (n == -1)  //Obsluga przypadku '...cos30)'
                                break;
                            else
                                n--;

                        funkcjaPodstawowa += funkcja[j++];
                    }
                    while (n != 0);

                    zwrot = funkcjaPodstawowa;
                }
                // FUNKCJA 2 LITEROWA
                else if ((funkcja[j] == 't' && funkcja[j + 1] == 'g') || (funkcja[j] == 'l' && funkcja[j + 1] == 'g') || (funkcja[j] == 'l' && funkcja[j + 1] == 'n'))
                {
                    string funkcjaPodstawowa;
                    funkcjaPodstawowa = (char.ToString(funkcja[j]) + char.ToString(funkcja[j + 1]));
                    j += 2;
                    int n = -1;

                    do
                    {
                        if ((j > funkcja.Length - 1) || (n == -1 && operatory.Contains(funkcja[j]))) break; // obsluga np cos30 zamiast cos(30)
                        if (funkcja[j] == '(')
                            if (n != -1) n++;
                            else n = 1;
                        else if (funkcja[j] == ')')
                            if (n == -1)  //Obsluga przypadku '...cos30)'
                                break;
                            else
                                n--;

                        funkcjaPodstawowa += funkcja[j++];
                    }
                    while (n != 0);

                    zwrot = funkcjaPodstawowa;
                }
                else // JAK OPERATOR
                {
                    zwrot = char.ToString(funkcja[j]);
                    j++;
                }

                funkcjaTablica[i] = zwrot;
            }
        }

        protected void KonwertujNaONP() // Konwertuje funkcja na ONP
        {
            // SPRAWDZENIE NAWIASÓW - jakos glupio ten kod napisalem
            int l, n, nn; // nn - zlicza ilosc nawiasow i ile miejsc w tablicy bedzie potrzebne na "dodatkowe" zera w przypadku np. (-3)

            n = nn = 0;

            if (funkcjaTablica[0] == "-") // obsługa pierwszej ujemnej liczby bez nawiasu np. "-3"
                nn--;

            //Zliczanie nawiasow (exp3*sin3-exp3*cos3)/2+(exp(-3)*(sin3+cos3))/2
            for (l = 0; l < funkcjaTablica.Length; l++)
            {
                string i = funkcjaTablica[l];
                switch (i)
                {
                    case "(": n++; if (funkcjaTablica[l + 1] != "-") nn++; break;
                    case ")": n--; nn++; break;
                }
            }

            //Ilosc lewych i prawych nawiasow nie zgadza sie
            if (n != 0)
                throw new FunkcjaException("Ilość lewych i prawych nawiasów nie zgadza się!");

            // PRZYGOTOWANIE ZMIENNYCH
            funkcjaONP = new string[funkcjaTablica.Length - nn];
            stos.wyczysc();

            // ZAMIANA NA ONP
            int j, k = 0;

            for (j = 0; j < funkcjaTablica.Length; j++)
            {
                string i = funkcjaTablica[j];

                if (CzyLiczba(i) || i == "," || i == "x" || i == "u" || i == "y") // JAK CYFRA
                    funkcjaONP[k++] = i;
                // JAK FUNKCJA
                else if (i.StartsWith("sin") || i.StartsWith("cos") || i.StartsWith("tg") || i.StartsWith("ctg") || i.StartsWith("asin") || i.StartsWith("acos") || i.StartsWith("atg") || i.StartsWith("actg") || i.StartsWith("exp") || i.StartsWith("sqrt") || i.StartsWith("ln") || i.StartsWith("lg") || i.StartsWith("log") || i.StartsWith("tgh") || i.StartsWith("tan") || i.StartsWith("ctn") || i.StartsWith("cot") || i.StartsWith("sec") || i.StartsWith("csc") || i.StartsWith("sinh") || i.StartsWith("cosh") || i.StartsWith("ctgh") || i.StartsWith("tanh") || i.StartsWith("ctnh") || i.StartsWith("coth"))
                    funkcjaONP[k++] = i;
                else // JAK OPERATOR
                {
                    switch (i) // JAKI OPERATOR
                    {
                        case "-":

                            if (j == 0 || funkcjaTablica[j - 1] == "(") // OBSŁUGA np. (-3)
                                funkcjaONP[k++] = "0";

                            if (stos.wartosc() == "(" || stos.pusty() == true)
                                stos.doloz(i);
                            else
                            {
                                while (stos.wartosc() == "+" || stos.wartosc() == "*" || stos.wartosc() == "/" || stos.wartosc() == "^" || stos.wartosc() == "-" || stos.wartosc() == "!")
                                    funkcjaONP[k++] = stos.zdejmij();

                                stos.doloz(i);
                            }
                            break;

                        case "+":

                            if (stos.wartosc() == "(" || stos.pusty() == true)
                                stos.doloz(i);
                            else
                            {
                                while (stos.wartosc() == "-" || stos.wartosc() == "*" || stos.wartosc() == "/" || stos.wartosc() == "^" || stos.wartosc() == "+" || stos.wartosc() == "!")
                                    funkcjaONP[k++] = stos.zdejmij();

                                stos.doloz(i);
                            }
                            break;

                        case "*":

                            if (stos.wartosc() == "(" || stos.wartosc() == "+" || stos.wartosc() == "-" || stos.pusty() == true)
                                stos.doloz(i);
                            else
                            {
                                while (stos.wartosc() == "/" || stos.wartosc() == "^" || stos.wartosc() == "*" || stos.wartosc() == "!")
                                    funkcjaONP[k++] = stos.zdejmij();

                                stos.doloz(i);
                            }
                            break;

                        case "/":

                            if (stos.wartosc() == "(" || stos.wartosc() == "+" || stos.wartosc() == "-" || stos.pusty() == true)
                                stos.doloz(i);
                            else
                            {
                                while (stos.wartosc() == "*" || stos.wartosc() == "^" || stos.wartosc() == "/" || stos.wartosc() == "!")
                                    funkcjaONP[k++] = stos.zdejmij();

                                stos.doloz(i);
                            }
                            break;

                        case "^":

                            if (stos.wartosc() == "(" || stos.wartosc() == "+" || stos.wartosc() == "-" || stos.wartosc() == "*" || stos.wartosc() == "/" || stos.wartosc() == "!" || stos.pusty() == true)
                                stos.doloz(i);
                            else
                                stos.doloz(i);

                            break;

                        case "!":

                            if (stos.wartosc() == "(" || stos.wartosc() == "+" || stos.wartosc() == "-" || stos.wartosc() == "*" || stos.wartosc() == "/" || stos.wartosc() == "^" || stos.pusty() == true)
                                stos.doloz(i);
                            else
                                stos.doloz(i);

                            break;

                        case "(":

                            stos.doloz(i);
                            break;

                        case ")":

                            while (stos.wartosc() != "(")
                                funkcjaONP[k++] = stos.zdejmij();

                            stos.zdejmij(); // ZDEJMUJE ZE STOSU TEZ NAWIAS
                            break;
                    }
                }
            }

            while (stos.pusty() == false)  // ZDJECIE ZE STOSU POZOSTALOSCI
                funkcjaONP[k++] = stos.zdejmij();
        }

    // KONSTRUKTOR ----------------------------
        public Funkcja(string funk) // KONSTRUKTOR
        {
            funkcja = funk;
        }
    }
}