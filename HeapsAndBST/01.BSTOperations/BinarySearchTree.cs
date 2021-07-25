namespace _01.BSTOperations
{
    using System;
    using System.Collections.Generic;

    public class BinarySearchTree<T> : IAbstractBinarySearchTree<T>
        where T : IComparable<T>
    {
        public BinarySearchTree()
        {
        }

        public BinarySearchTree(Node<T> root)
        {
            this.Root = root;
        }

        public Node<T> Root { get; private set; }

        public Node<T> LeftChild { get; private set; }

        public Node<T> RightChild { get; private set; }

        public T Value => this.Root.Value;

        private int _count { get; set; } = 0;

        public int Count => this._count;

        public bool Contains(T element)
        {
            return this.FindElementNode(element, this.Root) != null;
        }

        public void Insert(T element)
        {
            if (this.Root == null)
            {
                this.Root = new Node<T>(element, null, null);
            }
            else
            {
                InsertRec(element, this.Root);
            }
            this._count++;
        }

        private void InsertRec(T element, Node<T> node)
        {
            if (element.CompareTo(node.Value) < 0)
            {
                if (node.LeftChild == null)
                {
                    node.LeftChild = new Node<T>(element, null, null);
                }
                else
                {
                    InsertRec(element, node.LeftChild);
                }
            }
            else
            {
                if (node.RightChild == null)
                {
                    node.RightChild = new Node<T>(element, null, null);
                }
                else
                {
                    InsertRec(element, node.RightChild);
                }
            }
        }

        public IAbstractBinarySearchTree<T> Search(T element)
        {
            var elementNode = this.FindElementNode(element, this.Root);
            return new BinarySearchTree<T>()
            {
                Root = elementNode,
                LeftChild = elementNode.LeftChild,
                RightChild = elementNode.RightChild,
            };
        }

        private Node<T> FindElementNode(T element, Node<T> node)
        {
            if (node == null)
            {
                return null;
            }
            if (element.CompareTo(node.Value) == 0)
            {
                return node;
            }
            if (element.CompareTo(node.Value) < 0)
            {
                return FindElementNode(element, node.LeftChild);
            }
            else
            {
                return FindElementNode(element, node.RightChild);
            }
        }

        public void EachInOrder(Action<T> action)
        {
            this.EachInOrder(this.Root, action);
        }

        private void EachInOrder(Node<T> node, Action<T> action)
        {
            if (node == null)
            {
                return;
            }

            this.EachInOrder(node.LeftChild, action);
            action(node.Value);
            this.EachInOrder(node.RightChild, action);
        }

        public List<T> Range(T lower, T upper)
        {
            var toReturn = new List<T>();
            GetInRangeRec(lower, upper, this.Root, toReturn);
            return toReturn;
        }

        private void GetInRangeRec(T lower, T upper, Node<T> node, List<T> elements)
        {
            if (node == null)
            {
                return;
            }

            if (node.Value.CompareTo(lower) >= 0 && node.Value.CompareTo(upper) <= 0)
            {
                elements.Add(node.Value);
            }

            if (node.Value.CompareTo(lower) >= 0)
            {
                GetInRangeRec(lower, upper, node.LeftChild, elements);
            }
            if (node.Value.CompareTo(upper) <= 0)
            {
                GetInRangeRec(lower, upper, node.RightChild, elements);
            }
            
        }

        public void DeleteMin()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException();
            }
            if (this.Root.LeftChild == null)
            {
                this.Root = this.Root.RightChild;
                this._count--;
                return;
            }
            
            var parent = this.Root;
            var node = this.Root;
            while (node.LeftChild != null)
            {
                parent = node;
                node = node.LeftChild;
            }

            if (node.RightChild != null && node.LeftChild == null)
            {
                parent.LeftChild = node.RightChild;
            }
            else
            {
                parent.LeftChild = null;
                node = null;
            }
            
            this._count--;
        }

        public void DeleteMax()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException();
            }
            if (this.Root.RightChild == null)
            {
                this.Root = this.Root.LeftChild;
                this._count--;
                return;
            }
            var parent = this.Root;
            var node = this.Root;
            while (node.RightChild != null)
            {
                parent = node;
                node = node.RightChild;
            }

            if (node.LeftChild != null && node.RightChild == null)
            {
                parent.RightChild = node.LeftChild;
            }
            else
            {
                parent.RightChild = null;
                node = null;
            }
            this._count--;
        }

        public int GetRank(T element)
        {
            return GetRank(element, this.Root, 0);
        }

        private int GetRank(T element, Node<T> node, int count)
        {
            if (node == null)
            {
                return count;
            }
            if (element.CompareTo(node.Value) > 0)
            {
                count++;
            }

            count = this.GetRank(element, node.LeftChild, count);
            if (element.CompareTo(node.Value) > 0)
            {
                count = this.GetRank(element, node.RightChild, count);
            }

            return count;
        }
    }
}
