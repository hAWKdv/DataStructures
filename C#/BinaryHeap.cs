namespace PriorityQueue
{
    using System;
    using System.Collections.Generic;

    public class BinaryHeap<T> : IEnumerable<T>
        where T : IComparable<T>
    {
        private const int INIT_CAPACITY = 16;
        private const byte RESIZING_PERC = 75;
        private T[] container;
        private int count;

        public BinaryHeap()
        {
            this.count = 0;
            this.container = new T[INIT_CAPACITY];
        }

        public void Add(T element)
        {
            if (this.count == 0)
            {
                this.container[this.count] = element;
            }
            else
            {
                if (IsResizingNeeded())
                {
                    this.ResizeContainer();
                }

                this.container[this.count] = element;

                this.BalanceOnAdd(this.count);
            }

            this.count++;
        }

        public T RemoveSmallest()
        {
            if (this.count == 0)
            {
                throw new InvalidOperationException("There aren't any elements in the heap!");
            }

            T smallest = this.container[0];
            this.container[0] = this.container[this.count - 1];
            this.container[this.count - 1] = default(T);

            this.count--;

            this.BalanceOnRemove(0);

            return smallest;
        }

        private void BalanceOnAdd(int child)
        {
            int parent = this.GetParentIndex(child);

            if (parent < 0)
            {
                return;
            }

            int comparisonResult = this.container[child].CompareTo(this.container[parent]);

            // if child is bigger
            if (comparisonResult == -1)
            {
                this.SwapValues(child, parent);
                this.BalanceOnAdd(parent);
            }
        }

        private void BalanceOnRemove(int parent)
        {
            int child = this.GetSmallerChildren(parent);

            // if GetSmallerChildren reached the end (return the neutral index -1)
            if (child == -1)
            {
                return;
            }

            int comparisonResult = this.container[child].CompareTo(this.container[parent]);

            // if parent is bigger
            if (comparisonResult < 0)
            {
                this.SwapValues(child, parent);
                this.BalanceOnRemove(child);
            }
        }

        private void SwapValues(int firstIndex, int secondIndex)
        {
            T swap = this.container[firstIndex];
            this.container[firstIndex] = this.container[secondIndex];
            this.container[secondIndex] = swap;
        }

        private bool IsResizingNeeded()
        {
            int fillPercent = this.count * 100 / this.container.Length;

            if (fillPercent >= RESIZING_PERC)
            {
                return true;
            }

            return false;
        }

        private void ResizeContainer()
        {
            T[] transferContainer = new T[this.container.Length * 2];
            this.container.CopyTo(transferContainer, 0);
            this.container = new T[transferContainer.Length];
            transferContainer.CopyTo(this.container, 0);

            // Freeing some memory
            transferContainer = new T[1];
        }

        private int GetParentIndex(int childIndex)
        {
            if (childIndex % 2 == 0)
            {
                return (childIndex - 2) / 2;
            }
            else
            {
                return (childIndex - 1) / 2;
            }
        }

        private int GetSmallerChildren(int parentIndex)
        {
            int left = 2 * parentIndex + 1;
            int right = 2 * parentIndex + 2;

            // Retuns neutral value if outside of the container
            // Both children cells are empty (no children -> stop)
            if (left >= this.count && right >= this.count)
            {
                return -1;
            }
            // Only right child is empty (no right child)
            else if (right == this.count)
            {
                return left;
            }
            // Both are existing
            else
            {
                int comparisonResult = this.container[left].CompareTo(this.container[right]);

                // if left is bigger
                if (comparisonResult > 0)
                {
                    return right;
                }
                // if right is bigger or left == right
                else
                {
                    return left;
                }
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (T item in this.container)
            {
                if (item == null)
                {
                    break;
                }

                yield return item;
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
