namespace Client
{
    using System;
    using Graphs.Components;
    using Graphs.Components.Contracts;
    using Graphs;
    using Graphs.Algorithms;

    public class Program
    {
        public static void Main()
        {
            var a = new Node<string>("A");
            var b = new Node<string>("B");
            var c = new Node<string>("C");
            var d = new Node<string>("D");

            a.ConnectWith(b, 10);
            a.ConnectWith(c, 3);
            a.ConnectWith(d, 6);
            b.ConnectWith(c, 1);
            c.ConnectWith(d, 7);

            var graph = new Graph<string>();
            graph.AddNodes(a, b, c, d);

            foreach (var node in graph.Nodes)
            {
                PrintConnections(node);
            }

            //graph.RemoveNode("B");

            Dijkstra<string> dijkstra = new Dijkstra<string>(graph);
            Console.WriteLine(dijkstra.CalculateShortestPath(b, d));
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
