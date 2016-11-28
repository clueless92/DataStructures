package Hw02Combinatorics;

import java.util.Arrays;
import java.util.Scanner;

public class Pr02PermutationsIterative {
    static int permutationsCount = 0;
    static int arr[];

    public static void main(String[] args) {
        Scanner sc = new Scanner(System.in);
        System.out.print("n = ");
        int n = sc.nextInt();
        arr = new int[n];
        int[] arrHelp = new int[n + 1];
        for (int i = 0; i < n; i++) {
            arr[i] = i + 1;
            arrHelp[i] = i;
        }
        arrHelp[n] = n;
        permuteIteratively(arrHelp, n);
        System.out.printf("Total permutations: %s%n", permutationsCount);
    }

    static void permuteIteratively(int[] arrHelp, int n) {
        int i = 0;
        while (i < n) {
            arrHelp[i]--;
            int h;
            if (isOdd(i)) {
                h = arrHelp[i];
            }
            else {
                h = 0;
            }
            swap(h, i);
            i = 1;
            while (arrHelp[i] == 0) {
                arrHelp[i] = i;
                i++;
            }
            System.out.println(Arrays.toString(arr));
            permutationsCount++;
        }
    }

    static boolean isOdd(int i) {
        return i % 2 != 0;
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
