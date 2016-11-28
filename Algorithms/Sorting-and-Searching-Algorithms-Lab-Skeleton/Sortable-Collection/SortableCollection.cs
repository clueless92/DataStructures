namespace Sortable_Collection
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Sortable_Collection.Contracts;

    public class SortableCollection<T> where T : IComparable<T>
    {
        public SortableCollection()
        {
            this.Items = new List<T>();
        }

        public SortableCollection(IEnumerable<T> items)
        {
            this.Items = new List<T>(items);
        }

        public SortableCollection(params T[] items)
            : this(items.AsEnumerable())
        {
        }

        public List<T> Items { get; private set; }

        public int Count
        {
            get
            {
                return this.Items.Count;
            }
        }

        public void Sort(ISorter<T> sorter)
        {
            sorter.Sort(this.Items);
        }

        public int BinarySearch(T item)
        {
            return this.RecursiveBinarySearch(item, 0, this.Count - 1);
        }

        private int RecursiveBinarySearch(T item, int start, int end)
        {
            if (end < start)
            {
                return -1;
            }
            int mid = start + end >> 1;
            if (this.Items[mid].CompareTo(item) > 0)
            {
                this.RecursiveBinarySearch(item, start, mid - 1);
            }
            else if (this.Items[mid].CompareTo(item) < 0)
            {
                this.RecursiveBinarySearch(item, mid + 1, end);
            }
            return mid;
        }

        public int InterpolationSearch(T item)
        {
            throw new NotImplementedException();
        }

        public void Shuffle()
        {
            throw new NotImplementedException();
        }

        public T[] ToArray()
        {
            return this.Items.ToArray();
        }

        public override string ToString()
        {
            return string.Format("[{0}]", string.Join(", ", this.Items));
        }        
    }
}