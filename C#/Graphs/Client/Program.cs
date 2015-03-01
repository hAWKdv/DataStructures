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

            a.UndirectedConnection(b, 10);
            a.UndirectedConnection(c, 3);
            a.UndirectedConnection(d, 6);
            b.UndirectedConnection(c, 1);
            c.UndirectedConnection(d, 7);

            var graph = new Graph<string>();
            graph.AddNodes(a, b, c, d);

            foreach (var node in graph.Nodes)
            {
                PrintConnections(node);
            }

            //graph.RemoveNode("B");
            graph.ShortestPathStrategy = new Dijkstra<string>();
            graph.TraversingStrategy = new DFS<string>();
            Console.WriteLine(graph.CalculateShortestPath(b, d));

            TraverseManipulation<string> manipulation = delegate(INode<string> node)
            {
                Console.WriteLine("Just a test");
                Console.WriteLine(node.Value);
            };

            graph.Traverse(a, manipulation);
        }

        public static void PrintConnections(INode<string> node)
        {
            foreach (var connections in node.Connections)
            {
                Console.WriteLine("{0} -[{1}]-> {2}", node.Value, connections.Weight, connections.Node.Value);
            }

            Console.WriteLine();
        }
    }
}
