using System;
using System.Collections.Generic;

namespace BinaryHeap
{
    public class BinaryHeap<T> where T : IComparable<T>
    {
        private readonly List<T> heap;

        public BinaryHeap()
        {
            this.heap = new List<T>();
        }

        public BinaryHeap(T[] elements)
        {
            this.heap = new List<T>(elements);
            for (int i = this.heap.Count / 2; i >= 0; i--)
            {
                this.HeapifyDown(i);
            }
        }

        public int Count
        {
            get { return this.heap.Count; }
        }

        public T ExtractMax()
        {
            T max = this.heap[0];
            this.heap[0] = this.heap[this.heap.Count - 1];
            this.heap.RemoveAt(this.heap.Count - 1);
            if (this.heap.Count > 0)
            {
                this.HeapifyDown(0);
            }

            return max;
        }

        public T PeekMax()
        {
            T max = this.heap[0];
            return max;
        }

        public void Insert(T node)
        {
            this.heap.Add(node);
            this.HeapifyUp(this.heap.Count - 1);
        }

        private void HeapifyDown(int i)
        {
            int leftIndex = 2 * i + 1;
            int rightIndex = 2 * i + 2;
            int largest = i;
            if (leftIndex < this.heap.Count &&
                this.heap[leftIndex].CompareTo(this.heap[largest]) > 0)
            {
                largest = leftIndex;
            }

            if (rightIndex < this.heap.Count &&
                this.heap[rightIndex].CompareTo(this.heap[largest]) > 0)
            {
                largest = rightIndex;
            }

            if (largest != i)
            {
                T old = this.heap[i];
                this.heap[i] = this.heap[largest];
                this.heap[largest] = old;
                this.HeapifyDown(largest);
            }
        }

        private void HeapifyUp(int i)
        {
            int parent = i - 1 >> 1;
            while (i > 0 && this.heap[i].CompareTo(this.heap[parent]) > 0)
            {
                T old = this.heap[i];
                this.heap[i] = this.heap[parent];
                this.heap[parent] = old;

                i = parent;
                parent = i - 1 >> 1;
            }
        }
    }
}
