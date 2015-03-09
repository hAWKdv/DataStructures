namespace Graphs.Algorithms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;
    using Graphs.Algorithms.Components;
    using Graphs.Algorithms.Components.Contracts;
    using Graphs.Components.Contracts;

    public sealed class Prim<T> : IMinimalSpanningTree<T>
        where T : IComparable
    {
        public Prim()
        {
        }

        public Graph<T> Graph { get; set; }

        public Graph<T> FindMST()
        {
            Graph<T> tree = new Graph<T>();
            ISet<INode<T>> nodes = new HashSet<INode<T>>();
            INode<T> startNode = this.Graph.Nodes.FirstOrDefault();
            
            startNode.IsVisited = true;
            nodes.Add(startNode);

            while (nodes.Count < this.Graph.Nodes.Count)
            {
                IDualEdge<T> minEdge = this.FindMinimalEdge(nodes);

                this.CloneEdge(tree, minEdge);

                minEdge.SecondNode.IsVisited = true;
                nodes.Add(minEdge.SecondNode);
            }

            return tree;
        }

        private IDualEdge<T> FindMinimalEdge(ISet<INode<T>> nodes)
        {
            IDualEdge<T> minEdge = null;
            bool isSet = false;

            foreach (INode<T> node in nodes)
            {
                foreach (IEdge<T> edge in node.AdjacentEdges)
                {
                    if ((!isSet && !edge.Node.IsVisited) ||
                        (minEdge != null && edge.Weight < minEdge.Weight && !edge.Node.IsVisited))
                    {
                        minEdge = new DualEdge<T>(node, edge.Node, edge.Weight);
                        isSet = true;
                    }
                }
            }

            return minEdge;
        }

        private void CloneEdge(Graph<T> tree, IDualEdge<T> edge)
        {
            INode<T> firstNode = this.CloneNodeIfNonExistant(tree, edge.FirstNode);
            INode<T> secondNode = this.CloneNodeIfNonExistant(tree, edge.SecondNode);

            firstNode.UndirectedConnection(secondNode, edge.Weight);
        }

        private INode<T> CloneNodeIfNonExistant(Graph<T> tree, INode<T> node)
        {
            INode<T> newNode = tree.FindNode(node.Value);

            if (newNode == null)
            {
                newNode = (INode<T>)node.Clone();
                tree.AddNode(newNode);
            }

            return newNode;
        }
    }
}
