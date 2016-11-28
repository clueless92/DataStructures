package AlgorithmsExam29May2016;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.util.*;

public class Pr03EvaluationSlow {

    private static final int UNREACHABLE = Integer.MAX_VALUE >> 1;

    public static void main(String[] args) throws IOException {
        BufferedReader reader = new BufferedReader(new InputStreamReader(System.in));
        int roomCount = Integer.parseInt(reader.readLine());
        int[][] graph = new int[roomCount][roomCount];
        for (int i = 0; i < roomCount; i++) {
            for (int j = 0; j < roomCount; j++) {
                if (i == j) {
                    graph[i][j] = 0;
                } else {
                    graph[i][j] = UNREACHABLE;
                }
            }
        }

        String[] exitStr = reader.readLine().split(" ");
        HashSet<Integer> exits = new HashSet<>();
        for (int i = 0; i < exitStr.length; i++) {
            exits.add(Integer.parseInt(exitStr[i]));
        }

        int inputLen = Integer.parseInt(reader.readLine());
        for (int i = 0; i < inputLen; i++) {
            String[] edge = reader.readLine().split(" ");
            int nodeA = Integer.parseInt(edge[0]);
            int nodeB = Integer.parseInt(edge[1]);
            String[] timeStr = edge[2].split(":");
            int time = 60 * Integer.parseInt(timeStr[0]) + Integer.parseInt(timeStr[1]);
            graph[nodeA][nodeB] = time;
            graph[nodeB][nodeA] = time;
        }

        String[] minTimeStr = reader.readLine().split(":");
        int minTime = 60 * Integer.parseInt(minTimeStr[0]) + Integer.parseInt(minTimeStr[1]);

        // Floyd-Warshall with mod
        for (int k = 0; k < roomCount; k++) {
            for (int i = 0; i < roomCount; i++) {
                for (int j = 0; j < graph.length; j++) {
                    if (graph[i][k] + graph[k][j] < graph[i][j]) {
                        graph[i][j] = graph[i][k] + graph[k][j];
                    }
                }
            }
        }

        StringBuilder output = new StringBuilder();
        TreeMap<Integer, Integer> unsafeMap = new TreeMap<>();
        TreeMap<Integer, Integer> safeMap = new TreeMap<>();
        for (int roomID = 0; roomID < roomCount; roomID++) {
            for (int exit : exits) {
                if (exits.contains(roomID)) {
                    continue;
                }
                if (graph[roomID][exit] == UNREACHABLE) {
                    if (!safeMap.containsKey(roomID)) {
                        unsafeMap.put(roomID, UNREACHABLE);
                    }
                } else if (graph[roomID][exit] > minTime) {
                    if (!safeMap.containsKey(roomID)) {
                        if (unsafeMap.containsKey(roomID)) {
                            if (unsafeMap.get(roomID) > graph[roomID][exit]) {
                                unsafeMap.put(roomID, graph[roomID][exit]);
                            }
                        } else {
                            unsafeMap.put(roomID, graph[roomID][exit]);
                        }
                    }
                } else {
                    safeMap.put(roomID, graph[roomID][exit]);
                    if (unsafeMap.containsKey(roomID)) {
                        unsafeMap.remove(roomID);
                    }
                }
            }
        }

        if (unsafeMap.size() == 0) {
            System.out.println("Safe");
            int id = -1;
            int time = -1;
            for (Map.Entry<Integer, Integer> entry : safeMap.entrySet()) {
                if (entry.getValue() > time) {
                    time = entry.getValue();
                    id = entry.getKey();
                }
            }
            int hours = time / 3600;
            time %= 3600;
            int mins = time / 60;
            int secs = time % 60;
            System.out.printf("%d (%02d:%02d:%02d)%n", id, hours, mins, secs);
        } else {
            System.out.println("Unsafe");
            for (Map.Entry<Integer, Integer> entry : unsafeMap.entrySet()) {
                int time = entry.getValue();
                if (time == UNREACHABLE) {
                    output.append(entry.getKey()).append(" (unreachable), ");
                } else {
                    int hours = time / 3600;
                    time %= 3600;
                    int mins = time / 60;
                    int secs = time % 60;
                    output.append(String.format("%d (%02d:%02d:%02d), ", entry.getKey(), hours, mins, secs));
                }
            }
            output.setLength(output.length() - 2);
            System.out.println(output);
        }
    }
}
