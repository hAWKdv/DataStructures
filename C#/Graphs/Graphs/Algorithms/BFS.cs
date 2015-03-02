namespace Graphs.Algorithms
{
    using System;
    using System.Collections.Generic;
    using Contracts;
    using Graphs.Components.Contracts;
    using Graphs.Algorithms.Delegates;
    using Graphs.Algorithms.Common;

    public sealed class BFS<T> : AlgorithmCore<T>, ITraverse<T>
        where T : IComparable
    {
        public BFS()
        {
        }

        public void Traverse(INode<T> startNode, TraverseManipulation<T> manipulation)
        {
            Queue<INode<T>> queue = new Queue<INode<T>>();

            queue.Enqueue(startNode);

            while (queue.Count > 0)
            {
                INode<T> currentNode = queue.Dequeue();

                if (!currentNode.IsVisited)
                {
                    manipulation(currentNode);
                    currentNode.IsVisited = true;

                    foreach (IEdge<T> edge in currentNode.AdjacentEdges)
                    {
                        queue.Enqueue(edge.Node);
                    }
                }
            }

            this.UnvisitAllNodes();
        }
    }
}
