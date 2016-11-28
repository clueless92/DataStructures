package AlgorithmsExam29May2016;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;

public class Pr02StabilityCheck {
    public static void main(String[] args) throws IOException {
        BufferedReader reader = new BufferedReader(new InputStreamReader(System.in));
        int buildSize = Integer.parseInt(reader.readLine());
        int landSize = Integer.parseInt(reader.readLine());
        long[][] land = new long[landSize][landSize];
        long totalSize = 0;
        for (int r = 0; r < landSize; r++) {
            String[] line = reader.readLine().split(" ");
            for (int c = 0; c < landSize; c++) {
                land[r][c] = Integer.parseInt(line[c]);
                totalSize += land[r][c];
            }
        }

        if (buildSize == landSize) {
            System.out.println(totalSize);
            return;
        }

        long max = Integer.MIN_VALUE;
        long currMax = 0;
        //// slow sloution O(n2k2)
//        for (long r = 0; r <= landSize - buildSize; r++) {
//            for (long c = 0; c <= landSize - buildSize; c++) {
//                for (long br = r; br < buildSize + r; br++) {
//                    for (long bc = c; bc < buildSize + c; bc++) {
//                        currMax += land[br][bc];
//                    }
//                }
//                if (max < currMax) {
//                    max = currMax;
//                }
//                currMax = 0;
//            }
//        }


        //// fast solution
        long[][] stripSum = new long[landSize][landSize];
        for (int r = 0; r < landSize; r++) {
            // Calculate sum of first k x 1 rectangle in this column
            long sum = 0;
            for (int c = 0; c < buildSize; c++) {
                sum += land[c][r];
            }
            stripSum[0][r] = sum;

            // Calculate sum of remaining rectangles
            for (int c = 1; c < landSize - buildSize + 1; c++) {
                sum += (land[c + buildSize - 1][r] - land[c - 1][r]);
                stripSum[c][r] = sum;
            }
        }

        for (int r = 0; r < landSize - buildSize + 1; r++) {
            long sum = 0;
            for (int c = 0; c < buildSize; c++) {
                sum += stripSum[r][c];
            }

            if (sum > max) {
                max = sum;
            }

            for (int c = 1; c < landSize - buildSize + 1; c++) {
                sum += (stripSum[r][c + buildSize - 1] - stripSum[r][c-1]);
                if (sum > max) {
                    max = sum;
                }
            }
        }

        System.out.println(max);
    }
}
