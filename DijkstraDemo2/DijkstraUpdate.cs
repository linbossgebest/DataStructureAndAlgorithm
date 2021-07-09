using System;
using System.Collections.Generic;
using System.Text;

namespace DijkstraDemo2
{
    class DijkstraUpdate
    {
		public List<Point> points;
		public List<Edge> edges = new List<Edge>();
		//Point is the end of the Path
		private Dictionary<Point, Path> pathMap = new Dictionary<Point, Path>();
		//Point is the start of all Edge in the List
		private Dictionary<Point, List<Edge>> edgesMapByStart = new Dictionary<Point, List<Edge>>();
		//Point is the end of all Edge in the List
		private Dictionary<Point, List<Edge>> edgesMapByEnd = new Dictionary<Point, List<Edge>>();
		//蓝点集，存放两种点，1.初始化阶段Path.length!=INFINITY
		//2.resetPath 方法中重置过的点
		private HashSet<Point> bluePoints = new HashSet<Point>();
		private Point currentPoint;
		private double INFINITY = 999999;
		private double startToCurrent;

		public void init(Point start)
		{
			start.isSource = true;
			start.isRed = true;
			Point source = start;
			foreach (Point point in points)
			{
				List<Point> redPoints = new List<Point>();
				redPoints.Add(source);
				Path path = new Path(source, point, redPoints, INFINITY);
				pathMap.Add(point, path);
			}
			foreach (Edge edge in edges)
			{
				Point s = edge.start;
				Point e = edge.end;
				if (source.Equals(s))
				{
					pathMap[e].length = edge.length;
                    bluePoints.Add(e);
                }
                if (!edgesMapByStart.ContainsKey(s))
                {
                    edgesMapByStart[s] = new List<Edge>();
                    edgesMapByStart[s].Add(edge);
                }
                else
                {
					edgesMapByStart[s].Add(edge);
				}
				if (!edgesMapByEnd.ContainsKey(e))
				{
					edgesMapByEnd[e] = new List<Edge>();
					edgesMapByEnd[e].Add(edge);
				}
				else
				{
					edgesMapByEnd[e].Add(edge);
				}
			}
		}

		public void dijkstra()
		{
			dijkstra(null, null);
		}

		public void dijkstra(Point start, Point end)
		{
			init(start);
			while (bluePoints.Count > 0)
			{
				Point point = getMin();
				if (point == null)
				{
					continue;
				}
				double minDist = pathMap[point].length;
				if (minDist == INFINITY)
				{
					Console.WriteLine("有无法到达的顶点");
				}
				else
                {
                    currentPoint = point;
                    point.isRed = true;
                    if (edgesMapByStart.ContainsKey(point))
                    {
                        List<Edge> edges = edgesMapByStart[point];
                        if (edges != null)
                        {
                            foreach (Edge edge in edges)
                            {
                                if (!edge.end.isRed)
                                {
                                    bluePoints.Add(edge.end);
                                }
                            }

                        }
                        bluePoints.Remove(point);
                        if (end != null && end.Equals(currentPoint))
                        {
                            return;
                        }
                        pathMap[point].points.Add(currentPoint);
                        startToCurrent = minDist;
                    }

                }

                resetPaths();
            }
        }

        private void resetPaths()
		{
			IEnumerator<Point> it = bluePoints.GetEnumerator();
			while (it.MoveNext()) 
			{
				Point bluePoint = it.Current;
				List<Edge> edges = edgesMapByEnd[bluePoint];
				foreach (Edge edge in edges)
				{
					if (edge.end.isRed)
					{
						continue;
					}
					Path path = pathMap[edge.end];
					if (edge.start.Equals(currentPoint) && edge.end.Equals(path.end))
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
			//foreach (Point pointItem in bluePoints) 
			//{
			//	Point bluePoint = pointItem;
			//	List<Edge> edges = edgesMapByEnd[bluePoint];
			//	foreach (Edge edge in edges)
			//	{
			//		if (edge.end.isRed)
			//		{
			//			continue;
			//		}
			//		Path path = pathMap[edge.end];
			//		if (edge.start.Equals(currentPoint) && edge.end.Equals(path.end))
			//		{
			//			double currentToFringe = edge.length;
			//			double startToFringe = startToCurrent + currentToFringe;
			//			double pathLength = path.length;
			//			if (startToFringe < pathLength)
			//			{
			//				List<Point> points = pathMap[currentPoint].points;
			//				List<Point> copyPoints = new List<Point>();
			//				foreach (Point point in points)
			//				{
			//					copyPoints.Add(point);
			//				}
			//				path.points = copyPoints;
			//				path.length = startToFringe;
			//			}
			//		}
			//	}
			//}
			//Iterator<Point> it = bluePoints.iterator();
			//while (it.hasNext())
			//{
			//	Point bluePoint = it.next();
			//	List<Edge> edges = edgesMapByEnd.get(bluePoint);
			//	foreach  (Edge edge in edges)
			//	{
			//		if (edge.getEnd().isRed())
			//		{
			//			continue;
			//		}
			//		Path path = pathMap.get(edge.getEnd());
			//		if (edge.getStart().equals(currentPoint) && edge.getEnd().equals(path.getEnd()))
			//		{
			//			double currentToFringe = edge.getLength();
			//			double startToFringe = startToCurrent + currentToFringe;
			//			double pathLength = path.getLength();
			//			if (startToFringe < pathLength)
			//			{
			//				List<Point> points = pathMap.get(currentPoint).getPoints();
			//				List<Point> copyPoints = new ArrayList<Point>();
			//				for (Point point : points)
			//				{
			//					copyPoints.add(point);
			//				}
			//				path.setPoints(copyPoints);
			//				path.setLength(startToFringe);
			//			}
			//		}
			//	}
			//}
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

		private Point getMin()
		{
			double minDist = INFINITY;
			Point point = null;
			foreach (Point bluePoint in bluePoints)
			{
				Path path = pathMap[bluePoint];
				if (!path.end.isRed && path.length < minDist)
				{
					minDist = path.length;
					point = bluePoint;
				}

			}
			return point;
		}
	}
}
