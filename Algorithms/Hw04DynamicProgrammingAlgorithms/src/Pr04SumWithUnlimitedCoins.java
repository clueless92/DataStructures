import java.util.Scanner;

public class Pr04SumWithUnlimitedCoins {
    private static int count = 0;
    private static int[] coins;
//    private static List<Integer> result = new ArrayList<>();

    public static void main(String[] args) {
        Scanner sc = new Scanner(System.in);
        int targetSum = Integer.parseInt(sc.nextLine().substring(4));
        String[] coinsString = sc.nextLine().substring(9).split("\\D+");
        coins = new int[coinsString.length];
        for (int i = 0; i < coinsString.length; i++) {
            coins[i] = Integer.parseInt(coinsString[i]);
        }

        findCombinations(targetSum, 0);
        System.out.println(count);
    }

    private static void findCombinations(int targetSum, int currentIndex)
    {
        if (targetSum == 0)
        {
            count++;
//            System.out.println(result);
        }
        else if (targetSum > 0)
        {
            for (int i = currentIndex; i < coins.length; i++)
            {
                targetSum -= coins[i];
//                result.add(coins[i]);
                findCombinations(targetSum, i);
//                result.remove((Object)coins[i]);
                targetSum += coins[i];
            }
        }
    }
}
