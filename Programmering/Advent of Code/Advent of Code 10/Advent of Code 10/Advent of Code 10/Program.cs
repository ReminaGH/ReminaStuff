internal class Program {
    static void Main() {

        string filePath = @"C:\Users\major\OneDrive\Dokument\CPGIT\CpStuff\Programmering\Advent of Code\Advent of Code 10\Advent of Code 10\Input.txt";
        string[] lines;

        if (File.Exists(filePath)) {
            lines = File.ReadAllLines(filePath);

            char[,] grid = CreateGrid(lines);

           
            DisplayGrid(grid);

            FindAndPrintLocation(grid, 'S');
        }
    }

    static char[,] CreateGrid(string[] lines) {
        int rows = lines.Length;
        int columns = lines[0].Length;

        char[,] grid = new char[rows, columns];

        // Fill the grid based on the input lines
        for (int i = 0; i < rows; i++) {
            for (int j = 0; j < columns; j++) {
                grid[i, j] = lines[i][j];
            }
        }

        return grid;
    }


    static void DisplayGrid(char[,] grid) {
        // Display the grid
        for (int i = 0; i < grid.GetLength(0); i++) {
            for (int j = 0; j < grid.GetLength(1); j++) {
                Console.Write(grid[i, j]);
            }
            Console.WriteLine();
        }
    }
    static void FindAndPrintLocation(char[,] grid, char target) {
        // Iterate through the grid to find the target character
        for (int i = 0; i < grid.GetLength(0); i++) {
            for (int j = 0; j < grid.GetLength(1); j++) {
                if (grid[i, j] == target) {
                    Console.WriteLine($"Found '{target}' at location: ({i}, {j})");
                    return; // Stop searching after the first occurrence
                }
            }
        }

        // If the target is not found
        Console.WriteLine($"'{target}' not found in the grid.");
    }
}
