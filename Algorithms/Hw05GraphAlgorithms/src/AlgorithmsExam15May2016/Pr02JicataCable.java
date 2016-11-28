package AlgorithmsExam15May2016;

import java.util.Scanner;

public class Pr02JicataCable {
    public static void main(String[] args) {
        Scanner sc = new Scanner(System.in);
        String[] input = sc.nextLine().split(" ");
        int[] prices = new int[input.length];
        for (int i = 0; i < input.length; i++) {
            prices[i] = Integer.parseInt(input[i]);
        }
        int connectorPrice = sc.nextInt();
        StringBuilder output = new StringBuilder(input[0] + " ");

        for (int i = 1; i < prices.length; i++) {
            int limit = i >> 1;
            for (int j = i - 1, k = 0; j >= limit; j--, k++) {
                if (k + j + 1 != i) {
                    continue;
                }
                int newPrice = prices[j] + prices[k] - 2 * connectorPrice;
                if (newPrice > prices[i]) {
                    prices[i] = newPrice;
                }
            }
            output.append(prices[i]).append(" ");
        }

        System.out.println(output);
    }
}
