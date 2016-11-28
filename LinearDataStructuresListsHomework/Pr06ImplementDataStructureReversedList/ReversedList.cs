namespace Pr06ImplementDataStructureReversedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class ReversedList<T> : IEnumerable<T>
    {
        private const int DeffaultCapacity = 16;

        private T[] arr;

        public ReversedList(int capacity = DeffaultCapacity)
        {
            this.arr = new T[capacity];
        }

        public int Count { get; private set; }

        public int Capacity
        {
            get { return this.arr.Length; }
        }

        public void Add(T item)
        {
            if (this.Count >= this.arr.Length)
            {
                this.Regrow();
            }

            this.arr[this.Count] = item;
            this.Count++;
        }

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= this.Count)
                {
                    throw new IndexOutOfRangeException();
                }

                int realIndex = (index - this.Count + 1) * -1;
                return this.arr[realIndex];
            }

            set
            {
                if (index < 0 || index >= this.Count)
                {
                    throw new IndexOutOfRangeException();
                }

                int realIndex = (index - this.Count + 1) * -1;
                this.arr[realIndex] = value;
            }
        }

        public void Remove(int index)
        {
            if (index < 0 || index >= this.Count)
            {
                throw new IndexOutOfRangeException();
            }

            int realIndex = (index - this.Count + 1) * -1;
            T[] newArr = new T[this.arr.Length];
            for (int i = 0; i < realIndex; i++)
            {
                newArr[i] = this.arr[i];
            }

            for (int i = realIndex + 1; i < this.Count; i++, realIndex++)
            {
                newArr[realIndex] = this.arr[i];
            }

            this.arr = newArr;
            this.Count--;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = this.Count - 1; i >= 0; i--)
            {
                yield return this.arr[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private void Regrow()
        {
            T[] newArr = new T[this.arr.Length << 1];
            for (int i = 0; i < this.arr.Length; i++)
            {
                newArr[i] = this.arr[i];
            }

            this.arr = newArr;
        }
    }
}
