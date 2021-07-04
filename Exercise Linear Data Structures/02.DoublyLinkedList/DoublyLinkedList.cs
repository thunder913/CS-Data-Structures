namespace Problem02.DoublyLinkedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class DoublyLinkedList<T> : IAbstractLinkedList<T>
    {
        private Node<T> head;
        private Node<T> tail;

        public int Count { get; private set; }

        public void AddFirst(T item)
        {
            if (this.Count == 0)
            {
                AddElementWhenEmpty(item);
            }
            else 
            {
                var newHead = new Node<T>() { Item = item };
                newHead.Next = this.head;
                this.head.Previous = newHead;
                this.head = newHead;
                this.Count++;
            }
        }

        public void AddLast(T item)
        {
            if (this.Count == 0)
            {
                AddElementWhenEmpty(item);
            }
            else if (this.Count == 1)
            {
                this.head.Next = new Node<T>() { Item = item };
                this.head.Next.Previous = this.head;
                this.tail = this.head.Next;
                this.Count++;
            }
            else
            {
                this.tail.Next = new Node<T>() { Item = item, Previous = this.tail };
                this.tail = this.tail.Next;
                this.Count++;
            }
        }

        public T GetFirst()
        {
            CheckIfCountIsZero();
            return this.head.Item;
        }

        public T GetLast()
        {
            CheckIfCountIsZero();
            return this.tail.Item;
        }

        public T RemoveFirst()
        {
            CheckIfCountIsZero();
            var removed = this.head.Item;
            if (this.Count == 1)
            {
                this.head = null;
                this.tail = null;
                this.Count--;
            }
            else
            {
                this.head = this.head.Next;
                this.head.Previous = null;
                this.Count--;
            }
            return removed;
        }

        public T RemoveLast()
        {
            CheckIfCountIsZero();
            var removed = this.tail.Item;
            if (this.Count == 1)
            {
                this.head = null;
                this.tail = null;
                this.Count--;
            }
            else
            {
                this.tail = this.tail.Previous;
                this.tail.Next = null;
                this.Count--;
            }
            return removed;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var element = this.head;
            while (element != null)
            {
                yield return element.Item;
                element = element.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private void AddElementWhenEmpty(T item)
        {
            this.head = new Node<T>() { Item = item };
            this.tail = head;
            this.Count++;
        }

        private void CheckIfCountIsZero()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException("The list is empty!");
            }
        }
    }
}