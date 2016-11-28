package ExamPreparation;

import java.util.*;

public class Pr03GraphCycles {
    static SortedSet<Integer>[] graph;
    final static StringBuilder output = new StringBuilder();

    public static void main(String[] args) {
        Scanner sc = new Scanner(System.in);
        int n = sc.nextInt();
        sc.nextLine();
        graph = new SortedSet[n];
        for (int i = 0; i < n; i++) {
            String[] line = sc.nextLine().split(" ");
            int parent = Integer.parseInt(line[0]);
            graph[parent] = new TreeSet<>();
            if (line.length < 3) {
                continue;
            }
//            String[] children = line[1].split(" ");
            for (int c = 2; c < line.length; c++) {
                int child = Integer.parseInt(line[c]);
                graph[parent].add(child);
            }
        }

        for (int p = 0; p < n; p++) {
            for (int c : graph[p]) {
                if (p >= c) {
                    continue;
                }
                for (Integer gc : graph[c]) {
                    if (p >= gc) {
                        continue;
                    }
                    if (graph[gc].contains(p) && gc != c) {
                        output.append(String.format("{%d -> %d -> %d}%n", p, c, gc));
                    }
                }
            }
        }

        if (output.length() == 0) {
            System.out.println("No cycles found");
        } else {
            System.out.print(output);
        }
    }
}
