namespace Graphs.Algorithms
{
    using System;
    using Contracts;
    using Graphs.Components.Contracts;
    using Graphs.Algorithms.Delegates;

    public sealed class DFS<T> : ITraverse<T>
        where T : IComparable
    {
        public DFS()
        {
        }

        public Graph<T> Graph { get; set; }

        public void Traverse(INode<T> startNode, TraverseManipulation<T> manipulation)
        {
            // Just a test
            manipulation(startNode);
        }
    }
}
