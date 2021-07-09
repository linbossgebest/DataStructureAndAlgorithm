using System;
using System.Collections.Generic;
using System.Text;

namespace DijkstraDemo
{
    public class Vertex
    {
        /// <summary>
        /// 该点是否已访问
        /// </summary>
        public bool isInTree;

        /// <summary>
        /// 该店信息名称
        /// </summary>
        public string label;
        public Vertex(string label)
        {
            this.label = label;
            this.isInTree = false;
        }
    }
}
