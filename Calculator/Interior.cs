using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumericalCalculator
{
    public class Interior : Function
    {
    // ZMIENNE ------------------------------
        protected double x, u, y;

        public double X
        {
            get { return x; }
            set { x = value; }
        }

        public double U
        {
            get { return u; }
            set { u = value; }
        }

        public double Y
        {
            get { return y; }
            set { y = value; }
        }

    // METODY -------------------------------
        protected double EvaluateInterior()
        {
            // PRZYGOTOWANIE ZMIENNYCH
            stack.Clear();

            // LICZENIE
            int j;

            for (j = 0; j < functionONP.Length; j++)
            {
                string i = functionONP[j];
                double a, b, wynikAB = 0;

                if (CzyLiczba(i) || i == "x" || i == "u" || i == "y") // JEŚLI CYFRA
                    stack.Add(i);
                // funkcja 4 LITEROWA
                else if (i.StartsWith("actg") || i.StartsWith("asin") || i.StartsWith("acos") || i.StartsWith("sqrt") || i.StartsWith("sinh") || i.StartsWith("cosh") || i.StartsWith("ctgh") || i.StartsWith("tanh") || i.StartsWith("ctnh") || i.StartsWith("coth"))
                {
                    // obliczanie wnetrza
                    string sWnetrze = String.Empty;
                    int k = 4, n = -1;

                    do
                    {
                        if ((k > i.Length - 1) || (n == -1 && (i[k] == '+' || i[k] == '-' || i[k] == '*' || i[k] == '/' || i[k] == '^'))) break;
                        if (i[k] == '(')
                            if (n != -1) n++;
                            else n = 1;
                        else if (i[k] == ')') n--;
                        sWnetrze += i[k++];
                    }
                    while (n != 0);

                    Interior Wnetrze = new Interior(sWnetrze, x, y, u);
                    double wnetrze = Wnetrze.EvaluateInterior();

                    // obliczenie funkcji
                    double funkcjaPodstawowa = 0;

                    if (i.StartsWith("actg")) funkcjaPodstawowa = 1 / Math.Atan(wnetrze);
                    else if (i.StartsWith("acos")) funkcjaPodstawowa = Math.Acos(wnetrze);
                    else if (i.StartsWith("sqrt")) funkcjaPodstawowa = Math.Sqrt(wnetrze);
                    else if (i.StartsWith("asin")) funkcjaPodstawowa = Math.Asin(wnetrze);
                    else if (i.StartsWith("sinh")) funkcjaPodstawowa = Math.Sinh(wnetrze);
                    else if (i.StartsWith("cosh")) funkcjaPodstawowa = Math.Cosh(wnetrze);
                    else if (i.StartsWith("ctgh") || i.StartsWith("ctnh") || i.StartsWith("coth")) funkcjaPodstawowa = 1 / Math.Tanh(wnetrze);
                    else if (i.StartsWith("tanh")) funkcjaPodstawowa = Math.Tanh(wnetrze);

                    stack.Add(Convert.ToString(funkcjaPodstawowa));
                }
                // funkcja 3 literowa
                else if (i.StartsWith("sin") || i.StartsWith("cos") || i.StartsWith("ctg") || i.StartsWith("atg") || i.StartsWith("exp") || i.StartsWith("log") || i.StartsWith("tgh") || i.StartsWith("tan") || i.StartsWith("ctn") || i.StartsWith("cot") || i.StartsWith("sec") || i.StartsWith("csc"))
                {
                    // obliczanie wnetrza
                    string sWnetrze = String.Empty;
                    int k = 3, n = -1;

                    do
                    {
                        if ((k > i.Length - 1) || (n == -1 && operatory.Contains(i[k]))) break;
                        if (i[k] == '(')
                            if (n != -1) n++;
                            else n = 1;
                        else if (i[k] == ')') n--;
                        sWnetrze += i[k++];
                    }
                    while (n != 0);

                    Interior Wnetrze = new Interior(sWnetrze, x, y, u);
                    double wnetrze = Wnetrze.EvaluateInterior();

                    // obliczenie funkcji
                    double funkcjaPodstawowa = 0;

                    if (i.StartsWith("sin")) funkcjaPodstawowa = Math.Sin(wnetrze);
                    else if (i.StartsWith("cos")) funkcjaPodstawowa = Math.Cos(wnetrze);
                    else if (i.StartsWith("ctg") || i.StartsWith("ctn") || i.StartsWith("cot")) funkcjaPodstawowa = 1 / Math.Tan(wnetrze);
                    else if (i.StartsWith("atg")) funkcjaPodstawowa = Math.Atan(wnetrze);
                    else if (i.StartsWith("exp")) funkcjaPodstawowa = Math.Exp(wnetrze);
                    else if (i.StartsWith("log")) funkcjaPodstawowa = Math.Log10(wnetrze);
                    else if (i.StartsWith("tgh")) funkcjaPodstawowa = Math.Tanh(wnetrze);
                    else if (i.StartsWith("tan")) funkcjaPodstawowa = Math.Tan(wnetrze);
                    else if (i.StartsWith("sec")) funkcjaPodstawowa = 1 / Math.Cos(wnetrze);
                    else if (i.StartsWith("csc")) funkcjaPodstawowa = 1 / Math.Sin(wnetrze);

                    stack.Add(Convert.ToString(funkcjaPodstawowa));
                }
                else if (i.StartsWith("tg") || i.StartsWith("ln") || i.StartsWith("lg")) // TANGENS, LG, LN
                {
                    // obliczanie wnetrza
                    string sWnetrze = String.Empty;
                    int k = 2, n = -1;

                    do
                    {
                        if ((k > i.Length - 1) || (n == -1 && operatory.Contains(i[k]))) break;
                        if (i[k] == '(')
                            if (n != -1) n++;
                            else n = 1;
                        else if (i[k] == ')') n--;
                        sWnetrze += i[k++];
                    }
                    while (n != 0);

                    Interior Wnetrze = new Interior(sWnetrze, x, y, u);
                    double wnetrze = Wnetrze.EvaluateInterior();

                    // obliczenie funkcji
                    double funkcjaPodstawowa = 0;

                    if (i.StartsWith("tg")) funkcjaPodstawowa = Math.Tan(wnetrze);
                    else if (i.StartsWith("lg")) funkcjaPodstawowa = Math.Log(wnetrze, 2);
                    else if (i.StartsWith("ln")) funkcjaPodstawowa = Math.Log(wnetrze, Math.E);

                    stack.Add(Convert.ToString(funkcjaPodstawowa));
                }
                else if (operatoryBezSilniString.Contains(i)) // OPERATOR
                {
                    string wartosc = stack.Value();

                    //a
                    if (wartosc != "x" && wartosc != "u" && wartosc != "y")
                        a = Convert.ToDouble(stack.Pull());
                    else
                    {
                        stack.Pull();

                        switch (wartosc)
                        {
                            case "x":
                                a = x;
                                break;
                            case "u":
                                a = u;
                                break;
                            case "y":
                                a = y;
                                break;
                            default:
                                a = x;
                                break;
                        }                        
                    }

                    //b
                    wartosc = stack.Value();

                    if (wartosc != "x" && wartosc != "u" && wartosc != "y")
                        b = Convert.ToDouble(stack.Pull());
                    else
                    {
                        stack.Pull();

                        switch (wartosc)
                        {
                            case "x":
                                b = x;
                                break;
                            case "u":
                                b = u;
                                break;
                            case "y":
                                b = y;
                                break;
                            default:
                                b = x;
                                break;
                        }  
                    }

                    switch (i) // WYKONANIE DZIAŁAŃ
                    {
                        case "+": wynikAB = b + a; break;
                        case "-": wynikAB = b - a; break;
                        case "*": wynikAB = b * a; break;
                        case "/": wynikAB = b / a; break;
                        case "^": wynikAB = Math.Pow(b, a); break;
                    }

                    stack.Add(Convert.ToString(wynikAB));
                }
                else if (i == "!")
                {
                    string wartoscZeStosu = stack.Pull();

                    double c;

                    if (wartoscZeStosu != "x" && wartoscZeStosu != "u" && wartoscZeStosu != "y")
                        c = Convert.ToDouble(wartoscZeStosu);
                    else
                    {
                        switch (wartoscZeStosu)
                        {
                            case "x":
                                c = x;
                                break;
                            case "u":
                                c = u;
                                break;
                            case "y":
                                c = y;
                                break;
                            default:
                                c = x;
                                break;
                        }  
                    }

                    double wynik = Factorial.Compute(c);

                    stack.Add(Convert.ToString(wynik));
                }
            }

            string wartosc2 = stack.Value();
            if (wartosc2 == "x" || wartosc2 == "u" || wartosc2 == "y")
            {
                stack.Pull();

                switch (wartosc2)
                {
                    case "x":
                        stack.Add(Convert.ToString(x));
                        break;
                    case "u":
                        stack.Add(Convert.ToString(u));
                        break;
                    case "y":
                        stack.Add(Convert.ToString(y));
                        break;
                    default:
                        stack.Add(Convert.ToString(x));
                        break;
                }
            }

            return Convert.ToDouble(stack.Pull()); // WYNIK
        }

        public virtual double ComputeInterior()
        {
            double wynik = EvaluateInterior();

            //Formatowanie wyniku, żeby 4,0000000000001 wypluł jako 4
            if (Math.Abs(wynik - Math.Floor(wynik)) < 0.000000001)
                wynik = Math.Floor(wynik);
            else if (Math.Abs(wynik - Math.Ceiling(wynik)) < 0.000000001)
                wynik = Math.Ceiling(wynik);

            return wynik;
        }

    // KONSTRUKTOR --------------------------
        public Interior(string funkcja, double x, double y = 0, double u = 0) : base(funkcja)
        {
            this.x = x;
            this.y = y;
            this.u = u;

            ErrorCheck();
            ConvertToTable();
            ConvertToONP();
        }
    }
}
