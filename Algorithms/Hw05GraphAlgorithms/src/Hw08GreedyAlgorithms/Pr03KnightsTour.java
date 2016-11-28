package Hw08GreedyAlgorithms;

import java.util.Scanner;

public class Pr03KnightsTour {
    static int[][] board;
    static int size;
    public static void main(String[] args) {
        Scanner sc = new Scanner(System.in);
        size = sc.nextInt();
        board = new int[size][size];
        minChildGreedy();
        printBoard();
    }

    private static void printBoard() {
        for (int r = 0; r < size; r++) {
            for (int c = 0; c < size; c++) {
                System.out.printf("%4d", board[r][c]);
            }
            System.out.println();
        }
    }

    private static void minChildGreedy() {
        int[] modR = new int[] { -2, -1,  1,  2,  2,  1, -1, -2 };
        int[] modC = new int[] {  1,  2,  2,  1, -1, -2, -2, -1 };
        int currR = 0;
        int currC = 0;
        int moveCount = 1;
        board[currR][currC] = moveCount;
        int limit = size * size;
        while (moveCount < limit) {
            int minMoves = Integer.MAX_VALUE;
            int nextR = 0;
            int nextC = 0;
            for (int i = 0; i < 8; i++) {
                int newR = currR + modR[i];
                int newC = currC + modC[i];
                if (invalidMove(newR, newC)) {
                    continue;
                }
                int possibleMoves = 0;
                for (int p = 0; p < 8; p++) {
                    int testR = newR + modR[p];
                    int testC = newC + modC[p];
                    if(invalidMove(testR, testC)) {
                        continue;
                    }
                    possibleMoves++;
                }
                if(possibleMoves < minMoves) {
                    minMoves = possibleMoves;
                    nextR = newR;
                    nextC = newC;
                }
            }
            currR = nextR;
            currC = nextC;
            moveCount++;
            board[currR][currC] = moveCount;
        }
    }

    static boolean invalidMove(int r, int c) {
        return r < 0 || r >= size || c < 0 || c >= size || board[r][c] != 0;
    }
}
