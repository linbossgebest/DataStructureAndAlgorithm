using System;

namespace TestBinaryHeap
{
    class Program
    {
        static void Main(string[] args)
        {
            BinaryHeap<int> heap = new BinaryHeap<int>();
            heap.Enqueue(8);
            heap.Enqueue(2);
            heap.Enqueue(3);
            heap.Enqueue(1);
            heap.Enqueue(5);

            Console.WriteLine(heap.Dequeue());
            Console.WriteLine(heap.Dequeue());

            Console.ReadLine();
        }
    }
}
