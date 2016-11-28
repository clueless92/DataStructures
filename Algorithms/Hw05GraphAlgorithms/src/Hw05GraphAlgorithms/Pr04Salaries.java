package Hw05GraphAlgorithms;

import java.util.*;

public class Pr04Salaries {
    private static long[] salaries;
    private static ArrayList<Integer>[] firm;

    public static void main(String[] args) {
        Scanner sc = new Scanner(System.in);
        int employeesN = sc.nextInt();
        sc.nextLine();
        String regular = new String(new char[employeesN]).replace('\0', 'N'); // pattern for regular employee
        salaries = new long[employeesN];
        firm = new ArrayList[employeesN];

        for (int i = 0; i < employeesN; i++) {
            String manageString = sc.nextLine();
            salaries[i] = 0;
            firm[i] = new ArrayList<>();
            if (manageString.equals(regular)) {
                salaries[i] = 1; // here we set regular employee salaries
                continue;
            }
            for (int c = 0; c < employeesN; c++) {
                if (manageString.charAt(c) == 'Y') {
                    firm[i].add(c); // we build a graph with neighbor lists
                }
            }
        }

        long sum = 0;
        for (int i = 0; i < employeesN; i++) {
            if (salaries[i] == 0) {
                salaries[i] = getSalaries(i);
            }

            sum += salaries[i];
        }

        System.out.println(sum);
    }

    private static long getSalaries(int node) {
//        if (salaries[node] != 0) {
//            return salaries[node];
//        }

//        if (firm[node].size() == 0) {
//            salaries[node] = 1;
//            return 1;
//        }

        for (int child : firm[node]) {
            if (salaries[child] != 0) {
                salaries[node] += salaries[child];
            } else {
                salaries[node] += getSalaries(child);
            }
        }

        return salaries[node];
    }
}
