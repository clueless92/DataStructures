package Hw05GraphAlgorithms;

import java.util.*;

public class Pr03CyclesInGraph {
    private static HashSet<String> visited;
    private static HashMap<String, ArrayList<String>> graph;
    private static boolean isCyclic;

    public static void main(String[] args)
    {
        Scanner sc = new Scanner(System.in);
//        isCyclic = false;
        graph = new HashMap<>();
        visited = new HashSet<>();
        String first = "0";
        while (true)
        {
            String line = sc.nextLine();
            if(line.equals("")) {
                break;
            }

            String start = line.charAt(0) + "";
            if(first.equals("0")) {
                first = line.charAt(0) + "";
            }

            String end = line.charAt(line.length() - 1) + "";

            if (!graph.containsKey(start))
            {
                graph.put(start, new ArrayList<>());
            }

            if (!graph.containsKey(end))
            {
                graph.put(end, new ArrayList<>());
            }

            graph.get(start).add(end);
            graph.get(end).add(start);
        }

//        checkIsAcyclicRecursive(first, null);
        isCyclic = hasCycles(first, null);
        if (isCyclic) {
            System.out.println("Acyclic: No");
        } else {
            System.out.println("Acyclic: Yes");
        }
    }

    private static boolean hasCycles(String node, String parentNode)
    {
        Stack<String> currStack = new Stack<>();
        Stack<String> parentStack = new Stack<>();
        currStack.add(node);
        parentStack.add(parentNode);

        while (currStack.size() > 0) {
            node = currStack.pop();
            parentNode = parentStack.pop();

            if (!visited.contains(node)) {
                visited.add(node);
                for (String child : graph.get(node)) {
                    if (child.equals(parentNode)) {
                        continue;
                    }

                    currStack.add(child);
                    parentStack.add(node);
                }
            } else {
                return true;
            }
        }

        return false;
    }

//    private static void checkIsAcyclicRecursive(String node, String prevNode)
//    {
//        if (!visited.contains(node))
//        {
//            visited.add(node);
//            for (String child : graph.get(node))
//            {
//                if (!child.equals(prevNode))
//                {
//                    checkIsAcyclicRecursive(child, node);
//                }
//            }
//        }
//        else
//        {
//            isCyclic = false;
//        }
//    }
}
