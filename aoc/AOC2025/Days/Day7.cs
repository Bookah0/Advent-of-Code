namespace aoc.AOC2025.Days;

public static class Day7
{
    public static long Solve1()
    {
        var lines = File.ReadAllLines("AOC2025/Inputs/Input7.txt");
        var (posX, posY) = GetStartPosition(lines);
        var visited = new HashSet<(int, int)>();

        var splits = MoveDown(lines, posX, posY, visited);
        Console.WriteLine($"Total splits: {splits}");
        return splits;
    }

    private static int MoveDown(string[] lines, int posX, int posY, HashSet<(int, int)> visited, int splits = 0)
    {
        posX++;
        
        if(visited.Contains((posX, posY)))
            return 0;

        visited.Add((posX, posY));

        if(posX >= lines.Length)
            return 0;

        if(lines[posX][posY] == '^')
        {
            var leftSplit = MoveDown(lines, posX, posY-1, visited, splits+1);
            var rightSplit = MoveDown(lines, posX, posY+1, visited, splits+1);
            return leftSplit + rightSplit + 1;
        }
        else
        {
            return MoveDown(lines, posX, posY, visited, splits);
        }
    }

    public static long Solve2()
    {
        var lines = File.ReadAllLines("AOC2025/Inputs/Input7.txt");
        var (posX, posY) = GetStartPosition(lines);

        var visited = new Dictionary<(int, int), long>();
        var timelines = CountTimelines(lines, posX, posY, visited);

        Console.WriteLine($"Total timelines: {timelines}");
        return timelines;
    }

    private static long CountTimelines(string[] lines, int posX, int posY, Dictionary<(int, int), long> visited)
    {
        posX++;

        if(posX >= lines.Length)
            return 1;

        if(visited.TryGetValue((posX, posY), out var savedCount))
            return savedCount;

        long count;
        if (lines[posX][posY] == '^')
        {
            var leftTimeline = CountTimelines(lines, posX, posY-1, visited);
            var rightTimeline = CountTimelines(lines, posX, posY+1, visited);
            count = leftTimeline + rightTimeline;
        }
        else
        {
            count = CountTimelines(lines, posX, posY, visited);
        }

        visited[(posX, posY)] = count;
        return count;
    }

    private static (int, int) GetStartPosition(string[] lines)
    {
        for (int i = 0; i < lines.Length; i++)
        {
            for (int j = 0; j < lines[i].Length; j++)
            {
                if (lines[i][j] == 'S')
                    return (i, j);
            }
        }
        return (0,0);
    }
}