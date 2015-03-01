namespace Graphs.Components
{
    using System;
    using Graphs.Components.Contracts;

    public class Connection<T> : IConnection<T>
        where T : IComparable
    {
        private const uint DEF_WEIGHT = 0;

        public Connection(INode<T> node)
            : this(node, DEF_WEIGHT)
        {
        }

        public Connection(INode<T> node, uint weight)
        {
            this.Node = node;
            this.Weight = weight;
        }

        public INode<T> Node { get; private set; }

        public uint Weight { get; set; }
    }
}
