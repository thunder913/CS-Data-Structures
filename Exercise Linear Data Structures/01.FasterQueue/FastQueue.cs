namespace Problem01.FasterQueue
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class FastQueue<T> : IAbstractQueue<T>
    {
        private Node<T> _head;

        private Node<T> _tail;

        public int Count { get; private set; }

        public bool Contains(T item)
        {
            var element = _head;
            while (element != null)
            {
                if (element.Item.Equals(item))
                {
                    return true;
                }
                element = element.Next;
            }
            return false;
        }

        public T Dequeue()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException();
            }
            var toReturn = _head.Item;
            _head = _head.Next;
            this.Count--;
            return toReturn;
        }

        public void Enqueue(T item)
        {
            if (this.Count == 0)
            {
                this._head = new Node<T>();
                _head.Item = item;
                this._tail = new Node<T>();
                _tail.Item = item;
                this.Count = 1;
                return;
            }
            _tail.Next = new Node<T>();
            _tail = _tail.Next;
            _tail.Item = item;
            if (this.Count == 1)
            {
                _head.Next = _tail;
            }
            this.Count++;
        }

        public T Peek()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException();
            }
            return _head.Item;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var element = this._head;
            while (element != null)
            {
                yield return element.Item;
                element = element.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}