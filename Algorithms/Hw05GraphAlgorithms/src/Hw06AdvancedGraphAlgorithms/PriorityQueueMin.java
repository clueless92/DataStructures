package Hw06AdvancedGraphAlgorithms;

import java.util.ArrayList;
import java.util.LinkedHashMap;
import java.util.List;

public class PriorityQueueMin<T extends Comparable<T>> {
    private LinkedHashMap<T, Integer> searchCollection;
    private List<T> heap;

    public PriorityQueueMin() {
        this.heap = new ArrayList<>();
        this.searchCollection = new LinkedHashMap<>();
    }

    public int size() {
        return this.heap.size();
    }

    public T extractMin() {
        T min = this.heap.get(0);
        T last = this.heap.get(this.heap.size() - 1);
        this.searchCollection.put(last, 0);
        this.heap.set(0, last);
        this.heap.remove(this.heap.size() - 1);
        if (this.heap.size() > 0) {
            this.heapifyDown(0);
        }

        this.searchCollection.remove(min);
        return min;
    }

    public T peekMin() {
        return this.heap.get(0);
    }

    public void enqueue(T element) {
        this.searchCollection.put(element, this.heap.size());
        this.heap.add(element);
        this.heapifyUp(this.heap.size() - 1);
    }

    private void heapifyDown(int i)
    {
        int left = (2 * i) + 1;
        int right = (2 * i) + 2;
        int smallest = i;
        if (left < this.heap.size() && this.heap.get(left).compareTo(this.heap.get(smallest)) < 0) {
            smallest = left;
        }

        if (right < this.heap.size() && this.heap.get(right).compareTo(this.heap.get(smallest)) < 0) {
            smallest = right;
        }

        if (smallest != i) {
            T old = this.heap.get(i);
            this.searchCollection.put(old, smallest);
            this.searchCollection.put(this.heap.get(smallest), i);
            this.heap.set(i, this.heap.get(smallest));
            this.heap.set(smallest, old);
            this.heapifyDown(smallest);
        }
    }

    private void heapifyUp(int i) {
        int parent = (i - 1) / 2;
        while (i > 0 && this.heap.get(i).compareTo(this.heap.get(parent)) < 0) {
            T old = this.heap.get(i);
            this.searchCollection.put(old, parent);
            this.searchCollection.put(this.heap.get(parent), i);
            this.heap.set(i, this.heap.get(parent));
            this.heap.set(parent, old);
            i = parent;
            parent = (i - 1) / 2;
        }
    }

    public void decreaseKey(T element) {
        int index = this.searchCollection.get(element);
        this.heapifyUp(index);
    }
}
