package AlgorithmsExam06Dec2015;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;

public class Pr02Bridges2 {
    public static void main(String[] args) throws IOException {
        BufferedReader reader = new BufferedReader(new InputStreamReader(System.in));
        String[] input = reader.readLine().split(" ");
        int[] bridge = new int[input.length];
        for (int i = 0; i < input.length; i++) {
            bridge[i] = Integer.parseInt(input[i]);
        }

        int last = 0;
        int bridgeCount = 0;
        boolean[] shouldPrint = new boolean[bridge.length];
        for (int i = 1; i < input.length; i++) {
            for (int j = last; j < i; j++) {
                if (bridge[i] == bridge[j]) {
                    last = i;
                    shouldPrint[i] = true;
                    shouldPrint[j] = true;
                    bridgeCount++;
                    break;
                }
            }
        }

        StringBuilder output = new StringBuilder();
        if (bridgeCount == 0) {
            output.append("No bridges found").append(System.lineSeparator());
        } else if (bridgeCount == 1) {
            output.append("1 bridge found").append(System.lineSeparator());
        } else {
            output.append(bridgeCount).append(" bridges found").append(System.lineSeparator());
        }
        for (int i = 0; i < bridge.length; i++) {
            if (shouldPrint[i]) {
                output.append(String.format("%d ", bridge[i]));
            } else {
                output.append("X ");
            }
        }
        System.out.println(output.toString().trim());
    }
}
