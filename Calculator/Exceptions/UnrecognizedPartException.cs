using System;

namespace Rychusoft.NumericalLibraries.Calculator.Exceptions
{
	public class UnrecognizedPartException(string unrecognizedPart) : Exception($"Unrecognized part: {unrecognizedPart}")
	{
        public string UnrecognizedPart { get; set; } = unrecognizedPart;
    }
}