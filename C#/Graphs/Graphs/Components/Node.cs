namespace Graphs.Components
{
    using System;
    using System.Collections.Generic;
    using Contracts;

    public sealed class Node<T> : INode<T>
        where T : IComparable
    {
        private List<IEdge<T>> connections;

        public Node()
        {
            this.connections = new List<IEdge<T>>();
            this.Cost = int.MaxValue;
        }

        public Node(T value)
            : this()
        {
            this.Value = value;
        }

        public T Value { get; set; }

        public int Cost { get; set; }

        public bool IsVisited { get; set; }

        public IList<IEdge<T>> AdjacentEdges
        {
            get
            {
                return this.connections;
            }
        }

        public void UndirectedConnection(INode<T> node, int weight = 0)
        {
            this.AdjacentEdges.Add(new Edge<T>(node, weight));
            node.AdjacentEdges.Add(new Edge<T>(this, weight));
        }

        public void DirectedConnection(INode<T> node, int weight = 0)
        {
            this.AdjacentEdges.Add(new Edge<T>(node, weight));
        }

        public int CompareTo(INode<T> other)
        {
            return this.Value.CompareTo(other.Value);
        }

        public bool Equals(INode<T> other)
        {
            return this.Value.CompareTo(other.Value) == 0;
        }
    }
}
