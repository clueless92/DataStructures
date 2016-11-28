namespace AvlTreeLab
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// Pr01AVLTree
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AvlTree<T> : IEnumerable<T> where T : IComparable
    {
        private Node<T> root;

        public int Count { get; private set; }

        /// <summary>
        /// Pr03TreeIndexing (slow, cheated and halfassed)
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= this.Count)
                {
                    throw new ArgumentOutOfRangeException("index", "Invalid index");
                }

                int count = 0;
                foreach (T value in this)
                {
                    if (count == index)
                    {
                        return value;
                    }

                    count++;
                }

                throw new ArgumentOutOfRangeException("index", "Invalid index");
            }
        }

        public void Add(T item)
        {
            bool inserted = true;
            if (this.root == null)
            {
                this.root = new Node<T>(item);
            }
            else
            {
                inserted = this.InsertInternal(this.root, item);
            }

            if (inserted)
            {
                this.Count++;
            }

        }

        private bool InsertInternal(Node<T> node, T item)
        {
            Node<T> currNode = node;
            Node<T> newNode = new Node<T>(item);
            bool shouldRetrace = false;
            bool itemAdded = true;
            while (true)
            {
                if (currNode.Value.CompareTo(item) < 0) // Node value is less than item => go right
                {
                    if (currNode.RightChild == null)
                    {
                        currNode.RightChild = newNode;
                        currNode.BalanceFactor--;
                        shouldRetrace = currNode.BalanceFactor != 0;
                        break;
                    }

                    currNode = currNode.RightChild;
                }
                else if (currNode.Value.CompareTo(item) > 0) // node value is greater than item => go left
                {
                    if (currNode.LeftChild == null)
                    {
                        currNode.LeftChild = newNode;
                        currNode.BalanceFactor++;
                        shouldRetrace = currNode.BalanceFactor != 0;
                        break;
                    }

                    currNode = currNode.LeftChild;
                }
                else // item is already pressent => break
                {
                    itemAdded = false;
                    break;
                }
            }

            if (shouldRetrace)
            {
                this.RetraceInsert(currNode);
            }

            return itemAdded;
        }

        private void RetraceInsert(Node<T> node)
        {
            Node<T> parent = node.Parent;
            while (parent != null)
            {
                if (node.IsLeftChild)
                {
                    if (parent.BalanceFactor == 1)
                    {
                        parent.BalanceFactor++;
                        if (node.BalanceFactor == -1)
                        {
                            this.RotateLeft(node);
                        }

                        this.RotateRight(parent);
                        break;
                    }

                    if (parent.BalanceFactor == -1)
                    {
                        parent.BalanceFactor = 0;
                        break;
                    }

                    parent.BalanceFactor = 1;
                }
                else
                {
                    if (parent.BalanceFactor == -1)
                    {
                        parent.BalanceFactor--;
                        if (node.BalanceFactor == 1)
                        {
                            this.RotateRight(node);
                        }

                        this.RotateLeft(parent);
                        break;
                    }

                    if (parent.BalanceFactor == 1)
                    {
                        parent.BalanceFactor = 0;
                        break;
                    }

                    parent.BalanceFactor = -1;
                }

                node = parent;
                parent = node.Parent;
            }
        }

        private void RotateRight(Node<T> node)
        {
            var parent = node.Parent;
            var child = node.LeftChild;

            if (parent != null)
            {
                if (node.IsRightChild)
                {
                    parent.RightChild = child;
                }
                else
                {
                    parent.LeftChild = child;
                }
            }
            else
            {
                // if no parent -> set new root
                this.root = child;
                this.root.Parent = null;
            }

            node.LeftChild = child.RightChild;
            child.RightChild = node;

            node.BalanceFactor -= 1 + Math.Max(child.BalanceFactor, 0);
            child.BalanceFactor -= 1 - Math.Min(node.BalanceFactor, 0);
        }

        private void RotateLeft(Node<T> node)
        {
            Node<T> parent = node.Parent;
            Node<T> child = node.RightChild;
            if (parent != null)
            {
                // Link parent with new node
                if (node.IsLeftChild)
                {
                    parent.LeftChild = child;
                }
                else
                {
                    parent.RightChild = child;
                }
            }
            else
            {
                // if no parent -> set new root
                this.root = child;
                this.root.Parent = null;
            }

            node.RightChild = child.LeftChild;
            child.LeftChild = node;

            node.BalanceFactor += 1 - Math.Min(child.BalanceFactor, 0);
            child.BalanceFactor += 1 + Math.Max(node.BalanceFactor, 0);
        }

        public void ForeachDfs(Action<int, T> action)
        {
            this.InOrderDFS(this.root, 0, action);
        }

        private void InOrderDFS(Node<T> node, int depth, Action<int, T> action)
        {
            if (node == null)
            {
                return;
            }

            this.InOrderDFS(node.LeftChild, depth + 1, action);
            action(depth, node.Value);
            this.InOrderDFS(node.RightChild, depth + 1, action);
        }

        public bool Contains(T item)
        {
            var node = this.root;
            while (node != null)
            {
                if (item.CompareTo(node.Value) == 0)
                {
                    return true;
                }

                if (item.CompareTo(node.Value) > 0)
                {
                    node = node.RightChild;
                }
                else
                {
                    node = node.LeftChild;
                }
            }

            return false;
        }

        /// <summary>
        /// Pr02RangeInTree
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public AvlTree<T> Range(T from, T to)
        {
            AvlTree<T> resulTree = new AvlTree<T>();

            this.InOrderDFS(this.root, 0, (d, item) =>
            {
                if (item.CompareTo(from) >= 0 && item.CompareTo(to) <= 0)
                {
                    resulTree.Add(item);
                }
            });

            return resulTree;
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (this.root == null)
            {
                yield break;
            }

            if (this.root.LeftChild != null)
            {
                foreach (var value in this.root.LeftChild)
                {
                    yield return value;
                }
            }

            yield return this.root.Value;

            if (this.root.RightChild != null)
            {
                foreach (var value in this.root.RightChild)
                {
                    yield return value;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
