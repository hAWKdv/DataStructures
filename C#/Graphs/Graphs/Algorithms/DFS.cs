namespace Graphs.Algorithms
{
    using System;
    using System.Collections.Generic;
    using Contracts;
    using Graphs.Components.Contracts;
    using Graphs.Algorithms.Delegates;
    using Graphs.Algorithms.Common;

    public sealed class DFS<T> : AlgorithmCore<T>, ITraverse<T>
        where T : IComparable
    {
        public DFS()
        {
        }

        public void Traverse(INode<T> startNode, TraverseManipulation<T> manipulation)
        {
            Stack<INode<T>> stack = new Stack<INode<T>>();
            bool breakLoop = false;

            stack.Push(startNode);

            while (stack.Count > 0)
            {
                INode<T> currentNode = stack.Pop();

                if (!currentNode.IsVisited)
                {
                    manipulation(currentNode , ref breakLoop);

                    if (breakLoop)
                    {
                        break;
                    }

                    currentNode.IsVisited = true;

                    foreach (IEdge<T> edge in currentNode.AdjacentEdges)
                    {
                        stack.Push(edge.Node);
                    }
                }
            }

            this.UnvisitAllNodes();
        }
    }
}
