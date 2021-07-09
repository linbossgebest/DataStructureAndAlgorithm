using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DijkstraDemo5
{
    public class AdjacencyList<T>
    {
        public List<Vertex<T>> items;//图的顶点集合
        public AdjacencyList() : this(10) { }//构造方法
        public AdjacencyList(int capacity)//按指定的容量进行构造
        {
            items = new List<Vertex<T>>(capacity);
        }
        public void AddVertex(T item)//添加一个节点
        {
            if (Contains(item))
            {
                throw new ArgumentException("插入了重复节点！");
            }
            items.Add(new Vertex<T>(item));
        }

        /// <summary>
        /// 添加无向边
        /// </summary>
        /// <param name="from">起始点</param>
        /// <param name="to">结束点</param>
        /// <param name="weight">权值</param>
        public void AddEdge(T from, T to,int weight)
        {
            Vertex<T> fromVer = Find(from);//找到起始节点
            if (fromVer == null)
            {
                throw new ArgumentException("头节点并不存在！");
            }
            Vertex<T> toVer = Find(to);//找到结束节点
            if (toVer == null)
            {
                throw new ArgumentException("尾节点并不存在！");
            }
            //无向边两个节点都需记录的边的信息
            AddDirectedEdge(fromVer, toVer, weight);
            AddDirectedEdge(toVer, fromVer, weight);
        }

        /// <summary>
        /// 添加有向边
        /// </summary>
        /// <param name="from">起始点</param>
        /// <param name="to">结束点</param>
        /// <param name="weight">权值</param>
        public void AddDirectEdge(T from,T to,int weight)
        {
            Vertex<T> fromVer = Find(from);//找到起始节点
            if (fromVer == null)
            {
                throw new ArgumentException("头节点不存在");
            }
            Vertex<T> toVer = Find(to);//找到结束节点
            if (toVer == null)
            {
                throw new ArgumentException("尾节点并不存在！");
            }

            AddDirectedEdge(fromVer, toVer, weight);
        }

        public bool Contains(T item)//查图中是否包含某项
        {
            foreach (Vertex<T> v in items)
            {
                if (v.data.Equals(item))
                {
                    return true;
                }
            }
            return false;
        }
        private Vertex<T> Find(T item)//查找指定项并返回
        {
            foreach (Vertex<T> v in items)
            {
                if (v.data.Equals(item))
                {
                    return v;
                }
            }
            return null;
        }
        //添加有向边
        private void AddDirectedEdge(Vertex<T> fromVer, Vertex<T> toVer,int weight)
        {
            if (fromVer.firstEdge == null)//无邻接点的时候
            {
                fromVer.firstEdge = new Node(toVer, weight);
            }
            else
            {
                Node tmp, node = fromVer.firstEdge;
                do
                {
                    //检查是否添加了重复边
                    if (node.adjvex.data.Equals(toVer.data))
                    {
                        throw new ArgumentException("添加了重复的边！");
                    }
                    tmp = node;
                    node = node.next;
                } while (node != null);
                tmp.next = new Node(toVer, weight);//添加到链表末尾。
            }
        }
        public override string ToString()//仅用于测试
        {
            //打印每个顶点和它的邻接点
            string s = string.Empty;
            foreach (Vertex<T> v in items)
            {
                s += v.data.ToString() + ":";
                if (v.firstEdge != null)
                {
                    Node tmp = v.firstEdge;
                    while (tmp != null)
                    {
                        s += " " + v.data.ToString() + "->" + tmp.adjvex.data.ToString() + " ";
                        s += " weight:" + tmp.weight;
                        tmp = tmp.next;
                    }
                }
                s += "\r\n";
            }
            return s;
        }

        //嵌套类表示链表中的表节点
        public class Node
        {
            public Vertex<T> adjvex;//邻接点域
            public int weight;//权值
            public Node next;//下一个邻接点指针域
            public Node(Vertex<T> value,int weight)
            {
                adjvex = value;
                this.weight = weight;
            }
        }

        //嵌套类表示存放数组中的表头节点
        public class Vertex<TValue>
        {
            public TValue data;//数据
         
            public Node firstEdge;//邻接链表头指针
            public bool visited;//访问标志
            public Vertex(TValue value)
            {
                data = value;
            }
        }
    }

   
}
