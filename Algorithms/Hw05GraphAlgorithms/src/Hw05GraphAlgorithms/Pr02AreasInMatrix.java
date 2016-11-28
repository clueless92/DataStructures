package Hw05GraphAlgorithms;

import java.util.*;

public class Pr02AreasInMatrix {
    static char[][] graph;
    static boolean[][] visited;

    static int[] rowMod = {1, 0, 0, -1};
    static int[] colMod = {0, -1, 1, 0};

    public static void main(String[] args) {
        Scanner sc = new Scanner(System.in);
        HashMap<Character, Integer> charMap = new HashMap<>();
        int rows = Integer.parseInt(sc.nextLine().split(" ")[3]);
        graph = new char[rows][];
        visited = new boolean[rows][];
        for (int i = 0; i < rows; i++) {
            String line = sc.nextLine();
            graph[i] = new char[line.length()];
            visited[i] = new boolean[line.length()];
            for (int j = 0; j < line.length(); j++) {
                graph[i][j] = line.charAt(j);
            }
        }

        int areaCount = 0;
        for (int r = 0; r < rows; r++) {
            for (int c = 0; c < graph[r].length; c++) {
                if (visited[r][c]) {
                    continue;
                }

                char paint = markAreaAsVisited(r, c);
                if (!charMap.containsKey(paint)) {
                    charMap.put(paint, 0);
                }

                int val = charMap.get(paint) + 1;
                charMap.put(paint, val);
                areaCount++;
            }
        }

        System.out.printf("Areas: %d%n", areaCount);
        for (Character paint : charMap.keySet()) {
            System.out.printf("Letter \'%s\' -> %d%n", paint, charMap.get(paint));
        }
    }

    private static char markAreaAsVisited(int r, int c) {
        Queue<Integer> rowQueue = new ArrayDeque<>();
        Queue<Integer> colQueue = new ArrayDeque<>();
        rowQueue.add(r);
        colQueue.add(c);
        char paint = graph[r][c];
        while (rowQueue.size() > 0) {
            int currR = rowQueue.poll();
            int currC = colQueue.poll();
            visited[currR][currC] = true;
            for (int i = 0; i < 4; i++) {
                int newR = currR + rowMod[i];
                int newC = currC + colMod[i];
                if(newR < 0 || newR >= graph.length || newC < 0 || newC >= graph[0].length) {
                    continue;
                }
                if(visited[newR][newC] || graph[newR][newC] != paint) {
                    continue;
                }

                rowQueue.add(newR);
                colQueue.add(newC);
            }
        }

        return paint;
    }
}
