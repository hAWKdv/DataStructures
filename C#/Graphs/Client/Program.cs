namespace Client
{
    using System;
    using Graphs.Components;
    using Graphs.Components.Contracts;
    using Graphs;
    using Graphs.Algorithms;
    using Graphs.Algorithms.Delegates;

    public class Program
    {
        public static void Main()
        {
            var a = new Node<string>("A");
            var b = new Node<string>("B");
            var c = new Node<string>("C");
            var d = new Node<string>("D");
            var e = new Node<string>("E");
            var z = new Node<string>("Z");

            a.UndirectedConnection(b, 4);
            a.UndirectedConnection(c, 2);
            b.UndirectedConnection(d, 5);
            b.UndirectedConnection(c, 1);
            c.UndirectedConnection(d, 8);
            c.UndirectedConnection(e, 10);
            d.UndirectedConnection(e, 2);
            d.UndirectedConnection(z, 6);
            e.UndirectedConnection(z, 3);

            var graph = new Graph<string>();
            graph.AddNodes(a, b, c, d, e, z);

            graph.ShortestPathStrategy = new Dijkstra<string>();
            Console.WriteLine(graph.CalculateShortestPath(a, z));

            return;

            //graph.RemoveNode("B");

            //c.DisconnectFrom(d);

            graph.MSTStrategy = new Kruskal<string>();
            var mst = graph.FindMST();

            foreach (var node in mst.Nodes)
            {
                PrintConnections(node);
            }

            return;

            graph.ShortestPathStrategy = new Dijkstra<string>();
            graph.TraversingStrategy = new DFS<string>();
            Console.WriteLine(graph.CalculateShortestPath(a, z));
 

            //graph.Traverse(a, delegate(INode<string> node)
            //{
            //    Console.WriteLine(node.Value);
            //});

            Console.WriteLine();

            graph.TraversingStrategy = new BFS<string>();
            //graph.Traverse(a, delegate(INode<string> node)
            //{
            //    Console.WriteLine(node.Value);
            //});
        }

        public static void PrintConnections(INode<string> node)
        {
            foreach (var connections in node.AdjacentEdges)
            {
                Console.WriteLine("{0} -[{1}]-> {2}", node.Value, connections.Weight, connections.Node.Value);
            }

            Console.WriteLine();
        }
    }
}
