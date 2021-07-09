using System;
using System.Collections.Generic;
using System.Text;
namespace DijkstraDemo2
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime initStart = DateTime.Now;
            List<Point> points = new List<Point>();
            List<Edge> edges = new List<Edge>();
            int pointsCount = 100;
            Random rd = new Random();
            for (int i = 0; i < pointsCount; i++)
            {
                Point point = new Point("" + i);
                points.Add(point);
            }

            for (int i = 0; i < points.Count; i++)
            {
                for (int j = i; j < points.Count - i + 1; j++)
                {
                    if (j < points.Count && (j == i + 1 || j == 10 * i + 1))
                    {
                        
                        edges.Add(new Edge(points[i], points[j], 100 * rd.Next(5)));
                    }
                }
            }


            Console.WriteLine("边数"+edges.Count);
            DateTime initEnd = DateTime.Now;
            TimeSpan ts1 = initEnd - initStart;
            Console.WriteLine("init time"+ ts1.TotalSeconds);

            DateTime computeStart = DateTime.Now;
            //Dijkstra dijkstra = new Dijkstra();
            DijkstraUpdate dijkstra = new DijkstraUpdate();
            dijkstra.points = points;
            dijkstra.edges = edges;
            dijkstra.dijkstra(points[0],points[pointsCount-1]);
            dijkstra.display();
            DateTime computeEnd = DateTime.Now;
            TimeSpan ts2 = computeEnd - computeStart;
            Console.WriteLine("compute time"+ ts2.TotalSeconds);
        }
    }
}
