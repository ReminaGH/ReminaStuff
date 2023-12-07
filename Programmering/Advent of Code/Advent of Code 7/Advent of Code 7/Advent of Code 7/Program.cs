using System;
using System.Collections.Generic;
using System.IO;

class Program {
    static void Main() {
        string filePath = @"C:\Users\major\OneDrive\Dokument\CPGIT\CpStuff\Programmering\Advent of Code\Advent of Code 7\Advent of Code 7\Input.txt";

        List<List<string>> listOfLists = ReadFileAndSplitLines(filePath);

        AddFirstEntryAsThirdDimension(listOfLists);

        foreach (var innerList in listOfLists) {
            Console.WriteLine(string.Join(" ", innerList));
        }

        // Replace characters in the third part based on specific rules
        ReplaceCharactersInThirdPart(listOfLists, 2);

        // Display the modified list
        Console.WriteLine("Modified List of Lists:");
        foreach (var innerList in listOfLists) {
            Console.WriteLine(string.Join(" ", innerList));
        }
    }

    static void ReplaceCharactersInThirdPart(List<List<string>> listOfLists, int columnIndex) {
        // Iterate through each list in the list of lists
        foreach (var innerList in listOfLists) {
            // Check if the list has entries and the specified column index is valid
            if (innerList.Count > columnIndex) {
                // Replace characters in the third entry based on rules
                innerList[columnIndex] = ReplaceCharacters(innerList[columnIndex]);
            }
        }
    }

    static string ReplaceCharacters(string input) {
        // Replace characters based on specific rules
        input = input.Replace("A", "13")
                     .Replace("K", "12")
                     .Replace("Q", "11")
                     .Replace("J", "10")
                     .Replace("T", "9")
                     .Replace("9", "8")
                     .Replace("8", "7")
                     .Replace("7", "6")
                     .Replace("6", "5")
                     .Replace("5", "4")
                     .Replace("4", "3")
                     .Replace("3", "2")
                     .Replace("2", "1");

        return input;
    }

    static void AddFirstEntryAsThirdDimension(List<List<string>> listOfLists) {
        foreach (var innerList in listOfLists) {
            innerList.Insert(2, innerList[0]); // Add the first entry as the 3rd entry
        }
    }

    static List<List<string>> ReadFileAndSplitLines(string filePath) {
        List<List<string>> listOfLists = new List<List<string>>();

        // Read all lines from the file
        string[] lines = File.ReadAllLines(filePath);

        // Split each line into a list based on spaces
        foreach (string line in lines) {
            List<string> currentList = new List<string>(line.Split(' '));
            listOfLists.Add(currentList);
        }

        return listOfLists;
    }
}