package Hw05GraphAlgorithms;

public class Pr01DistanceBetweenVertices {

    static final int U = Integer.MAX_VALUE >> 1; // unknown

    static int[][] graph = {
            //  1  2  3  4  5  6  7  8
            {0, U, U, 1, U, U, U, U}, // 1
            {U, 0, U, 1, U, U, U, U}, // 2
            {U, U, 0, 1, 1, U, U, U}, // 3
            {U, U, U, 0, U, 1, U, U}, // 4
            {U, U, 1, U, 0, U, 1, 1}, // 5
            {U, U, U, U, U, 0, U, U}, // 6
            {U, U, U, U, U, U, 0, 1}, // 7
            {U, U, U, U, U, U, U, 0}, // 8
    };

    public static void main(String[] args) {
        for (int k = 0; k < graph.length; k++) {
            for (int i = 0; i < graph.length; i++) {
                for (int j = 0; j < graph.length; j++) {
                    if (graph[i][k] + graph[k][j] < graph[i][j]) {
                        graph[i][j] = graph[i][k] + graph[k][j];
                    }
                }
            }
        }

        System.out.println(graph[0][5] == U ? -1 : graph[0][5]);
        System.out.println(graph[0][4] == U ? -1 : graph[0][4]);
        System.out.println(graph[4][5] == U ? -1 : graph[4][5]);
        System.out.println(graph[4][7] == U ? -1 : graph[4][7]);
    }
}
