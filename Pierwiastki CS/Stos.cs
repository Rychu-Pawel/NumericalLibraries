using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumericalCalculator
{
// ELEMENT STOSU
    class elementStosu
    {
        public elementStosu poprzedni;
        public string wartosc;
    }

// STOS
    class Stos
    {
        elementStosu glowa = null; // GLOWA STOSU

        public string wartosc() // WARTOSC NA SZCZYSCIE STOSU
        {
            if (pusty() == true)
                return string.Empty;
            else
                return glowa.wartosc;
        }

        public void doloz(string element) // DOLOZ ELEMENT DO STOSU
        {
            elementStosu nowy = new elementStosu();
            nowy.poprzedni = glowa;
            nowy.wartosc = element;
            glowa = nowy;
        }

        public string zdejmij() // ZDEJMIJ ELEMENT ZE STOSU - ZWRACA STRING (PUSTY JAK STOS PUSTY)
        {
            if (pusty() == false)
            {
                string wartoscZdjeta;
                wartoscZdjeta = glowa.wartosc;
                glowa = glowa.poprzedni;
                return wartoscZdjeta;
            }
            else
                return "0";
        }

        public bool pusty() // CZY STOS JEST PUSTY - ZWRACA TRUE JAK PUSTY I FALSE JAK POSIADA ELEMENTY
        {
            if (glowa == null)
                return true;
            else
                return false;            
        }

        public void wyczysc() // CZYSZCZENIE STOSU
        {
            while (pusty() == false)
                zdejmij();
        }
    }
}
