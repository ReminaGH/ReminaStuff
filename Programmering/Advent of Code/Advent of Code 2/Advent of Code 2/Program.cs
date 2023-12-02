namespace Advent_of_Code_2 {
    internal class Program {
            record Game(int Num, IReadOnlyList<Set> Sets);

            record Set(int Red, int Green, int Blue);

            private static void Main(string[] args) {
                var inputLines = File.ReadAllLines(@"D:\Git\CpStuff\Programmering\Advent of Code\Advent of Code 2\AoC_Input.txt");
                var games = inputLines
                    .Select(l => {
                        var gameAndSets = l.Split(":");
                        return new Game(
                            Num: int.Parse(gameAndSets[0].Split(" ")[1]),
                            Sets: gameAndSets[1].Split(";")
                                .Select(subset => {
                                    var rawSet = subset.Split(",")
                                        .Select(v => {
                                            var colorAndValue = v.Split(" ");
                                            return (Color: colorAndValue[2], Value: int.Parse(colorAndValue[1]));
                                        }).ToList();

                                    return new Set(
                                        Red: rawSet.SingleOrDefault(cv => cv.Color is "red").Value,
                                        Green: rawSet.SingleOrDefault(cv => cv.Color is "green").Value,
                                        Blue: rawSet.SingleOrDefault(cv => cv.Color is "blue").Value);
                                }).ToList());
                    }).ToList();

                var answer1 = games
                    .Where(g => g.Sets.All(gs => gs is { Red: <= 12, Green: <= 13, Blue: <= 14 }))
                    .Sum(r => r.Num);
                Console.WriteLine($"Answer 1: {answer1}");

                var answer2 = games
                    .Select(g => g.Sets.Max(r => r.Red) * g.Sets.Max(r => r.Green) * g.Sets.Max(r => r.Blue))
                    .Sum();
                Console.WriteLine($"Answer 2: {answer2}");
            }
        }
    }