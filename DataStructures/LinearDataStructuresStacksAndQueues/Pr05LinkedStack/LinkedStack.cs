namespace Pr05LinkedStack
{
    using System;

    public class LinkedStack<T>
    {
        private class Node<T>
        {
            public Node(T value, Node<T> nextNode = null)
            {
                this.Value = value;
                this.NextNode = nextNode;
            }

            public T Value { get; private set; }

            public Node<T> NextNode { get; private set; }
        }

        private Node<T> firstNode;

        public int Count { get; private set; }

        public void Push(T element)
        {
            this.firstNode = new Node<T>(element, this.firstNode);
            this.Count++;
        }

        public T Pop()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException("Stack is empty");
            }

            T element = this.firstNode.Value;
            this.firstNode = this.firstNode.NextNode;
            this.Count--;
            return element;
        }

        public T[] ToArray()
        {
            T[] arr = new T[this.Count];
            Node<T> currNode = this.firstNode;
            for (int i = this.Count - 1; i >= 0; i--)
            {
                arr[i] = currNode.Value;
                currNode = currNode.NextNode;
            }

            return arr;
        }
    }
}
