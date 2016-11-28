namespace Pr04and05BalancedOrderedSet
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class BalancedOrderedSet<T> : IEnumerable<T> where T : IComparable
    {
        private Node<T> root;

        public int Count { get; private set; }

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
                else // item duplication => do not add
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

        private Node<T> RotateRight(Node<T> node)
        {
            var parent = node.Parent;
            var left = node.LeftChild;

            if (parent != null)
            {
                if (node.IsRightChild)
                {
                    parent.RightChild = left;
                }
                else
                {
                    parent.LeftChild = left;
                }
            }
            else
            {
                // if no parent -> set new root
                this.root = left;
                this.root.Parent = null;
            }

            node.LeftChild = left.RightChild;
            left.RightChild = node;

            node.BalanceFactor -= 1 + Math.Max(left.BalanceFactor, 0);
            left.BalanceFactor -= 1 - Math.Min(node.BalanceFactor, 0);

            return left;
        }

        private Node<T> RotateLeft(Node<T> node)
        {
            Node<T> parent = node.Parent;
            Node<T> right = node.RightChild;
            if (parent != null)
            {
                // Link parent with new node
                if (node.IsLeftChild)
                {
                    parent.LeftChild = right;
                }
                else
                {
                    parent.RightChild = right;
                }
            }
            else
            {
                // if no parent -> set new root
                this.root = right;
                this.root.Parent = null;
            }

            node.RightChild = right.LeftChild;
            right.LeftChild = node;

            node.BalanceFactor += 1 - Math.Min(right.BalanceFactor, 0);
            right.BalanceFactor += 1 + Math.Max(node.BalanceFactor, 0);

            return right;
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

        public bool Remove(T item)
        {
            Node<T> node = this.root;

            while (node != null)
            {
                if (item.CompareTo(node.Value) < 0)
                {
                    node = node.LeftChild;
                }
                else if (item.CompareTo(node.Value) > 0)
                {
                    node = node.RightChild;
                }
                else
                {
                    var left = node.LeftChild;
                    var right = node.RightChild;

                    if (left == null)
                    {
                        if (right == null)
                        {
                            if (node == this.root)
                            {
                                this.root = null;
                            }
                            else
                            {
                                var parent = node.Parent;

                                if (parent.LeftChild == node)
                                {
                                    parent.LeftChild = null;

                                    this.DeleteBalance(parent, -1);
                                }
                                else
                                {
                                    parent.RightChild = null;

                                    this.DeleteBalance(parent, 1);
                                }
                            }
                        }
                        else
                        {
                            Replace(node, right);

                            this.DeleteBalance(node, 0);
                        }
                    }
                    else if (right == null)
                    {
                        Replace(node, left);

                        this.DeleteBalance(node, 0);
                    }
                    else
                    {
                        var successor = right;

                        if (successor.LeftChild == null)
                        {
                            var parent = node.Parent;

                            successor.Parent = parent;
                            successor.LeftChild = left;
                            successor.BalanceFactor = node.BalanceFactor;

                            if (left != null)
                            {
                                left.Parent = successor;
                            }

                            if (node == this.root)
                            {
                                this.root = successor;
                            }
                            else
                            {
                                if (parent.LeftChild == node)
                                {
                                    parent.LeftChild = successor;
                                }
                                else
                                {
                                    parent.RightChild = successor;
                                }
                            }

                            this.DeleteBalance(successor, 1);
                        }
                        else
                        {
                            while (successor.LeftChild != null)
                            {
                                successor = successor.LeftChild;
                            }

                            var parent = node.Parent;
                            var successorParent = successor.Parent;
                            var successorRight = successor.RightChild;

                            if (successorParent.LeftChild == successor)
                            {
                                successorParent.LeftChild = successorRight;
                            }
                            else
                            {
                                successorParent.RightChild = successorRight;
                            }

                            if (successorRight != null)
                            {
                                successorRight.Parent = successorParent;
                            }

                            successor.Parent = parent;
                            successor.LeftChild = left;
                            successor.BalanceFactor = node.BalanceFactor;
                            successor.RightChild = right;
                            right.Parent = successor;

                            if (left != null)
                            {
                                left.Parent = successor;
                            }

                            if (node == this.root)
                            {
                                this.root = successor;
                            }
                            else
                            {
                                if (parent.LeftChild == node)
                                {
                                    parent.LeftChild = successor;
                                }
                                else
                                {
                                    parent.RightChild = successor;
                                }
                            }

                            this.DeleteBalance(successorParent, -1);
                        }
                    }

                    this.Count--;
                    return true;
                }
            }

            return false;
        }

        public void Clear()
        {
            this.Count = 0;
            this.root = null;
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (this.Count == 0)
            {
                throw new NullReferenceException("Set is empty.");
            }

            return this.root.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private void DeleteBalance(Node<T> node, int balance)
        {
            while (node != null)
            {
                balance = node.BalanceFactor += balance;

                if (balance == 2)
                {
                    if (node.LeftChild.BalanceFactor >= 0)
                    {
                        node = this.RotateRight(node);

                        if (node.BalanceFactor == -1)
                        {
                            return;
                        }
                    }
                    else
                    {
                        node = this.RotateLeft(node);
                        node = this.RotateRight(node);
                    }
                }
                else if (balance == -2)
                {
                    if (node.RightChild.BalanceFactor <= 0)
                    {
                        node = this.RotateLeft(node);

                        if (node.BalanceFactor == 1)
                        {
                            return;
                        }
                    }
                    else
                    {
                        node = this.RotateRight(node);
                        node = this.RotateLeft(node);
                    }
                }
                else if (balance != 0)
                {
                    return;
                }

                var parent = node.Parent;

                if (parent != null)
                {
                    balance = parent.LeftChild == node ? -1 : 1;
                }

                node = parent;
            }
        }

        private static void Replace(Node<T> target, Node<T> source)
        {
            var left = source.LeftChild;
            var right = source.RightChild;

            target.BalanceFactor = source.BalanceFactor;
            target.Value = source.Value;
            target.LeftChild = left;
            target.RightChild = right;

            if (left != null)
            {
                left.Parent = target;
            }

            if (right != null)
            {
                right.Parent = target;
            }
        }
    }
}
