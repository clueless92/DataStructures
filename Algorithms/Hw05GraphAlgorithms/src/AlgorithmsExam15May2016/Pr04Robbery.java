package AlgorithmsExam15May2016;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.util.*;

public class Pr04Robbery {
    private static class PriorityQueue<T extends Comparable<T>> {
        private LinkedHashMap<T, Integer> searchCollection;
        private List<T> heap;

        PriorityQueue() {
            this.heap = new ArrayList<>();
            this.searchCollection = new LinkedHashMap<>();
        }

        public int size() {
            return this.heap.size();
        }

        T extractMin() {
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

        void enqueue(T element) {
            this.searchCollection.put(element, this.heap.size());
            this.heap.add(element);
            this.heapifyUp(this.heap.size() - 1);
        }

        private void heapifyDown(int i) {
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

        void decreaseKey(T element) {
            int index = this.searchCollection.get(element);
            this.heapifyUp(index);
        }
    }

    static class Cam implements Comparable<Cam> {
        int energyFromStart;
        int id;
        boolean visited;
        boolean canMoveTo;
        int turnsFromStart;
//        Cam prev;

        Cam(int id)
        {
            this.id = id;
            this.energyFromStart = Integer.MAX_VALUE;
        }

        @Override
        public int compareTo(Cam o) {
            return Integer.compare(this.energyFromStart, o.energyFromStart);
        }
    }

    private static HashMap<Cam, Integer>[] graph;
    private static int waitCost;

    public static void main(String[] args) throws IOException {
        BufferedReader sc = new BufferedReader(new InputStreamReader(System.in));
        ArrayList<Cam> nodes = new ArrayList<>();
        String[] camStr = sc.readLine().split(" ");

        for (int i = 0; i < camStr.length; i++) {
            int lastIndex = camStr[i].length() - 1;
            char lastChar = camStr[i].charAt(lastIndex);
            nodes.add(new Cam(i));
            if(lastChar == 'w') {
                nodes.get(i).canMoveTo = true;
            }
        }
        int energy = Integer.parseInt(sc.readLine());
        waitCost = Integer.parseInt(sc.readLine());
        int startNodeId = Integer.parseInt(sc.readLine());
        int finishNodeId = Integer.parseInt(sc.readLine());
        int edgeCount = Integer.parseInt(sc.readLine());
        graph = new HashMap[nodes.size()];

        for (int i = 0; i < edgeCount; i++) {
            String[] input = sc.readLine().split(" ");
            Cam cam1 = nodes.get(Integer.parseInt(input[0]));
            Cam cam2 = nodes.get(Integer.parseInt(input[1]));
            int distance = Integer.parseInt(input[2]);
            if (graph[cam1.id] == null) {
                graph[cam1.id] = new HashMap<>();
            }
            graph[cam1.id].put(cam2, distance);
        }

        sc.close();
        findEconomyPath(nodes.get(startNodeId), nodes.get(finishNodeId));
        int result = energy - nodes.get(finishNodeId).energyFromStart;

        if (result >= 0) {
            System.out.println(result);
        } else {
            System.out.printf("Busted - need %d more energy%n", -result);
        }
    }

    // Dijkstra with minPriorityQueue (heap) and addition of weights
    private static void findEconomyPath(Cam sourceNode, Cam destinationNode) {
        PriorityQueue<Cam> minQueue = new PriorityQueue<>();
        sourceNode.visited = true;
        sourceNode.energyFromStart = 0;
        sourceNode.turnsFromStart = 0;
        minQueue.enqueue(sourceNode);

        while (minQueue.size() > 0) {
            Cam currCam = minQueue.extractMin();
            if (currCam.id == destinationNode.id) {
                break;
            }

            for (Cam child : graph[currCam.id].keySet()) {
                if(graph[child.id] == null && child.id != destinationNode.id) {
                    continue;
                }
                if (!child.visited) {
                    minQueue.enqueue(child);
                    child.visited = true;
                }
                int nrgCost = currCam.energyFromStart + graph[currCam.id].get(child);
                child.turnsFromStart = currCam.turnsFromStart + 1;
                if(currCam.turnsFromStart % 2 == 0) {
                    if (!child.canMoveTo) {
                        nrgCost += waitCost;
                        child.turnsFromStart += 1;
                    }
                } else {
                    if (child.canMoveTo) {
                        nrgCost += waitCost;
                        child.turnsFromStart += 1;
                    }
                }
                if (nrgCost < child.energyFromStart) {
                    child.energyFromStart = nrgCost;
//                    child.prev = currCam;
                    minQueue.decreaseKey(child);
                }
            }
        }
//        if (destinationNode.energyFromStart == Integer.MAX_VALUE) {
//            System.out.println("No path found");
//            return;
//        }
//        List<Integer> path = new ArrayList<>();
//        Cam current = destinationNode;
//
//        while (current != null) {
//            path.add(current.id);
//            current = current.prev;
//        }
//        Collections.reverse(path); // test
//        System.out.println(path); // test
    }
}