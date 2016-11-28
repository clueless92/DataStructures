import java.util.*;

public class Pr02LongestZigzagSubsequence {
    public static void main(String[] args) {
        Scanner sc = new Scanner(System.in);
        String[] input = sc.nextLine().split(",");
        int[] sequence = new int[input.length];
        for (int i = 0; i < input.length; i++) {
            sequence[i] = Integer.parseInt(input[i]);
        }

        List<Integer> longestSeq = longestZigZag(sequence);
        System.out.println(longestSeq);
    }

    public static List<Integer> longestZigZag(int[] sequence) {
        List<Integer> seq = new ArrayList<>();
        int[] diff = new int[sequence.length - 1];

        for (int i = 1; i < sequence.length; i++) {
            diff[i - 1] = sequence[i] - sequence[i - 1];
        }

        int prevSign = sign(diff[0]);
        seq.add(sequence[0]);

        for (int i = 1; i < diff.length; i++) {
            int sign = sign(diff[i]);
            if (prevSign * sign == -1) {
                prevSign = sign;
                if (seq.size() == 1) {
                    seq.add(sequence[i]);
                }
                seq.add(sequence[i + 1]);
            }
        }

        return seq;
    }

    public static int sign(int a) {
        if (a == 0) {
            return 0;
        }

        return a / Math.abs(a);
    }

    public static List<Integer> findLongestZigZagSubsequence(int[] sequence) {
        int[] len = new int[sequence.length];
        int[] prev = new int[sequence.length];
        int maxLen = 0;
        int lastIndex = -1;

        for (int curr = 0; curr < sequence.length; curr++) {
            len[curr] = 1;
            prev[curr] = -1;
            int comparer = -1;
            for (int i = 0; i < curr; i++) {
                if (Integer.compare(sequence[i], sequence[curr]) == comparer && len[i] >= len[curr]) {
                    len[curr] = len[i] + 1;
                    prev[curr] = i;
                    comparer *= -1;
                }
            }

            if (len[curr] > maxLen) {
                maxLen = len[curr];
                lastIndex = curr;
            }
        }

        ArrayList<Integer> longestSeq = new ArrayList<>();
        while (lastIndex != -1) {
            longestSeq.add(sequence[lastIndex]);
            lastIndex = prev[lastIndex];
        }

        Collections.reverse(longestSeq);

        return longestSeq;
    }
}
