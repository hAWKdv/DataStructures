namespace Graphs.Algorithms
{
    using System;
    using Graphs.Components.Contracts;
    using Graphs.Exceptions;
    using Contracts;
    using Graphs.Algorithms.Common;

    public sealed class Dijkstra<T> : AlgorithmCore<T>, IShortestPath<T>
        where T : IComparable
    {
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

            while (true)
            {
                INode<T> smallest = this.GetSmallestNode();

                if (!smallest.Equals(targetNode))
                {
                    this.CalculateNeighboursCost(smallest);
                }
                else
                {
                    cost = targetNode.Cost;
                    break;
                }
            }

            this.UnvisitAllNodes();

            return cost;
        }

        private INode<T> GetSmallestNode()
        {
            INode<T> smallest = null;
            bool isSet = false;

            foreach (INode<T> node in this.Graph.Nodes)
            {
                if ((!isSet && !node.IsVisited) ||
                    (smallest != null && smallest.Cost > node.Cost && !node.IsVisited))
                {
                    smallest = node;
                    isSet = true;
                }
            }

            return smallest;
        }

        private void CalculateNeighboursCost(INode<T> node)
        {
            foreach (IEdge<T> connection in node.AdjacentEdges)
            {
                int newCost = node.Cost + connection.Weight;

                if (connection.Node.Cost > newCost)
                {
                    connection.Node.Cost = newCost;
                }
            }

            node.IsVisited = true;
        }
    }
}
