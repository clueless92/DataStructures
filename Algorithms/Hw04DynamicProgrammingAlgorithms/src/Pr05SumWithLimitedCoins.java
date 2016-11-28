import java.util.*;

class Pr05SumWithLimitedCoins {
    private static int count = 0;
    private static int[] coins;
    private static List<Integer> result = new ArrayList<>();
    private static List<List<Integer>> results = new ArrayList<>();

    public static void main(String[] args) {
        Scanner sc = new Scanner(System.in);
        int targetSum = Integer.parseInt(sc.nextLine().substring(4));
        String[] coinsString = sc.nextLine().substring(9).split("\\D+");
        coins = new int[coinsString.length];
        for (int i = 0; i < coinsString.length; i++) {
            coins[i] = Integer.parseInt(coinsString[i]);
        }

        Arrays.sort(coins);
        findCombinations(targetSum, 0);
        System.out.println(count);
    }

    private static void findCombinations(int targetSum, int currentIndex) {
        if (targetSum == 0) {
            if (!Contains(results, result)) {
                results.add(new ArrayList<>(result));
                count++;
                System.out.println(result);
            }
        } else if (targetSum > 0) {
            for (int i = currentIndex; i < coins.length; i++) {
                targetSum -= coins[i];
                result.add(coins[i]);
                findCombinations(targetSum, i + 1);
                result.remove((Object) coins[i]);
                targetSum += coins[i];
            }
        }
    }

    private static boolean Contains(List<List<Integer>> containingList ,List<Integer> listToCheck) {
        for (List<Integer> list : containingList) {
            if (list.size() != listToCheck.size()) {
                continue;
            }

            boolean listsAreEqual = true;
            for (int i = 0; i < list.size(); i++) {
                if(!list.get(i).equals(listToCheck.get(i))) {
                    listsAreEqual = false;
                    break;
                }
            }

            if(listsAreEqual) {
                return true;
            }
        }

        return false;
    }
}
