using System.Runtime.CompilerServices;
using System.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

class Advent_Of_Code_1 {
    static void Main() {
        const string INPUT_FILE = @"D:\Git\CpStuff\Programmering\Advent of Code\Advent of Code 1\AoC_Input.txt";

        var counter = 0;

        var lines = File.ReadAllLines(INPUT_FILE);
        foreach (var line in lines) {
            var cleanLine = line
                .Replace("one", "o1e")
                .Replace("two", "t2o")
                .Replace("three", "t3e")
                .Replace("four", "f4r")
                .Replace("five", "f5e")
                .Replace("six", "s6x")
                .Replace("seven", "s7n")
                .Replace("eight", "e8t")
                .Replace("nine", "n9e");

            // Get the first number from the line
            var firstNumber = cleanLine.First(Char.IsDigit);

            // Get the Second Number
            var lastNumber = cleanLine.Last(Char.IsDigit);

            // Concat the first number with the second number
            var combinedNumber = firstNumber.ToString() + lastNumber.ToString();

            // Convert the combined numbers to an int and add it to the counter
            counter += int.Parse(combinedNumber);
        }

        Console.WriteLine(counter);
    }
}