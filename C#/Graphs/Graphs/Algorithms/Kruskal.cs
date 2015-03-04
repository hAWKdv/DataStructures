namespace Graphs.Algorithms
{
    using System;
    using System.Collections.Generic;
    using Components.Contracts;
    using Contracts;
    using Components;
    using System.Linq;
    using Graphs.Components.Contracts;

    /// <summary>
    /// Finds the minimal spanning tree of the provided undirected graph using Kruskal's algorithm.
    /// WARNING: Every .FindMST() execution creates a deep copy of the graph since the output tree
    /// is a separate object (i.e. it is not interfering with graph nodes references).
    /// </summary>
    /// <typeparam name="T">IComparable generic type</typeparam>
    public sealed class Kruskal<T> : IMinimalSpanningTree<T>
        where T : IComparable 
    {
        private ISet<INode<T>> clonedNodes;

        public Kruskal()
        {
            this.Edges = new Queue<IDualEdge<T>>();
            this.clonedNodes = new HashSet<INode<T>>();
        }

        public Graph<T> Graph { get; set; }

        public Queue<IDualEdge<T>> Edges { get; private set; }

        /// <summary>
        /// Creates a deep copy of the graph and returns a new MST based on it.
        /// </summary>
        /// <returns>Returns Graph<T> minimal spanning tree</returns>
        public Graph<T> FindMST()
        {
            this.CloneNodes();
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

        private void CloneNodes()
        {
            Graph<T> clonedGraph = (Graph<T>)this.Graph.Clone();
            this.clonedNodes = clonedGraph.Nodes;
        }
    }
}
