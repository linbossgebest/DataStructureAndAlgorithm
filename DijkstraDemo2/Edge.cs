using System;
using System.Collections.Generic;
using System.Text;

namespace DijkstraDemo2
{
    public class Edge
    {
        public Point start;
        public Point end;
        public double length;

       public Edge(Point start, Point end, double length) {
            this.start = start;
            this.end = end;
            this.length = length;
        
        }

    }
}
