# NumericalLibraries
Numerical Libraries in .NET C#

Easy to use and free numerical .NET C# library that can compute text math formulas.

You can try them on: http://pawelrychlicki.pl/NumericalCalculator

Or in the sample application: http://pawelrychlicki.pl/Application/Details/6/numerical-calculator-14

Sources for the sample application can be found here: https://github.com/Rychu-Pawel/NumericalCalculator

Compiled libraries can be downloaded from nuget: https://www.nuget.org/packages?q=rychusoft.numericallibraries

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

Library is divided into parts (follow links for more details and usage info):
* Bessel - computes Bessel's function thanks to my professor's, Pawel Syty, library
* Calculator - http://pawelrychlicki.pl/Application/Details/14/numerical-calculator-net-library-10 - the main part of the library which implements the basic functions such as Calculator itself and mean, proportion, factorial
* Chart - http://pawelrychlicki.pl/Application/Details/47/chart-net-library-10 - part responsible for drawing function chart
* Derivative - http://pawelrychlicki.pl/Application/Details/19/numerical-derivative-net-library-10 - computes first and second derivative
* Differential - http://pawelrychlicki.pl/Application/Details/24/numerical-differential-net-library-10
* FourierTransform - http://pawelrychlicki.pl/Application/Details/28/numerical-fourier-transform-net-library-10 - computes FT and inverse the result back
* Hybrid - http://pawelrychlicki.pl/Application/Details/30/numerical-function-root-hybrid-net-library-10 - used for computing function root (zero)
* Integral - http://pawelrychlicki.pl/Application/Details/32/numerical-integral-net-library-10
* Interpolation - http://pawelrychlicki.pl/Application/Details/37/analytical-interpolation-and-approximation-net-library-10 - Function interpolation and approximation
* LinearEquation - http://pawelrychlicki.pl/Application/Details/42/numerical-linear-equation-net-library-10

These libraries were one of two main parts of my master's thesis. Second one was the sample application mentioned above.
