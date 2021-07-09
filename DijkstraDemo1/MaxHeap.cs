using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace DijkstraDemo1
{
    /// <summary>
    /// 最大堆 [0,70,40,50,20,30,25,45] 存放元素从index=1开始
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class MaxHeap<T> where T:IComparable<T>
    {
        private T[] heap;
        private int N;

        public MaxHeap(int capacity)
        {
            heap = new T[capacity + 1];
            N = 0;
        }

        public MaxHeap() : this(10) { }

        public int Count { get { return N; } }

        public bool IsEmpty { get { return N == 0; } }

        public void Insert(T t) 
        {
            if (N == (heap.Length - 1))//判断是否需要扩容
                ResetCapacity(heap.Length * 2);

            heap[N + 1] = t;
            N++;
            Swim(N);
        }

        public T RemoveMax()
        {
            if (IsEmpty)
                throw new InvalidOperationException("堆为空");

            Swap(1, N);
            T max = heap[N];
            heap[N] = default(T);
            N--;

            Sink(1);

            if (N == (heap.Length - 1) / 4)
                ResetCapacity(heap.Length / 2);

            return max;
        }

        public T Max()
        {
            if (IsEmpty)
                throw new InvalidOperationException("堆为空");

            return heap[1];
        }

        /// <summary>
        /// 元素下沉操作
        /// </summary>
        /// <param name="k"></param>
        private void Sink(int k) 
        {
            while (2 * k <= N)
            {
                int j = 2 * k;

                if (j + 1 <= N && heap[j + 1].CompareTo(heap[j]) > 0)
                    j++;

                if (heap[k].CompareTo(heap[j]) >= 0)
                    break;

                Swap(k, j);

                k = j;
            }
        }

        /// <summary>
        /// 元素上游操作
        /// </summary>
        /// <param name="k"></param>
        private void Swim(int k)
        {
            while (k > 1 && heap[k].CompareTo(heap[k / 2]) > 0) 
            {
                Swap(k, k / 2);
                k = k / 2;
            }
        }

        /// <summary>
        /// 交换操作
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        private void Swap(int i, int j)
        {
            T t = heap[i];
            heap[i] = heap[j];
            heap[j] = t;
        }

        /// <summary>
        /// 扩容
        /// </summary>
        /// <param name="newCapacity"></param>
        private void ResetCapacity(int newCapacity)
        {
            T[] newHeap = new T[newCapacity];
            for (int i = 1; i <= N; i++)
                newHeap[i] = heap[i];

            heap = newHeap;
        }
    }
}
