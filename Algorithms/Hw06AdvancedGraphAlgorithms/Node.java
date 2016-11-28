package Hw06AdvancedGraphAlgorithms;

public class Node implements Comparable<Node> {
    private double percentFromStart;
    private int id;

    public Node(int id, double percent) {
        this.id = id;
        this.percentFromStart = percent;
    }

    public Node(int id)
    {
        this(id, Double.NEGATIVE_INFINITY);
    }

    public double getPercentFromStart() {
        return percentFromStart;
    }

    public void setPercentFromStart(double percentFromStart) {
        this.percentFromStart = percentFromStart;
    }

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    @Override
    public int compareTo(Node o) {
        return Double.compare(this.percentFromStart, o.percentFromStart);
    }
}
