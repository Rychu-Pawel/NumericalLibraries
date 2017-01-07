using System;
using System.Collections.Generic;
using NumericalLibraries.Calculator.Exceptions;

namespace NumericalLibraries.Calculator
{
    public abstract class Function
    {
        protected string function;
        protected string[] functionTable;
        protected string[] functionONP;

        internal Stack.Stack stack = new Stack.Stack();

        protected HashSet<char> allowedSignes = new HashSet<char>() { 'E', ' ', ',', '.', 'l', 's', 'i', 'n', 'c', 'o', 't', 'g', 'q', 'r', 'e', 'x', 'p', 'a', '(', ')', '+', '-', '*', '/', '^', '!', 'y', 'h', 'u', 'y', '\'' };
        protected HashSet<char> operators = new HashSet<char>() { '+', '-', '*', '/', '^', '!' };
        protected HashSet<char> operatorsWithoutFactorial = new HashSet<char>() { '+', '-', '*', '/', '^' };
        protected HashSet<char> operatorWithoutFactorialWithDotAndPeriod = new HashSet<char>() { '+', '-', '*', '/', '^', ',', '.' };
        protected HashSet<char> operatorWithoutPlusSign = new HashSet<char>() { '*', '/', '^', '!' };

        protected HashSet<string> operatorWithoutFactorialString = new HashSet<string>() { "+", "-", "*", "/", "^" };
        
        protected bool IsNumber(string mayBeANum)
        {
            double number;
            return double.TryParse(mayBeANum, out number);
        }

        /// <summary>
        /// Checks if formula is correct
        /// </summary>
        protected virtual void ErrorCheck()
        {
            string cache;

            //Change "PI" to value
            function = function.Replace("PI", Math.PI.ToString());

            for (int i = 0; i < function.Length; i++)
            {
                char c = function[i];

                if (c == ' ')  // space trim
                {
                    cache = function.Substring(i + 1, function.Length - (i + 1));
                    function = function.Substring(0, i);
                    function += cache;
                    i--;
                }

                //Change e.g., E-05 na 10^(-5)
                if (c == 'E')
                {
                    try
                    {
                        int bracketsCount = 1, counter = 1; //counter is how many signes after E need to be places in brackets

                        if (function[i + counter] == '+' || function[i + counter] == '-')
                            counter++;

                        if (function[i + counter] == '(')
                        {
                            counter++;

                            while (bracketsCount != 0)
                            {
                                if (function[i + counter] == '(')
                                    bracketsCount++;
                                else if (function[i + counter] == ')')
                                    bracketsCount--;

                                counter++;
                            }
                        }

                        while (i + counter < function.Length && (char.IsDigit(function[i + counter]) || function[i + counter] == ',' || function[i + counter] == '.'))
                            counter++;

                        //Change E to *10^(
                        function = function.Substring(0, i) + "*10^(" + function.Substring(i + 1, counter - 1) + ")" + function.Substring(i + counter, function.Length - i - counter);
                    }
                    catch (SystemException)
                    {
                        throw new IncorrectEOperatorOccurrenceException();
                    }
                }

                //There can't be an operator at the beginning (except of + and -) and at the end (except of the factorial)
                if (i == 0 && operatorWithoutPlusSign.Contains(c))
                    throw new OperatorAtTheBeginningOfTheExpressionException(c);

                if (i == function.Length - 1 && operatorsWithoutFactorial.Contains(c))
                    throw new OperatorAtTheEndOfTheExpressionException(c);

                //Check two operators one by one (exception factorial as a first sign)
                if (i != 0)
                    if (operators.Contains(c) && operatorWithoutFactorialWithDotAndPeriod.Contains(function[i - 1]))
                        throw new TwoOperatorsOccurredSideBySideException();

                //Check two factorials one by one
                if (i != 0)
                    if (c == '!' && function[i - 1] == '!')
                        throw new TwoFactorialsOccuredSideBySideException();

                //Check sign is allowed
                if (!(allowedSignes.Contains(c) || char.IsDigit(c)))
                    throw new ForbiddenSignDetectedException(c);
            }

            if (function == string.Empty)
                throw new EmptyFunctionStringException();
        }

        protected void ConvertToTable()// CONVERTS string function to table
        {
            int i, cache = 0; // CACHE - how many function.Length to cut to declare good table length

            for (i = 0; i < function.Length - 1; i++) // count numbers (not digits) and functions
            {
                if ((char.IsDigit(function[i]) || function[i] == ',' || function[i] == '.') && (char.IsDigit(function[i + 1]) || function[i + 1] == ',' || function[i + 1] == '.'))
                    cache++;

                // IF TWO SIGN FUNCTION
                if ((i + 1 <= function.Length - 1) && ((function[i] == 't' && function[i + 1] == 'g') || (function[i] == 'l' && function[i + 1] == 'n') || (function[i] == 'l' && function[i + 1] == 'g')))
                {
                    int n = -1;
                    cache++;
                    i += 2;

                    do
                    {
                        if ((i > function.Length - 1) || (n == -1 && operators.Contains(function[i]))) break;
                        if (function[i] == '(')
                            if (n != -1) n++;
                            else n = 1;
                        else if (function[i] == ')')
                            if (n == -1)  //handling '...cos30)'
                                break;
                            else
                                n--;

                        cache++;
                        i++;
                    }
                    while (n != 0);
                }
                // IF THREE SIGN FUNCTION
                else if ((i + 2 <= function.Length - 1) && ((function[i] == 's' && function[i + 1] == 'i' && function[i + 2] == 'n') || (function[i] == 'c' && function[i + 1] == 'o' && function[i + 2] == 's') || (function[i] == 'c' && function[i + 1] == 't' && function[i + 2] == 'g') || (function[i] == 'e' && function[i + 1] == 'x' && function[i + 2] == 'p') || (function[i] == 'a' && function[i + 1] == 't' && function[i + 2] == 'g') || (function[i] == 'l' && function[i + 1] == 'o' && function[i + 2] == 'g') || (function[i] == 't' && function[i + 1] == 'g' && function[i + 2] == 'h') || (function[i] == 't' && function[i + 1] == 'a' && function[i + 2] == 'n') || (function[i] == 'c' && function[i + 1] == 't' && function[i + 2] == 'n') || (function[i] == 'c' && function[i + 1] == 'o' && function[i + 2] == 't') || (function[i] == 's' && function[i + 1] == 'e' && function[i + 2] == 'c') || (function[i] == 'c' && function[i + 1] == 's' && function[i + 2] == 'c')))
                {
                    int bracketsCount = -1;
                    cache += 2;
                    i += 3; //move loop index

                    do
                    {
                        if ((i > function.Length - 1) || (bracketsCount == -1 && operators.Contains(function[i])))
                            break;
                        if (function[i] == '(')
                            if (bracketsCount != -1)
                                bracketsCount++;
                            else
                                bracketsCount = 1;
                        else if (function[i] == ')')
                            if (bracketsCount == -1) //handling '...cos30)'
                                break;
                            else
                                bracketsCount--;

                        cache++;
                        i++;
                    }
                    while (bracketsCount != 0);
                }
                // IF FOUR SIGN FUNCTION
                else if ((i + 3 <= function.Length - 1) && ((function[i] == 's' && function[i + 1] == 'q' && function[i + 2] == 'r' && function[i + 3] == 't') || (function[i] == 'a' && ((function[i + 1] == 's' && function[i + 2] == 'i' && function[i + 3] == 'n') || (function[i + 1] == 'c' && function[i + 2] == 'o' && function[i + 3] == 's') || (function[i + 1] == 'c' && function[i + 2] == 't' && function[i + 3] == 'g'))) || (function[i] == 's' && function[i + 1] == 'i' && function[i + 2] == 'n' && function[i + 3] == 'h') || (function[i] == 'c' && function[i + 1] == 'o' && function[i + 2] == 's' && function[i + 3] == 'h') || (function[i] == 'c' && function[i + 1] == 't' && function[i + 2] == 'g' && function[i + 3] == 'h') || (function[i] == 't' && function[i + 1] == 'a' && function[i + 2] == 'n' && function[i + 3] == 'h') || (function[i] == 'c' && function[i + 1] == 't' && function[i + 2] == 'n' && function[i + 3] == 'h') || (function[i] == 'c' && function[i + 1] == 'o' && function[i + 2] == 't' && function[i + 3] == 'h')))
                {
                    int n = -1;
                    cache += 3;
                    i += 4;

                    do
                    {
                        if ((i > function.Length - 1) || (n == -1 && operators.Contains(function[i]))) break;
                        if (function[i] == '(')
                            if (n != -1) n++;
                            else n = 1;
                        else if (function[i] == ')')
                            if (n == -1)  //handling '...cos30)'
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

            // CONVERSION
            int j = 0;

            for (i = 0; i < functionTable.Length; i++)
            {
                string result;

                if (char.IsDigit(function[j]) || function[j] == ',' || function[j] == '.' || function[j] == 'x' || function[j] == 'u' || function[j] == 'y') // IF DIGIT
                {
                    result = char.ToString(function[j]);

                    while ((j + 1 < function.Length) && (char.IsDigit(function[j + 1]) || (function[j + 1] == ',') || (function[j + 1] == '.'))) // IF NUMBER
                        result += function[j++ + 1];

                    j++;
                }
                // FOUR SIGN FUNCTION
                else if ((function[j] == 's' && function[j + 1] == 'q' && function[j + 2] == 'r' && function[j + 3] == 't') || (function[j] == 'a' && ((function[j + 1] == 's' && function[j + 2] == 'i' && function[j + 3] == 'n') || (function[j + 1] == 'c' && function[j + 2] == 'o' && function[j + 3] == 's') || (function[j + 1] == 'c' && function[j + 2] == 't' && function[j + 3] == 'g'))) || (function[j] == 's' && function[j + 1] == 'j' && function[j + 2] == 'n' && function[j + 3] == 'h') || (function[j] == 'c' && function[j + 1] == 'o' && function[j + 2] == 's' && function[j + 3] == 'h') || (function[j] == 'c' && function[j + 1] == 't' && function[j + 2] == 'g' && function[j + 3] == 'h') || (function[j] == 't' && function[j + 1] == 'a' && function[j + 2] == 'n' && function[j + 3] == 'h') || (function[j] == 'c' && function[j + 1] == 't' && function[j + 2] == 'n' && function[j + 3] == 'h') || (function[j] == 'c' && function[j + 1] == 'o' && function[j + 2] == 't' && function[j + 3] == 'h'))
                {
                    string basicFunction;
                    basicFunction = (char.ToString(function[j]) + char.ToString(function[j + 1]) + char.ToString(function[j + 2]) + char.ToString(function[j + 3]));
                    j += 4;
                    int n = -1;

                    do
                    {
                        if ((j > function.Length - 1) || (n == -1 && operators.Contains(function[j]))) break; // handling cos30 instead of cos(30)
                        if (function[j] == '(')
                            if (n != -1) n++;
                            else n = 1;
                        else if (function[j] == ')')
                            if (n == -1)  //handling'...cos30)'
                                break;
                            else
                                n--;

                        basicFunction += function[j++];
                    }
                    while (n != 0);

                    result = basicFunction;
                }
                // THREE SIGN FUNCTION
                else if ((function[j] == 's' && function[j + 1] == 'i' && function[j + 2] == 'n') || (function[j] == 'c' && function[j + 1] == 'o' && function[j + 2] == 's') || (function[j] == 'c' && function[j + 1] == 't' && function[j + 2] == 'g') || (function[j] == 'e' && function[j + 1] == 'x' && function[j + 2] == 'p') || (function[j] == 'a' && function[j + 1] == 't' && function[j + 2] == 'g') || (function[j] == 'l' && function[j + 1] == 'o' && function[j + 2] == 'g') || (function[j] == 't' && function[j + 1] == 'g' && function[j + 2] == 'h') || (function[j] == 't' && function[j + 1] == 'a' && function[j + 2] == 'n') || (function[j] == 'c' && function[j + 1] == 't' && function[j + 2] == 'n') || (function[j] == 'c' && function[j + 1] == 'o' && function[j + 2] == 't') || (function[j] == 's' && function[j + 1] == 'e' && function[j + 2] == 'c') || (function[j] == 'c' && function[j + 1] == 's' && function[j + 2] == 'c'))
                {
                    string basicFunction;
                    basicFunction = (char.ToString(function[j]) + char.ToString(function[j + 1]) + char.ToString(function[j + 2]));
                    j += 3;
                    int n = -1;

                    do
                    {
                        if ((j > function.Length - 1) || (n == -1 && operators.Contains(function[j])))
                            break; // handling cos30 instead of cos(30)
                        if (function[j] == '(')
                            if (n != -1)
                                n++;
                            else
                                n = 1;
                        else if (function[j] == ')')
                            if (n == -1)  //handling '...cos30)'
                                break;
                            else
                                n--;

                        basicFunction += function[j++];
                    }
                    while (n != 0);

                    result = basicFunction;
                }
                // TWO SIGN FUNCTION
                else if ((function[j] == 't' && function[j + 1] == 'g') || (function[j] == 'l' && function[j + 1] == 'g') || (function[j] == 'l' && function[j + 1] == 'n'))
                {
                    string basicFunction;
                    basicFunction = (char.ToString(function[j]) + char.ToString(function[j + 1]));
                    j += 2;
                    int n = -1;

                    do
                    {
                        if ((j > function.Length - 1) || (n == -1 && operators.Contains(function[j]))) break; // handling cos30 instead of cos(30)
                        if (function[j] == '(')
                            if (n != -1) n++;
                            else n = 1;
                        else if (function[j] == ')')
                            if (n == -1)  //handling '...cos30)'
                                break;
                            else
                                n--;

                        basicFunction += function[j++];
                    }
                    while (n != 0);

                    result = basicFunction;
                }
                else // operator
                {
                    result = char.ToString(function[j]);
                    j++;
                }

                functionTable[i] = result;
            }
        }

        protected void ConvertToONP() // convert to ONP
        {
            // Check brackets
            int l, n, nn; // nn - counts brackets and how many places in the table will be need for aditional zeros e.g., for (-3)

            n = nn = 0;

            if (functionTable[0] == "-") // handling first number less then zero e.g., "-3"
                nn--;

            //Counting brackets (exp3*sin3-exp3*cos3)/2+(exp(-3)*(sin3+cos3))/2
            for (l = 0; l < functionTable.Length; l++)
            {
                string i = functionTable[l];
                switch (i)
                {
                    case "(": n++; if (functionTable[l + 1] != "-") nn++; break;
                    case ")": n--; nn++; break;
                }
            }

            //Brackets count does not match
            if (n != 0)
                throw new LeftAndRightBracketsAmountDoesNotMatchException();

            // variables
            functionONP = new string[functionTable.Length - nn];
            stack.Clear();

            // change to ONP
            int j, k = 0;

            for (j = 0; j < functionTable.Length; j++)
            {
                string i = functionTable[j];

                if (IsNumber(i) || i == "," || i == "x" || i == "u" || i == "y") // DIGIT
                    functionONP[k++] = i;
                // Function
                else if (i.StartsWith("sin") || i.StartsWith("cos") || i.StartsWith("tg") || i.StartsWith("ctg") || i.StartsWith("asin") || i.StartsWith("acos") || i.StartsWith("atg") || i.StartsWith("actg") || i.StartsWith("exp") || i.StartsWith("sqrt") || i.StartsWith("ln") || i.StartsWith("lg") || i.StartsWith("log") || i.StartsWith("tgh") || i.StartsWith("tan") || i.StartsWith("ctn") || i.StartsWith("cot") || i.StartsWith("sec") || i.StartsWith("csc") || i.StartsWith("sinh") || i.StartsWith("cosh") || i.StartsWith("ctgh") || i.StartsWith("tanh") || i.StartsWith("ctnh") || i.StartsWith("coth"))
                    functionONP[k++] = i;
                else // operator
                {
                    switch (i)
                    {
                        case "-":

                            if (j == 0 || functionTable[j - 1] == "(") // handling e.g., (-3)
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

                            stack.Pull();
                            break;
                    }
                }
            }

            while (!stack.Empty)  // take the rest
                functionONP[k++] = stack.Pull();
        }
        
        public Function(string function)
        {
            this.function = function;
        }
    }
}