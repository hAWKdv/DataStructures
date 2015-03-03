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
        private HashSet<INode<T>> nodes;
        private IShortestPath<T> shortestPathStrategy;
        private ITraverse<T> traversingStrategy;
        private IMinimalSpanningTree<T> mstStrategy;

        public Graph()
        {
            this.nodes = new HashSet<INode<T>>();
        }

        public int Size
        {
            get
            {
                return this.nodes.Count;
            }
        }

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

        public HashSet<INode<T>> Nodes
        {
            get
            {
                return this.nodes;
            }
        }

        public void AddNode(INode<T> node)
        {
            this.nodes.Add(node);
        }

        public void AddNodes(params INode<T>[] nodes)
        {
            foreach (INode<T> node in nodes)
            {
                this.AddNode(node);
            }
        }

        public void RemoveNode(T value)
        {
            INode<T> node = this.FindNode(value);

            if (node != null)
            {
                if (node.AdjacentEdges.Count != 0)
                {
                    for (int i = 0; i < node.AdjacentEdges.Count; i++)
                    {
                        var connection = node.AdjacentEdges[i];

                        for (int j = 0; j < connection.Node.AdjacentEdges.Count; j++)
                        {
                            var neighbourConnection = connection.Node.AdjacentEdges[j];

                            if (neighbourConnection.Node.Equals(node))
                            {
                                connection.Node.AdjacentEdges.Remove(neighbourConnection);
                            }
                        }
                    }
                }

                this.nodes.Remove(node);
            }
        }

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

        public int CalculateShortestPath(INode<T> startNode, INode<T> targetNode)
        {
            if (this.ShortestPathStrategy == null)
            {
                throw new NullAlgorithmStrategyException();
            }

            return this.ShortestPathStrategy.CalculateShortestPath(startNode, targetNode);
        }

        public void Traverse(INode<T> startNode, TraverseManipulation<T> manipulation)
        {
            if (this.TraversingStrategy == null)
            {
                throw new NullAlgorithmStrategyException();
            }

            this.traversingStrategy.Traverse(startNode, manipulation);
        }

        public Graph<T> FindMST()
        {
            if (this.MSTStrategy == null)
            {
                throw new NullAlgorithmStrategyException();
            }

            return this.mstStrategy.FindMST();
        }

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
