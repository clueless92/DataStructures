package AlgorithmsExam29May2016;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.util.ArrayList;
import java.util.List;

public class Pr01Sowing {
    private static String soil;
    private static StringBuilder currSoil;
    private static List<Integer> arr = new ArrayList<>();

    public static void main(String[] args) throws IOException {
        BufferedReader reader = new BufferedReader(new InputStreamReader(System.in));
        int chuckCount = Integer.parseInt(reader.readLine());
        String[] soilInput = reader.readLine().split(" ");
        currSoil = new StringBuilder();
        for (int i = 0; i < soilInput.length; i++) {
            currSoil.append(soilInput[i]);
            if (soilInput[i].equals("1")) {
                arr.add(i);
            }
        }
        soil = currSoil.toString();

        combinationsIteratively(arr.size() + 1, chuckCount);
    }

    private static void combinationsIteratively(int n, int k) {
        while (arr.get(k) < n) {
            List<Integer> sad = new ArrayList<>();
            for (int i = 0; i < k; i++) {
                sad.add(arr.get(i));
            }
            if (isValid(sad)) {
//                print(sad);
                System.out.println(sad);
            }
        }
    }

    private static void print(List<Integer> arr) {
        StringBuilder soilBuilder = new StringBuilder();
        for (int i = 0, j = 0; j < arr.size(); i++) {
            if (i == arr.get(j)) {
                soilBuilder.append('.');
                j++;
            } else {
                soilBuilder.append(soil.charAt(i));
            }
        }

        for (int i = soilBuilder.length(); i < soil.length(); i++) {
            soilBuilder.append(soil.charAt(i));
        }

        System.out.println(soilBuilder);
    }

    private static boolean isValid(List<Integer> sad) {
        for (int i = 1; i < sad.size(); i++) {
            if (sad.get(i) - sad.get(i - 1) <= 1 ||
                    soil.charAt(sad.get(i)) == '0' ||
                    sad.get(i) >= soil.length()) {
                return false;
            }
        }

        if (sad.get(0) >= soil.length() || soil.charAt(sad.get(0)) == '0') {
            return false;
        }

        return true;
    }
}
