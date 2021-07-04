namespace Problem03.ReversedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class ReversedList<T> : IAbstractList<T>
    {
        private const int DefaultCapacity = 4;

        private T[] _items;

        public ReversedList()
            : this(DefaultCapacity) { }

        public ReversedList(int capacity)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException(nameof(capacity));

            this._items = new T[capacity];
        }

        public T this[int index]
        {
            get
            {
                this.CheckIndex(index);
                return this._items[this.Count - 1 - index];
            }
            set
            {
                this.CheckIndex(index);
                this._items[index] = value;
            }
        }

        public int Count { get; private set; } = 0;

        public void Add(T item)
        {
            if (this.Count + 1 > this._items.Length)
            {
                this._items = this.DoubleSize(this._items);
            }
            this._items[this.Count++] = item; 
        }

        public bool Contains(T item)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (this._items[i].Equals(item))
                {
                    return true;
                }
            }
            return false;
        }

        public int IndexOf(T item)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (this._items[i].Equals(item))
                {
                    return this.Count - i - 1;
                }
            }
            return -1;
        }

        public void Insert(int index, T item)
        {
            this.CheckIndex(index);
            index = this.Count - index;
            if (this.Count + 1 > this._items.Length)
            {
                this._items = this.DoubleSize(this._items);
            }
            this.Count++;
            var oldElement = this._items[index];
            this._items[index] = item;
            for (int i = index+1; i < this.Count; i++)
            {
                var element = this._items[i];
                this._items[i] = oldElement;
                oldElement = element;
            }
        }

        public bool Remove(T item)
        {
            int index = -1;
            for (int i = 0; i < this.Count; i++)
            {
                if (this._items[i].Equals(item))
                {
                    index = i;
                    break;
                }
            }
            if (index != -1)
            {
                RestructureArrayOnRemove(index);
                this.Count--;
                return true;
            }
            else
            {
                return false;
            }
            
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= this.Count)
            {
                throw new IndexOutOfRangeException();
            }
            index = this.Count - index;
            RestructureArrayOnRemove(index);
            this.Count--;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = this.Count-1; i >= 0 ; i--)
            {
                yield return this._items[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private T[] DoubleSize(T[] array)
        {
            T[] newArray = new T[array.Length * 2];
            for (int i = 0; i < array.Length; i++)
            {
                newArray[i] = array[i];
            }
            return newArray;
        }

        private void RestructureArrayOnRemove(int index)
        {
            for (int i = index; i < this.Count-1; i++)
            {
                this._items[i] = this._items[i + 1];
            }
            if (index < this.Count - 1)
            {
                this._items[this.Count - 1] = default;
            }
        }

        private void CheckIndex(int index)
        {
            if (index < 0 || index >= this.Count)
            {
                throw new IndexOutOfRangeException();
            }
        }
    }
}