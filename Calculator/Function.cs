using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumericalCalculator
{
    public abstract class Function
    {
        // ZMIENNE --------------------------------
        protected string function;
        protected string[] functionTable;
        protected string[] functionONP;

        protected Stack stack = new Stack();

        protected HashSet<char> dozwoloneZnaki = new HashSet<char>() { 'E', ' ', ',', '.', 'l', 's', 'i', 'n', 'c', 'o', 't', 'g', 'q', 'r', 'e', 'x', 'p', 'a', '(', ')', '+', '-', '*', '/', '^', '!', 'y', 'h', 'u', 'y', '\'' };
        protected HashSet<char> operatory = new HashSet<char>() { '+', '-', '*', '/', '^', '!' };
        protected HashSet<char> operatorsWithoutFactorial = new HashSet<char>() { '+', '-', '*', '/', '^' };
        protected HashSet<char> operatoryBezSilniAleZKropkaPrzecinek = new HashSet<char>() { '+', '-', '*', '/', '^', ',', '.' };
        protected HashSet<char> operatoryBezPlusMinus = new HashSet<char>() { '*', '/', '^', '!' };

        protected HashSet<string> operatoryBezSilniString = new HashSet<string>() { "+", "-", "*", "/", "^" };

        // METODY ---------------------------------
        protected bool CzyLiczba(string mayBeANum) // SPRAWDZA CZY STRING JEST LICZBA
        {
            double number;
            return double.TryParse(mayBeANum, out number);
        }

        protected virtual void ErrorCheck() // SPRAWDZA POPRAWNOSC WPROWADZONYCH DANYCH
        {
            string cache;

            //Tak na szybko zamiana PI na wartość
            function = function.Replace("PI", Math.PI.ToString());

            for (int i = 0; i < function.Length; i++)
            {
                char c = function[i];

                if (c == ' ')  // Usuwanie spacji
                {
                    cache = function.Substring(i + 1, function.Length - (i + 1));
                    function = function.Substring(0, i);
                    function += cache;
                    i--;
                }

                //Zamiana np. E-05 na 10^(-5)
                if (c == 'E') // Zliczanie licznika
                {
                    try
                    {
                        int licznikNawiasow = 1, licznik = 1; //licznik = znakow po E trzeba objac w nawias

                        if (function[i + licznik] == '+' || function[i + licznik] == '-')
                            licznik++;

                        if (function[i + licznik] == '(')
                        {
                            licznik++;

                            while (licznikNawiasow != 0)
                            {
                                if (function[i + licznik] == '(')
                                    licznikNawiasow++;
                                else if (function[i + licznik] == ')')
                                    licznikNawiasow--;

                                licznik++;
                            }
                        }

                        while (i + licznik < function.Length && (char.IsDigit(function[i + licznik]) || function[i + licznik] == ',' || function[i + licznik] == '.'))
                            licznik++;

                        //Podmiana E na *10^(
                        function = function.Substring(0, i) + "*10^(" + function.Substring(i + 1, licznik - 1) + ")" + function.Substring(i + licznik, function.Length - i - licznik);
                    }
                    catch (SystemException)
                    {
                        throw new IncorrectEOperatorOccurrenceException();//FunctionException("Niepoprawne wystąpienie operatora \"E\"");
                    }
                }

                //Operator nie moze stac na poczatku wyrazenia (za wyjatkiem + i -), i na koncu (za wyjątkiem silni)
                if (i == 0 && operatoryBezPlusMinus.Contains(c))
                    throw new OperatorAtTheBeginningOfTheExpressionException(c);//FunctionException("Operator \"" + c + "\" nie może stać na początku wyrażenia!");

                if (i == function.Length - 1 && operatorsWithoutFactorial.Contains(c))
                    throw new OperatorAtTheEndOfTheExpressionException(c);//FunctionException("Operator nie może stać na końcu wyrażenia!");

                //Czy nie stoja kolo siebie dwa operatory (za wyjątkiem silni jako pierwszej z dwóch operatorów)
                if (i != 0)
                    if (operatory.Contains(c) && operatoryBezSilniAleZKropkaPrzecinek.Contains(function[i - 1]))
                        throw new TwoOperatorsOccurredSideBySideException();//FunctionException("Dwa operatory występują obok siebie!");

                //Sprawdzenie czy nie ma dwóch silni obok siebie
                if (i != 0)
                    if (c == '!' && function[i - 1] == '!')
                        throw new TwoFactorialsOccuredSideBySideException();//FunctionException("Dwie silnie występują obok siebie!");

                //Przepuszcza tylko dozwolone znaki
                if (!(dozwoloneZnaki.Contains(c) || char.IsDigit(c)))
                    throw new ForbiddenSignDetectedException(c);//FunctionException("Występuje niedozwolony znak \'" + c.ToString() + "\' !");

                // TO DO: Sprawdzenie czy funkcje np. exp(x) sa dobrze wpisane, np. nie epx, albo exp)
            }

            if (function == string.Empty)
                throw new EmptyFunctionStringException();//FunctionException("Wpisz funkcję!");
        }

        protected void ConvertToTable()// Konwertuje string funkcja na string[] funkcja
        {
            // DEKLARACJA funkcjaTablica
            int i, cache = 0; // CACHE - ILE TRZEBA UCIAC funkcja.Length ŻEBY ZADEKLAROWAC DOBRA WIELKOSC TABLICY

            for (i = 0; i < function.Length - 1; i++) // SPRAWDZENIE ILE JEST LICZB, a nie CYFR, oraz ile jest funkcji typu cos, sin, exp...
            {
                if ((char.IsDigit(function[i]) || function[i] == ',' || function[i] == '.') && (char.IsDigit(function[i + 1]) || function[i + 1] == ',' || function[i + 1] == '.'))
                    cache++;

                // IF FUNKCJA DWULITEROWA
                if ((i + 1 <= function.Length - 1) && ((function[i] == 't' && function[i + 1] == 'g') || (function[i] == 'l' && function[i + 1] == 'n') || (function[i] == 'l' && function[i + 1] == 'g')))
                {
                    int n = -1;
                    cache++;
                    i += 2;

                    do
                    {
                        if ((i > function.Length - 1) || (n == -1 && operatory.Contains(function[i]))) break;
                        if (function[i] == '(')
                            if (n != -1) n++;
                            else n = 1;
                        else if (function[i] == ')')
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
                else if ((i + 2 <= function.Length - 1) && ((function[i] == 's' && function[i + 1] == 'i' && function[i + 2] == 'n') || (function[i] == 'c' && function[i + 1] == 'o' && function[i + 2] == 's') || (function[i] == 'c' && function[i + 1] == 't' && function[i + 2] == 'g') || (function[i] == 'e' && function[i + 1] == 'x' && function[i + 2] == 'p') || (function[i] == 'a' && function[i + 1] == 't' && function[i + 2] == 'g') || (function[i] == 'l' && function[i + 1] == 'o' && function[i + 2] == 'g') || (function[i] == 't' && function[i + 1] == 'g' && function[i + 2] == 'h') || (function[i] == 't' && function[i + 1] == 'a' && function[i + 2] == 'n') || (function[i] == 'c' && function[i + 1] == 't' && function[i + 2] == 'n') || (function[i] == 'c' && function[i + 1] == 'o' && function[i + 2] == 't') || (function[i] == 's' && function[i + 1] == 'e' && function[i + 2] == 'c') || (function[i] == 'c' && function[i + 1] == 's' && function[i + 2] == 'c')))
                {
                    int n = -1; //n - zlicza nawiasy
                    cache += 2;
                    i += 3; //przesuniecie indeksu petli for o np. "cos"

                    do
                    {
                        if ((i > function.Length - 1) || (n == -1 && operatory.Contains(function[i])))
                            break;
                        if (function[i] == '(')
                            if (n != -1)
                                n++;
                            else
                                n = 1;
                        else if (function[i] == ')')
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
                else if ((i + 3 <= function.Length - 1) && ((function[i] == 's' && function[i + 1] == 'q' && function[i + 2] == 'r' && function[i + 3] == 't') || (function[i] == 'a' && ((function[i + 1] == 's' && function[i + 2] == 'i' && function[i + 3] == 'n') || (function[i + 1] == 'c' && function[i + 2] == 'o' && function[i + 3] == 's') || (function[i + 1] == 'c' && function[i + 2] == 't' && function[i + 3] == 'g'))) || (function[i] == 's' && function[i + 1] == 'i' && function[i + 2] == 'n' && function[i + 3] == 'h') || (function[i] == 'c' && function[i + 1] == 'o' && function[i + 2] == 's' && function[i + 3] == 'h') || (function[i] == 'c' && function[i + 1] == 't' && function[i + 2] == 'g' && function[i + 3] == 'h') || (function[i] == 't' && function[i + 1] == 'a' && function[i + 2] == 'n' && function[i + 3] == 'h') || (function[i] == 'c' && function[i + 1] == 't' && function[i + 2] == 'n' && function[i + 3] == 'h') || (function[i] == 'c' && function[i + 1] == 'o' && function[i + 2] == 't' && function[i + 3] == 'h')))
                {
                    int n = -1;
                    cache += 3;
                    i += 4;

                    do
                    {
                        if ((i > function.Length - 1) || (n == -1 && operatory.Contains(function[i]))) break;
                        if (function[i] == '(')
                            if (n != -1) n++;
                            else n = 1;
                        else if (function[i] == ')')
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

            functionTable = new string[function.Length - cache];

            // KONWERSJA
            int j = 0;

            for (i = 0; i < functionTable.Length; i++)
            {
                string zwrot;

                if (char.IsDigit(function[j]) || function[j] == ',' || function[j] == '.' || function[j] == 'x' || function[j] == 'u' || function[j] == 'y') // JAK CYFRA
                {
                    zwrot = char.ToString(function[j]);

                    while ((j + 1 < function.Length) && (char.IsDigit(function[j + 1]) || (function[j + 1] == ',') || (function[j + 1] == '.'))) // JEŚLI LICZBA
                        zwrot += function[j++ + 1];

                    j++;
                }
                // FUNKCJA 4 ZNAKOWA
                else if ((function[j] == 's' && function[j + 1] == 'q' && function[j + 2] == 'r' && function[j + 3] == 't') || (function[j] == 'a' && ((function[j + 1] == 's' && function[j + 2] == 'i' && function[j + 3] == 'n') || (function[j + 1] == 'c' && function[j + 2] == 'o' && function[j + 3] == 's') || (function[j + 1] == 'c' && function[j + 2] == 't' && function[j + 3] == 'g'))) || (function[j] == 's' && function[j + 1] == 'j' && function[j + 2] == 'n' && function[j + 3] == 'h') || (function[j] == 'c' && function[j + 1] == 'o' && function[j + 2] == 's' && function[j + 3] == 'h') || (function[j] == 'c' && function[j + 1] == 't' && function[j + 2] == 'g' && function[j + 3] == 'h') || (function[j] == 't' && function[j + 1] == 'a' && function[j + 2] == 'n' && function[j + 3] == 'h') || (function[j] == 'c' && function[j + 1] == 't' && function[j + 2] == 'n' && function[j + 3] == 'h') || (function[j] == 'c' && function[j + 1] == 'o' && function[j + 2] == 't' && function[j + 3] == 'h'))
                {
                    string funkcjaPodstawowa;
                    funkcjaPodstawowa = (char.ToString(function[j]) + char.ToString(function[j + 1]) + char.ToString(function[j + 2]) + char.ToString(function[j + 3]));
                    j += 4;
                    int n = -1;

                    do
                    {
                        if ((j > function.Length - 1) || (n == -1 && operatory.Contains(function[j]))) break; // obsluga np cos30 zamiast cos(30)
                        if (function[j] == '(')
                            if (n != -1) n++;
                            else n = 1;
                        else if (function[j] == ')')
                            if (n == -1)  //Obsluga przypadku '...cos30)'
                                break;
                            else
                                n--;

                        funkcjaPodstawowa += function[j++];
                    }
                    while (n != 0);

                    zwrot = funkcjaPodstawowa;
                }
                // FUNKCJA 3 ZNAKOWA
                else if ((function[j] == 's' && function[j + 1] == 'i' && function[j + 2] == 'n') || (function[j] == 'c' && function[j + 1] == 'o' && function[j + 2] == 's') || (function[j] == 'c' && function[j + 1] == 't' && function[j + 2] == 'g') || (function[j] == 'e' && function[j + 1] == 'x' && function[j + 2] == 'p') || (function[j] == 'a' && function[j + 1] == 't' && function[j + 2] == 'g') || (function[j] == 'l' && function[j + 1] == 'o' && function[j + 2] == 'g') || (function[j] == 't' && function[j + 1] == 'g' && function[j + 2] == 'h') || (function[j] == 't' && function[j + 1] == 'a' && function[j + 2] == 'n') || (function[j] == 'c' && function[j + 1] == 't' && function[j + 2] == 'n') || (function[j] == 'c' && function[j + 1] == 'o' && function[j + 2] == 't') || (function[j] == 's' && function[j + 1] == 'e' && function[j + 2] == 'c') || (function[j] == 'c' && function[j + 1] == 's' && function[j + 2] == 'c'))
                {
                    string funkcjaPodstawowa;
                    funkcjaPodstawowa = (char.ToString(function[j]) + char.ToString(function[j + 1]) + char.ToString(function[j + 2]));
                    j += 3;
                    int n = -1;

                    do
                    {
                        if ((j > function.Length - 1) || (n == -1 && operatory.Contains(function[j])))
                            break; // obsluga np cos30 zamiast cos(30)
                        if (function[j] == '(')
                            if (n != -1)
                                n++;
                            else
                                n = 1;
                        else if (function[j] == ')')
                            if (n == -1)  //Obsluga przypadku '...cos30)'
                                break;
                            else
                                n--;

                        funkcjaPodstawowa += function[j++];
                    }
                    while (n != 0);

                    zwrot = funkcjaPodstawowa;
                }
                // FUNKCJA 2 LITEROWA
                else if ((function[j] == 't' && function[j + 1] == 'g') || (function[j] == 'l' && function[j + 1] == 'g') || (function[j] == 'l' && function[j + 1] == 'n'))
                {
                    string funkcjaPodstawowa;
                    funkcjaPodstawowa = (char.ToString(function[j]) + char.ToString(function[j + 1]));
                    j += 2;
                    int n = -1;

                    do
                    {
                        if ((j > function.Length - 1) || (n == -1 && operatory.Contains(function[j]))) break; // obsluga np cos30 zamiast cos(30)
                        if (function[j] == '(')
                            if (n != -1) n++;
                            else n = 1;
                        else if (function[j] == ')')
                            if (n == -1)  //Obsluga przypadku '...cos30)'
                                break;
                            else
                                n--;

                        funkcjaPodstawowa += function[j++];
                    }
                    while (n != 0);

                    zwrot = funkcjaPodstawowa;
                }
                else // JAK OPERATOR
                {
                    zwrot = char.ToString(function[j]);
                    j++;
                }

                functionTable[i] = zwrot;
            }
        }

        protected void ConvertToONP() // Konwertuje funkcja na ONP
        {
            // SPRAWDZENIE NAWIASÓW - jakos glupio ten kod napisalem
            int l, n, nn; // nn - zlicza ilosc nawiasow i ile miejsc w tablicy bedzie potrzebne na "dodatkowe" zera w przypadku np. (-3)

            n = nn = 0;

            if (functionTable[0] == "-") // obsługa pierwszej ujemnej liczby bez nawiasu np. "-3"
                nn--;

            //Zliczanie nawiasow (exp3*sin3-exp3*cos3)/2+(exp(-3)*(sin3+cos3))/2
            for (l = 0; l < functionTable.Length; l++)
            {
                string i = functionTable[l];
                switch (i)
                {
                    case "(": n++; if (functionTable[l + 1] != "-") nn++; break;
                    case ")": n--; nn++; break;
                }
            }

            //Ilosc lewych i prawych nawiasow nie zgadza sie
            if (n != 0)
                throw new LeftAndRightBracketsAmountDoesNotMatchException();//FunctionException("Ilość lewych i prawych nawiasów nie zgadza się!");

            // PRZYGOTOWANIE ZMIENNYCH
            functionONP = new string[functionTable.Length - nn];
            stack.Clear();

            // ZAMIANA NA ONP
            int j, k = 0;

            for (j = 0; j < functionTable.Length; j++)
            {
                string i = functionTable[j];

                if (CzyLiczba(i) || i == "," || i == "x" || i == "u" || i == "y") // JAK CYFRA
                    functionONP[k++] = i;
                // JAK FUNKCJA
                else if (i.StartsWith("sin") || i.StartsWith("cos") || i.StartsWith("tg") || i.StartsWith("ctg") || i.StartsWith("asin") || i.StartsWith("acos") || i.StartsWith("atg") || i.StartsWith("actg") || i.StartsWith("exp") || i.StartsWith("sqrt") || i.StartsWith("ln") || i.StartsWith("lg") || i.StartsWith("log") || i.StartsWith("tgh") || i.StartsWith("tan") || i.StartsWith("ctn") || i.StartsWith("cot") || i.StartsWith("sec") || i.StartsWith("csc") || i.StartsWith("sinh") || i.StartsWith("cosh") || i.StartsWith("ctgh") || i.StartsWith("tanh") || i.StartsWith("ctnh") || i.StartsWith("coth"))
                    functionONP[k++] = i;
                else // JAK OPERATOR
                {
                    switch (i) // JAKI OPERATOR
                    {
                        case "-":

                            if (j == 0 || functionTable[j - 1] == "(") // OBSŁUGA np. (-3)
                                functionONP[k++] = "0";

                            if (stack.Value() == "(" || stack.Empty)
                                stack.Add(i);
                            else
                            {
                                while (stack.Value() == "+" || stack.Value() == "*" || stack.Value() == "/" || stack.Value() == "^" || stack.Value() == "-" || stack.Value() == "!")
                                    functionONP[k++] = stack.Pull();

                                stack.Add(i);
                            }
                            break;

                        case "+":

                            if (stack.Value() == "(" || stack.Empty)
                                stack.Add(i);
                            else
                            {
                                while (stack.Value() == "-" || stack.Value() == "*" || stack.Value() == "/" || stack.Value() == "^" || stack.Value() == "+" || stack.Value() == "!")
                                    functionONP[k++] = stack.Pull();

                                stack.Add(i);
                            }
                            break;

                        case "*":

                            if (stack.Value() == "(" || stack.Value() == "+" || stack.Value() == "-" || stack.Empty == true)
                                stack.Add(i);
                            else
                            {
                                while (stack.Value() == "/" || stack.Value() == "^" || stack.Value() == "*" || stack.Value() == "!")
                                    functionONP[k++] = stack.Pull();

                                stack.Add(i);
                            }
                            break;

                        case "/":

                            if (stack.Value() == "(" || stack.Value() == "+" || stack.Value() == "-" || stack.Empty)
                                stack.Add(i);
                            else
                            {
                                while (stack.Value() == "*" || stack.Value() == "^" || stack.Value() == "/" || stack.Value() == "!")
                                    functionONP[k++] = stack.Pull();

                                stack.Add(i);
                            }
                            break;

                        case "^":

                            if (stack.Value() == "(" || stack.Value() == "+" || stack.Value() == "-" || stack.Value() == "*" || stack.Value() == "/" || stack.Value() == "!" || stack.Empty)
                                stack.Add(i);
                            else
                                stack.Add(i);

                            break;

                        case "!":

                            if (stack.Value() == "(" || stack.Value() == "+" || stack.Value() == "-" || stack.Value() == "*" || stack.Value() == "/" || stack.Value() == "^" || stack.Empty)
                                stack.Add(i);
                            else
                                stack.Add(i);

                            break;

                        case "(":

                            stack.Add(i);
                            break;

                        case ")":

                            while (stack.Value() != "(")
                                functionONP[k++] = stack.Pull();

                            stack.Pull(); // ZDEJMUJE ZE STOSU TEZ NAWIAS
                            break;
                    }
                }
            }

            while (!stack.Empty)  // ZDJECIE ZE STOSU POZOSTALOSCI
                functionONP[k++] = stack.Pull();
        }

        // KONSTRUKTOR ----------------------------
        public Function(string function) // KONSTRUKTOR
        {
            this.function = function;
        }
    }

    public class IncorrectEOperatorOccurrenceException : Exception
    { }

    public class OperatorAtTheBeginningOfTheExpressionException : Exception
    {
        public char Operator { get; set; }

        public OperatorAtTheBeginningOfTheExpressionException(char op)
        {
            Operator = op;
        }
    }

    public class OperatorAtTheEndOfTheExpressionException : Exception
    {
        public char Operator { get; set; }

        public OperatorAtTheEndOfTheExpressionException(char op)
        {
            Operator = op;
        }
    }

    public class TwoOperatorsOccurredSideBySideException : Exception
    { }

    public class TwoFactorialsOccuredSideBySideException : Exception
    { }

    public class ForbiddenSignDetectedException : Exception
    {
        public char Sign { get; set; }

        public ForbiddenSignDetectedException(char sign)
        {
            Sign = sign;
        }
    }

    public class EmptyFunctionStringException : Exception
    { }

    public class LeftAndRightBracketsAmountDoesNotMatchException : Exception
    { }
}