using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DijkstraDemo5
{
    /// <summary>
    /// 迪杰斯特拉算法
    /// </summary>
    public class Dijkstra
    {
        /// <summary>节点集合</summary>
        public ConcurrentDictionary<String, Node> LN { get; set; }

        /// <summary>开始节点</summary>
        public Node StartNode { get; set; }

        /// <summary>结束节点</summary>
        public Node EndNode { get; set; }

        /// <summary>Dijkstra构造函数</summary>
        /// <param name="list">节点集合</param>
        /// <param name="start">开始节点</param>
        /// <param name="end">结束节点</param>
        public Dijkstra(ConcurrentDictionary<String, Node> list, String start, String end)
        {
            LN = list;
            Init(start, end);
        }

        /// <summary>Dijkstra构造函数</summary>
        /// <param name="list">节点集合</param>
        /// <param name="start">开始节点</param>
        /// <param name="end">结束节点</param>
        public Dijkstra(IEnumerable<Map> list, String start, String end)
        {
            LN = InitNode(list);
            Init(start, end);
        }

        /// <summary>查找最短路径</summary>
        public bool Find()
        {
            return FindMin(new List<Node> { StartNode }, EndNode);
        }

        /// <summary>初始化</summary>
        private void Init(String start, String end)
        {
            StartNode = LN[start];
            EndNode = LN[end];
            if (StartNode == null || EndNode == null)
            {
                throw new ArgumentNullException();//空异常
            }
            StartNode.SetRank(null);
            StartNode.IsFind = true;

            InitRank(new List<Node> { StartNode });
        }

        /// <summary>初始化点阵的Rank </summary>
        /// <param name="srcs">节点集合</param>
        private void InitRank(IEnumerable<Node> srcs)
        {
            var nextNode = new List<Node>();
            foreach (var node in srcs)
            {
                foreach (var edge in node.LE)
                {
                    edge.CurrentNode.SetRank(node);
                    if (edge.CurrentNode.Rank == (node.Rank + 1) && !nextNode.Contains(edge.CurrentNode))
                        nextNode.Add(edge.CurrentNode);
                }
            }
            if (nextNode.Count > 0) InitRank(nextNode);
        }

        /// <summary>查找</summary>
        /// <param name="srcs">开始结点集合</param>
        /// <param name="dest">结束节点</param>
        private bool FindMin(List<Node> srcs, Node dest)
        {
            dest.GetRank();
            var minLen = 0;
            var isFind = false;
            var nextNodes = new List<Node>();
            string tmpPath;
            foreach (var node in srcs)
            {
                if (node.Equals(dest)) return false;
                foreach (var edge in node.LE)
                {
                    var tempDestRank = edge.CurrentNode.Rank;
                    if (tempDestRank != (node.Rank + 1)) continue;

                    if (!nextNodes.Contains(edge.CurrentNode))
                    {
                        nextNodes.Add(edge.CurrentNode);
                    }
                    edge.CurrentNode.MinDistance = node.MinDistance + edge.Weight;
                    if (!edge.CurrentNode.Equals(dest)) continue;

                    minLen = node.MinDistance + edge.Weight;
                    isFind = true;
                    break;
                }
            }

            if (isFind)
            {
                foreach (var node in srcs)
                {
                    tmpPath = FindMinx(node, dest, node.MinDistance, node.Rank, "", ref minLen);
                    if (tmpPath == "") continue;
                    dest.Path = node.Path + tmpPath;
                    dest.MinDistance = minLen;
                }
            }
            else
            {
                foreach (var next in nextNodes)
                {
                    minLen = -1;
                    foreach (var node in srcs)
                    {
                        if (minLen == -1) minLen = next.MinDistance;
                        tmpPath = FindMinx(node, next, node.MinDistance, node.Rank, "", ref minLen);
                        if (tmpPath == "") continue;
                        next.Path = node.Path + tmpPath;
                        next.MinDistance = minLen;
                    }
                }
                if (nextNodes.Count == 0) return false;
                FindMin(nextNodes, dest);
            }

            return isFind;
        }

        /// <summary>
        /// 寻找起始节点到目标节点的最小路径，此处采用递归查找。目标节点固定，起始节点递归。
        /// </summary>
        /// <param name="src">起始节点，为临时递归节点</param>
        /// <param name="dest">查找路径中的目标节点</param>
        /// <param name="minx">当前查找最小路径值，此值在递归中共享</param>
        /// <param name="startDist">当前节点以src节点的距离</param>
        /// <param name="srcRank">源节点src的级别</param>
        /// <param name="path">查找中经过的路径</param>
        private string FindMinx(Node src, Node dest, int startDist, int srcRank, string path, ref int minx)
        {
            var goalPath = "";
            var tmpPath1 = "," + path + ",";
            var tmpPath2 = "," + src.Path + ",";
            foreach (var node in src.LE)
            {
                string tmpPath = path;
                node.CurrentNode.SetRank(src);
                var tmpRank = node.CurrentNode.Rank;
                var tmpNodeName = "," + node.CurrentNode.Name + ",";
                //扩散级别大于等于目标级别并且是未走过的节点。
                if (tmpRank <= srcRank || tmpPath1.IndexOf(tmpNodeName, StringComparison.Ordinal) != -1 ||
                    tmpPath2.IndexOf(tmpNodeName, StringComparison.Ordinal) != -1) continue;
                var tmpLength = node.Weight + startDist;
                if (node.CurrentNode.Equals(dest))
                {
                    if (minx > tmpLength)
                    {
                        minx = tmpLength;
                        tmpPath += "," + node.CurrentNode.Name;
                        goalPath = tmpPath;
                    }
                    else if (minx == tmpLength)
                    {
                        tmpPath += "," + node.CurrentNode.Name;
                        goalPath = tmpPath;
                    }
                }
                else
                {
                    if (tmpLength >= minx) continue;
                    //路程小于最小值，查询下个子节点
                    tmpPath += "," + node.CurrentNode.Name;
                    tmpPath = FindMinx(node.CurrentNode, dest, tmpLength, srcRank, tmpPath, ref minx);
                    if (tmpPath != "")
                        goalPath = tmpPath;
                }
            }
            return goalPath;
        }

        /// <summary>初始化图</summary>
        /// <param name="list">图点集合</param>
        private ConcurrentDictionary<String, Node> InitNode(IEnumerable<Map> list)
        {
            var node = new ConcurrentDictionary<String, Node>();

            foreach (var item in list)
            {
                Node n1;
                Node n2;
                if (!node.ContainsKey(item.N1))
                {
                    n1 = new Node(item.N1);
                    node.TryAdd(item.N1, n1);
                }
                else
                {
                    n1 = node[item.N1];
                }
                if (!node.ContainsKey(item.N2))
                {
                    n2 = new Node(item.N2);
                    node.TryAdd(item.N2, n2);
                }
                else
                {
                    n2 = node[item.N2];
                }
                n1.LE.Add(new Edge(item.N2, item.Weight, n2));
            }
            return node;
        }

        #region 拷贝
        public object Clone()
        {
            return MemberwiseClone();
        }

        /// <summary>浅拷贝</summary>
        public Dijkstra CloneEntity()
        {
            return Clone() as Dijkstra;
        }
        #endregion
    }

    /// <summary>
    /// 节点
    /// </summary>
    public class Node : ICloneable
    {
        /// <summary>节点名称</summary>
        public String Name { get; set; }

        /// <summary>节点边集合</summary>
        public List<Edge> LE { get; set; }

        /// <summary>节点级别</summary>
        public Int32 Rank { get; set; }

        /// <summary>最短距离</summary>
        public Int32 MinDistance { get; set; }

        /// <summary>路径</summary>
        public String Path { get; set; }

        /// <summary>查询标识</summary>
        public bool IsFind { get; set; }

        public Node(String name)
        {
            Name = name;
            IsFind = false;
            Rank = -1;
            MinDistance = 0;
            LE = new List<Edge>();
        }

        /// <summary>设置节点级别</summary>
        /// <param name="parentNode">父节点</param>
        public void SetRank(Node parentNode)
        {
            if (Rank != -1) return;

            Rank = parentNode != null ? parentNode.Rank + 1 : 0;
        }

        /// <summary>获取节点级别</summary>
        public Int32 GetRank()
        {
            return Rank;
        }

        #region 拷贝
        public object Clone()
        {
            return MemberwiseClone();
        }

        /// <summary>浅拷贝</summary>
        public Node CloneEntity()
        {
            return Clone() as Node;
        }
        #endregion
    }

    /// <summary>
    /// 节点边
    /// </summary>
    public class Edge : ICloneable
    {
        /// <summary>边名称</summary>
        public String Name { get; set; }

        /// <summary>权值，代价 ,距离</summary>
        public Int32 Weight { get; set; }

        /// <summary>当前向量终点节点</summary>
        public Node CurrentNode { get; set; }

        public Edge(String name, Int32 weight, Node node)
        {
            Name = name;
            Weight = weight;
            CurrentNode = node;
        }

        /// <summary>设置当前节点</summary>
        /// <param name="node">当前向量终点节点</param>
        public void SetCurrentNode(Node node)
        {
            CurrentNode = node;
        }

        #region 拷贝
        public object Clone()
        {
            return MemberwiseClone();
        }

        /// <summary>浅拷贝</summary>
        public Edge CloneEntity()
        {
            return Clone() as Edge;
        }
        #endregion

    }

    /// <summary>图型</summary>
    public class Map : ICloneable
    {
        /// <summary>节点1</summary>
        public string N1 { get; set; }

        /// <summary>节点2</summary>
        public string N2 { get; set; }

        /// <summary>权值，代价 ,距离</summary>
        public int Weight { get; set; }

        public Map()
        {
        }

        public Map(string n1, string n2, int weight)
        {
            N1 = n1;
            N2 = n2;
            Weight = weight;
        }

        #region 拷贝
        public object Clone()
        {
            return MemberwiseClone();
        }

        /// <summary>浅拷贝</summary>
        public Map CloneEntity()
        {
            return Clone() as Map;
        }
        #endregion
    }
}
