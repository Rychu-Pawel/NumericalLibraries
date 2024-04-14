namespace Tests.Integration.Calculator
{
    [TestClass()]
    public class ComputeTests
    {
        [TestMethod]
        [DataRow("2+2", 4)]
        [DataRow("  2  +  2  ", 4)]
        [DataRow("22-(+3)", 19)]
        [DataRow("5^3/4+cos(20)*(tgh(10)-exp(3))", 23.461534739887718)]
        [DataRow("  5  ^  3 /  4 + cos  (2 0) * (tg h (10 ) - exp(3)  )", 23.461534739887718)]
        [DataRow("5.17+2.88*(3.11-(0.1+21.87*(35.01-22.4*58.11+7.1*((2.2)-(8.4)+((0.001/22.18)*0.025-8.5)*8.1)))*(-25+88-(84*(25*0.058))))", -6664546.15806812)]
        [DataRow("sin(sin(5.17)+sinh(2.88))*(cos(3.11)-(cosh(0.1)+tan(21.87)*(sqrt(tanh(35.01*log(3)*10^5)^3)-cot(22.4)*coth(58.11)+sec(7.1)*((csc(2.2))-(asin(0.1))+((acos(0.001)/atan(22.18))*acot(0.025)-exp(8.5))*sqrt(8.1))))*(lg(25)+ln(88)-(log(84)*(7!*0.058))))+PI", 1365467.7933224458)]
        [DataRow("ctnh(COT(0,517)+ctgh(0,288))*(+COS(0,311)-(+COSH(0,1)+tg(0,2187)*(LN(tgh(0,5*LOG(4,5)*10^5)^3)-COT(0,224)*ctnh(+0,324)+SEC(0,45)*((CSC(0,21))-(ASIN(0,1))+(+(ACOS(0,001)/ATAN(0,67))*ACOT(0,025)-EXP(8,5))))))", 1214.0445331075816)]
        [DataRow("PI+5E2/(-5E-2)+5E+1", -9946.85840734641)]
        public void Compute_ReturnsCorrectValue(string formula, double expected)
        {
            var calculator = new Rychusoft.NumericalLibraries.Calculator.Calculator(formula);
            var result = calculator.Compute();

            Assert.AreEqual(expected, result);
        }
    }
}
