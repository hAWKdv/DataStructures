namespace Graphs.Components.Contracts
{
    using System;

    public interface IEdge<T>
        where T : IComparable
    {
        INode<T> Node { get; }

        uint Weight { get; }
    }
}
