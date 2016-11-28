package AlgorithmsExam06Dec2015;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.util.Stack;

public class Pr01SequencesOfLimitedSum {
    static StringBuilder output = new StringBuilder();

    public static void main(String[] args) throws IOException {
        BufferedReader reader = new BufferedReader(new InputStreamReader(System.in));
        int n = Integer.parseInt(reader.readLine());
        Stack<Integer> stack = new Stack<>();
        stack.push(1);
        int sum = 1;
        while (stack.size() > 0) {
            appendStack(stack);
            sum += stack.push(1);
            while (sum > n) {
                if(stack.isEmpty()) {
                    break;
                }
                sum -= stack.pop();
                if(stack.isEmpty()) {
                    break;
                }
                int curr = stack.pop();
                sum -= curr;
                sum += stack.push(curr + 1);
            }
        }

        System.out.println(output);
    }

    private static void appendStack(Stack<Integer> stack) {
        for (Integer integer : stack) {
            output.append(integer).append(' ');
        }
        output.append(System.lineSeparator());
    }
}
