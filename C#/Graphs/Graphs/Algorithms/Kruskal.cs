namespace Graphs.Algorithms
{
    using System;
    using System.Collections.Generic;
    using Components.Contracts;
    using Contracts;
    using Components;
    using System.Linq;
    using Graphs.Components.Contracts;

    public sealed class Kruskal<T> : IMinimalSpanningTree<T>
        where T : IComparable 
    {
        private ISet<INode<T>> clonedNodes;
        private Graph<T> graph;

        public Kruskal()
        {
            this.Edges = new Queue<IDualEdge<T>>();
            this.clonedNodes = new HashSet<INode<T>>();
        }

        public Graph<T> Graph
        {
            get { return this.graph; }

            set
            {
                this.graph = value;
                Graph<T> clonedGraph = (Graph<T>)this.graph.Clone();
                this.clonedNodes = clonedGraph.Nodes;
            }
        }

        public Queue<IDualEdge<T>> Edges { get; private set; }

        // TODO: Optimization
        public Graph<T> FindMST()
        {
            this.FillEdges();

            Graph<T> forest = new Graph<T>();
            forest.TraversingStrategy = new DFS<T>();

            while (this.Edges.Count > 0 /* and !spanning */)
            {
                IDualEdge<T> edge = this.Edges.Dequeue();

                forest.AddNodes(edge.FirstNode, edge.SecondNode);
                edge.FirstNode.UndirectedConnection(edge.SecondNode, edge.Weight);

                forest.Traverse(edge.FirstNode, delegate(INode<T> node, ref bool breakLoop)
                {
                    int visited = 0;

                    foreach (IEdge<T> adjNode in node.AdjacentEdges)
                    {
                        if (adjNode.Node.IsVisited)
                        {
                            visited++;
                        }
                    }

                    if (visited > 1)
                    {
                        edge.FirstNode.Disconnect(edge.SecondNode);
                        breakLoop = true;
                    }
                });
            }

            return forest;
        }

        private void FillEdges()
        {
            ISet<IDualEdge<T>> edges = new HashSet<IDualEdge<T>>();

            foreach (var node in this.clonedNodes)
            {
                foreach (var edge in node.AdjacentEdges)
                {
                    edges.Add(new DualEdge<T>(node, edge.Node, edge.Weight));
                }
            }

            IDualEdge<T>[] edgesArray = edges.OrderBy(e => e.Weight).ToArray();

            this.Edges = new Queue<IDualEdge<T>>(edgesArray);

            foreach (var node in this.clonedNodes)
            {
                node.ClearAllLinks();
            }
        }
    }
}
