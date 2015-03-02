namespace Graphs.Algorithms.Components
{
    using System;
    using Contracts;
    using Graphs.Components.Contracts;

    public sealed class DualEdge<T> : IDualEdge<T>
        where T : IComparable
    {
        public DualEdge(INode<T> firstNode, INode<T> secondNode, int weight)
        {
            this.FirstNode = firstNode;
            this.SecondNode = secondNode;
            this.Weight = weight;
        }

        public INode<T> FirstNode { get; set; }

        public INode<T> SecondNode { get; set; }

        public int Weight { get; set; }

        public bool Equals(IDualEdge<T> x, IDualEdge<T> y)
        {
            bool areMatching = x.FirstNode.Equals(y.FirstNode) && x.SecondNode.Equals(y.SecondNode);
            bool areDifferent = x.FirstNode.Equals(y.SecondNode) && x.SecondNode.Equals(y.FirstNode);

            return areMatching || areDifferent;
        }

        public int GetHashCode(IDualEdge<T> obj)
        {
            return (int)((obj.FirstNode.Cost + obj.SecondNode.Cost) % 34);
        }
    }
}
