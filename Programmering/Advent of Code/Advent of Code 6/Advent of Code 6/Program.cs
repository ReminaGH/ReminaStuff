class Program {
    static void Main() {
        int maxHoldTime = 79;
        int minDistanceToWin = 1471;

        int totalWins = CalculateTotalWins(maxHoldTime, minDistanceToWin);

        Console.WriteLine($"You can win {totalWins} times!");
        int tot = 21 * 28 * 33 * 26;
        Console.WriteLine(tot);
    }

    static int CalculateTotalWins(int maxHoldTime, int minDistanceToWin) {
        int totalWins = 0;

        // Iterate over each possible holding time
        for (int holdTime = 1; holdTime <= maxHoldTime; holdTime++) {
            // Calculate the total distance traveled
            int totalDistance = (maxHoldTime - holdTime) + (holdTime * (holdTime + 1) / 2);

            // Check if the total distance is greater than or equal to the minimum distance to win
            if (totalDistance >= minDistanceToWin) {
                totalWins++;
            }
        }

        return totalWins;
    }
}

//21