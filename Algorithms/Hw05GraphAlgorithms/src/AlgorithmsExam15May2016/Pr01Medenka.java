package AlgorithmsExam15May2016;

import java.util.ArrayList;
import java.util.Scanner;

public class Pr01Medenka {
    private static final ArrayList<Integer> zerosCounts = new ArrayList<>();
    private static int firstIndex = -1;
    private final static StringBuilder medenka = new StringBuilder();

    public static void main(String[] args) {
        Scanner sc = new Scanner(System.in);
        String medenkaStr = sc.nextLine();
        int medenkaLen = medenkaStr.length();
        int onesCount = 0;
        int limit = 1;

        for (int i = 0; i < medenkaLen; i++) {
            char c = medenkaStr.charAt(i);

            if(c == '1') {
                medenka.append('1');
                onesCount++;
                if (firstIndex == -1) {
                    firstIndex = medenka.length();
                }

                if (i < medenkaLen - 1) {
                    medenka.append('|');
                    zerosCounts.add(0);
                }
                if (onesCount > 1) {
                    limit *= zerosCounts.get(onesCount - 2) + 1;
                }
            } else if (c == '0') {
                medenka.append('0');
                if (onesCount > 0) {
                    int zeroCount = zerosCounts.get(onesCount - 1) + 1;
                    zerosCounts.set(onesCount - 1, zeroCount);
                }
            }
        }
//        System.out.println(medenka);
        int[] arr = new int[zerosCounts.size()];

        for (int i = 0; i < limit; i++) {
            //System.out.println(Arrays.toString(arr));
            generateMedenka(arr);
            for (int j = zerosCounts.size() - 1; j >= 0 ; j--) {
                arr[j]++;
                if(arr[j] > zerosCounts.get(j)) {
                    arr[j] = 0;
                } else {
                    break;
                }
            }
        }
    }

    private static void generateMedenka(int[] arr) {
        medenka.setLength(firstIndex);

        for (int i = 0; i < arr.length; i++) {
            int j = 0;
            for (; j < zerosCounts.get(i); j++) {
                if (j == arr[i]) {
                    medenka.append('|');
                }
                medenka.append('0');
            }
            if (j == arr[i]) {
                medenka.append('|');
            }
            medenka.append('1');
        }
        System.out.println(medenka);
    }
}
