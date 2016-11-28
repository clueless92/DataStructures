namespace Pr04and05BalancedOrderedSet
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Node<T> : IEnumerable<T> where T : IComparable
    {
        private Node<T> leftChild;
        private Node<T> rightChild;

        public Node(T value)
        {
            this.Value = value;
        }

        public T Value { get; set; }

        public Node<T> LeftChild
        {
            get
            {
                return this.leftChild;
            }

            set
            {
                if (value != null)
                {
                    value.Parent = this;
                }

                this.leftChild = value;
            }
        }

        public Node<T> RightChild
        {
            get
            {
                return this.rightChild;
            }

            set
            {
                if (value != null)
                {
                    value.Parent = this;
                }

                this.rightChild = value;
            }
        }

        public Node<T> Parent { get; set; }

        public int BalanceFactor { get; set; }

        public bool IsLeftChild
        {
            get { return this.Parent.LeftChild == this; }
        }

        public bool IsRightChild
        {
            get { return this.Parent.RightChild == this; }
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (this.LeftChild != null)
            {
                foreach (var v in this.LeftChild)
                {
                    yield return v;
                }
            }

            yield return this.Value;

            if (this.RightChild != null)
            {
                foreach (var v in this.RightChild)
                {
                    yield return v;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public override string ToString()
        {
            return this.Value.ToString();
        }
    }
}
