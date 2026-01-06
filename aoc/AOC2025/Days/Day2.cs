using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;

namespace aoc.AOC2025.Days;

public static class Day2
{
    public static long Solve1()
    {
        var sum = ParseInput(1)!
            .Where(id => !IsValidId(id.ToString(), 1))
            .Sum();

        Console.WriteLine($"Sum of all invalid IDs: {sum}");
        return sum;
    }

    public static long Solve2()
    {
        var sum = ParseInput(2)!
            .Where(id => !IsValidId(id.ToString(), 2))
            .Sum();
        
        Console.WriteLine($"Sum of all invalid IDs: {sum}");
        return sum;
    }

    private static List<long>? ParseInput(int part)
    {
        var lines = File.ReadAllLines("AOC2025/Inputs/Input2.txt");

        return lines
            .SelectMany(s => s.Split(',', StringSplitOptions.RemoveEmptyEntries))
            .SelectMany(range => ParseRange(range, part))
            .ToList();

        static IEnumerable<long> ParseRange(string range, int part)
        {
            var bounds = range.Split('-', StringSplitOptions.RemoveEmptyEntries);
            if (part == 1 && bounds[0].Length % 2 != 0 && bounds[1].Length % 2 != 0)
                return [];
            
            long start = long.Parse(bounds[0]);
            long end = long.Parse(bounds[1]);
            return LongRange(start, end - start + 1);   
        }
    }

    public static IEnumerable<long> LongRange(long start, long count)
    {
        for (long i = 0; i < count; i++)
        {
            yield return start + i;
        }
    }

    private static bool IsValidId(string id, int part)
    {
        if (id[0] == '0') return false;
        
        return !FindPattern(id, part);
    }

    private static bool FindPattern(string id, int part)
    {
        if(part == 1)
        {
            if (id.Length % 2 != 0) return false;

            return id[..(id.Length / 2)] == id[(id.Length / 2)..];
        }

        if (IsPrime(id.Length))
        {
            foreach (var c in id)
            {
                if (c != id[0]) return false;
            }
            return true;
        }

        for (int size = 2; size <= id.Length / 2; size++)
        {
            var allMatch = true;
            var pattern = id[..size];

            if (id.Length % size != 0) continue;

            for (int start = size; start < id.Length; start += size)
            {
                if (pattern != id.Substring(start, size))
                {
                    allMatch = false;
                    break;
                }
            }

            if (allMatch) return true;   
        }
        return false;
    }


public static bool IsPrime(int number)
    {
        if (number <= 1) return false;
        if (number == 2) return true;
        if (number % 2 == 0) return false;

        var boundary = (int)Math.Floor(Math.Sqrt(number));

        for (int i = 3; i <= boundary; i += 2)
        {
            if (number % i == 0) return false;
        }

        return true;
    }
}