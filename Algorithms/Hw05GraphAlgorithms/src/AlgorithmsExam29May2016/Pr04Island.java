package AlgorithmsExam29May2016;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.util.ArrayList;
import java.util.List;
import java.util.Stack;

public class Pr04Island {
    public static void main(String[] args) throws IOException {
        BufferedReader reader = new BufferedReader(new InputStreamReader(System.in), 1024000);
        String input = reader.readLine();
        List<Integer> heights = new ArrayList<>();
        int from = 0;
        while (true) {
            int to = input.indexOf(' ', from);
            String toAdd;
            if (to == - 1) {
                toAdd = input.substring(from);
                heights.add(Integer.parseInt(toAdd));
                break;
            } else {
                toAdd = input.substring(from, to);
                heights.add(Integer.parseInt(toAdd));
                from = ++to;
            }
        }

        int max = solution3(heights);
        System.out.println(max);
    }

    private static int solution3(List<Integer> height) {
        int n = height.size();
        if(n == 0) {
            return 0;
        }

        Stack<Integer> left = new Stack<>();
        Stack<Integer> right = new Stack<>();
        int[] width = new int[n];// widths of intervals.
        for (int i = 0; i < width.length; i++) {
            width[i] = 1;
        }
        for (int i = 0; i < n; i++){
            // count # of consecutive higher bars on the left of the (i+1)th bar
            while(!left.isEmpty() && height.get(i) <= height.get(left.peek())) {
                // while there are bars stored in the stack, we check the bar on the top of the stack.
                left.pop();
            }

            if(left.isEmpty()){
                // all elements on the left are larger than height[i].
                width[i] += i;
            } else {
                // bar[left.peek()] is the closest shorter bar.
                width[i] += i - left.peek() - 1;
            }
            left.push(i);
        }

        for (int i = n-1; i >=0; i--) {
            while(!right.isEmpty() && height.get(i) <= height.get(right.peek())){
                right.pop();
            }

            if(right.isEmpty()){
                // all elements to the right are larger than height[i]
                width[i] += n - 1 - i;
            } else {
                width[i] += right.peek() - i - 1;
            }
            right.push(i);
        }

        int max = Integer.MIN_VALUE;
        for(int i = 0; i < n; i++){
            // find the maximum value of all rectangle areas.
            max = Math.max(max, width[i] * height.get(i));
        }

        return max;
    }
}
