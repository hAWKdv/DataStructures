namespace Graphs.Components
{
    using System;
    using System.Collections.Generic;
    using Contracts;

    public sealed class Node<T> : INode<T>
        where T : IComparable
    {
        private List<IConnection<T>> connections;

        public Node()
        {
            this.connections = new List<IConnection<T>>();
            this.Cost = uint.MaxValue;
        }

        public Node(T value)
            : this()
        {
            this.Value = value;
        }

        public T Value { get; set; }

        public uint Cost { get; set; }

        public bool IsVisited { get; set; }

        public IList<IConnection<T>> Connections
        {
            get
            {
                return this.connections;
            }
        }

        public void ConnectWith(INode<T> node, uint weight = 0)
        {
            this.Connections.Add(new Connection<T>(node, weight));
            node.Connections.Add(new Connection<T>(this, weight));
        }

        public void ConnectTo(INode<T> node, uint weight = 0)
        {
            this.Connections.Add(new Connection<T>(node, weight));
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
