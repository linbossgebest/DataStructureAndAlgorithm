using System;

namespace DijkstraDemo
{
    class Program
    {
        static void Main(string[] args)
        {

            //Graph theGraph = new Graph(7);
            //theGraph.AddVertex("A");
            //theGraph.AddVertex("B");
            //theGraph.AddVertex("C");
            //theGraph.AddVertex("D");
            //theGraph.AddVertex("E");
            //theGraph.AddVertex("F");
            //theGraph.AddVertex("G");
            //theGraph.AddEdge(0, 1, 2);
            //theGraph.AddEdge(0, 3, 1);
            //theGraph.AddEdge(1, 3, 3);
            //theGraph.AddEdge(1, 4, 10);
            //theGraph.AddEdge(2, 5, 5);
            //theGraph.AddEdge(2, 0, 4);
            //theGraph.AddEdge(3, 2, 2);
            //theGraph.AddEdge(3, 5, 8);
            //theGraph.AddEdge(3, 4, 2);
            ////theGraph.AddEdge(3, 6, 4);
            ////theGraph.AddEdge(4, 6, 6);
            ////theGraph.AddEdge(6, 5, 1);
            //Console.WriteLine();
            //Console.WriteLine("Shortest paths:");
            //Console.WriteLine();
            //theGraph.Path();
            //Console.WriteLine();
            //Console.ReadKey();

            int pointsCount = 30000;
            int edgesCount = 0;
            Random rd = new Random();
            Graph theGraph = new Graph(pointsCount);
            for (int i = 0; i < pointsCount; i++)
            {
                theGraph.AddVertex(i.ToString());
            }

            for (int i = 0; i < pointsCount; i++)
            {
                for (int j = i; j < pointsCount - i + 1; j++)
                {
                    if (j < pointsCount && (j == i + 1 || j == 10 * i + 1))
                    {
                        theGraph.AddEdge(i, j, 100 * rd.Next(5));
                        edgesCount++;
                    }
                }
            }
           
            Console.WriteLine();
            Console.WriteLine($"顶点数：{pointsCount},边数：{edgesCount}");
            Console.WriteLine("Shortest paths:");
            Console.WriteLine();

            DateTime computeStart = DateTime.Now;
            theGraph.Path();
            DateTime computeEnd = DateTime.Now;
            TimeSpan ts2 = computeEnd - computeStart;
            Console.WriteLine("compute time:" + ts2.TotalSeconds+"s");

            Console.ReadKey();
        }
    }
}
