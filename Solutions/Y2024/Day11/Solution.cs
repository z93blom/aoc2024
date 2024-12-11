using AdventOfCode.Framework;
using AdventOfCode.Utilities;
using System;

namespace AdventOfCode.Solutions.Y2024.Day11;

[RegisterKeyedTransient("2024-11")] partial class Solution { }
[RegisterTransient()] partial class Solution { }

partial class Solution : ISolver
{
    public int Year => 2024;
    public int Day => 11;
    public string GetName() => "Plutonian Pebbles";

    public IEnumerable<object> Solve(string input, Func<TextWriter> getOutputFunction)
    {
        // var emptyOutput = () => new NullTextWriter();
        yield return PartOne(input, getOutputFunction);
        yield return PartTwo(input, getOutputFunction);
    }

    static object PartOne(string input, Func<TextWriter> getOutputFunction)
    {
        //var stones = input.Longs().ToList();
        //for (var i = 0; i < 25; i++)
        //{
        //    for (var index = stones.Count - 1; index >= 0; index--)
        //    {
        //        var stone = stones[index];
        //        if (stone == 0)
        //        {
        //            stones[index] = 1;
        //            continue;
        //        }

        //        var s = stone.ToString();
        //        if (s.Length % 2 == 0)
        //        {
        //            var chars = s.Length / 2;
        //            var firstPart = long.Parse(s[..chars]);
        //            var secondPart = long.Parse(s[chars..]);
        //            stones[index] = firstPart;
        //            stones.Insert(index + 1, secondPart);
        //            continue;
        //        }

        //        stones[index] *= 2024;
        //    }
        //}

        //return stones.Count;
        var stones = input.Longs().ToList();
        var count = Count(stones, 25);

        return count;

    }

    private static long Count(List<long> stones, int blinks)
    {
        var count = 0L;
        Dictionary<(long, int), long> memory = new();
        foreach (var stone in stones)
        {
            var numberOfStones = NumberOfStones(stone, blinks, memory);
            count += numberOfStones;
        }

        return count;
    }

    static long NumberOfStones(long stone, int blinks, Dictionary<(long, int), long> memory)
    {
        if (memory.ContainsKey((stone, blinks)))
        {
            return memory[(stone, blinks)];
        }

        if (blinks == 0)
            return 1;

        if (stone == 0)
        {
            var count = NumberOfStones(1, blinks - 1, memory);
            memory[(stone, blinks)] = count;
            return memory[(stone, blinks)];
        }

        var s = stone.ToString();
        if (s.Length % 2 == 0)
        {
            var chars = s.Length / 2;
            var firstPart = long.Parse(s[..chars]);
            var secondPart = long.Parse(s[chars..]);

            var countFirstPart = NumberOfStones(firstPart, blinks - 1, memory);
            var countSecondPart = NumberOfStones(secondPart, blinks - 1, memory);
            memory[(stone, blinks)] = countFirstPart + countSecondPart;
            return memory[(stone, blinks)];
        }

        var c = NumberOfStones(stone* 2024, blinks - 1, memory);
        memory[(stone, blinks)] = c;
        return memory[(stone, blinks)];
    }

    static object PartTwo(string input, Func<TextWriter> getOutputFunction)
    {
        var stones = input.Longs().ToList();
        var count = Count(stones, 75);

        return count;
    }
}