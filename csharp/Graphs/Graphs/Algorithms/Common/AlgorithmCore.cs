namespace Graphs.Algorithms.Common
{
    using System;    
    using Graphs.Components.Contracts;

    public abstract class AlgorithmCore<T>
        where T : IComparable
    {
        public AlgorithmCore()
        {
        }

        public Graph<T> Graph { get; set; }

        protected void UnvisitAllNodes()
        {
            foreach (INode<T> node in this.Graph.Nodes)
            {
                node.IsVisited = false;
            }
        }
    }
}
