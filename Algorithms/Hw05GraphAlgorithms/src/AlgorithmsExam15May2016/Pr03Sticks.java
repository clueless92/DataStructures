package AlgorithmsExam15May2016;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.util.*;

public class Pr03Sticks {
    private static class PriorityQueueMax<T extends Comparable<T>> {
        private LinkedHashMap<T, Integer> searchCollection;
        private List<T> heap;

        PriorityQueueMax() {
            this.heap = new ArrayList<>();
            this.searchCollection = new LinkedHashMap<>();
        }

        public int size() {
            return this.heap.size();
        }

        T extractMax() {
            T max = this.heap.get(0);
            T last = this.heap.get(this.heap.size() - 1);
            this.searchCollection.put(last, 0);
            this.heap.set(0, last);
            this.heap.remove(this.heap.size() - 1);
            if (this.heap.size() > 0) {
                this.heapifyDown(0);
            }

            this.searchCollection.remove(max);
            return max;
        }

        public T peekMax() {
            return this.heap.get(0);
        }

        void enqueue(T element) {
            this.searchCollection.put(element, this.heap.size());
            this.heap.add(element);
            this.heapifyUp(this.heap.size() - 1);
        }

        private void heapifyDown(int i)
        {
            int left = (2 * i) + 1;
            int right = (2 * i) + 2;
            int smallest = i;
            if (left < this.heap.size() && this.heap.get(left).compareTo(this.heap.get(smallest)) > 0) {
                smallest = left;
            }

            if (right < this.heap.size() && this.heap.get(right).compareTo(this.heap.get(smallest)) > 0) {
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
            while (i > 0 && this.heap.get(i).compareTo(this.heap.get(parent)) > 0) {
                T old = this.heap.get(i);
                this.searchCollection.put(old, parent);
                this.searchCollection.put(this.heap.get(parent), i);
                this.heap.set(i, this.heap.get(parent));
                this.heap.set(parent, old);
                i = parent;
                parent = (i - 1) / 2;
            }
        }

        public boolean containsKey(T element) {
            return this.searchCollection.containsKey(element);
        }

        void increaseKey(T element) {
            int index = this.searchCollection.get(element);
            this.heapifyUp(index);
        }
    }

    private static ArrayList<Integer>[] graph;
    private static int[] depCount;

    public static void main(String[] args) throws IOException {
        BufferedReader sc = new BufferedReader(new InputStreamReader(System.in));
        int stickCount = Integer.parseInt(sc.readLine());
        graph = new ArrayList[stickCount];
        depCount = new int[stickCount];

        for (int i = 0; i < stickCount; i++) {
            graph[i] = new ArrayList<>();
        }
        int edgeCount = Integer.parseInt(sc.readLine());
        for (int i = 0; i < edgeCount; i++) {
            String[] input = sc.readLine().split(" ");
            int first = Integer.parseInt(input[0]);
            int second = Integer.parseInt(input[1]);
            graph[first].add(second);
            depCount[second]++;
        }
        topSort();
    }

    private static void topSort() {
        StringBuilder output = new StringBuilder();
        PriorityQueueMax<Integer> noDep = new PriorityQueueMax<>();
        int grapthSize = graph.length;

        for (int i = 0; i < depCount.length; i++) {
            if (depCount[i] == 0) {
                noDep.enqueue(i);
                grapthSize--;
            }
        }

        while (noDep.size() > 0) {
            int curr = noDep.extractMax();
            output.append(curr).append(" ");
            for (int i = 0; i < graph[curr].size(); i++) {
                int child = graph[curr].get(i);
                depCount[child]--;
                if (depCount[child] == 0) {
                    noDep.enqueue(child);
                    grapthSize--;
                }
            }
        }

        if (grapthSize != 0) {
            System.out.println("Cannot lift all sticks");
        }
        System.out.println(output);
    }
}
