using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumericalLibraries.Calculator
{
// ELEMENT STOSU
    internal class StackElement
    {
        public StackElement previous;
        public string value;
    }

// STOS
    internal class Stack
    {
        StackElement head = null; // GLOWA STOSU

        public string Value() // WARTOSC NA SZCZYSCIE STOSU
        {
            if (this.Empty)
                return string.Empty;
            else
                return head.value;
        }

        public void Add(string element) // DOLOZ ELEMENT DO STOSU
        {
            StackElement newElement = new StackElement();
            newElement.previous = head;
            newElement.value = element;
            head = newElement;
        }

        public string Pull() // ZDEJMIJ ELEMENT ZE STOSU - ZWRACA STRING (PUSTY JAK STOS PUSTY)
        {
            if (!this.Empty)
            {
                string wartoscZdjeta;
                wartoscZdjeta = head.value;
                head = head.previous;
                return wartoscZdjeta;
            }
            else
                return "0";
        }

        public bool Empty // CZY STOS JEST PUSTY - ZWRACA TRUE JAK PUSTY I FALSE JAK POSIADA ELEMENTY
        {
            get
            {
                return head == null;
            }
        }

        public void Clear() // CZYSZCZENIE STOSU
        {
            while (!Empty)
                Pull();
        }
    }
}
