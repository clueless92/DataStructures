package AlgorithmsLiveExamPreparation;

import java.util.*;

public class Pr01Parenthesis {
    final static List<Character> arr = new ArrayList<>();
    final static StringBuilder outputBuilder = new StringBuilder();
    static int permutationsWithRepCount;
    static int asd;

    public static void main(String[] args) {
        Scanner sc = new Scanner(System.in);
        int n = sc.nextInt();
        for (int i = 1; i < n; i++) {
            arr.add('(');
        }
        for (int i = 1; i < n; i++) {
            arr.add(')');
        }
        permuteWithRep(0, arr.size());
        System.out.print(outputBuilder);
        System.out.println(permutationsWithRepCount);
        System.out.println(asd);
    }

    private static boolean validatePerm() {
        int validator = 1;
        for (Character c : arr) {
            if (c == '(') {
                validator++;
            } else {
                validator--;
            }
            if (validator < 0) {
                return false;
            }
        }
        validator--;
        return validator == 0;
    }

    public static String getPermString() {
        StringBuilder output = new StringBuilder("(");
        for (Character c : arr) {
            output.append(c);
        }
        output.append(')');
        return output.toString();
    }

    private static void permuteWithRep(int start, int size)
    {
        String sad = getPermString();
        if (validatePerm()) {
            outputBuilder.append(sad);
            outputBuilder.append("\r\n");
            asd++;
        }
        permutationsWithRepCount++;
        char swap;
        if (start < size)
        {
            for (int i = size - 2; i >= start; i--)
            {
                for (int k = i + 1; k < size; k++)
                {
                    if (arr.get(i) != arr.get(k))
                    {
                        swap = arr.get(i);
                        arr.set(i, arr.get(k));
                        arr.set(k, swap);
                        permuteWithRep(i + 1, size);
                    }
                }
                // comment the code below to see the difference
                swap = arr.get(i);
                for (int k = i; k < size - 1; )
                {
                    arr.set(k, arr.get(++k));
                }
                arr.set(size - 1, swap);
            }
        }
    }
}
