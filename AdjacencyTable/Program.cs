using System;
using System.Diagnostics;

namespace AdjacencyTable
{
    class Program
    {
   
        static void Main(string[] args)
        {
            AdjacencyList alist = new AdjacencyList();
            alist.CreateAlGraph();
            alist.ShowALGraph();
            Console.ReadKey();
        }
    }

  
}
