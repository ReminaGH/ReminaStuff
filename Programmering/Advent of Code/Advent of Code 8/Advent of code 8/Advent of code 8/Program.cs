namespace Advent_of_code_8 {
    internal class Program {
        static void Main(string[] args) {
            const string INPUT1 = @"C:\Users\datahaxx\Documents\CPGit\CpStuff\Programmering\Advent of Code\Advent of Code 8\Advent of code 8\input1.txt";
            const string INPUT2 = @"C:\Users\datahaxx\Documents\CPGit\CpStuff\Programmering\Advent of Code\Advent of Code 8\Advent of code 8\input2.txt";

            string[] mapLines = File.ReadAllLines(INPUT1);
            string instructions = File.ReadAllText(INPUT2);
            string instructionsAltered = instructions.Replace("L", "0").Replace("R", "1");
            string[,] mapArray = new string[mapLines.Length, 3];

            bool searching = false;

            int index = 0;

            Console.WriteLine(instructions[0]);

            List<List<string>> leftList = new List<List<string>>();
            List<List<string>> rightList = new List<List<string>>();

            string[] parts;

            foreach (string line in mapLines) {

                parts = line.Split('=');


                string[] secondSetSeperated = parts[1].Trim().Split(',');

                char[] charsToTrim = { '(', ')', ' ', };
                string firstList = parts[0].Trim(charsToTrim);
                string secondListOne = secondSetSeperated[0].Trim(charsToTrim);
                string secondListTwo = secondSetSeperated[1].Trim(charsToTrim);

                Console.WriteLine(line);
                Console.WriteLine("First part of the list: {0}, second part split up in 2 parts, first: {1}, second: {2}\n", parts[0], secondListOne, secondListTwo);

                mapArray[index, 0] = firstList;
                mapArray[index, 1] = secondListOne;
                mapArray[index, 2] = secondListTwo;

                index++;
                
                


            }
            for (int i = 0; i < mapArray.GetLength(0); i++) {

                if (mapArray[i, 0] == "AAA") {
                    index = i;
                    Console.WriteLine("Found at index {0}", index);
                    Console.WriteLine(instructionsAltered);
                    searching = true;
                    break;


                }
                Console.WriteLine(mapArray[i, 0]);
            }

            Console.WriteLine(index);

            int arrayFirstIndex = index; //mapArray[this][0]
            int arraySecondIndex = 0; //mapArray[0][this]

            while (searching) {
                for (int i = 0; i < instructionsAltered.Length; i++) {
                    
                   
                    char instructionsChar = instructionsAltered[i];

                    Console.WriteLine(instructionsChar);
                    switch (instructionsChar) {

                        case '0':
                            Console.WriteLine("Left : 0 and " + arraySecondIndex);
                            arraySecondIndex = 2;
                            break;
                        case '1':
                            Console.WriteLine("Right : 1 and " + arraySecondIndex);
                            arraySecondIndex = 1;
                            break;
                        }
                    if (mapArray[arrayFirstIndex, arraySecondIndex] == "ZZZ") {
                        Console.WriteLine("Found");
                        //searching = false;
                        //break;

                    }
                    else {
                        Console.WriteLine("[{0},{1}]", arrayFirstIndex, arraySecondIndex);
                        Console.WriteLine(mapArray[arrayFirstIndex, arraySecondIndex]);
                        arrayFirstIndex++;
                        Console.ReadKey();
                        }
                    }
                }
            }
        }
    }