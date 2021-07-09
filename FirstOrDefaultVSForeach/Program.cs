using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace FirstOrDefaultVSForeach
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> ls = new List<string>();
            for (int i = 0; i < 20000000; i++)
            {
                ls.Add("数据"+i);
            }


            Stopwatch t1 = new Stopwatch();
            t1.Start();
            ls.FirstOrDefault(f => f == "数据20000000");
            t1.Stop();

            Console.WriteLine("FirstOrDefault:" + t1.ElapsedMilliseconds+"ms");

            Stopwatch t2 = new Stopwatch();
            t2.Start();
            foreach (var item in ls)
            {
                if (item == "数据20000000")
                    break;
            }
            t2.Stop();

            Console.WriteLine("Foreach:" + t2.ElapsedMilliseconds + "ms");
            Console.WriteLine();

            Console.ReadKey();
        }
    }
}
