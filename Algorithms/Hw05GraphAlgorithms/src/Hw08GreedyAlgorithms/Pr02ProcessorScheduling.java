package Hw08GreedyAlgorithms;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.Scanner;

public class Pr02ProcessorScheduling {
    public static void main(String[] args) {
        Scanner sc = new Scanner(System.in);
        int tasks = Integer.parseInt(sc.nextLine().split(" ")[1]);
        int ticks = 0;
        HashMap<Integer, ArrayList<Value>> valuesByDeadline = new HashMap<>();
        for (int i = 0; i < tasks; i++) {
            String[] input = sc.nextLine().split("\\W+");
            int value = Integer.parseInt(input[0]);
            int deadline = Integer.parseInt(input[1]);
            if(deadline > ticks) {
                ticks = deadline;
            }
            if(!valuesByDeadline.containsKey(deadline)) {
                valuesByDeadline.put(deadline, new ArrayList<>());
            }
            valuesByDeadline.get(deadline).add(new Value(value, i + 1));
        }
        int totalValue = 0;
        PriorityQueueMax<Value> valueQueue = new PriorityQueueMax<>();
        int[] outputIndexes = new int[ticks];
        for (int i = ticks; i >= 1; i--) {
            for (Value val : valuesByDeadline.get(i)) {
                valueQueue.enqueue(val);
            }
            Value maxValue = valueQueue.extractMax();
            totalValue += maxValue.amount;
            outputIndexes[i - 1] = maxValue.index;
        }
        printOutput(totalValue, outputIndexes);
    }

    private static void printOutput(int totalValue, int[] outputIndexes) {
        System.out.print("Optimal schedule: ");
        for (int i = 0; i < outputIndexes.length; i++) {
            if (i == 0) {
                System.out.print(outputIndexes[i]);
            } else {
                System.out.printf(" -> %d", outputIndexes[i]);
            }
        }
        System.out.printf("%nTotal value: %d%n", totalValue);
    }

    static class Value implements Comparable<Value> {
        public int amount;
        public int index;

        public Value(int amount, int index) {
            this.amount = amount;
            this.index = index;
        }

        @Override
        public int compareTo(Value o) {
            return Integer.compare(this.amount, o.amount);
        }
    }
}
