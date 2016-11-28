package AlgorithmsLiveExamPreparation;

import java.util.Scanner;

//public class Pr01ParenthasisStolen {
//    static void brackets(int openStock, int closeStock, String s) {
//        if (openStock == 0 && closeStock == 0) {
//            System.out.println(s);
//        }
//        if (openStock > 0) {
//            brackets(openStock-1, closeStock+1, s + "(");
//        }
//        if (closeStock > 0) {
//            brackets(openStock, closeStock-1, s + ")");
//        }
//    }
//    public static void main(String[] args) {
//        Scanner sc = new Scanner(System.in);
//        int n = sc.nextInt();
//        brackets(n, 0, "");
//    }
//}

public class Pr01ParenthasisStolen {
    final static StringBuilder output = new StringBuilder();

    public static void main(String[] args) {
        Scanner sc = new Scanner(System.in);
        int n = sc.nextInt();
        brackets(n);
        System.out.print(output);
    }
    static void brackets(final int N) {
        brackets(N, 0, 0, new char[N * 2]);
    }
    static void brackets(int openStock, int closeStock, int index, char[] arr) {
        while (closeStock >= 0) {
            if (openStock > 0) {
                arr[index] = '(';
                brackets(openStock-1, closeStock+1, index+1, arr);
            }
            if (closeStock-- > 0) {
                arr[index++] = ')';
                if (index == arr.length) {
//                    System.out.println(arr);
                    output.append(arr);
                    output.append("\r\n");
                }
            }
        }
    }
}
