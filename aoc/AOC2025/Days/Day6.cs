using System.Globalization;

namespace aoc.AOC2025.Days;

public static class Day6
{
    public static long Solve1()
    {        
        var (numbers, opLine) = ParseInput();
        var sum = 0L;

        for (int i = 0; i < numbers[0].Length; i++)
        {
            var curSum = 0L;
            
            for (int j = 0; j < numbers.Length; j++)
            {
                curSum = Op(curSum, numbers[j][i], opLine[i]);
            }
            sum += curSum;
        }

        Console.WriteLine($"Total sum: {sum}");
        return sum;
    }

    public static long Solve2()
    {        
        var (lines, opLine) = ParseInput2();
        
        var sum = 0L;
        var curStart = 0;
        var maxHeight = lines.Length;

        for (int opIndex = 0; opIndex < opLine.Length; opIndex++)
        {
            var curSum = 0L;
            var curMaxLen = GetMaxLength(lines, curStart);

            for (int i = 0; i < curMaxLen; i++)
            {   
                var curNum = GetHorizontalNumber(maxHeight, lines, curStart, i);
                curSum = Op(curSum, curNum, opLine[opIndex]);
            }
            
            sum += curSum;
            curStart += curMaxLen+1;
        }

        Console.WriteLine($"Total sum: {sum}");
        return sum;
    }

    private static int GetMaxLength(string[] lines, int curStartInd)
    {
        var maxLength = 0;
        for (int i = 0; i < lines.Length; i++)
        {
            for (int j = curStartInd; j < lines[i].Length; j++)
            {   
                if(lines[i][j] != ' ' && (j == lines[i].Length-1 || lines[i][j+1] == ' '))
                {
                    maxLength = Math.Max(maxLength, j-curStartInd+1);
                    break;
                }
            }            
        }
        return maxLength;
    }

    private static int GetHorizontalNumber(int maxHeight, string[] lines, int curStart, int i)
    {
        var number = "";
        for (int j = 0; j < maxHeight; j++)
        {   
            if(lines[j][curStart + i] != ' ')
            {
                number += lines[j][curStart + i];                        
            }
        }
        return int.Parse(number);
    }
    private static (int[][], char[]) ParseInput(){
        var lines = File.ReadAllLines("AOC2025/Inputs/Input6.txt");
        
        var numbers = lines[..^1].Select(line => 
            line.Split()
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .Select(int.Parse)
                .ToArray())
            .ToArray();

        var opLine = lines[^1]
            .Where(c => c != ' ')
            .ToArray();

        return (numbers, opLine);
    }

    private static (string[], char[]) ParseInput2()
    {
        var lines = File.ReadAllLines("AOC2025/Inputs/Input6.txt");
        
        var opLine = lines[^1]
            .Where(c => c != ' ')
            .ToArray();
        
        return (lines[..^1], opLine);
    }

    private static long Op(long x, long y, char op) =>
        op == '+' ? x + y : (x == 0 ? y : x * y);

}