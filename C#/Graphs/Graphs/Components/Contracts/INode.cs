namespace Graphs.Components.Contracts
{
    using System;
    using System.Collections.Generic;

    public interface INode<T> : IComparable<INode<T>>, IEquatable<INode<T>>
        where T : IComparable
    {
        /// <summary>
        /// IComparable value of the node
        /// </summary>
        T Value { get; }

        /// <summary>
        /// Cost of the node.
        /// Set to infinity by default.
        /// </summary>
        uint Cost { get; set; }

        /// <summary>
        /// 
        /// </summary>
        bool IsVisited { get; set; }

        /// <summary>
        /// Set of neighbour nodes.
        /// </summary>
        IList<IEdge<T>> Connections { get; }

        /// <summary>
        /// Performs dual (undirected) connection with the specified node
        /// </summary>
        /// <param name="node">Node to connect with</param>
        void UndirectedConnection(INode<T> node, uint weight);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <param name="weight"></param>
        void DirectedConnection(INode<T> node, uint weight);
    }
}
