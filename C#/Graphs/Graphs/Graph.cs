namespace Graphs
{
    using System;
    using System.Collections.Generic;
    using Graphs.Components.Contracts;
    using Graphs.Algorithms.Contracts;
    using Graphs.Exceptions;
    using Graphs.Exceptions.Common;

    public sealed class Graph<T>
        where T : IComparable
    {
        private HashSet<INode<T>> nodes;
        private IShortestPath<T> shortestPathStrategy;

        public Graph()
        {
            this.nodes = new HashSet<INode<T>>();
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
                if (node.Connections.Count != 0)
                {
                    for (int i = 0; i < node.Connections.Count; i++)
                    {
                        var connection = node.Connections[i];

                        for (int j = 0; j < connection.Node.Connections.Count; j++)
                        {
                            var neighbourConnection = connection.Node.Connections[j];

                            if (neighbourConnection.Node.Equals(node))
                            {
                                connection.Node.Connections.Remove(neighbourConnection);
                            }
                        }
                    }
                }

                this.nodes.Remove(node);
                node = null;
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

        public uint CalculateShortestPath(INode<T> startNode, INode<T> targetNode)
        {
            if (this.ShortestPathStrategy == null)
            {
                throw new NullAlgorithmStrategyException();
            }

            return this.ShortestPathStrategy.CalculateShortestPath(startNode, targetNode);
        }
    }
}
