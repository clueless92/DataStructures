package Hw09ProblemSolving;
import java.util.*;

public class Pr01ShortestPathInMatrix {
    static Cell[][] matrix;
    static int rowCount;
    static int colCount;

    public static void main(String[] args) {
        Scanner sc = new Scanner(System.in);
        rowCount = sc.nextInt();
        colCount = sc.nextInt();
        matrix = new Cell[rowCount][colCount];
        for (int r = 0; r < rowCount; r++) {
            for (int c = 0; c < colCount; c++) {
                matrix[r][c] = new Cell(sc.nextInt(), r, c);
            }
        }
        Cell startCell = matrix[0][0];
        Cell endCell = matrix[rowCount - 1][colCount - 1];
        List<Cell> path = findPath(startCell, endCell);
        print(path);
    }

    private static void print(List<Cell> path) {
        System.out.printf("Length: %d%nPath:", path.get(0).distFromStart + matrix[0][0].value);
        for (int i = path.size() - 1; i >= 0; i--) {
            System.out.printf(" %d", path.get(i).value);
        }
        System.out.println();
    }

    private static List<Cell> findPath(Cell startCell, Cell endCell) {
        int[] modR = new int[] {  1,  0, -1,  0 };
        int[] modC = new int[] {  0,  1,  0, -1 };
        PriorityQueueMin<Cell> priorityQueue = new PriorityQueueMin<>();
        startCell.visited = true;
        startCell.distFromStart = 0;
        priorityQueue.enqueue(startCell);
        while (priorityQueue.size() > 0) {
            Cell currCell = priorityQueue.extractMin();
            if (currCell.equals(endCell)) {
                break;
            }
            for (int i = 0; i < 4; i++) {
                int newR = currCell.row + modR[i];
                int newC = currCell.col + modC[i];
                if (invalidCoords(newR, newC)) {
                    continue;
                }
                Cell child = matrix[newR][newC];
                if (!child.visited) {
                    priorityQueue.enqueue(child);
                    child.visited = true;
                }
                int newDist = currCell.distFromStart + child.value;
                if (newDist < child.distFromStart) {
                    child.distFromStart = newDist;
                    child.prev = currCell;
                    priorityQueue.decreaseKey(child);
                }
            }
        }
        if (endCell.distFromStart == Integer.MAX_VALUE) {
            return null;
        }
        List<Cell> path = new ArrayList<>();
        Cell current = endCell;
        while (current != null) {
            path.add(current);
            current = current.prev;
        }
        return path;
    }

    private static boolean invalidCoords(int childR, int childC) {
        return childR < 0 || childR >= rowCount ||
                childC < 0 || childC >= colCount;
    }
}

class Cell implements Comparable<Cell> {
    public int distFromStart;
    public int value;
    public int row;
    public int col;
    public Cell prev;
    public boolean visited;

    public Cell(int value, int row, int col, int distFromStart) {
        this.value = value;
        this.row = row;
        this.col = col;
        this.distFromStart = distFromStart;
        this.prev = null;
        this.visited = false;
    }

    public Cell(int value, int row, int col)
    {
        this(value, row, col, Integer.MAX_VALUE);
    }

    @Override
    public int compareTo(Cell other) {
        int result = Integer.compare(this.distFromStart, other.distFromStart);
        if (result == 0) {
            result = Integer.compare(this.row, other.row);
            if (result == 0) {
                result = Integer.compare(this.col, other.col);
            }
        }
        return result;
    }
}

class PriorityQueueMin<T extends Comparable<T>> {
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

