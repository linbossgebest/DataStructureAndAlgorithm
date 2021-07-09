using System;
using System.Collections.Generic;
using System.Text;

namespace DijkstraDemo2
{
    public class Point
    {
        public String name;
        //经度
        public double x = 0;
        //纬度
        public double y = 0;
        public int lu = 0;
        //是否为交叉点
        public bool isCross;
        //是否为红点，如果源点到该点的最短路径已经求出，该点变为红点
        public bool isRed;
        //是否为源点
        public bool isSource;

        public Point(string name)
        {
            this.name = name;
        }

    }
}
