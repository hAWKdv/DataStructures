namespace Graphs.Algorithms.Contracts
{
    using System;
    using Graphs.Components.Contracts;
    using Graphs.Algorithms.Delegates;

    public interface ITraverse<T>
        where T : IComparable
    {
        Graph<T> Graph { get; set; }

        void Traverse(INode<T> startNode, TraverseManipulation<T> manipulation);
    }
}
