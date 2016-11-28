package ExamPreparation;

import java.util.*;

public class Pr01GroupPermutations {
    final static HashMap<Character, String> charMap = new HashMap<>();
    final static List<Character> arr = new ArrayList<>();
    final static StringBuilder output = new StringBuilder();

    public static void main(String[] args) {
        Scanner sc = new Scanner(System.in);
        String input = sc.nextLine();
        for (int i = 0; i < input.length(); i++) {
            char c = input.charAt(i);
            if (!charMap.containsKey(c)) {
                charMap.put(c, "");
                arr.add(c);
            }
            String newStr = charMap.get(c) + c;
            charMap.put(c, newStr);
        }
        permuteRec(0);
        System.out.print(output);
    }


    static void permuteRec(int index) {
        int size = arr.size();
        if (index >= size - 1) {
            addPerm();
        } else {
            for (int i = index; i < size; i++) {
                swap(index, i);
                permuteRec(index + 1);
                swap(index, i);
            }
        }
    }

    private static void addPerm() {
        for (Character c : arr) {
            output.append(charMap.get(c));
        }
        output.append("\n");
    }

    static void swap(int i1, int i2) {
        if (i1 == i2) {
            return;
        }
        char temp = arr.get(i1);
        arr.set(i1, arr.get(i2));
        arr.set(i2, temp);
    }
}
