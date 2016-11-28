package AlgorithmsExam15May2016;

import java.util.*;

public class Pr04RobberyAuthors {
    private static class Edge {
        Edge(int parent, int child, int distance) {
            this.Parent = parent;
            this.Child = child;
            this.Distance = distance;
        }

        int Parent;

        int Child;

        int Distance;
    }

    static class Node implements Comparable<Node> {
        Node(int id, boolean isBlack) {
            this.Id = id;
            this.StartedBlack = isBlack;
            this.TurnsFromStart = 0;
            this.EnergyFromStart = Integer.MIN_VALUE;
            this.Edges = new ArrayList<>();
        }

        int Id;

        boolean StartedBlack;

        int TurnsFromStart;

        int EnergyFromStart;

        List<Edge> Edges;

        public int compareTo(Node other) {
            return Integer.compare(this.EnergyFromStart, other.EnergyFromStart);
        }
    }

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

    public static void main(String[] args) {
        Scanner sc = new Scanner(System.in);

        LinkedHashMap<Integer, Node> graph = new LinkedHashMap<>();
        String[] parameters = sc.nextLine().split(" ");
        for (String node : parameters) {
            boolean isBlack = node.endsWith("b");
            int number = Integer.parseInt(node.substring(0, node.length() - 1));
            graph.put(number, new Node(number, isBlack));
        }

        int startingEnergy = sc.nextInt();
        int waitingCost = sc.nextInt();
        int startingNode = sc.nextInt();
        int endingNode = sc.nextInt();

        int edgesCount = sc.nextInt();
        for (int i = 0; i < edgesCount; i++) {
            int parent = sc.nextInt();
            int child = sc.nextInt();
            int distance = sc.nextInt();
            graph.get(parent).Edges.add(new Edge(parent, child, distance));
        }

        Dijkstra(graph, startingNode, endingNode, waitingCost, startingEnergy);
    }

    private static void Dijkstra(LinkedHashMap<Integer, Node> graph, int startNode, int endNode, int waitCost, int startingEnergy) {
        boolean[] visited = new boolean[graph.size()];
        //Integer[] prev = new Integer[graph.size()];
        graph.get(startNode).EnergyFromStart = startingEnergy;
        PriorityQueueMax<Node> priorityQueue = new PriorityQueueMax<>();
        priorityQueue.enqueue(graph.get(startNode));

        while (priorityQueue.size() > 0) {
            Node current = priorityQueue.extractMax();

            if (current.Id == endNode) {
                break;
            }

            for (Edge edge : current.Edges) {
                if (!visited[edge.Child]) {
                    priorityQueue.enqueue(graph.get(edge.Child));
                    visited[edge.Child] = true;
                }

                int energy = current.EnergyFromStart - edge.Distance;
                int turns = 1;

                if ((current.TurnsFromStart % 2 == 0 && graph.get(edge.Child).StartedBlack) ||
                        (current.TurnsFromStart % 2 == 1 && !graph.get(edge.Child).StartedBlack)) {
                    energy -= waitCost;
                    turns = 2;
                }

                if (energy > graph.get(edge.Child).EnergyFromStart) {
                    graph.get(edge.Child).EnergyFromStart = energy;
                    graph.get(edge.Child).TurnsFromStart = current.TurnsFromStart + turns;
                    priorityQueue.increaseKey(graph.get(edge.Child));
                    //prev[edge.Child] = current.Id;
                }
            }
        }

        if (graph.get(endNode).EnergyFromStart < 0) {
            System.out.printf("Busted - need %d more energy%n", Math.abs(graph.get(endNode).EnergyFromStart));
        } else {
            System.out.println(graph.get(endNode).EnergyFromStart);
        }
    }
}
