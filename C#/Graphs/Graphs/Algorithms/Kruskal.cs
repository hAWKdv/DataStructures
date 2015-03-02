namespace Graphs.Algorithms
{
    using System;
    using System.Collections.Generic;
    using Components.Contracts;
    using Contracts;
    using Components;

    public class Kruskal<T> : IMinimalSpanningTree<T>
        where T : IComparable 
    {
        public Kruskal()
        {
            this.Edges = new HashSet<IDualEdge<T>>();
        }

        public Graph<T> Graph { get; set; }

        public ISet<IDualEdge<T>> Edges { get; private set; }

        public Graph<T> FindMST()
        {
            this.FillEdges();

            foreach (var item in this.Edges)
            {
                Console.WriteLine("[{0}, {1}] -> {2}", item.FirstNode.Value, item.SecondNode.Value, item.Weight);
            }
            Console.WriteLine();

            return null;
        }

        private void FillEdges()
        {
            this.Edges.Clear();

            foreach (var node in this.Graph.Nodes)
            {
                foreach (var edge in node.AdjacentEdges)
                {
                    var test = new DualEdge<T>(node, edge.Node, (int)edge.Weight);
                    var test2 = new DualEdge<T>(edge.Node, node, (int)edge.Weight);

                    Console.WriteLine(test.Equals(test2));

                    this.Edges.Add(test);
                }
            }
        }
    }
}
