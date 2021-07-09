using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace DijkstraDemo4
{
    class Program
    {

        static void Main(string[] args)
        {

            List<List<Edge>> e;

            Console.WriteLine("Hello World!");
        }

        public static void dijkstra(int s)
        {

        }

        public class Edge : IComparer<Edge>
        {
            int to;
            int cost;
            public Edge(int _to, int _cost)
            {
                this.to = _to;
                this.cost = _cost;
            }

            public int Compare([AllowNull] Edge x, [AllowNull] Edge y)
            {
                return y.cost.CompareTo(x.cost);
            }
        }

    }
}
