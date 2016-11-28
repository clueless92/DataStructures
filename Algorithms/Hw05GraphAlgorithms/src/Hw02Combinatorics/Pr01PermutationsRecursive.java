package Hw02Combinatorics;

import java.util.Arrays;
import java.util.Scanner;

public class Pr01PermutationsRecursive {
    static int permutationsCount = 0;
    static int[] arr;

    public static void main(String[] args) {
        Scanner sc = new Scanner(System.in);
        int n = Integer.parseInt(sc.nextLine());
        arr = new int[n];
        for (int i = 0; i < n; i++) {
            arr[i] = i + 1;
        }
        permuteRec(0);
        System.out.printf("Total permutations: %d%n", permutationsCount);
    }

    static void permuteRec(int index) {
        int size = arr.length;
        if (index >= size - 1) {
            System.out.println(Arrays.toString(arr));
            permutationsCount++;
        } else {
            for (int i = index; i < size; i++) {
                swap(index, i);
                permuteRec(index + 1);
                swap(index, i);
            }
        }
    }

    static void swap(int i1, int i2) {
        if (i1 == i2) {
            return;
        }
        int temp = arr[i1];
        arr[i1] = arr[i2];
        arr[i2] = temp;
    }
}
