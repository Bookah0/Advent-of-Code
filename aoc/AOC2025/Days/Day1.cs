using System.Diagnostics;

namespace aoc.AOC2025.Days;

public static class Day1
{
    public static int Solve1()
    {
        var dial = 50;
        var timesOnZero = 0;
        var sequences = File.ReadAllLines("AOC2025/Inputs/Input1.txt");

        foreach (var sequence in sequences)
        {
            var clicks = int.Parse(sequence[1..]);
            dial += sequence[0] == 'R' ? clicks : -clicks;

            dial = ((dial % 100) + 100) % 100;

            if (dial == 0)
                timesOnZero++;
        }

        Console.WriteLine($"Dial stopped at zero {timesOnZero} times.");
        return timesOnZero;
    }

    public static int Solve2(int dial = 50, string[]? sequences = null)
    {
        var timesPassedZero = 0;
        sequences = sequences is null ? File.ReadAllLines("AOC2025/Inputs/Input1.txt") : sequences;

        foreach (var sequence in sequences)
        {
            var clicks = int.Parse(sequence[1..]);

            if (sequence[0] == 'R')
            {
                timesPassedZero += (dial + clicks) / 100;
                dial = (dial + clicks) % 100;
            }
            else
            {
                var passes = (dial == 0) ? -1 : 0;
                dial -= clicks;
                passes += (dial == 0) ? 1 : 0;
                
                if (dial < 0)
                {
                    passes += -(int)Math.Floor((double)dial / 100);
                    
                    if (dial % 100 == 0)
                        passes++;
                }

                timesPassedZero += passes > 0 ? passes : 0;
                dial = ((dial % 100) + 100) % 100;
            }
        }
        Console.WriteLine($"Dial passed zero {timesPassedZero} times.");
        return timesPassedZero;
    }

    public static void Test2()
    {
        var testCases = new List<(string name, int startPos, string[] sequences, int expected)>
        {
            // Basic cases
            ("No zero crossing", 50, ["R30"], 0),
            ("End at zero from right", 50, ["R50"], 1),
            ("End at zero from left", 50, ["L50"], 1),

            // Starting at zero
            ("Start at 0, move right", 0, ["R50"], 0),
            ("Start at 0, move left", 0, ["L50"], 0),
            ("Start at 0, full rotation right", 0, ["R100"], 1),
            ("Start at 0, full rotation left", 0, ["L100"], 1),

            // Multiple crossings in one move
            ("Multiple crossings right", 50, ["R250"], 3),
            ("Multiple crossings left", 50, ["L250"], 3),
            ("Large rotation example", 50, ["R1000"], 10),

            // Complex sequences
            ("Two moves to zero", 50, ["R30", "R20"], 1),
            ("Cross then return", 50, ["R60", "L60"], 2),

            // The given example
            ("Full example", 50, ["L68", "L30", "R48", "L5", "R60", "L55", "L1", "L99", "R14", "L82"], 6),

            // Edge: exactly at boundaries
            ("From 99 to 0", 99, ["R1"], 1),
            ("From 1 to 0", 1, ["L1"], 1),

            // Multiple passes through 0 in sequence
            ("Three zeros in a row", 50, ["R50", "R100", "R100"], 3),

            // Rotations over zero
            ("Small right movement, no crossing", 1, ["R3"], 0),
            ("Small left movement, no crossing", 50, ["L10"], 0),
            ("Right to first crossing", 50, ["R100"], 1),
            ("Left to first crossing", 50, ["L100"], 1),
            ("Right crossing twice", 50, ["R200"], 2),
            ("Left crossing twice", 50, ["L200"], 2),
            ("Right 50 from 50 (land on 0)", 50, ["R50"], 1),
            ("Right 150 from 50", 50, ["R150"], 2),
            ("Right 250 from 50", 50, ["R250"], 3),
            ("Left 50 from 50 (land on 0)", 50, ["L50"], 1),
            ("Left 150 from 50", 50, ["L150"], 2),
            ("Left 250 from 50", 50, ["L250"], 3),
            ("Start at 0, right 123", 0, ["R123"], 1),
            ("Start at 0, right 258", 0, ["R258"], 2),
            ("Start at 0, right 200", 0, ["R200"], 2),
            ("Start at 0, left 123", 0, ["L123"], 1),
            ("Start at 0, left 258", 0, ["L258"], 2),
            ("Start at 0, left 200", 0, ["L200"], 2),
        };


        foreach (var (name, startPos, sequences, expected) in testCases)
        {
            var actual = Solve2(startPos, sequences);

            Debug.Assert(actual == expected, $"Test '{name}' failed: expected {expected}, got {actual}");
        }

        Console.WriteLine("All tests passed.");
    }
}