namespace aoc.AOC2025.Days;

public static class Day3
{
    public static int Solve1()
    {
        var banks = File.ReadAllLines("AOC2025/Inputs/Input3.txt"); ;
        var sum = banks.Sum(GetHighestJoltage);
        Console.WriteLine($"Sum of highest joltage adapters: {sum}");
        return sum;
    }

    private static int GetHighestJoltage(string bank)
    {
        var (highest, atInd) = (0, 0);
        var sndHighest = 0;

        for (int i = 0; i < bank.Length - 1; i++)
        {
            var battery = ToInt(bank[i]);
            if (battery > highest)
                (highest, atInd) = (battery, i);
        }
        for (int i = atInd + 1; i < bank.Length; i++)
        {
            var battery = ToInt(bank[i]);
            if (battery > sndHighest)
                sndHighest = battery;
        }

        Console.WriteLine($"Highest: {highest}, 2nd Highest: {sndHighest}");
        return int.Parse(highest.ToString() + sndHighest.ToString());
    }

    public static long Solve2()
    {
        var banks = File.ReadAllLines("AOC2025/Inputs/Input3.txt"); ;
        var sum = banks.Sum(GetHighestJoltage3);
        Console.WriteLine($"Sum of highest joltage adapters: {sum}");
        return sum;
    }
 
    private static long GetHighestJoltage3(string bank)
    {
        var atInd = 0;
        var nextInd = 0;
        var pressed = "";

        while(pressed.Length != 12)
        {
            var highest = -1;
            for (int i = atInd; i <= bank.Length - (12 - pressed.Length); i++)
            {
                var battery = ToInt(bank[i]);
                if (battery > highest)
                {
                    highest = battery;
                    nextInd = i;
                }
            } 
            
            pressed += highest.ToString();
            atInd = nextInd + 1;
        }

        Console.WriteLine($"Bank: {bank} => Pressed adapters: {pressed}");
        return long.Parse(pressed);
    }

    private static int ToInt(char c) => c - '0'; 
}