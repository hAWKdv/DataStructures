namespace Graphs.Algorithms
{
    using System;
    using System.Collections.Generic;
    using Components.Contracts;
    using Contracts;
    using Components;
    using System.Linq;
    using Graphs.Components.Contracts;

    public class Kruskal<T> : IMinimalSpanningTree<T>
        where T : IComparable 
    {
        public Kruskal()
        {
            this.Edges = new Queue<IDualEdge<T>>();
        }

        public Graph<T> Graph { get; set; }

        public Queue<IDualEdge<T>> Edges { get; private set; }

        // Under heavy construction
        public Graph<T> FindMST()
        {
            this.FillEdges();

            Graph<T> forest = new Graph<T>();
            forest.TraversingStrategy = new BFS<T>();

            while (this.Edges.Count > 0)
            {
                IDualEdge<T> edge = this.Edges.Dequeue();

                if (!forest.Nodes.Contains(edge.FirstNode))
                {
                    edge.FirstNode.UndirectedConnection(edge.SecondNode, edge.Weight);
                    forest.AddNode(edge.FirstNode);
                }

                if (!forest.Nodes.Contains(edge.SecondNode))
                {
                    forest.AddNode(edge.SecondNode);
                }

                forest.Traverse(edge.FirstNode, delegate(INode<T> node)
                {
                    foreach (IEdge<T> adjEdge in node.AdjacentEdges)
                    {
                        if (adjEdge.Node.IsVisited)
                        {
                            edge.FirstNode.DisconnectFrom(edge.SecondNode);
                            return;
                        }
                    }
                });
            }

            return forest;
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

            IDualEdge<T>[] edgesArray = edges.OrderBy(e => e.Weight).ToArray();

            this.Edges = new Queue<IDualEdge<T>>(edgesArray);
        }
    }
}
