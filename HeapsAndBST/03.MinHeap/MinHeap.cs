namespace _03.MinHeap
{
    using System;
    using System.Collections.Generic;

    public class MinHeap<T> : IAbstractHeap<T>
        where T : IComparable<T>
    {
        private List<T> _elements;

        public MinHeap()
        {
            this._elements = new List<T>();
        }


        public int Size => this._elements.Count;

        public T Dequeue()
        {
            if (this.Size == 0)
            {
                throw new InvalidOperationException();
            }
            var toRemove = this._elements[0];
            this._elements[0] = this._elements[this.Size - 1];
            this._elements.RemoveAt(this.Size-1);
            this.SwapPlacesToMinRec(0);

            return toRemove;
        }

        private void SwapPlacesToMinRec(int index)
        {
            var leftChildIndex = index * 2 + 1;
            var rightChildIndex = index * 2 + 2;
            if (leftChildIndex >= this.Size)
            {
                return;
            }
            if (rightChildIndex >= this.Size)
            {
                this.SwapElements(index, leftChildIndex);
                return;
            }

            if (this._elements[leftChildIndex].CompareTo(this._elements[rightChildIndex]) < 0)
            {
                this.SwapElements(index, leftChildIndex);
                index = leftChildIndex;
            }
            else
            {
                this.SwapElements(index, rightChildIndex);
                index = rightChildIndex;
            }
            SwapPlacesToMinRec(index);
        }

        public void Add(T element)
        {
            this._elements.Add(element);
            Heapify(this.Size - 1);
        }

        private void Heapify(int index)
        {
            int parentIndex = (index - 1) / 2;
            while (index > 0 && IsSmallerThanParent(parentIndex, index))
            {
                this.SwapElements(index, parentIndex);
                index = parentIndex;
                parentIndex = (index - 1) / 2;
            }
        }

        private void SwapElements(int first, int second)
        {
            var temp = this._elements[first];
            this._elements[first] = this._elements[second];
            this._elements[second] = temp;
        }

        private bool IsSmallerThanParent(int parentIndex, int childIndex)
        {
            return this._elements[childIndex].CompareTo(this._elements[parentIndex]) < 0;
        }

        public T Peek()
        {
            if (this.Size == 0)
            {
                throw new InvalidOperationException();
            }
            return this._elements[0];
        }
    }
}
