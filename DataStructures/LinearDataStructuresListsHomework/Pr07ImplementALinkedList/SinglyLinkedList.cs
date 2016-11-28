namespace Pr07ImplementALinkedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    public class SinglyLinkedList<T> : IEnumerable<T>
    {
        public class ListNode<T>
        {
            public ListNode(T value)
            {
                this.Value = value;
            }

            public T Value { get; set; }

            public ListNode<T> Next { get; set; } 
        }
        
        private ListNode<T> firstNode;
        private ListNode<T> lastNode;

        public SinglyLinkedList()
        {
        }

        public int Count { get; private set; }

        public void Add(T item)
        {
            ListNode<T> newNode = new ListNode<T>(item);
            if (this.Count == 0)
            {
                this.firstNode = newNode;
                this.lastNode = newNode;
            }
            else if (this.Count == 1)
            {
                this.lastNode = newNode;
                this.firstNode.Next = this.lastNode;
            }
            else
            {
                this.lastNode.Next = newNode;
                this.lastNode = newNode;
            }

            this.Count++;
        }

        public void Remove(int index)
        {
            if (index < 0 || index >= this.Count)
            {
                throw new IndexOutOfRangeException();
            }

            int currIndex = 0;
            ListNode<T> currNode = this.firstNode;
            ListNode<T> prevNode = null;

            if (index == 0)
            {
                this.firstNode = this.firstNode.Next;
                return;
            }

            while (currIndex < index)
            {
                prevNode = currNode;
                currNode = currNode.Next;
                currIndex++;
            }

            // Remove the found element from the list of nodes
            prevNode.Next = currNode.Next;
        }

        public int FirstIndexOf(T item)
        {
            ListNode<T> currNode = this.firstNode;
            int index = 0;
            while (currNode != null)
            {
                if (currNode.Value.Equals(item))
                {
                    return index;
                }

                index++;
                currNode = currNode.Next;
            }

            return -1;
        }

        public int LastIndexOf(T item)
        {
            ListNode<T> currNode = this.firstNode;
            int index = 0;
            int lastIndex = -1;
            while (currNode != null)
            {
                if (currNode.Value.Equals(item))
                {
                    lastIndex = index;
                }

                index++;
                currNode = currNode.Next;
            }

            return lastIndex;
        }

        public IEnumerator<T> GetEnumerator()
        {
            ListNode<T> currNode = this.firstNode;
            while (currNode != null)
            {
                yield return currNode.Value;
                currNode = currNode.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
