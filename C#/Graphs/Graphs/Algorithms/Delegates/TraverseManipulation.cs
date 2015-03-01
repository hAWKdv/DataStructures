namespace Graphs.Algorithms.Delegates
{
    using System;
    using Graphs.Components.Contracts;

    public delegate void TraverseManipulation<T>(INode<T> node) where T : IComparable;
}
