import java.util.Scanner;

public class Pr01BinomialCoefficients {
    public static void main(String[] args) {
        Scanner sc = new Scanner(System.in);
        int row = sc.nextInt();
        int col = sc.nextInt();
        sc.nextLine();
        int[][] arr = new int[row + 1][];

        for (int r = 0; r <= row; r++) {
            int colSize = r + 1;
            arr[r] = new int[colSize];
            arr[r][0] = 1;
            arr[r][colSize - 1] = 1;
            if(r > 1) {
                for (int c = 1; c < r; c++) {
                    arr[r][c] = arr[r-1][c] + arr[r-1][c-1];
                }
            }
        }

        System.out.println(arr[row][col]);
    }
}
