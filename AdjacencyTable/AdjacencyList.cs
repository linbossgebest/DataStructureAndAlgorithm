using System;

/// <summary>
/// 地图邻接表结构
/// </summary>
public class AdjacencyList
{
    /// <summary>
    /// 边表结点
    /// </summary>
    public class EdgeNode
    {
        /// <summary>
        /// 顶点在数组中的下标
        /// </summary>
        public int adjvex;

        /// <summary>
        /// 指向下一个边表结点
        /// </summary>
        public EdgeNode next;

        /// <summary>
        /// 边表结点的构造函数
        /// </summary>
        /// <param name="adjvex">顶点在数组中的下标</param>
        public EdgeNode(int adjvex)
        {
            this.adjvex = adjvex;
        }
    }

    /// <summary>
    /// 顶点表
    /// </summary>
    public class VertexNode
    {
        /// <summary>
        /// 顶点储存的数据
        /// </summary>
        public string _fruitType;

        /// <summary>
        /// 指向的第一个顶点
        /// </summary>
        public EdgeNode _firstEdge;

        /// <summary>
        /// 顶点表的构造函数
        /// </summary>
        /// <param name="_fruitType">所代表的水果种类</param>
        public VertexNode(string _fruitType)
        {
            this._fruitType = _fruitType;
            this._firstEdge = null;
        }
    }

    /// <summary>
    /// 邻接表
    /// </summary>
    public class GraphAdjList
    {
        /// <summary>
        /// 顶点结点数组
        /// </summary>
        public VertexNode[] _vertexNodes;

        /// <summary>
        /// 顶点数，边数
        /// </summary>
        public int numVertexes, numEdges;

        public GraphAdjList(int numVertexes, int numEdges, string fruits)
        {
            this.numVertexes = numVertexes;
            this.numEdges = numEdges;
            string[] fruit = fruits.Split(',');
            _vertexNodes = new VertexNode[numEdges];
            for (int i = 0; i < this.numVertexes; i++)
            {
                _vertexNodes[i] = new VertexNode(fruit[i]);
            }
        }
    }

    /// <summary>
    /// 创建邻接表
    /// </summary>
    private GraphAdjList graphAdjList = new GraphAdjList(4, 5, "A,B,C,D") { };

    /// <summary>
    /// 初始化边联系，无向邻接表即为双向邻接表
    /// </summary>
    /// <param name="fromVertexNode">起始顶点结点</param>
    /// <param name="toVertexNode">目标顶点结点</param>
    public void InitEdges(int fromVertexNode, int toVertexNode)
    {
        EdgeNode temp = new EdgeNode(fromVertexNode) { next = graphAdjList._vertexNodes[toVertexNode]._firstEdge };
        graphAdjList._vertexNodes[toVertexNode]._firstEdge = temp;
        temp = new EdgeNode(toVertexNode) { next = graphAdjList._vertexNodes[fromVertexNode]._firstEdge };
        graphAdjList._vertexNodes[fromVertexNode]._firstEdge = temp;
    }

    /// <summary>
    /// 正式开始创建邻接表
    /// </summary>
    public void CreateAlGraph()
    {
        InitEdges(0, 1);
        InitEdges(0, 2);
        InitEdges(0, 3);
        InitEdges(2, 1);
        InitEdges(2, 3);
    }

    /// <summary>
    /// 展示邻接表
    /// </summary>
    public void ShowALGraph()
    {
        for (int i = 0; i < graphAdjList.numVertexes; i++)
        {
            Console.WriteLine("顶点" + i + "为:" + graphAdjList._vertexNodes[i]._fruitType + "--FirstEdge--");
            EdgeNode temp = new EdgeNode(0);
            temp = graphAdjList._vertexNodes[i]._firstEdge;
            while (temp != null)
            {
                Console.WriteLine(temp.adjvex + "--Next--");
                temp = temp.next;
            }

            Console.WriteLine("END " + graphAdjList._vertexNodes[i]._fruitType);
        }
    }
}