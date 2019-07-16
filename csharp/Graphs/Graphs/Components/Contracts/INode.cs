namespace Graphs.Components.Contracts
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Standard interface for graph nodes.
    /// </summary>
    /// <typeparam name="T">IComparable generic type</typeparam>
    public interface INode<T> : IComparable<INode<T>>, IEquatable<INode<T>>, ICloneable
        where T : IComparable
    {
        /// <summary>
        /// IComparable value of the node
        /// </summary>
        T Value { get; }

        /// <summary>
        /// Cost of the node.
        /// Set to infinity (Int32 max) by default.
        /// </summary>
        int Cost { get; set; }

        /// <summary>
        /// Determines whether the has been visited or not.
        /// Note: Used for traversal.
        /// </summary>
        bool IsVisited { get; set; }

        /// <summary>
        /// Set of adjacent nodes.
        /// </summary>
        IList<IEdge<T>> AdjacentEdges { get; }

        /// <summary>
        /// Performs dual (undirected) connection with the specified node.
        /// </summary>
        /// <param name="node">Node to connect with</param>
        /// <param name="weight">Weight of the edge</param>
        void UndirectedConnection(INode<T> node, int weight);

        /// <summary>
        /// Performs a directed connection with the node.
        /// INode this => INode node.
        /// </summary>
        /// <param name="node">Targed node</param>
        /// <param name="weight">Weight of the edge</param>
        void DirectedConnection(INode<T> node, int weight);

        /// <summary>
        /// Disconnects undirected edge.
        /// </summary>
        /// <param name="node">Node to disconnect from</param>
        void Disconnect(INode<T> node);

        /// <summary>
        /// Disconnects directed edge.
        /// </summary>
        /// <param name="node">Node to disconnect from</param>
        void DisconnectFrom(INode<T> node);

        /// <summary>
        /// Clears all adjacent edges of the node.
        /// </summary>
        void ClearAllLinks();
    }
}
