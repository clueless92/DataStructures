package Hw02Combinatorics;

import java.util.Arrays;
import java.util.Scanner;

public class Pr05PermutationsWithRepetition {
    static int permutationsWithRepCount = 0;
    static int[] arr;

    public static void main(String[] args) {
        arr = new int[] { 3, 1, 2, 3 };
        Arrays.sort(arr);
        permuteWithRep(0, arr.length);
        System.out.printf("Total permutations: %d%n", permutationsWithRepCount);
    }

    private static void permuteWithRep(int start, int size)
    {
        System.out.println(Arrays.toString(arr));
        permutationsWithRepCount++;
        int swap = 0;
        if (start < size)
        {
            for (int i = size - 2; i >= start; i--)
            {
                for (int k = i + 1; k < size; k++)
                {
                    if (arr[i] != arr[k])
                    {
                        swap = arr[i];
                        arr[i] = arr[k];
                        arr[k] = swap;
                        permuteWithRep(i + 1, size);
                    }
                }
                // comment the code below to see the difference
                swap = arr[i];
                for (int k = i; k < size - 1; )
                {
                    arr[k] = arr[++k];
                }
                arr[size - 1] = swap;
            }
        }
    }
}
