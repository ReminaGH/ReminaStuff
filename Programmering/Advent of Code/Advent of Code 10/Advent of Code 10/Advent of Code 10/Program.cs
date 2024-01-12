internal class Program {
    static void Main() {

        string filePath = @"C:\Users\major\OneDrive\Dokument\CPGIT\CpStuff\Programmering\Advent of Code\Advent of Code 10\Advent of Code 10\Input.txt";
        string[] lines;

        if (File.Exists(filePath)) {
            lines = File.ReadAllLines(filePath);

            char[,] grid = CreateGrid(lines);
            
           
            //DisplayGrid(grid);

            var location = FindLocation(grid, 'S');
            
            // Check if 'S' is found and print the location
            if (location != null) {
                Console.WriteLine($"Found 'S' at location: {location.Item1}, {location.Item2}");

                // Now you can use the 'location' tuple for later use
                // For example: int row = location.Item1; int column = location.Item2;
            }

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

    static Tuple<int, int> FindLocation(char[,] grid, char target) {
        // Iterate through the grid to find the target character
        for (int i = 0; i < grid.GetLength(0); i++) {
            for (int j = 0; j < grid.GetLength(1); j++) {
                if (grid[i, j] == target) {
                    // Return the location as a tuple
                    return Tuple.Create(i, j);
                }
            }
        }

        // If the target is not found, return null
        return null;
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
}



