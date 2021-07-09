using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorting
{
    public class MergeSort
    {
        public static void Sort(int[] arr)
        {
            int n = arr.Length;
            int[] temp = new int[n];
            Sort(arr, temp, 0, n - 1);

            for (int i = 0; i < arr.Length; i++)
                Console.WriteLine(arr[i]);
        }

        private static void Sort(int[] arr, int[] temp, int l, int r)
        {
            if (l >= r)
                return;

            int mid = l + (r - l) / 2;
            Sort(arr, temp, l, mid);
            Sort(arr, temp, mid + 1, r);
            Merge(arr, temp, l, mid, r);
        }

        private static void Merge(int[] arr,int[] temp, int l, int mid, int r)
        {
            int i = l;
            int j = mid + 1;
            int k = l;

            //左右半边都有元素
            while (i <= mid && j <= r)
            {
                if (arr[i] < arr[j])
                    temp[k++] = arr[i++];
                else
                    temp[k++] = arr[j++];
                   
            }

            //左半边还有元素，右半边没有
            while (i <= mid)
            {
                temp[k++] = arr[i++];
            }
            //右半边还有元素，左半边没有
            while (j <= r)
            {
                temp[k++] = arr[j++];
            }

            for (int z = l; z <= r; z++)
                arr[z] = temp[z];
        }
    }
}
