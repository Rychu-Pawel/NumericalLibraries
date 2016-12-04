# NumericalLibraries
Numerical Libraries in .NET C#

Easy to use and free numerical .NET C# library that can compute text math formulas.

You can try them on: http://pawelrychlicki.pl/NumericalCalculator

Or in the sample application: http://pawelrychlicki.pl/Application/Details/6/numerical-calculator-14

Sources for the sample application can be found here: https://github.com/Rychu-Pawel/NumericalCalculator

Functions which library can handle:

    Sine = sin
    Hyperbolic sine = sinh
    Cosine = cos
    Hyperbolic cosine = cosh
    Tangent = tg, tan
    Hyperbolic tangent = tgh, tanh
    Cotangent = ctg, ctn, cot
    Hyperbolic cotangent = ctgh, ctnh, coth
    Secant = sec
    Cosecant = csc
    Arcsine = asin
    Arccosine = acos
    Arctangent = atg
    Arccotangent = actg
    Exponential function with e base = exp
    Square root = sqrt
    Base 2 logarithm = lg
    Base e logarithm = ln
    Base 10 logarithm = log

Factorial of natural numbers less then 20 is computed with standard formula:
5! = 1*2*3*4*5
Factorial of real numbers and natural numbers greater than 19 is gamma function.

You can use sinx instead of sin(x) but it's not recommended - in some cases it can affect the result.
For example sinx+2 is sin(x)+2 not sin(x+2).

In formulas you can use PI and E. PI will be calculated as 3.14159265358979,
and E as 10 raised to the power of what is after E.
For example: E-05 = 10^(-5)

Spaces in formulas are ignored.

Application can't compute imaginary numbers. If during computations
Numerical Calculator encounter imaginary numbers it will return "not a number" result.

You can input nested expressions like sin(cos(30)+tg(sqrt(0,4)))

Library is divided into parts:
* Bessel - computes Bessel's function thanks to my professor's, Pawel Syty, library
* Calculator - the main part of the library which implements the basic functions such as Calculator itself and mean, proportion, factorial
* Chart - part responsible for drawing function chart
* Derivative - computes first and second derivative
* Differential
* FourierTransform - computes FT and inverse the result back
* Hybrid - used for computing function root
* Integral
* Interpolation - Function interpolation and approximation
* LinearEquation

