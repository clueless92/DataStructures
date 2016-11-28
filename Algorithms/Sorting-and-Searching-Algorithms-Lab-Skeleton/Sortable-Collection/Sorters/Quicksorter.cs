namespace Sortable_Collection.Sorters
{
    using System;
    using System.Collections.Generic;

    using Sortable_Collection.Contracts;

    public class Quicksorter<T> : ISorter<T> where T : IComparable<T>
    {
        public void Sort(List<T> collection)
        {
            QuickSort(collection, 0, collection.Count - 1);
        }

        private void QuickSort(List<T> array, int start, int end)
        {
            if (start >= end)
            {
                return;
            }
            T pivot = array[start];
            int storeIndex = start + 1;
            for (int i = start + 1; i <= end; i++)
			{
                if (array[i].CompareTo(pivot) < 0)
                {
                    T swapperInner = array[i];
                    array[i] = array[storeIndex];
                    array[storeIndex] = swapperInner;

                    storeIndex++;
                }
			}
            storeIndex--;

            T swapperOuter = array[storeIndex];
            array[storeIndex] = array[start];
            array[start] = swapperOuter;

            QuickSort(array, start, storeIndex - 1);
            QuickSort(array, storeIndex + 1, end);
        }
    }
}
