namespace Tests.Integration.Calculator
{
    internal sealed class FunctionTestProxy : Rychusoft.NumericalLibraries.Calculator.Function
    {
        public FunctionTestProxy(string function) : base(function)
        {
        }

        public string[] ConvertToTableWithValidation()
        {
            ErrorCheck();
            ConvertToTable();

            return functionTable;
        }

        public string[] ConvertToOnpWithValidation()
        {
            ErrorCheck();
            ConvertToTable();
            ConvertToONP();

            return functionONP;
        }
    }
}
