using System;

namespace Sorting
{
    class Program
    {
        static void Main(string[] args)
        {

            int[] nums = { 9, 8, 7, 6, 5, 4, 3, 2, 1, 0 };
            //BubbleSort.Sort(nums);
            //SelectSort.Sort(nums);
            InsertSort.Sort(nums);
            //MergeSort.Sort(nums);

            Console.Read();
        }
    }
}
