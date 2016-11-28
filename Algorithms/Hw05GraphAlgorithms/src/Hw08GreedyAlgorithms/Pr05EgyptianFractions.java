package Hw08GreedyAlgorithms;

import java.util.Scanner;

public class Pr05EgyptianFractions {
    public static void main(String[] args) {
        Scanner sc = new Scanner(System.in);
        String[] fraction = sc.nextLine().split("/");
        int numerator = Integer.parseInt(fraction[0]);
        int denominator = Integer.parseInt(fraction[1]);
        if (numerator > denominator) {
            System.out.println("Error (fraction is equal to or greater than 1)");
            return;
        }
        System.out.printf("%d / %d = ", numerator, denominator);
        printEgyptian(numerator, denominator);
    }

    static void printEgyptian(int numerator, int denominator) {
        // If either numerator or denominator is 0
        if (denominator == 0 || numerator == 0) {
            return;
        }
//        // If numerator is more than denominator
//        if (numerator > denominator) {
//            System.out.printf("%d + ", numerator/denominator);
//            printEgyptian(numerator % denominator, denominator);
//            return;
//        }
        System.out.printf("%d / %d = ", numerator, denominator);
        // If numerator divides denominator,
        // then a simple division makes the fraction in 1/n form
        if (denominator % numerator == 0) {
            System.out.printf("1/%d%n", denominator / numerator);
            return;
        }
        // If denominator divides numerator,
        // then the given number is not a fraction
        if (numerator % denominator == 0) {
            System.out.println(numerator / denominator);
            return;
        }
        // We reach here dr > nr and dr%nr is non-zero
        // Find ceiling of dr/nr and print it as first
        // fraction
        int n = denominator / numerator + 1;
        System.out.printf("1/%d + ", n);
        // Recur for remaining part
        printEgyptian(numerator * n - denominator, denominator * n);
    }
}
