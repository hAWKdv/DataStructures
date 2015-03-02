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

        public void DisconnectFrom(INode<T> node)
        {
            if (this.AdjacentEdges.Count != 0)
            {
                this.DestroyLink(this, node);
                this.DestroyLink(node, this);
            }
        }

        public int CompareTo(INode<T> other)
        {
            return this.Value.CompareTo(other.Value);
        }

        public bool Equals(INode<T> other)
        {
            return this.Value.CompareTo(other.Value) == 0;
        }

        public object Clone()
        {
            INode<T> node = new Node<T>(this.Value);

            return node;
        }

        private void DestroyLink(INode<T> x, INode<T> y)
        {
            for (int i = 0; i < x.AdjacentEdges.Count; i++)
            {
                IEdge<T> edge = x.AdjacentEdges[i];

                if (edge.Node.Equals(y))
                {
                    x.AdjacentEdges.Remove(edge);
                }
            }
        }
    }
}
