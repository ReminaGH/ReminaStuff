using System.Text.RegularExpressions;

internal class Program {
    private static void Main(string[] args) {
        const string INPUT_FILE = @"C:\Users\major\OneDrive\Dokument\CPGIT\CpStuff\Programmering\Advent of Code\Advent of Code 0\Input.txt";

        string[] logFile = File.ReadAllLines(INPUT_FILE);

        List<List<int>> numberedElfs = ProcessNumberedElfs(logFile);

        DisplayElfs(numberedElfs);
        FindBiggestElf(numberedElfs);
        FindTop3Elfs(numberedElfs);
    }

    static List<List<int>> ProcessNumberedElfs(string[] logFile) {

        List<List<int>> numberedElfs = new List<List<int>>();
        List<int> currentElf = new List<int>();

        foreach (string line in logFile) {

            if (string.IsNullOrEmpty(line)) {

                if (currentElf.Count > 0) {

                    numberedElfs.Add(currentElf);
                    currentElf = new List<int>();
                }
            }

            else {

                if (int.TryParse(line, out int number)) {

                    currentElf.Add(number);
                }

                else {

                    Console.WriteLine($"Unable to handle elf");
                }
            }
        }

        if (currentElf.Count > 0) {

            numberedElfs.Add(currentElf);
        }

        return numberedElfs;
    }

    static void DisplayElfs(List<List<int>> numberedElfs) {

        int elfGroup = 1;

        foreach (List<int> group in numberedElfs) {

            Console.WriteLine($"Elf group {elfGroup++}");
            foreach (int number in group) {

                Console.WriteLine(number);
            }
            Console.WriteLine();
        }

    }
    static void FindBiggestElf(List<List<int>> numberedElfs) {

        var sumElf = numberedElfs.Select(group => group.Sum()).ToList();

        int indexOfLargestElf = sumElf.IndexOf(sumElf.Max());

        Console.WriteLine($"The {indexOfLargestElf + 1} Elf has the largest sum of calories: {sumElf.Max()}");
    }
    static void FindTop3Elfs(List<List<int>> numberedElfs) { 
        
        var sumElf = numberedElfs.Select(group => group.Sum()).ToList();

        var top3Elfs = sumElf
            .Select((sum, index) => new {Index = index, Sum = sum })
            .OrderByDescending(x => x.Sum)
            .Take(3)
            .Select(x => x.Index)
            .ToList();

        Console.WriteLine("Top 3 Elfs:");

        foreach (int index in top3Elfs) {
            Console.WriteLine($"Elf number: {index + 1}, Their total calories: {sumElf[index]}");
        }

        int sumOfTop3 = top3Elfs.Sum(index => sumElf[index]);

        Console.WriteLine($"Sum of the 3 top elfs together: {sumOfTop3}");
    }
}
