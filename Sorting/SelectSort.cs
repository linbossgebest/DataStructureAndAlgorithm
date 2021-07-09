using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorting
{
    public class SelectSort
    {
        public static void Sort(int[] arr)
        {
            int n = arr.Length - 1;
            int temp = 0;
            for (int i = 0; i < n; i++)
            {
                int minVal = arr[i];
                int minIndex = i;

                for (int j = i + 1; j <= n; j++)
                {
                    if (minVal > arr[j])
                    {
                        minVal = arr[j];
                        minIndex = j;
                    }
                }
                temp = arr[i];
                arr[i] = arr[minIndex];
                arr[minIndex] = temp;
            }

            for (int i = 0; i < arr.Length; i++)
                Console.WriteLine(arr[i]);
        }
    }
}
