namespace Graphs.Algorithms.Components
{
    using System;
    using Contracts;
    using Graphs.Components.Contracts;

    public sealed class DualEdge<T> : IDualEdge<T>
        where T : IComparable
    {
        private const int HASH_CODE_C = 34;

        public DualEdge(INode<T> firstNode, INode<T> secondNode, int weight)
        {
            this.FirstNode = firstNode;
            this.SecondNode = secondNode;
            this.Weight = weight;
        }

        public INode<T> FirstNode { get; set; }

        public INode<T> SecondNode { get; set; }

        public int Weight { get; set; }

        public override bool Equals(object sec)
        {
            IDualEdge<T> y = (IDualEdge<T>)sec;

            bool areMatching = this.FirstNode.Equals(y.FirstNode) && this.SecondNode.Equals(y.SecondNode);
            bool areDifferent = this.FirstNode.Equals(y.SecondNode) && this.SecondNode.Equals(y.FirstNode);

            return areMatching || areDifferent;
        }

        public override int GetHashCode()
        {
            return (this.FirstNode.Cost + this.SecondNode.Cost) % HASH_CODE_C;
        }
    }
}
