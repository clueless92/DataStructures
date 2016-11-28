package Hw02Combinatorics;

import java.util.Arrays;
import java.util.Scanner;

public class Pr04CombinationsNoRep {
    static int[] items;
    static int[] itemsK;
    static int k;
    static int n;

    public static void main(String[] args) {
        Scanner sc = new Scanner(System.in);
        items = new int[] { 1, 2, 3, 4, 5 };
        System.out.print("k=");
        k = Integer.parseInt(sc.nextLine());
        itemsK = new int[k];
        n = items.length;
        combinationsWithNoRepetition(0, 0);
    }

    static void combinationsWithNoRepetition(int index, int start) {
        if (index >= k) {
            System.out.println(Arrays.toString(itemsK));
        }
        else {
            for (int i = start; i < items.length; i++) {
                itemsK[index] = items[i];
                combinationsWithNoRepetition(index + 1, i + 1);
            }
        }
    }
}
