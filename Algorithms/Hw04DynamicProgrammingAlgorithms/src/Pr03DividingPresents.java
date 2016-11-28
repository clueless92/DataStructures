import java.util.ArrayList;
import java.util.Arrays;
import java.util.Scanner;

public class Pr03DividingPresents {
    public static void main(String[] args) {
        Scanner sc = new Scanner(System.in);
        String[] input = sc.nextLine().split(",");
        Integer[] presents = new Integer[input.length];
        for (int i = 0; i < input.length; i++) {
            presents[i] = Integer.parseInt(input[i]);
        }

        Arrays.sort(presents);
        ArrayList<Integer> bobs = new ArrayList<>();
        int bobTotal = 0;
        ArrayList<Integer> alans = new ArrayList<>();
        int alanTotal = 0;
        for (int i = presents.length - 1; i >= 0; i--) {
            if (alanTotal <= bobTotal) {
                alanTotal += presents[i];
                alans.add(presents[i]);
            } else {
                bobTotal += presents[i];
                bobs.add(presents[i]);
            }
        }

        if (alanTotal > bobTotal) {
            int temp = alanTotal;
            alanTotal = bobTotal;
            bobTotal = temp;

            ArrayList<Integer> tempList = new ArrayList<>(alans);
            alans = new ArrayList<>(bobs);
            bobs = new ArrayList<>(tempList);
        }

        System.out.printf("Difference: %d%n", bobTotal - alanTotal);
        System.out.printf("Alan: %d Bob: %d%n", alanTotal, bobTotal);
        System.out.printf("Alan takes: %s%n", alans);
        System.out.println("Bob takes the rest.");
    }
}
