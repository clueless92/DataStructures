package AlgorithmsLiveExamPreparation;

import java.util.Scanner;

public class Pr02Guitar {
    public static void main(String[] args) {
        Scanner sc = new Scanner(System.in);
        String[] stepsStr = sc.nextLine().split(", ");
        int[] steps = new int[stepsStr.length];
        for (int i = 0; i < stepsStr.length; i++) {
            steps[i] = Integer.parseInt(stepsStr[i]);
        }

        int val = sc.nextInt();
        int max = sc.nextInt();
        int mid = max / 2;
        for (int i = 0; i < steps.length; i++) {
            int valPlus = val + steps[i];
            int valMinus = val - steps[i];
            if (valPlus > max && valMinus < 0) {
                System.out.println(-1);
                return;
            }
        }
    }
}
