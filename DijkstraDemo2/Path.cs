using System;
using System.Collections.Generic;
using System.Text;

namespace DijkstraDemo2
{
    public class Path
    {
        public Point source;
        public Point end;
        public List<Point> points;
        public double length;

        public Path(Point source, Point end, List<Point> points, double length) 
        {
            this.source = source;
            this.end = end;
            this.points = points;
            this.length = length;
        }
    }
}
