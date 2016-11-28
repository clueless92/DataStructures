namespace Pr07LinkedQueue
{
    using System;

    public class LinkedQueue<T>
    {
        private class QueueNode<T>
        {
            public QueueNode(T value, QueueNode<T> nextNode = null)//, QueueNode<T> prevNode = null)
            {
                this.Value = value;
                this.NextNode = nextNode;
                //this.PrevNode = prevNode;
            }

            public T Value { get; private set; }

            public QueueNode<T> NextNode { get; set; }

            //public QueueNode<T> PrevNode { get; set; } // BUG: PrevNode is useless for a queue
        }

        private QueueNode<T> firstNode;
        private QueueNode<T> lastNode;


        public int Count { get; private set; }

        public void Enqueue(T element)
        {
            QueueNode<T> newNode = new QueueNode<T>(element);
            if (this.Count == 0)
            {
                this.firstNode = newNode;
                this.lastNode = newNode;
            }
            else
            {
                this.lastNode.NextNode = newNode;
                this.lastNode = newNode;
            }
            this.Count++;
        }

        public T Dequeue()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException("Queue is empty");
            }

            T output = this.firstNode.Value;
            this.firstNode = this.firstNode.NextNode;
            this.Count--;
            return output;
        }

        public T[] ToArray()
        {
            T[] arr = new T[this.Count];
            QueueNode<T> node = this.firstNode;
            for (int i = 0; i < this.Count; i++)
            {
                arr[i] = node.Value;
                node = node.NextNode;
            }

            return arr;
        }
    }
}
