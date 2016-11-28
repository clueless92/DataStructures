package ExamPreparation;

import java.util.Arrays;
import java.util.Scanner;

public class Pr02Bridges {
    static int[][] maxBridges;
    static int[] north;
    static int[] south;
    public static void main(String[] args) {
        Scanner sc = new Scanner(System.in);
        String[] northString = sc.nextLine().split(" ");
        north = new int[northString.length];
        for (int i = 0; i < north.length; i++) {
            north[i] = Integer.parseInt(northString[i]);
        }
        String[] southString = sc.nextLine().split(" ");
        south = new int[southString.length];
        for (int i = 0; i < south.length; i++) {
            south[i] = Integer.parseInt(southString[i]);
        }
        maxBridges = new int[north.length][south.length];
//        for (int n = 0; n < north.length; n++) {
//            for (int s = 0; s < south.length; s++) {
//                maxBridges[n][s] = -1;
//            }
//        }
        calcMaxBridges(north.length - 1, south.length - 1);
        System.out.println(maxBridges[north.length - 1][south.length - 1]);
    }

    private static int calcMaxBridges(int x, int y) {
        if (x < 0 || y < 0) {
            return 0;
        }
//        if (maxBridges[x][y] != -1) {
//            return maxBridges[x][y];
//        }
        int northLeft = calcMaxBridges(x - 1, y);
        int southLeft = calcMaxBridges(x, y - 1);
        if(north[x] == south[y]) {
            maxBridges[x][y] = 1 + Math.max(northLeft, southLeft);
        } else {
            maxBridges[x][y] = Math.max(northLeft, southLeft);
        }
        return maxBridges[x][y];
    }
}
