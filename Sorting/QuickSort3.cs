using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorting
{
    /// <summary>
    /// 三向切分随机化快速排序(可包含大量重复数据的数组)
    /// </summary>
    public class QuickSort3
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

            int lt = l;     //arr[l+1...lt]<v
            int gt = r + 1; //arr[gt...r]>v
            int i = l + 1;  //arr[lt+1...i-1]==v

            while (i < gt)
            {
                if (arr[i] < v)
                {
                    lt++;
                    Swap(arr, i, lt);
                    i++;
                }
                else if (arr[i] > v)
                {
                    gt--;
                    Swap(arr, i, gt);
                }
                else
                    i++;
            }

            Swap(arr, l, lt);
            Sort(arr, l, lt - 1);
            Sort(arr, gt, r);
        }

        private static void Swap(int[] arr, int i, int j)
        {
            int temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }
    }
}
