using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc.TagHelpers;

namespace aoc.AOC2025.Days;

public static class Day8
{
    public static long Solve(int part)
    {
        var nPairs = 1000;
        var nextCurcuitId = 1;
        var visitedIndexPairs = new HashSet<(int, int)>();        
        var allPairs = new List<(int, int, long)>();        
        var junctionBoxes = File.ReadAllLines("AOC2025/Inputs/Input8.txt")
            .Select(line => line.Split(',').Select(int.Parse).ToArray())
            .Select(coord => new Box(coord[0], coord[1], coord[2], 0))
            .ToList();

        for (int i = 0; i < junctionBoxes.Count; i++)
        {
            for (int j = i + 1; j < junctionBoxes.Count; j++)
            {
                allPairs.Add((i, j, CalculateDistanceSquared(junctionBoxes[i], junctionBoxes[j])));
            }
        }

        allPairs = [.. allPairs.OrderBy(pair => pair.Item3)];
        int p = 0;

        while(true)
        {            
            var (closestInd1, closestInd2, dist) = allPairs[p];
            
            (var updatedId, var allConnected) = UpdateCircuits(junctionBoxes, junctionBoxes[closestInd1], junctionBoxes[closestInd2], nextCurcuitId);
            nextCurcuitId = updatedId;

            if(allConnected && part == 2)
            {   
                return junctionBoxes[closestInd1].X * junctionBoxes[closestInd2].X;
            }

            if(p == nPairs && part == 1)
                break;

            if(allPairs.Count == p + 1)
            {   
                p = 0;
                continue;
            }
            p++;
        }

        var circuitSizeDict = new Dictionary<int, int>();
    
        foreach (var box in junctionBoxes)
        {
            if(box.Circuit > 0)
            {
                circuitSizeDict[box.Circuit] = circuitSizeDict.TryGetValue(box.Circuit, out int value) ? value+1 : 1;
            }
        }

        var circuitLengths = circuitSizeDict.Values.OrderByDescending(len => len).ToArray();
        var sum = 1;

        if(circuitLengths.Length > 0) sum *= circuitLengths[0];
        if(circuitLengths.Length > 1) sum *= circuitLengths[1];
        if(circuitLengths.Length > 2) sum *= circuitLengths[2];

        Console.WriteLine($"Total product of boxes in circuits: {sum}");
        return sum;
    }

    private static (int, bool) UpdateCircuits(List<Box> junctionBoxes, Box box1, Box box2, int nextCurcuitId)
    {
        int circuit1 = box1.Circuit;
        int circuit2 = box2.Circuit;
        
        if(circuit1 == 0 && circuit2 == 0)
        {
            box1.Circuit = nextCurcuitId;
            box2.Circuit = nextCurcuitId;
            return (nextCurcuitId + 1, false);
        }
        else if(circuit1 != 0 && circuit2 == 0)
        {
            box2.Circuit = circuit1;
            return (nextCurcuitId, false);
        }
        else if(circuit1 == 0 && circuit2 != 0)
        {
            box1.Circuit = circuit2;
            return (nextCurcuitId, false);
        }
        else if(circuit1 != circuit2)
        {
            var allConnected = true;
            foreach (var box in junctionBoxes)
            {
                if(box.Circuit == circuit2)
                {
                    box.Circuit = circuit1;
                }
                else
                {
                    allConnected = false;
                }
            }
            return (nextCurcuitId, allConnected);
        }
        return (nextCurcuitId, false);   
    }

    private static long CalculateDistanceSquared(Box box1, Box box2)
    {
        long dx = box1.X - box2.X;
        long dy = box1.Y - box2.Y;
        long dz = box1.Z - box2.Z;
        return dx * dx + dy * dy + dz * dz;
    }
}

internal class Box(int x, int y, int z, int circuit = 0)
{
    public int X { get; } = x;
    public int Y { get; } = y;
    public int Z { get; } = z;
    public int Circuit { get; set; } = circuit;
}