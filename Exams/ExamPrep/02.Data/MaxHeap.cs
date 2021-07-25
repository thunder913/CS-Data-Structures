using _02.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace _02.Data
{
    public class MaxHeap<T> where T : IEntity
    {
        private List<T> heap;

        public MaxHeap()
        {
            heap = new List<T>();
        }

        public int Size { get { return heap.Count; } }

        public T Peek()
        {
            return heap[0];
        }

        public List<T> GetAsList()
        {
            return this.heap;
        }

        public List<T> GetByType(string type) 
        {
            if (type != "Invoice" && type != "StoreClient" && type != "User")
            {
                throw new InvalidOperationException("Invalid type: " + type);
            }

            var toReturn = new List<T>();

            for (int i = 0; i < this.Size; i++)
            {
                if (this.heap[i].GetType().Name == type)
                {
                    toReturn.Add(this.heap[i]);
                }
            }

            return toReturn;
        }

        public T PeekMostRecent()
        {
            if (this.Size == 0)
            {
                throw new InvalidOperationException("Operation on empty Data");
            }

            return this.heap[0];
        }

        public void Add(T element)
        {
            heap.Add(element);
            Heapify(heap.Count - 1);
        }

        public IEntity RemoveTopElement()
        {
            if (this.Size == 0)
            {
                throw new InvalidOperationException("Operation on empty Data");
            }

            var top = this.heap[0];
            this.heap[0] = heap[heap.Count - 1];
            heap.RemoveAt(heap.Count - 1);
            this.HeapifyDown(0);

            return top;
        }

        private void Heapify(int index)
        {
            if (index == 0) return;

            int parentIndex = (index - 1) / 2;

            if (heap[index].CompareTo(heap[parentIndex]) < 0)
            {
                T temp = heap[index];
                heap[index] = heap[parentIndex];
                heap[parentIndex] = temp;
                Heapify(parentIndex);
            }
        }

        private void HeapifyDown(int index)
        {
            int leftChildIndex = index * 2 + 1;
            int rightChildIndex = index * 2 + 2;
            int maxChildIndex = leftChildIndex;

            if (leftChildIndex >= heap.Count) return;

            if ((rightChildIndex < heap.Count) && heap[leftChildIndex].CompareTo(heap[rightChildIndex]) > 0)
                maxChildIndex = rightChildIndex;

            if (heap[index].CompareTo(heap[maxChildIndex]) > 0)
            {
                T temp = heap[index];
                heap[index] = heap[maxChildIndex];
                heap[maxChildIndex] = temp;
                HeapifyDown(maxChildIndex);
            }
        }
    }
}
