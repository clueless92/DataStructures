package AlgorithmsExam06Dec2015;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.util.*;

public class Pr03MessageSharing {
    private static HashMap<String, ArrayList<String>> graph = new HashMap<>();
    private static HashMap<String, Integer> visited = new HashMap<>();
    private static ArrayList<TreeSet<String>> steps = new ArrayList<>();

    public static void main(String[] args) throws IOException {
        BufferedReader reader = new BufferedReader(new InputStreamReader(System.in));
        String[] people = reader.readLine().substring(8).split(", ");
        String[] connections = reader.readLine().substring(13).split(", ");
        String[] starts = reader.readLine().substring(7).split(", ");

        for (int i = 0; i < people.length; i++) {
            graph.put(people[i], new ArrayList<>());
            visited.put(people[i], -1);
        }
        for (int i = 0; i < connections.length; i++) {
            String[] pair = connections[i].split(" - ");
            graph.get(pair[0]).add(pair[1]);
            graph.get(pair[1]).add(pair[0]);
        }

        spreadTheWordBFS(starts);

        StringBuilder output = new StringBuilder();
        if (visited.values().stream().anyMatch(integer -> integer == -1)) {
            output.append("Cannot reach: ");
            visited.entrySet().stream()
                    .filter(entry -> entry.getValue() == -1)
                    .sorted((entry1, entry2) -> entry1.getKey().compareTo(entry2.getKey()))
                    .forEach(entry -> output.append(entry.getKey()).append(", "));
            output.setLength(output.length() - 2);
        } else {
            int lastStep = steps.size() - 1;
            output.append(String.format("All people reached in %d steps%n", lastStep));
            output.append("People at last step: ");
            for (String slow : steps.get(lastStep)) {
                output.append(slow).append(", ");
            }
            output.setLength(output.length() - 2);
        }

        System.out.println(output);
    }

    private static void spreadTheWordBFS(String[] starts) {
        Queue<String> queue = new ArrayDeque<>();
        steps.add(new TreeSet<>());
        for (int i = 0; i < starts.length; i++) {
            String name = starts[i];
            queue.add(name);
            visited.put(name, 0);
            steps.get(0).add(name);
        }

        while (!queue.isEmpty()) {
            String curr = queue.poll();
            for (String child : graph.get(curr)) {
                if (visited.get(child) == -1) {
                    queue.add(child);
                    int step = visited.get(curr) + 1;
                    visited.put(child, step);
                    if(steps.size() <= step) {
                        steps.add(new TreeSet<>());
                    }
                    steps.get(step).add(child);
                }
            }
        }
    }
}
