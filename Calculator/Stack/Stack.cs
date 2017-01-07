namespace Rychusoft.NumericalLibraries.Calculator.Stack
{
    internal class Stack
    {
        StackElement head = null;

        public string Value()
        {
            if (this.Empty)
                return string.Empty;
            else
                return head.value;
        }

        public void Add(string element)
        {
            StackElement newElement = new StackElement();
            newElement.previous = head;
            newElement.value = element;
            head = newElement;
        }

        public string Pull()
        {
            if (!this.Empty)
            {
                string value = head.value;
                head = head.previous;
                return value;
            }

            return "0";
        }

        public bool Empty
        {
            get
            {
                return head == null;
            }
        }

        public void Clear()
        {
            while (!Empty)
                Pull();
        }
    }
}
