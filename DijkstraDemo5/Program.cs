
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace DijkstraDemo5
{
    class Program
    {
        const int u = 10000;

        static void Main(string[] args)
        {
            AdjacencyList<int> alist = new DijkstraDemo5.AdjacencyList<int>();
            int pointsCount = 8;//顶点个数
            int[] dist = new int[pointsCount]; //起点到各点最短距离数组
            int[] visited = new int[pointsCount];//创建已访问数组
            int edgesCount = 0;
            Random rd = new Random();

            #region 初始化数据

            DateTime initStart = DateTime.Now;

            for (int i = 0; i < pointsCount; i++)
            {
                alist.AddVertex(i);
                dist[i] = u;
            }
            //for (int i = 0; i < pointsCount; i++)
            //{
            //    for (int j = i; j < pointsCount - i + 1; j++)
            //    {
            //        if (j < pointsCount && (j == i + 1 || j == 10 * i + 1))
            //        {
            //            int distance = rd.Next(10);
            //            alist.AddDirectEdge(i, j, distance);
            //            edgesCount++;
            //        }
            //    }
            //}

            DateTime initEnd = DateTime.Now;
            TimeSpan ts1 = initEnd - initStart;

            alist.AddDirectEdge(0, 1, 2);
            alist.AddDirectEdge(0, 2, 4);
            alist.AddDirectEdge(0, 3, 1);
            alist.AddDirectEdge(1, 3, 3);
            alist.AddDirectEdge(1, 4, 10);
            alist.AddDirectEdge(2, 0, 4);
            alist.AddDirectEdge(2, 5, 5);
            alist.AddDirectEdge(3, 2, 2);
            alist.AddDirectEdge(3, 4, 2);
            alist.AddDirectEdge(3, 5, 8);
            alist.AddDirectEdge(3, 6, 4);
            alist.AddDirectEdge(4, 6, 6);
            alist.AddDirectEdge(6, 4, 6);
            alist.AddDirectEdge(6, 5, 1);
            alist.AddDirectEdge(7, 0, 7);
            alist.AddDirectEdge(7, 5, 9);

            #endregion

            // Console.WriteLine(alist);
            //AdjacencyList<string> alist = new DijkstraDemo5.AdjacencyList<string>();

            Console.WriteLine(alist);



            Console.WriteLine("请输入要计算的起始点：");
            string a = Console.ReadLine();
            int start = Int32.Parse(a);

            DateTime computeStart = DateTime.Now;
            var test = CustomDijstra(alist, dist, visited, start);
            DateTime computeEnd = DateTime.Now;
            TimeSpan ts2 = computeEnd - computeStart;
            Console.WriteLine("init time:" + ts1.TotalSeconds + "s");
            Console.WriteLine("compute time:" + ts2.TotalSeconds + "s");

            for (int i = 0; i < test.Length; i++)
            {
                Console.WriteLine($"起点{start}->{i}点的最短距离为{test[i]}");
            }

            Console.WriteLine($"顶点数：{pointsCount},边数：{edgesCount}");

        }

        /// <summary>
        /// 自定义Dijkstra(堆优化+邻接表)
        /// </summary>
        public static int[] CustomDijstra(AdjacencyList<int> adjList, int[] dist, int[] visited, int start)
        {
            PriorityQueue<StartToPoint> pq = new PriorityQueue<StartToPoint>();
            dist[start] = 0;
            StartToPoint firststp = new StartToPoint(start, 0);
            pq.Push(firststp);
            while (pq.Count > 0)
            {
                StartToPoint s = pq.Top();
                pq.Pop();

                int sk = s.Index;
                int sv = s.Distance;
                if (visited[sk] == 1)
                    continue;
                visited[sk] = 1;

                var node = adjList.items[sk].firstEdge;
                while (node != null)
                {
                    int v = node.adjvex.data;
                    int w = node.weight;
                    if (visited[v] == 0 && dist[v] > sv + w)
                    {
                        dist[v] = sv + w;
                        StartToPoint stp = new StartToPoint(v, dist[v]);
                        pq.Push(stp);
                    }
                    node = node.next;
                }
            }

            return dist;


        }





        public struct StartToPoint : IComparable
        {
            public StartToPoint(int index, int distance)
            {
                this.Index = index;
                this.Distance = distance;
            }
            public int Index { get; set; }

            public int Distance { get; set; }

            public int CompareTo(object obj)
            {
                if (!(obj is StartToPoint))
                {
                    throw new ArgumentException("Compared Object is not of StartToPoint");
                }
                StartToPoint stp = (StartToPoint)obj;
                return stp.Distance.CompareTo(this.Distance);
                //return this.Distance.CompareTo(stp.Distance);
            }
        }
    }
}
