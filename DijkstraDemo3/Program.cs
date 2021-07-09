
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace DijkstraDemo3
{
    class Program
    {
        const int u = 10000;

        static void Main(string[] args)
        {
            int pointsCount = 8;
            int edgesCount = 0;
            int effectedgesCount = 0;
            Random rd = new Random();
            int[,] matrix;//存放点与点距离的二维数组
            List<ShortPath> list;//创建展示最短路径列表


            matrix = new int[pointsCount, pointsCount];
            list = new List<ShortPath>();//创建展示最短路径列表
            Console.WriteLine("请输入要计算的起始点：");
            string a = Console.ReadLine();
            int start = Int32.Parse(a);

            SortedList mySorted = new SortedList();
            PriorityQueue<StartToPoint> priorityQueue = new PriorityQueue<StartToPoint>(pointsCount, new StartToPointComparer());

            #region 初始化数据


            DateTime initStart = DateTime.Now;

            //for (int i = 0; i < pointsCount; i++)
            //{
            //    for (int j = 0; j < pointsCount; j++)
            //    {
            //        matrix[i, j] = u;
            //        edgesCount++;
            //    }

            //}

            //for (int i = 0; i < pointsCount; i++)
            //{
            //    for (int j = i; j < pointsCount - i + 1; j++)
            //    {
            //        //if (j < pointsCount)
            //        //{
            //        //    matrix[i, j] = 100 * rd.Next(5);
            //        //    edgesCount++;
            //        //}
            //        if (j < pointsCount && (j == i + 1 || j == 10 * i + 1))
            //        {
            //            matrix[i, j] = 100 * rd.Next(5);
            //            effectedgesCount++;
            //        }

            //    }
            //}


            matrix = new int[8, 8]{
                {0,2,4,1,u,u,u,u},
                {u,0,u,3,10,u,u,u},
                {4,u,0,u,u,5,u,u},
                {u,u,2,0,2,8,4,u},
                {u,u,u,u,0,u,6,u},
                {u,u,u,u,u,0,u,u},
                {u,u,u,u,6,1,0,u},
                {7,u,u,u,u,9,u,0}
            };

            DateTime initEnd = DateTime.Now;
            TimeSpan ts1 = initEnd - initStart;
          

            for (int i = 0; i < pointsCount; i++)//初始化从自定义顶点出发到所有顶点的展示列表
            {
                list.Add(new ShortPath() { Index = i, Name = start + "->" + i, Path = start + "->" + i, Distance = matrix[0, i] });
                //mySorted.Add(matrix[0, i], i);
                if (matrix[start, i] != u)
                {
                    StartToPoint stp = new StartToPoint(i, matrix[start, i]);
                    priorityQueue.Push(stp);
                }


            }

     
            //StartToPoint stp = new StartToPoint(0, matrix[0, 0]);
            //priorityQueue.Push(stp);

            //ICollection key = mySorted.Keys;
            //foreach (string k in key)
            //{
            //    Console.WriteLine(k + ": " + mySorted[k]);
            //}


            #endregion

            Console.WriteLine("初始化数据完成");

            DateTime computeStart = DateTime.Now;
            var showlist = Dijkstra(matrix, start, list, priorityQueue);
            DateTime computeEnd = DateTime.Now;
            TimeSpan ts2 = computeEnd - computeStart;


            for (int i = 0; i < pointsCount; i++)
            {
                Console.WriteLine("从" + a + "出发到" + i + "的最短距离为:" + list[i].Distance + ",最短路径为:" + showlist[i].Path);
            }
            Console.WriteLine($"顶点数：{pointsCount},边数：{edgesCount},有效边数：{effectedgesCount}");
            Console.WriteLine("init time:" + ts1.TotalSeconds + "s");
            Console.WriteLine("compute time:" + ts2.TotalSeconds + "s");
            Console.ReadKey();

        }

        public static List<ShortPath> Dijkstra(int[,] matrix, int start, List<ShortPath> list, PriorityQueue<StartToPoint> priorityQueue)
        {
            int n = matrix.GetLength(0);//几个点
             //int l=matrix.Length;
            int[] visited = new int[n];//创建已访问数组
            int i = 1;
            StartToPoint stp = new StartToPoint();
            //stp.Index = 3;
            //stp.Distance = 1;
            //priorityQueue.Push(stp);
            while (priorityQueue.Count > 0)
            {
                StartToPoint t = priorityQueue.Top();
                priorityQueue.Pop();

                int pointIndex = t.Index;
                int distance = t.Distance;
                if (visited[pointIndex] == 1) continue;
                visited[pointIndex] = 1;//该点修改为已访问 1
                list[pointIndex].Distance = distance;//展示列表中，起点到该点的最短距离直接设置为变量dmin
               
                //Console.WriteLine($"访问顶点{pointIndex}");

                for (int j = 0; j < n; j++)//循环顶点
                {
                    if (visited[j] == 0 && matrix[pointIndex, j] != u && matrix[start, pointIndex] + matrix[pointIndex, j] < matrix[start, j])//如果该点没有被访问 并且 （起点到已访问顶点k+顶点k到j顶点）的距离<起点到j顶点的距离
                    {
                        matrix[start, j] = matrix[start, pointIndex] + matrix[pointIndex, j];
                        list[j].Path = list[pointIndex].Path + "->" + j;
                        list[j].Distance = matrix[start, j];
                        //StartToPoint stp = new StartToPoint(j, matrix[start, j]);
                        stp.Index = j;
                        stp.Distance = matrix[start, j];
                        priorityQueue.Push(stp);
                    }
                    //Console.WriteLine($"第{i}次：" + list[j].Path + "   距离：" + list[j].Distance);
                }
                i++;

            }

            //visited[start] = 1;//出发点修改为已访问 1
            //for (int i = 1; i < n; i++)//循环顶点
            //{
            //    int k = -1;
            //    int dmin = u;
            //    for (int j = 0; j < n; j++)//循环顶点  找到未被访问的点 & 起点到该点为最短距离
            //    {
            //        if (visited[j] == 0 && matrix[start, j] < dmin)
            //        {
            //            dmin = matrix[start, j];
            //            k = j;
            //        }
            //    }
            //    if (k == -1)
            //    {
            //        //Console.WriteLine($"无法到达{n}顶点");
            //        continue;
            //    }

            //    list[k].Distance = dmin;//展示列表中，起点到该点的最短距离直接设置为变量dmin

            //    visited[k] = 1;//该点修改为已访问 1
            //    Console.WriteLine($"访问顶点{k}");
            //    for (int j = 0; j < n; j++)//循环顶点
            //    {
            //        if (visited[j] == 0 && matrix[start, k] + matrix[k, j] < matrix[start, j])//如果该点没有被访问 并且 （起点到已访问顶点k+顶点k到j顶点）的距离<起点到j顶点的距离
            //        {
            //            matrix[start, j] = matrix[start, k] + matrix[k, j];
            //            list[j].Path = list[k].Path + "->" + j;
            //            list[j].Distance = matrix[start, j];
            //        }
            //        Console.WriteLine($"第{i}次：" + list[j].Path + "   距离：" + list[j].Distance);
            //    }
            //}

            return list;
        }
    }

    public class ShortPath
    {
        private int index;
        private string name;
        private string path;
        private int distance;

        public string Path { get => path; set => path = value; }
        public int Distance { get => distance; set => distance = value; }
        public string Name { get => name; set => name = value; }
        public int Index { get => index; set => index = value; }
    }

    public struct StartToPoint
    {
        public StartToPoint(int index, int distance)
        {
            this.Index = index;
            this.Distance = distance;
        }
        public int Index { get; set; }

        public int Distance { get; set; }

    }

    public class StartToPointComparer : IComparer<StartToPoint>
    {
        public int Compare([AllowNull] StartToPoint x, [AllowNull] StartToPoint y)
        {
            return y.Distance.CompareTo(x.Distance);
        }
    }
}
