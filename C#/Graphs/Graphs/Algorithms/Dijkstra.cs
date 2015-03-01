namespace Graphs.Algorithms
{
    using System;
    using Graphs.Components.Contracts;
    using Graphs.Exceptions.Common;

    public sealed class Dijkstra<T>
        where T : IComparable
    {
        public Dijkstra(Graph<T> graph)
        {
            this.Graph = graph;
        }

        public Graph<T> Graph { get; private set; }

        public uint CalculateShortestPath(INode<T> startNode, INode<T> targetNode)
        {
            if (!this.Graph.Nodes.Contains(startNode) || !this.Graph.Nodes.Contains(targetNode))
            {
                throw new NodeNotFoundException();
            }

            uint cost = 0;
            startNode.Cost = 0;

            while (true)
            {
                INode<T> smallest = this.GetSmallestNode();

                if (!smallest.Equals(targetNode))
                {
                    this.Traverse(smallest);
                }
                else
                {
                    cost = targetNode.Cost;
                    break;
                }
            }

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

        private void Traverse(INode<T> node)
        {
            foreach (IConnection<T> connection in node.Connections)
            {
                uint newCost = node.Cost + connection.Weight;

                if (connection.Node.Cost > newCost)
                {
                    connection.Node.Cost = newCost;
                }
            }

            node.IsVisited = true;
        }
    }
}
