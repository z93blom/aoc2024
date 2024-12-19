using AdventOfCode.Framework;
using AdventOfCode.Utilities;

namespace AdventOfCode.Solutions.Y2024.Day19;

[RegisterKeyedTransient("2024-19")] partial class Solution { }
[RegisterTransient()] partial class Solution { }

partial class Solution : ISolver
{
    public int Year => 2024;
    public int Day => 19;
    public string GetName() => "Linen Layout";

    public IEnumerable<object> Solve(string input, Func<TextWriter> getOutputFunction)
    {
        // var emptyOutput = () => new NullTextWriter();
        yield return PartOne(input, getOutputFunction);
        yield return PartTwo(input, getOutputFunction);
    }

    static object PartOne(string input, Func<TextWriter> getOutputFunction)
    {
        var parts = input.SplitByDoubleNewline().ToArray();
        var towels = parts[0].Split(", ").ToArray();
        var patterns = parts[1].SplitBySingleNewline().ToArray();

        var matchCount = new Dictionary<string, long>();
        foreach (var pattern in patterns)
        {
            matchCount[pattern] = GetMatches(towels, pattern, new System.Collections.Concurrent.ConcurrentDictionary<string, long>());
        }

        return matchCount.Sum(kvp => kvp.Value == 0 ? 0 : 1);
    }

    private static long GetMatches(string[] towels, string pattern, System.Collections.Concurrent.ConcurrentDictionary<string, long> cache)
    {
        return cache.GetOrAdd(pattern, p => p switch
        {
            "" => 1,
            _ => towels.Where(p.StartsWith)
                .Sum(towel => GetMatches(towels, p[towel.Length ..], cache))
        });
    }

    static object PartTwo(string input, Func<TextWriter> getOutputFunction)
    {
        var parts = input.SplitByDoubleNewline().ToArray();
        var towels = parts[0].Split(", ").ToArray();
        var patterns = parts[1].SplitBySingleNewline().ToArray();

        var matchCount = new Dictionary<string, long>();
        foreach (var pattern in patterns)
        {
            matchCount[pattern] = GetMatches(towels, pattern, new System.Collections.Concurrent.ConcurrentDictionary<string, long>());
        }

        return matchCount.Sum(kvp => kvp.Value);
    }
}