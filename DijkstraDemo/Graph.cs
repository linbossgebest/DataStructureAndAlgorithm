using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace DijkstraDemo
{
    public class Graph
    {
        private const int max_verts = 20;
        int infinity = 1000000;
        Vertex[] vertexList;//顶点数组
        public int[,] adjMat;//两个顶点构成线
        int nVerts;//顶点个数
        int nTree;//
        DistOriginal[] sPath;//点与点之间的距离数组
        int currentVert;//当前顶点
        int startToCurrent;//起点到当前顶点的距离


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="verticesnum">构造的顶点数</param>
        public Graph(int verticesnum)
        {
            //this.nVerts = verticesnum;
            vertexList = new Vertex[verticesnum];
            adjMat = new int[verticesnum, verticesnum];
            nTree = 0;
            nVerts = 0;
            for (int i = 0; i < verticesnum; i++)
                for (int j = 0; j < verticesnum; j++)
                    adjMat[i, j] = infinity;

            sPath = new DistOriginal[verticesnum];
        }

        /// <summary>
        /// 添加顶点
        /// </summary>
        /// <param name="label"></param>
        public void AddVertex(string label)
        {
            vertexList[nVerts] = new Vertex(label);
            nVerts++;
        }

        /// <summary>
        /// 添加两个顶点间的连线及权重
        /// </summary>
        /// <param name="start">起点</param>
        /// <param name="end">终点</param>
        /// <param name="weight">权重</param>
        public void AddEdge(int start, int end,int weight)
        {
            adjMat[start, end] = weight;
        }

        /// <summary>
        /// 计算最短路径
        /// </summary>
        public void Path()
        {
            int startTree = 0;
            vertexList[startTree].isInTree = true;
            nTree = 1;
            for (int i = 0; i < nVerts; i++)//初始化起点到各个顶点的距离
            {
                int tempDist = adjMat[startTree, i];
                sPath[i] = new DistOriginal(startTree, tempDist);
            }
            while (nTree < nVerts)
            {
                int indexMin = GetMin();
                int minDist = sPath[indexMin].distance;
                currentVert = indexMin;
                startToCurrent = sPath[indexMin].distance;
                vertexList[currentVert].isInTree = true;
                nTree++;
                AdjustShortPath();
            }

            DisplayPaths();
            nTree = 0;
            for (int j = 0; j <= nVerts - 1; j++)
                vertexList[j].isInTree = false;
        }

        /// <summary>
        /// 获取未被访问最短距离顶点下标
        /// </summary>
        /// <returns></returns>
        public int GetMin()
        {
            int minDist = infinity;
            int indexMin = 0;
            for (int i = 1; i < nVerts; i++)
            {
                if (!vertexList[i].isInTree && sPath[i].distance < minDist)//未被访问的顶点&&起点到该顶点距离最短 ==> 顶点
                {
                    minDist = sPath[i].distance;
                    indexMin = i;
                }
            }
            return indexMin;
        }

        /// <summary>
        /// 调整最短路径
        /// </summary>
        public void AdjustShortPath()
        {
            int column = 1;
            while (column < nVerts)
            {
                if (vertexList[column].isInTree)
                    column++;
                else 
                {
                    int sPathDist = sPath[column].distance;//起点A到column顶点的距离
                    int currentToFringe = adjMat[currentVert, column];//当前顶点到column顶点的距离
                    int startToFringe = startToCurrent + currentToFringe;//更新后的距离  startToCurrent==>起点到当前顶点的距离
                    if (startToFringe < sPathDist)// 起点->当前顶点->column顶点的距离 < 起点->column顶点的距离
                    {
                        sPath[column].parentVert = currentVert;
                        sPath[column].distance = startToFringe;
                    }
                    column++;
                }
            }
        }

        /// <summary>
        /// 展示路径（最短路径 + 该点路径所经过的上一个节点）
        /// </summary>
        public void DisplayPaths()
        {
            for (int j = 0; j < nVerts; j++)
            {
                Console.Write(vertexList[j].label + "=");
                if (sPath[j].distance == infinity)
                    Console.Write("inf");
                else
                    Console.Write(sPath[j].distance);
                string parent = vertexList[sPath[j].parentVert].label;
                Console.Write("(" + parent + ") ");
            }
        }
    }
}
