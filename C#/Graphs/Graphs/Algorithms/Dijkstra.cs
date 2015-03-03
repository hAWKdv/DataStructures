namespace Graphs.Algorithms
{
    using System;
    using Graphs.Components.Contracts;
    using Graphs.Exceptions;
    using Contracts;
    using Graphs.Algorithms.Common;
    using System.Collections.Generic;

    public sealed class Dijkstra<T> : AlgorithmCore<T>, IShortestPath<T>
        where T : IComparable
    {
        private ISet<INode<T>> nodes;

        public Dijkstra()
        {
        }

        public int CalculateShortestPath(INode<T> startNode, INode<T> targetNode)
        {
            if (!this.Graph.Nodes.Contains(startNode) || !this.Graph.Nodes.Contains(targetNode))
            {
                throw new NodeNotFoundException();
            }

            int cost = 0;
            startNode.Cost = 0;

            this.nodes = new HashSet<INode<T>>(this.Graph.Nodes);

            while (this.nodes.Count > 0)
            {
                INode<T> smallest = this.GetSmallestNode();

                if (!smallest.Equals(targetNode))
                {
                    this.CalculateAdjacentNodesCost(smallest);
                    this.nodes.Remove(smallest);
                }
                else
                {
                    cost = targetNode.Cost;
                    break;
                }
            }

            this.ResetNodesCosts();

            return cost;
        }

        private INode<T> GetSmallestNode()
        {
            INode<T> smallest = null;
            bool isSet = false;

            foreach (INode<T> node in this.nodes)
            {
                if ((!isSet) || (smallest != null && smallest.Cost > node.Cost))
                {
                    smallest = node;
                    isSet = true;
                }
            }

            return smallest;
        }

        private void CalculateAdjacentNodesCost(INode<T> node)
        {
            foreach (IEdge<T> edge in node.AdjacentEdges)
            {
                int newCost = node.Cost + edge.Weight;

                if (edge.Node.Cost > newCost)
                {
                    edge.Node.Cost = newCost;
                }
            }
        }

        private void ResetNodesCosts()
        {
            foreach (INode<T> node in this.Graph.Nodes)
            {
                node.Cost = int.MaxValue;
            }
        }
    }
}
