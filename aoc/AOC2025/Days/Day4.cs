using Microsoft.AspNetCore.SignalR;

namespace aoc.AOC2025.Days;

public static class Day4
{
    public static int Solve1()
    {
        var grid = File.ReadAllLines("AOC2025/Inputs/Input4.txt"); ;
        var markedIndices = new HashSet<(int, int)>();

        for (int i = 0; i < grid.Length; i++)
        {
            for (int j = 0; j < grid[i].Length; j++)
            {
                if (grid[i][j] != '@')
                    continue;

                if (MarkAdjacentRolls(grid, i, j))
                    markedIndices.Add((i, j));
            }
        }

        Console.WriteLine($"Total marked rolls: {markedIndices.Count}");
        return markedIndices.Count;
    }

    public static int Solve2()
    {
        var grid = File.ReadAllLines("AOC2025/Inputs/Input4.txt"); ;
        var nRolls = 0;

        while (true)
        {
            var markedIndices = new HashSet<(int, int)>();
            
            for (int i = 0; i < grid.Length; i++)
            {
                for (int j = 0; j < grid[i].Length; j++)
                {
                    if (grid[i][j] != '@')
                        continue;

                    if (MarkAdjacentRolls(grid, i, j))
                        markedIndices.Add((i, j));
                }
            }

            if (markedIndices.Count == 0)
                break;

            nRolls += markedIndices.Count;

            foreach (var (i, j) in markedIndices)
            {
                var row = grid[i];
                row = row.Remove(j, 1).Insert(j, ".");
                grid[i] = row;
            }
        }

        Console.WriteLine($"Total marked rolls: {nRolls}");
        return nRolls;
    }

    private static bool MarkAdjacentRolls(string[] grid, int i, int j)
    {
        var markedIndices = new HashSet<(int, int)>();
        if (GetAdjacent(grid, i - 1, j) == '@')
            markedIndices.Add((i - 1, j));
        if (GetAdjacent(grid, i - 1, j + 1) == '@')
            markedIndices.Add((i - 1, j + 1));
        if (GetAdjacent(grid, i - 1, j - 1) == '@')
            markedIndices.Add((i - 1, j - 1));

        if (GetAdjacent(grid, i, j - 1) == '@')
            markedIndices.Add((i, j - 1));
        if (GetAdjacent(grid, i, j + 1) == '@')
            markedIndices.Add((i, j + 1));

        if (GetAdjacent(grid, i + 1, j + 1) == '@')
            markedIndices.Add((i + 1, j + 1));
        if (GetAdjacent(grid, i + 1, j - 1) == '@')
            markedIndices.Add((i + 1, j - 1));
        if (GetAdjacent(grid, i + 1, j) == '@')
            markedIndices.Add((i + 1, j));

        if (markedIndices.Count > 3)
            return false;

        return true;
    }

    public static char? GetAdjacent(string[] grid, int i, int j)
    {
        return i >= 0 && i < grid.Length && j >= 0 && j < grid[0].Length ? grid[i][j] : null;
    }
}