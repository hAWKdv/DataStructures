namespace Graphs.Algorithms.Contracts
{
    using System;
    using System.Collections.Generic;
    using Graphs.Components.Contracts;
    using Graphs.Algorithms.Components.Contracts;

    public interface IMinimalSpanningTree<T>
        where T : IComparable 
    {
        Graph<T> Graph { get; set; }

        Graph<T> FindMST();
    }
}
