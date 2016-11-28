package Hw08GreedyAlgorithms;
import java.util.Map;
import java.util.Scanner;
import java.util.TreeMap;

public class Pr01FractionalKnapsackProblem {
    public static void main(String[] args) {
        Scanner sc = new Scanner(System.in);
        double capacity = Double.parseDouble(sc.nextLine().split(" ")[1]);
        int itemCount = Integer.parseInt(sc.nextLine().split(" ")[1]);
        TreeMap<Double, Double> itemMap = new TreeMap<>((o1, o2) -> o2.compareTo(o1));
        for (int i = 0; i < itemCount; i++) {
            String[] input = sc.nextLine().split(" -> ");
            double price = Double.parseDouble(input[0]);
            double quantity = Double.parseDouble(input[1]);
            double priceForOne = price / quantity;
            itemMap.put(priceForOne, quantity);
        }
        double totalRevenue = 0d;
        for (Map.Entry<Double, Double> item : itemMap.entrySet()) {
            double pricePerOne = item.getKey();
            double weight = item.getValue();
            if(capacity == 0d) {
                break;
            }
            double percentToTake = capacity / weight;
            if (percentToTake > 1) {
                percentToTake = 1;
            }
            double price = pricePerOne * weight;
            double revenue = price * percentToTake;
            capacity -= percentToTake * weight;
            totalRevenue += revenue;
            System.out.printf("Take %.2f%% of item with price %.2f and weight %.2f%n",
                    percentToTake * 100d, price, weight);
        }
        System.out.printf("Total price: %.2f", totalRevenue);
    }
}
