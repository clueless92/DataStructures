package Hw06AdvancedGraphAlgorithms;

public class Edge implements Comparable<Edge> {
    private int startNode;
    private int endNode;
    private int distance;

    public Edge(int startNode, int endNode, int distance) {
        this.startNode = startNode;
        this.endNode = endNode;
        this.distance = distance;
    }

    public int getStartNode() {
        return startNode;
    }

    public void setStartNode(int startNode) {
        this.startNode = startNode;
    }

    public int getEndNode() {
        return endNode;
    }

    public void setEndNode(int endNode) {
        this.endNode = endNode;
    }

    public int getDistance() {
        return distance;
    }

    public void setDistance(int distance) {
        this.distance = distance;
    }

    @Override
    public String toString()
    {
        return String.format("{%s, %s} -> %s", this.startNode, this.endNode, this.distance);
    }

    @Override
    public int compareTo(Edge o) {
        return Integer.compare(this.distance, o.distance);
    }
}
