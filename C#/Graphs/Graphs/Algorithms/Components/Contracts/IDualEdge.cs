namespace Graphs.Algorithms.Components.Contracts
{
    using System;
    using Graphs.Components.Contracts;
    using System.Collections.Generic;

    public interface IDualEdge<T>
        where T : IComparable
    {
        INode<T> FirstNode { get; set; }

        INode<T> SecondNode { get; set; }

        int Weight { get; set; }
    }
}
