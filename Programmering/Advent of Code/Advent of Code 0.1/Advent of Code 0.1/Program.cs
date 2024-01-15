const string FILE = @"C:\Users\datahaxx\Documents\CPGit\CpStuff\Programmering\Advent of Code\Advent of Code 0.1\input.txt";

var input = File.ReadAllLines(FILE);
var watch = System.Diagnostics.Stopwatch.StartNew();
var instructions = input[0].Select(x => x == 'L' ? 0 : 1).ToArray();
var nodes =
    input.Skip(2)
    .Select(x => x.Split(new[] { ' ', ',', '(', ')', '=' }, StringSplitOptions.RemoveEmptyEntries))
    .ToDictionary(x => x[0], x => x[1..]);

long result1 = 0;
long result2 = 0; { // Part 1
    for (var node = "AAA"; node != "ZZZ"; result1++)
        node = nodes[node][instructions[result1 % instructions.Length]];
} { // Part2 (Utilizing loop frequency harmony)
    var findloopFrequency = (string node) =>  // Scan until an end node is seen twice, first index is phase, index difference is period
    {
        var endSeen = new Dictionary<string, long>();
        for (long i = 0; true; i++) {
            if (node[2] == 'Z') {
                if (endSeen.TryGetValue(node, out var lastSeen))
                    return (phase: lastSeen, period: i - lastSeen);
                else
                    endSeen[node] = i;
            }
            node = nodes[node][instructions[i % instructions.Length]];
        }
    };

    var frequencies =
        nodes.Keys
        .Where(x => x[2] == 'A')
        .Select(x => findloopFrequency(x))
        .ToList();

    // Find harmony by moving harmony phase forward and increasing harmony period until it matches all frequencies
    var harmonyPhase = frequencies[0].phase;
    var harmonyPeriod = frequencies[0].period;
    foreach (var freq in frequencies.Skip(1)) {
        // Find new harmonyPhase by increasing phase in harmony period steps until harmony matches freq
        for (; harmonyPhase < freq.phase || (harmonyPhase - freq.phase) % freq.period != 0; harmonyPhase += harmonyPeriod) ;

        // Find the new harmonyPeriod by looking for the next position the harmony frequency matches freq (brute force least common multiplier)
        long sample = harmonyPhase + harmonyPeriod;
        for (; (sample - freq.phase) % freq.period != 0; sample += harmonyPeriod) ;
        harmonyPeriod = sample - harmonyPhase;
    }
    result2 = harmonyPhase;
}

watch.Stop();

Console.WriteLine($"Result1 = {result1}");
Console.WriteLine($"Result2 = {result2}");
Console.WriteLine($"Runtime = {watch.ElapsedMilliseconds}ms");