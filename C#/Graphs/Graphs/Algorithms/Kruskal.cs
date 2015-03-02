namespace Graphs.Algorithms
{
    using System;
    using System.Collections.Generic;
    using Components.Contracts;
    using Contracts;
    using Components;
    using System.Linq;

    public class Kruskal<T> : IMinimalSpanningTree<T>
        where T : IComparable 
    {
        public Kruskal()
        {
            this.Edges = new List<IDualEdge<T>>();
        }

        public Graph<T> Graph { get; set; }

        public IList<IDualEdge<T>> Edges { get; private set; }

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
            ISet<IDualEdge<T>> edges = new HashSet<IDualEdge<T>>();

            foreach (var node in this.Graph.Nodes)
            {
                foreach (var edge in node.AdjacentEdges)
                {
                    edges.Add(new DualEdge<T>(node, edge.Node, edge.Weight));
                }
            }

            this.Edges = edges.OrderBy(e => e.Weight).ToList();
        }
    }
}
