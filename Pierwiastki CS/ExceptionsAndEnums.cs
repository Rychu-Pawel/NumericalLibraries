using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pierwiastki_CS
{
    class BrakFunkcjiException : Exception
    { }

    class PunktConversionException : Exception
    { }

    class FromConversionException : Exception
    { }

    class ToConversionException : Exception
    { }

    class FromIIConversionException : Exception
    { }

    class ToIIConversionException : Exception
    { }

    class xOdException : Exception
    { }

    class xDoException : Exception
    { }

    class yOdException : Exception
    { }

    class yDoException : Exception
    { }

    class BesselePierwszyArgumentException : Exception
    {
        public BesselePierwszyArgumentException()
            : base("Niepoprawny pierwszy argument!")
        { }

        public BesselePierwszyArgumentException(string msg)
            : base(msg)
        { }
    }

    class BesseleDrugiArgumentException : Exception
    {
        public BesseleDrugiArgumentException()
            : base("Niepoprawny drugi argument!")
        { }

        public BesseleDrugiArgumentException(string msg)
            : base(msg)
        { }
    }

    class BesseleTrzeciArgumentException : Exception
    {
        public BesseleTrzeciArgumentException()
            : base("Niepoprawny trzeci argument!")
        { }

        public BesseleTrzeciArgumentException(string msg)
            : base(msg)
        { }
    }

    class BesseleCzwartyArgumentException : Exception
    {
        public BesseleCzwartyArgumentException()
            : base("Niepoprawny czwarty argument!")
        { }

        public BesseleCzwartyArgumentException(string msg)
            : base(msg)
        { }
    }

    class WystepujeZmiennaException : Exception
    { }

    class XOdWiekszeNizXDoException : Exception
    { }

    class YOdWiekszeNizYDoException : Exception
    { }

    class FunkcjaException : Exception
    {
        public FunkcjaException(string msg)
            : base(msg)
        { }
    }

    class WspolrzedneXException : Exception
    {
        public WspolrzedneXException(string msg)
            : base(msg)
        { }
    }

    class WspolrzedneYException : Exception
    {
        public WspolrzedneYException(string msg)
            : base(msg)
        { }
    }

    class NoneWykresOptionCheckedException : Exception
    {
        public NoneWykresOptionCheckedException()
            : base("Nie wybrano żadnej opcji wykresu!") { }
    }

    enum TypFunkcji
    {
        Funkcja,
        Pochodna,
        DrugaPochodna,
        Rozniczka,
        RozniczkaII,
        Bessel,
        BesselSferyczny,
        BesselPochodnaSferycznej,
        Neumann,
        NeumannSferyczny,
        NeumannPochodnaSferycznej,
        Hipergeometryczna01,
        Hipergeometryczna11,
        Hipergeometryczna21,
    }

    enum TypFunkcjiBessela
    {
        Bessel,
        BesselSferyczny,
        BesselPochodnaSferycznej,
        Neumann,
        NeumannSferyczny,
        NeumannPochodnaSferycznej,
        Hipergeometryczna01,
        Hipergeometryczna11,
        Hipergeometryczna21,
    }
}
