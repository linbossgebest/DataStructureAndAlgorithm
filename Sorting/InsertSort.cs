using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorting
{
    public class InsertSort
    {
        public static void Sort(int[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = i; j > 0; j--)
                {
                    if (arr[j] > arr[j - 1])
                        break;
                    else
                    {
                        int temp = arr[j];
                        arr[j] = arr[j - 1];
                        arr[j - 1] = temp;
                    }
                }
            }

            for (int i = 0; i < arr.Length; i++)
                Console.WriteLine(arr[i]);
        }
    }
}
