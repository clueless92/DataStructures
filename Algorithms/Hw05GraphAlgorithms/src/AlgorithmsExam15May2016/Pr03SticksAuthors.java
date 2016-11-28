package AlgorithmsExam15May2016;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.util.ArrayList;
import java.util.List;

public class Pr03SticksAuthors {
    public static void main(String[] args) throws IOException {
        BufferedReader sc = new BufferedReader(new InputStreamReader(System.in));
        int stickCount = Integer.parseInt(sc.readLine());
        List<Integer>[] graph = new ArrayList[stickCount];

        for (int i = 0; i < graph.length; i++) {
            graph[i] = new ArrayList<>();
        }
        int edgeCount = Integer.parseInt(sc.readLine());
        int[] parentCount = new int[stickCount];

        for (int i = 0; i < edgeCount; i++) {
            String[] input = sc.readLine().split(" ");
            int first = Integer.parseInt(input[0]);
            int second = Integer.parseInt(input[1]);
            graph[first].add(second);
            parentCount[second]++;
        }
        boolean[] lifted = new boolean[stickCount];
        boolean cycle = false;
        StringBuilder liftOrder = new StringBuilder();

        for (int i = 0; i < stickCount; i++) {
            int nextStick = -1;
            // Get next stick to lift
            for (int j = 0; j < stickCount; j++) {
                if (!lifted[j] && parentCount[j] == 0 && j > nextStick) {
                    nextStick = j;
                }
            }

            if (nextStick == -1) {// There are still sticks but no stick has 0 parents
                cycle = true;
                break;
            }

            for (int c = 0; c < graph[nextStick].size(); c++) {
                int child = graph[nextStick].get(c);
                parentCount[child]--;
            }

            liftOrder.append(nextStick).append(" ");
            lifted[nextStick] = true;
        }

        if (cycle) {
            System.out.println("Cannot lift all sticks");
        }
        System.out.println(liftOrder);
    }
}
