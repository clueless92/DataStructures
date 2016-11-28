package AlgorithmsExam29May2016;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.util.Stack;

public class Pr01SowingStack {

    private static int chuckCount;

    public static void main(String[] args) throws IOException {
        BufferedReader reader = new BufferedReader(new InputStreamReader(System.in));
        chuckCount = Integer.parseInt(reader.readLine());
        String[] soilInput = reader.readLine().split(" ");
        StringBuilder currSoil = new StringBuilder();
        for (int i = 0; i < soilInput.length; i++) {
            currSoil.append(soilInput[i]);
        }
        String soil = currSoil.toString();

        Stack<Character> soilStack = new Stack<>();
        int i = 0;
        int currChuck = 0;
        while (true) {
            if (currChuck == chuckCount) {
                System.out.println(soilStack);
                continue;
            }
            if (soil.charAt(i) == '1') {
                soilStack.push('.');
                chuckCount++;
                i++;
                if (i >= soil.length()) {
                    i = popBack(soil, soilStack, i);
                } else {
                    soilStack.push(soil.charAt(i));
                }
                i++;
                if (i >= soil.length()) {
                    i = popBack(soil, soilStack, i);
                }
            } else if (soil.charAt(i) == '0') {
                soilStack.push('0');
                i++;
                if (i >= soil.length()) {
                    i = popBack(soil, soilStack, i);
                }
            }
        }
    }

    private static int popBack(String soil, Stack<Character> soilStack, int i) {
//        System.out.println(soilStack);
        boolean stop = false;
        while (true) {
            char c = '\0';
            if (soilStack.isEmpty()) {
                i++;
                soilStack.push(soil.charAt(i));
            } else {
                c = soilStack.pop();
                i--;
            }
            if (c == '.') {
                if (stop || soilStack.isEmpty()) {
                    i++;
                    soilStack.push(soil.charAt(i));
                    i++;
                    break;
                }
                stop = true;
            }
        }
        return i;
    }
}
