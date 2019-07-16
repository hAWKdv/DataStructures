namespace Graphs.Algorithms.Contracts
{
    using System;
    using Graphs.Components.Contracts;

    public interface IShortestPath<T>
        where T : IComparable
    {
        Graph<T> Graph { get; set; }

        int CalculateShortestPath(INode<T> startNode, INode<T> targetNode);
    }
}
