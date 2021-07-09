using System;
using System.Collections.Generic;
using System.Text;

namespace test
{
    public class Graph
    {
        /// <summary>
        /// 图的顶点数
        /// </summary>
        private int vertices_num;

        /// <summary>
        /// 图的顶点集合
        /// </summary>
        private Vertex[] vertices;

        /// <summary>
        /// 二维数组，两个顶点之间的连接线关系，构成邻接矩阵
        /// </summary>
        private int[,] adjMatrix;

        /// <summary>
        /// 顶点集合索引
        /// </summary>
        int numVerts;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="verticesnum"></param>
        public Graph(int verticesnum)
        {
            this.vertices_num = verticesnum;
            vertices = new Vertex[vertices_num];
            adjMatrix = new int[vertices_num, vertices_num];
            numVerts = 0;
            for (int i = 0; i < vertices_num; i++)
                for (int j = 0; j < vertices_num; j++)
                    adjMatrix[i, j] = 0;
        }

        /// <summary>
        /// 添加顶点
        /// </summary>
        /// <param name="label"></param>
        public void AddVertex(string label)
        {
            vertices[numVerts] = new Vertex(label);
            numVerts++;
        }

        /// <summary>
        /// 添加两个顶点间的连线
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public void AddEdge(int start, int end)
        {
            adjMatrix[start, end] = 1;
        }

        /// <summary>
        /// 展示顶点
        /// </summary>
        /// <param name="index"></param>
        public void ShowVertex(int index)
        {
            Console.WriteLine(vertices[index] + " ");
        }

        /// <summary>
        /// 寻找没有后继的顶点
        /// </summary>
        /// <returns></returns>
        public int NoSuccessors()
        {
            bool isEdge;
            for (int row = 0; row < vertices_num; row++)
            {
                isEdge = false;
                for (int col = 0; col < vertices_num; col++)
                {
                    if (adjMatrix[row, col] > 0)
                    {
                        isEdge = true;
                        break;
                    }
                }
                if (!isEdge)
                    return row;
            }
            return -1;//没有找到，返回-1
        }

        /// <summary>
        /// 删除顶点
        /// </summary>
        /// <param name="vert">顶点索引</param>
        public void DelVertex(int vert)
        {
            if (vert != vertices_num - 1)
            {
                for (int i = vert; i < vertices_num - 1; i++)
                    vertices[i] = vertices[i + 1];

                for (int row = vert; row < vertices_num - 1; row++)
                    MoveRow(row, vertices_num);

                for (int col = vert; col < vertices_num - 1; col++)
                    MoveCol(col, vertices_num);

            }
            vertices_num--;
        }

        /// <summary>
        /// 删除行,删除行后，该行的下一行向上移动
        /// </summary>
        /// <param name="row"></param>
        /// <param name="length"></param>
        private void MoveRow(int row, int length)
        {
            for (int col = 0; col < length; col++)
                adjMatrix[row, col] = adjMatrix[row + 1, col];
        }

        /// <summary>
        /// 删除列，删除列后，该列的右边向左边移动
        /// </summary>
        /// <param name="col"></param>
        /// <param name="length"></param>
        private void MoveCol(int col, int length)
        {
            for (int row = 0; row < length; row++)
                adjMatrix[row, col] = adjMatrix[row, col + 1];
        }

        /// <summary>
        /// 拓扑排序算法
        /// </summary>
        public void TopSort()
        {
            Stack<string> gStack = new Stack<string>();
            while (vertices_num > 0)
            {
                int currVertex = NoSuccessors();
                if (currVertex == -1)
                {
                    Console.WriteLine("Error:garph has cycles");
                    return;
                }
                gStack.Push(vertices[currVertex].label);
                DelVertex(currVertex);
            }
            Console.WriteLine("排序：");
            while (gStack.Count > 0)
                Console.WriteLine(gStack.Pop() + " ");
        }

        /// <summary>
        /// 检查未访问的相邻顶点
        /// </summary>
        /// <param name="v"></param>
        /// <returns>顶点的索引</returns>
        public int GetAdjUnvisitedVertex(int v)
        {
            for (int i = 0; i < vertices_num; i++)
            {
                if ((adjMatrix[v, i] == 1) && (vertices[v].wasVisited = false))
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// 深度优先搜索
        /// </summary>
        public void DepthFirstSearch()
        {
            Stack<int> gStack = new Stack<int>();
            vertices[0].wasVisited = true;
            ShowVertex(0);
            gStack.Push(0);
            int v;
            while (gStack.Count > 0)
            {
                v = GetAdjUnvisitedVertex(gStack.Peek());
                if (v == -1)
                    gStack.Pop();
                else
                {
                    vertices[v].wasVisited = true;
                    ShowVertex(v);
                    gStack.Push(v);
                }
            }

            ClearVertices();
        }
        
        /// <summary>
        /// 深度优先搜索
        /// </summary>
        public void BreadthFirstSearch()
        {
            Queue<int> gQueue = new Queue<int>();
            vertices[0].wasVisited = true;
            ShowVertex(0);
            gQueue.Enqueue(0);
            int vert1, vert2;
            while (gQueue.Count > 0)
            {
                vert1 = gQueue.Dequeue();
                vert2 = GetAdjUnvisitedVertex(vert1);
                while (vert2 != -1)
                {
                    vertices[vert2].wasVisited = true;
                    ShowVertex(vert2);
                    gQueue.Enqueue(vert2);
                    vert2 = GetAdjUnvisitedVertex(vert1);
                }
            }

            ClearVertices();
        }

        /// <summary>
        /// 最小生成树
        /// </summary>
        public void Mst()
        {
            Stack<int> gStack = new Stack<int>();
            vertices[0].wasVisited = true;
            gStack.Push(0);
            int currVertex, ver;
            while (gStack.Count > 0)
            {
                currVertex = gStack.Peek();
                ver = GetAdjUnvisitedVertex(currVertex);
                if (ver == -1)
                    gStack.Pop();
                else
                {
                    vertices[ver].wasVisited = true;
                    gStack.Push(ver);
                    ShowVertex(currVertex);
                    ShowVertex(ver);
                    Console.WriteLine(" ");
                }
            }

            ClearVertices();
        }

        /// <summary>
        /// 清空顶点数组被访问状态
        /// </summary>
        private void ClearVertices()
        {
            for (int i = 0; i < vertices_num; i++)
                vertices[i].wasVisited = false;
        }
    }
}
