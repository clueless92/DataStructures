namespace SumOfCoins
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;

    public class SumOfCoins
    {
        public static void Main(string[] args)
        {
            var availableCoins = new[] { 1, 2, 5, 10, 20, 50 };
            var targetSum = 923;

            var selectedCoins = ChooseCoins(availableCoins, targetSum);

            Console.WriteLine("Number of coins to take: {0}", selectedCoins.Values.Sum());
            foreach (var selectedCoin in selectedCoins)
            {
                Console.WriteLine("{0} coin(s) with value {1}", selectedCoin.Value, selectedCoin.Key);
            }
        }

        public static Dictionary<int, int> ChooseCoins(IList<int> coins, int targetSum)
        {
            List<int> sortedCoins = coins.OrderBy(c => -c).ToList();
            var chosenCoins = new Dictionary<int, int>();
            var currentSum = 0;
            int coinIndex = 0;
            while (currentSum != targetSum && coinIndex < sortedCoins.Count)
            {
                int currentCoin = sortedCoins[coinIndex];
                int remainingSum = targetSum - currentSum;
                var numberOfCoinsToTake = remainingSum / currentCoin;
                if (numberOfCoinsToTake > 0)
                {
                    chosenCoins.Add(currentCoin, numberOfCoinsToTake);
                    currentSum += currentCoin * numberOfCoinsToTake;
                }

                coinIndex++;
            }

            if (currentSum != targetSum)
            {
                throw new InvalidOperationException();
            }

            return chosenCoins;
        }
    }
}