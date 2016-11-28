package Hw02Combinatorics;

import java.util.Arrays;
import java.util.Scanner;

public class Pr03CombinationsIteratively {
    static int[] arr;

    public static void main(String[] args) {
        Scanner sc = new Scanner(System.in);
        System.out.print("n=");
        int n = Integer.parseInt(sc.nextLine());
        System.out.print("k=");
        int k = Integer.parseInt(sc.nextLine());
        arr = new int[k];
        for (int i = 0; i < k; i++) {
            arr[i] = i;
        }
        combinationsIteratively(n, k);
    }

    static void combinationsIteratively(int n, int k) {
        while (arr[k - 1] < n) {
            for (int i = 0; i < k; i++) {
                System.out.printf("%s ", arr[i] + 1);
            }
            System.out.println();

            int t = k - 1;
            while (t != 0 && arr[t] == n - k + t) {
                t--;
            }
            arr[t]++;
            for (int i = t + 1; i < k; i++) {
                arr[i] = arr[i - 1] + 1;
            }
        }
    }
}
