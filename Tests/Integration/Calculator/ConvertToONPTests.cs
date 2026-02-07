using Rychusoft.NumericalLibraries.Calculator.Exceptions;

namespace Tests.Integration.Calculator
{
    [TestClass]
    public class ConvertToONPTests
    {
        [DataTestMethod]
        [DataRow("2+3*4", "2|3|4|*|+")]
        [DataRow("(2+3)*4", "2|3|+|4|*")]
        [DataRow("-3+5", "0|3|-|5|+")]
        [DataRow("(-3)+(+2)", "0|3|-|0|2|+|+")]
        [DataRow("sin(2)+sqrt(4)", "sin(2)|sqrt(4)|+")]
        [DataRow("3!+2", "3|!|2|+")]
        [DataRow("x+2*y", "x|2|y|*|+")]
        [DataRow("2^3^2", "2|3|2|^|^")]
        [DataRow("5.17+2.88*(3.11-(0.1+21.87*(35.01-22.4*58.11+7.1*((2.2)-(8.4)+((0.001/22.18)*0.025-8.5)*8.1)))*(-25+88-(84*(25*0.058))))", "5.17|2.88|3.11|0.1|21.87|35.01|22.4|58.11|*|-|7.1|2.2|8.4|-|0.001|22.18|/|0.025|*|8.5|-|8.1|*|+|*|+|*|+|0|25|-|88|+|84|25|0.058|*|*|-|*|-|*|+")]
        [DataRow("sin(sin(5.17)+sinh(2.88))*(cos(3.11)-(cosh(0.1)+tan(21.87)*(sqrt(tanh(35.01*log(3)*10^5)^3)-cot(22.4)*coth(58.11)+sec(7.1)*((csc(2.2))-(asin(0.1))+((acos(0.001)/atan(22.18))*acot(0.025)-exp(8.5))*sqrt(8.1))))*(lg(25)+ln(88)-(log(84)*(7!*0.058))))", "sin(sin(5.17)+sinh(2.88))|cos(3.11)|cosh(0.1)|tan(21.87)|sqrt(tanh(35.01*log(3)*10^5)^3)|cot(22.4)|coth(58.11)|*|-|sec(7.1)|csc(2.2)|asin(0.1)|-|acos(0.001)|atan(22.18)|/|acot(0.025)|*|exp(8.5)|-|sqrt(8.1)|*|+|*|+|*|+|lg(25)|ln(88)|+|log(84)|7|!|0.058|*|*|-|*|-|*")]
        [DataRow("ctnh(COT(0,517)+ctgh(0,288))*(+COS(0,311)-(+COSH(0,1)+tg(0,2187)*(LN(tgh(0,5*LOG(4,5)*10^5)^3)-COT(0,224)*ctnh(+0,324)+SEC(0,45)*((CSC(0,21))-(ASIN(0,1))+(+(ACOS(0,001)/ATAN(0,67))*ACOT(0,025)-EXP(8,5))))))", "ctnh(cot(0,517)+ctgh(0,288))|0|cos(0,311)|+|0|cosh(0,1)|+|tg(0,2187)|ln(tgh(0,5*log(4,5)*10^5)^3)|cot(0,224)|ctnh(+0,324)|*|-|sec(0,45)|csc(0,21)|asin(0,1)|-|0|acos(0,001)|atan(0,67)|/|acot(0,025)|*|+|exp(8,5)|-|+|*|+|*|+|-|*")]
        [DataRow("5E2/(-5E-2)+5E+1", "5|10|2|^|*|0|5|10|0|2|-|^|*|-|/|5|10|0|1|+|^|*|+")]
        public void ConvertToONP_ReturnsExpectedTokens(string formula, string expectedTokens)
        {
            var function = new FunctionTestProxy(formula);

            var result = function.ConvertToOnpWithValidation();
            var expected = expectedTokens.Split('|', StringSplitOptions.RemoveEmptyEntries);

            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ConvertToONP_WhenBracketsDoNotMatch_ThrowsException()
        {
            var function = new FunctionTestProxy("2+(3*4");

            Assert.ThrowsException<LeftAndRightBracketsAmountDoesNotMatchException>(
                () => function.ConvertToOnpWithValidation());
        }
    }
}
