namespace aoc.AOC2025.Days;

public static class Day5
{
    public static int Solve1()
    {
        var (ranges, numbers) = ParseInsput();
        var nInRange = numbers.Sum(n => IsInRange(ranges, n) ? 1 : 0);

        Console.WriteLine($"Total numbers in range: {nInRange}");
        return nInRange;
    }

    public static long Solve2()
    {
        var (ranges, _) = ParseInsput();

        for (int i = 0; i < ranges.Count; i++)
        {
            for (int j = i + 1; j < ranges.Count; j++)
            {
                var (start1, end1) = ranges[i];
                var (start2, end2) = ranges[j];

                if (start2 <= end1 + 1 && end2 >= start1 - 1)
                {
                    var rangeUnion = (Math.Min(start1, start2), Math.Max(end1, end2));
                    ranges[i] = rangeUnion;
                    ranges.RemoveAt(j);
                    i = -1;
                    break;
                }
            }
        }

        var nInRange = ranges.Sum(r => r.Item2 - r.Item1 + 1);
        
        Console.WriteLine($"Total numbers in range: {nInRange}");
        return nInRange;
    }

    private static bool IsInRange(List<(long, long)> rangeList, long number) =>
        rangeList.Any(r => number >= r.Item1 && number <= r.Item2);

    private static (List<(long, long)>, List<long>) ParseInsput()
    {
        var data = File.ReadAllLines("AOC2025/Inputs/Input5.txt");
        var ranges = new List<(long, long)>();
        var numbers = new List<long>();

        var addingRanges = true;

        foreach (var line in data)
        {
            if (line == "")
            {
                addingRanges = false;
                continue;
            }

            if (addingRanges)
            {
                var parts = line.Split('-');
                ranges.Add((long.Parse(parts[0]), long.Parse(parts[1])));
            }
            else
            {
                numbers.Add(long.Parse(line));
            }
        }
        return (ranges, numbers);
    }
}