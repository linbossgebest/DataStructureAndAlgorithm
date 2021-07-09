using System;
using System.Collections.Generic;
using System.Text;

namespace DijkstraDemo2
{
    public class Dijkstra
    {
        public List<Point> points;
        public List<Edge> edges = new List<Edge>();
        //Point is the end of the Path
        private Dictionary<Point, Path> pathMap = new Dictionary<Point, Path>();
        //当前变为红点的点
        private Point currentPoint;
        private double INFINITY = 999999;
        //源点到当前红点的长度
        private double startToCurrent;

        public void init(Point start)
        {
            start.isSource = true;
            start.isRed = true;
            Point source = start;
            //设置路径的终点
            foreach (Point point in points)
            {
                List<Point> redPoints = new List<Point>();
                redPoints.Add(source);
                Path path = new Path(source, point, redPoints, INFINITY);
                pathMap.Add(point, path);
            }
            //设置路径的长度，当存在这样的边：起始，结束点分别和路径的源点，终点相同
            foreach (Edge edge in edges)
            {
                if (source.name == (edge.start.name))
                {
                    pathMap[edge.end].length = edge.length;
                }
            }
        }


        public void dijkstra()
        {
            dijkstra(null, null);
        }

        public void dijkstra(Point start, Point end)
        {
            DateTime startTime = DateTime.Now;
            init(start);
            DateTime endTime = DateTime.Now;
            TimeSpan timeSpan = endTime - startTime;
            Console.WriteLine(timeSpan.TotalSeconds);

            for (int i = 0; i < points.Count; i++)
            {
                int indexMin = getMin();
                double minDist = pathMap[points[indexMin]].length;
                if (minDist == INFINITY)
                {
                    Console.WriteLine("有无法到达的顶点");
                }
                else if (i != points.Count - 1)
                {
                    //获取当前循环已经求出最短路径的点
                    currentPoint = points[indexMin];
                    points[indexMin].isRed = true;
                    if (end != null && end.Equals(currentPoint))
                    {
                        return;
                    }
                    pathMap[points[indexMin]].points.Add(currentPoint);
                    startToCurrent = minDist;
                }

                resetPaths();
            }
        }

        private int getMin()
        {
            double minDist = INFINITY;
            int indexMin = 0;
            for (int i = 0; i < points.Count; i++)
            {
                Path path = pathMap[points[i]];
                if (!path.end.isRed && path.length < minDist)
                {
                    minDist = path.length;
                    indexMin = i;
                }
            }
            return indexMin;
        }


        /**
	 * 在当前红点发生变化后，源点到其他点的路径也相应变化，通过当前红点，
	 * 之前不可到的点有可能变为可到达，
	 * 之前可到达的点路径长度会发生改变
	 * 所以要重置其他路径的长度
	 */
        private void resetPaths()
        {
            foreach (Edge edge in edges)
            {
                if (edge.end.isRed)
                {
                    continue;
                }
                Path path = pathMap[edge.end];
                //if (edge.getStart().getName().equals(currentPoint.getName()) && edge.getEnd().getName().equals(path.getEnd().getName()))
                if (currentPoint != null)
                {
                    if (edge.start.name == currentPoint.name && edge.end.name == path.end.name)
                    {
                        double currentToFringe = edge.length;
                        double startToFringe = startToCurrent + currentToFringe;
                        double pathLength = path.length;
                        if (startToFringe < pathLength)
                        {
                            List<Point> points = pathMap[currentPoint].points;
                            List<Point> copyPoints = new List<Point>();
                            foreach (Point point in points)
                            {
                                copyPoints.Add(point);
                            }
                            path.points = copyPoints;
                            path.length = startToFringe;
                        }
                    }
                }
            }
        }


        public void display()
        {
            foreach (Point point in pathMap.Keys)
            {
                Path path = pathMap[point]; ;
                Console.Write(path.source.name + "-->" + path.end.name + ":");
                foreach (Point point2 in path.points)
                {
                    Console.Write(point2.name + "  ");
                }
                Console.Write(path.length);
                Console.WriteLine();
            }
        }

    }
}
