using System.Reflection.PortableExecutable;

class Program {
    static void Main() {


        const string INPUT = @"C:\Users\major\OneDrive\Dokument\CPGIT\CpStuff\Programmering\Advent of Code\Advent of Code 4\Advent of Code 4\AoC_Input.txt";

        string[] lines = File.ReadAllLines(INPUT);

        // Create lists to store the numbers
        List<List<int>> firstLists = new List<List<int>>();
        List<List<int>> secondLists = new List<List<int>>();

        // Process each line
        foreach (string line in lines) {
            // Split each line into parts based on ":"
            string[] parts = line.Split(':');
            // Ensure there are enough parts
            if (parts.Length == 2) {
                // Extract the numbers after "|"
                string[] numberSets = parts[1].Trim().Split('|');

                // Trim each set to remove leading and trailing spaces
                string firstSet = numberSets[0].Trim();
                string secondSet = numberSets[1].Trim();

                // Split each set into individual numbers
                string[] firstNumbers = firstSet.Split(' ');
                string[] secondNumbers = secondSet.Split(' ');

                // Convert the string arrays to lists of integers
                List<int> firstList = new List<int>();
                List<int> secondList = new List<int>();

                foreach (string num in firstNumbers) {
                    if (int.TryParse(num, out int parsedNum)) {
                        firstList.Add(parsedNum);
                    }
                }

                foreach (string num in secondNumbers) {
                    if (int.TryParse(num, out int parsedNum)) {
                        secondList.Add(parsedNum);
                    }
                }

                // Add the lists to the collection
                firstLists.Add(firstList);
                secondLists.Add(secondList);
            }
            else {
                Console.WriteLine("Invalid input format");
            }
        }
    }
}

//List into smaller list of the winning copies and so on