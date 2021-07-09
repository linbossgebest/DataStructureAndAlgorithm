using System;
using System.Collections.Generic;
using System.Text;

namespace DijkstraDemo
{
    /// <summary>
    /// 原始顶点和远距离顶点关系类
    /// </summary>
    public class DistOriginal
    {
        public int distance;
        public int parentVert;
        public DistOriginal(int pv, int dis)
        {
            this.distance = dis;
            this.parentVert = pv;
        }
    }
}
