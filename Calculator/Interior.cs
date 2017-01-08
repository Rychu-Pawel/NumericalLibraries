using System;

namespace Rychusoft.NumericalLibraries.Calculator
{
    public class Interior : Function
    {
        // ZMIENNE ------------------------------
        protected double x, u, y;

        // METODY -------------------------------
        protected double EvaluateInterior()
        {
            stack.Clear();
            
            int j;

            for (j = 0; j < functionONP.Length; j++)
            {
                string i = functionONP[j];
                double a, b, resultAB = 0;

                if (IsNumber(i) || i == "x" || i == "u" || i == "y") // Digit
                    stack.Add(i);
                // Four sign function
                else if (i.StartsWith("actg") || i.StartsWith("asin") || i.StartsWith("acos") || i.StartsWith("sqrt") || i.StartsWith("sinh") || i.StartsWith("cosh") || i.StartsWith("ctgh") || i.StartsWith("tanh") || i.StartsWith("ctnh") || i.StartsWith("coth"))
                {
                    // compute interior
                    string sInterior = string.Empty;
                    int k = 4, n = -1;

                    do
                    {
                        if ((k > i.Length - 1) || (n == -1 && (i[k] == '+' || i[k] == '-' || i[k] == '*' || i[k] == '/' || i[k] == '^'))) break;
                        if (i[k] == '(')
                            if (n != -1) n++;
                            else n = 1;
                        else if (i[k] == ')') n--;
                        sInterior += i[k++];
                    }
                    while (n != 0);

                    Interior interior = new Interior(sInterior, x, y, u);
                    double interiorValue = interior.EvaluateInterior();

                    // compute function
                    double basicFunction = 0;

                    if (i.StartsWith("actg")) basicFunction = 1 / Math.Atan(interiorValue);
                    else if (i.StartsWith("acos")) basicFunction = Math.Acos(interiorValue);
                    else if (i.StartsWith("sqrt")) basicFunction = Math.Sqrt(interiorValue);
                    else if (i.StartsWith("asin")) basicFunction = Math.Asin(interiorValue);
                    else if (i.StartsWith("sinh")) basicFunction = Math.Sinh(interiorValue);
                    else if (i.StartsWith("cosh")) basicFunction = Math.Cosh(interiorValue);
                    else if (i.StartsWith("ctgh") || i.StartsWith("ctnh") || i.StartsWith("coth")) basicFunction = 1 / Math.Tanh(interiorValue);
                    else if (i.StartsWith("tanh")) basicFunction = Math.Tanh(interiorValue);

                    stack.Add(Convert.ToString(basicFunction));
                }
                // three sign function
                else if (i.StartsWith("sin") || i.StartsWith("cos") || i.StartsWith("ctg") || i.StartsWith("atg") || i.StartsWith("exp") || i.StartsWith("log") || i.StartsWith("tgh") || i.StartsWith("tan") || i.StartsWith("ctn") || i.StartsWith("cot") || i.StartsWith("sec") || i.StartsWith("csc"))
                {
                    // compute interior
                    string sInterior = string.Empty;
                    int k = 3, n = -1;

                    do
                    {
                        if ((k > i.Length - 1) || (n == -1 && operators.Contains(i[k]))) break;
                        if (i[k] == '(')
                            if (n != -1) n++;
                            else n = 1;
                        else if (i[k] == ')') n--;
                        sInterior += i[k++];
                    }
                    while (n != 0);

                    Interior interior = new Interior(sInterior, x, y, u);
                    double interiorValue = interior.EvaluateInterior();

                    // obliczenie funkcji
                    double basicFunction = 0;

                    if (i.StartsWith("sin")) basicFunction = Math.Sin(interiorValue);
                    else if (i.StartsWith("cos")) basicFunction = Math.Cos(interiorValue);
                    else if (i.StartsWith("ctg") || i.StartsWith("ctn") || i.StartsWith("cot")) basicFunction = 1 / Math.Tan(interiorValue);
                    else if (i.StartsWith("atg")) basicFunction = Math.Atan(interiorValue);
                    else if (i.StartsWith("exp")) basicFunction = Math.Exp(interiorValue);
                    else if (i.StartsWith("log")) basicFunction = Math.Log10(interiorValue);
                    else if (i.StartsWith("tgh")) basicFunction = Math.Tanh(interiorValue);
                    else if (i.StartsWith("tan")) basicFunction = Math.Tan(interiorValue);
                    else if (i.StartsWith("sec")) basicFunction = 1 / Math.Cos(interiorValue);
                    else if (i.StartsWith("csc")) basicFunction = 1 / Math.Sin(interiorValue);

                    stack.Add(Convert.ToString(basicFunction));
                }
                else if (i.StartsWith("tg") || i.StartsWith("ln") || i.StartsWith("lg")) // TANGENS, LG, LN
                {
                    // compute interior
                    string sInterior = string.Empty;
                    int k = 2, n = -1;

                    do
                    {
                        if ((k > i.Length - 1) || (n == -1 && operators.Contains(i[k]))) break;
                        if (i[k] == '(')
                            if (n != -1) n++;
                            else n = 1;
                        else if (i[k] == ')') n--;
                        sInterior += i[k++];
                    }
                    while (n != 0);

                    Interior interior = new Interior(sInterior, x, y, u);
                    double interiorValue = interior.EvaluateInterior();

                    // compute function
                    double basicFunction = 0;

                    if (i.StartsWith("tg")) basicFunction = Math.Tan(interiorValue);
                    else if (i.StartsWith("lg")) basicFunction = Math.Log(interiorValue, 2);
                    else if (i.StartsWith("ln")) basicFunction = Math.Log(interiorValue, Math.E);

                    stack.Add(Convert.ToString(basicFunction));
                }
                else if (operatorWithoutFactorialString.Contains(i)) // OPERATOR
                {
                    string value = stack.Value();

                    //a
                    if (value != "x" && value != "u" && value != "y")
                        a = Convert.ToDouble(stack.Pull());
                    else
                    {
                        stack.Pull();

                        switch (value)
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
                    value = stack.Value();

                    if (value != "x" && value != "u" && value != "y")
                        b = Convert.ToDouble(stack.Pull());
                    else
                    {
                        stack.Pull();

                        switch (value)
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
                        case "+": resultAB = b + a; break;
                        case "-": resultAB = b - a; break;
                        case "*": resultAB = b * a; break;
                        case "/": resultAB = b / a; break;
                        case "^": resultAB = Math.Pow(b, a); break;
                    }

                    stack.Add(Convert.ToString(resultAB));
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

                    double result = Factorial.Compute(c);

                    stack.Add(Convert.ToString(result));
                }
            }

            string result2 = stack.Value();
            if (result2 == "x" || result2 == "u" || result2 == "y")
            {
                stack.Pull();

                switch (result2)
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

        /// <summary>
        /// Compute given formula
        /// </summary>
        /// <returns></returns>
        protected virtual double ComputeInterior()
        {
            double result = EvaluateInterior();

            //Format the output (4,0000000000001 is 4)
            if (Math.Abs(result - Math.Floor(result)) < 0.000000001)
                result = Math.Floor(result);
            else if (Math.Abs(result - Math.Ceiling(result)) < 0.000000001)
                result = Math.Ceiling(result);

            return result;
        }
        
        /// <summary>
        /// Interior constructor
        /// </summary>
        /// <param name="formula">Formula</param>
        /// <param name="x">Variable value</param>
        /// <param name="y">First order differential starting value. Used only for computing differentials.</param>
        /// <param name="u">Second order differential starting value. Used only for computing second order differential.</param>
        public Interior(string formula, double x, double y = 0, double u = 0)
            : base(formula)
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
