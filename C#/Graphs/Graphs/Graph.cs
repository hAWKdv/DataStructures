namespace Graphs
{
    using System;
    using System.Collections.Generic;
    using Graphs.Components.Contracts;
    using Graphs.Algorithms.Contracts;
    using Graphs.Exceptions;
    using Graphs.Exceptions.Common;
    using Graphs.Algorithms.Delegates;

    public sealed class Graph<T> : ICloneable
        where T : IComparable
    {
        private ISet<INode<T>> nodes;
        private IShortestPath<T> shortestPathStrategy;
        private ITraverse<T> traversingStrategy;
        private IMinimalSpanningTree<T> mstStrategy;

        public Graph()
        {
            this.nodes = new HashSet<INode<T>>();
        }

        /// <summary>
        /// Returns the number of nodes in the graph
        /// </summary>
        public int Size
        {
            get
            {
                return this.nodes.Count;
            }
        }

        /// <summary>
        /// Sets a strategy algorithm for finding the shortest bath between two nodes.
        /// </summary>
        public IShortestPath<T> ShortestPathStrategy
        {
            get
            {
                return this.shortestPathStrategy;
            }

            set
            {
                this.shortestPathStrategy = value;
                this.shortestPathStrategy.Graph = this;
            }
        }

        /// <summary>
        /// Sets a strategy algorithm for traversing the graph.
        /// </summary>
        public ITraverse<T> TraversingStrategy
        {
            get
            {
                return this.traversingStrategy;
            }

            set
            {
                this.traversingStrategy = value;
                this.traversingStrategy.Graph = this;
            }
        }

        /// <summary>
        /// Sets a strategy algorithm for finding the minimal spanning tree of the graph.
        /// </summary>
        public IMinimalSpanningTree<T> MSTStrategy
        {
            get
            {
                return this.mstStrategy;
            }

            set
            {
                this.mstStrategy = value;
                this.mstStrategy.Graph = this;
            }
        }

        /// <summary>
        /// Returns reference of all nodes of the graph.
        /// </summary>
        public ISet<INode<T>> Nodes
        {
            get
            {
                return this.nodes;
            }
        }

        /// <summary>
        /// Add a single INode to the graph.
        /// </summary>
        /// <param name="node">Node to be added</param>
        public void AddNode(INode<T> node)
        {
            this.nodes.Add(node);
        }

        /// <summary>
        /// Add a set of INodes to the graph.
        /// </summary>
        /// <param name="nodes">Set of nodes to be added</param>
        public void AddNodes(params INode<T>[] nodes)
        {
            foreach (INode<T> node in nodes)
            {
                this.AddNode(node);
            }
        }

        /// <summary>
        /// Remove a node from the graph.
        /// Note: All connections are destroyed.
        /// </summary>
        /// <param name="value">Generic value of the node</param>
        public void RemoveNode(T value)
        {
            INode<T> node = this.FindNode(value);

            if (node != null)
            {
                if (node.AdjacentEdges.Count != 0)
                {
                    for (int i = 0; i < node.AdjacentEdges.Count; i++)
                    {
                        IEdge<T> adjEdge = node.AdjacentEdges[i];

                        for (int j = 0; j < adjEdge.Node.AdjacentEdges.Count; j++)
                        {
                            IEdge<T> adjEdgeLinks = adjEdge.Node.AdjacentEdges[j];

                            if (adjEdgeLinks.Node.Equals(node))
                            {
                                adjEdge.Node.AdjacentEdges.Remove(adjEdgeLinks);
                            }
                        }
                    }
                }

                this.nodes.Remove(node);
            }
        }

        /// <summary>
        /// Finds a node in the graph by a provided generic value.
        /// </summary>
        /// <param name="value">Value of the node</param>
        /// <returns>Returns INode or null if there isn't such node</returns>
        public INode<T> FindNode(T value)
        {
            foreach (INode<T> node in this.nodes)
            {
                if (node.Value.Equals(value))
                {
                    return node;
                }
            }

            return null;
        }

        /// <summary>
        /// Calculates the cost of the shorted path between the nodes.
        /// </summary>
        /// <param name="startNode">Start INode</param>
        /// <param name="targetNode">Target INode</param>
        /// <returns>Returns an integer with the total cost of the path</returns>
        public int CalculateShortestPath(INode<T> startNode, INode<T> targetNode)
        {
            if (this.ShortestPathStrategy == null)
            {
                throw new NullAlgorithmStrategyException("The shortest path strategy of the graph is not set.");
            }

            return this.ShortestPathStrategy.CalculateShortestPath(startNode, targetNode);
        }

        /// <summary>
        /// Peforms a traversal of the whole graph.
        /// </summary>
        /// <param name="startNode">Start node</param>
        /// <param name="manipulation">A delegate that defines the operations executed on every visited node</param>
        public void Traverse(INode<T> startNode, TraverseManipulation<T> manipulation)
        {
            if (this.TraversingStrategy == null)
            {
                throw new NullAlgorithmStrategyException("The traversing strategy of the graph is not set.");
            }

            this.traversingStrategy.Traverse(startNode, manipulation);
        }

        /// <summary>
        /// Finds the minimal spanning tree of the graph.
        /// Note: If the strategy is Kruskal, read its description.
        /// </summary>
        /// <returns>Returns Graph<T> tree</returns>
        public Graph<T> FindMST()
        {
            if (this.MSTStrategy == null)
            {
                throw new NullAlgorithmStrategyException("The minimal spanning tree strategy of the graph is not set.");
            }

            return this.mstStrategy.FindMST();
        }

        /// <summary>
        /// Creates a deep copy/clone of the graph.
        /// </summary>
        /// <returns>Returns an object (Graph<T>)</returns>
        public object Clone()
        {
            Graph<T> clonedGraph = new Graph<T>();

            foreach (INode<T> node in this.Nodes)
            {
                INode<T> clonedNode = clonedGraph.FindNode(node.Value);
                this.CloneNullNode(ref clonedNode, node, clonedGraph);

                foreach (IEdge<T> edge in node.AdjacentEdges)
                {
                    INode<T> edgeNode = clonedGraph.FindNode(edge.Node.Value);
                    this.CloneNullNode(ref edgeNode, edge.Node, clonedGraph);

                    clonedNode.DirectedConnection(edgeNode, edge.Weight);
                }
            }

            return clonedGraph;
        }

        private void CloneNullNode(ref INode<T> nodeClone, INode<T> node, Graph<T> graphClone)
        {
            if (nodeClone == null)
            {
                nodeClone = (INode<T>)node.Clone();
                graphClone.AddNode(nodeClone);
            }
        }
    }
}
