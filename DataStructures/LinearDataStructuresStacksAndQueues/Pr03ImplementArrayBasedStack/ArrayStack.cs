namespace Pr03ImplementArrayBasedStack
{
    using System;

    public class ArrayStack<T>
    {
        private const int InitialCapacity = 16;

        private T[] elements;

        public ArrayStack(int capacity = InitialCapacity)
        {
            this.elements = new T[capacity];
        }

        public int Count { get; private set; }

        public int Capacity
        {
            get { return this.elements.Length; }
        }

        public void Push(T element)
        {
            if (this.Count == this.elements.Length)
            {
                this.Grow();
            }

            this.elements[this.Count] = element;
            this.Count++;
        }

        public T Pop()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException("Stack is empty");
            }

            this.Count--;
            T element = this.elements[this.Count];
            this.elements[this.Count] = default(T);
            return element;
        }

        public T[] ToArray()
        {
            T[] array = new T[this.Count];
            for (int i = 0; i < this.Count; i++)
            {
                array[i] = this.elements[i];
            }

            return array;
        }

        private void Grow()
        {
            T[] newElements = new T[this.elements.Length * 2];
            for (int i = 0; i < this.Count; i++)
            {
                newElements[i] = this.elements[i];
            }

            this.elements = newElements;
        }
    }
}
