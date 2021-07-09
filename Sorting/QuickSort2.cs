using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorting
{
    public class QuickSort2
    {
        private static Random rd = new Random();
        public static void Sort(int[] arr)
        {
            int n = arr.Length;
            Sort(arr, 0, n - 1);

            for (int i = 0; i < arr.Length; i++)
                Console.WriteLine(arr[i]);
        }

        private static void Sort(int[] arr, int l, int r)
        {
            if (l >= r)
                return;

            int p = l + rd.Next(r - l + 1);

            Swap(arr, l, p);

            int v = arr[l];

            int j = l;//arr[l+1...j]<v  arr[j+1...i-1]>v

            for (int i = l + 1; i <= r; i++)
            {
                if (arr[i] < v)
                {
                    j++;
                    Swap(arr, i, j);
                }  
            }

            Swap(arr, l, j);
            Sort(arr, l, j - 1);
            Sort(arr, j + 1, r);

        }

        private static void Swap(int[] arr, int i, int j)
        {
            int temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }
    }
}
