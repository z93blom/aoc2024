using AdventOfCode.Framework;
using AdventOfCode.Utilities;

namespace AdventOfCode.Solutions.Y2024.Day01;

[RegisterKeyedTransient("2024-01")] partial class Solution { }
[RegisterTransient()] partial class Solution { }

partial class Solution : ISolver
{
    public int Year => 2024;
    public int Day => 1;
    public string GetName() => "Historian Hysteria";

    public IEnumerable<object> Solve(string input, Func<TextWriter> getOutputFunction)
    {
        // var emptyOutput = () => new NullTextWriter();
        yield return PartOne(input, getOutputFunction);
        yield return PartTwo(input, getOutputFunction);
    }

    static object PartOne(string input, Func<TextWriter> getOutputFunction)
    {
        var i = input
            .Lines()
            .Select(l => l.ParseNumbers().ToList());
        var left = new List<int>();
        var right = new List<int>();
        foreach (var l in i)
        {
            left.Add(l[0]);
            right.Add(l[1]);
        }

        left.Sort();
        right.Sort();

        var sum = left.Zip(right).Select(t => Math.Abs(t.Item1 - t.Item2)).Sum();

        return sum;
    }

    static object PartTwo(string input, Func<TextWriter> getOutputFunction)
    {
        var i = input
            .Lines()
            .Select(l => l.ParseNumbers().ToList());
        var left = new List<int>();
        var right = new Dictionary<int, int>();
        foreach (var l in i)
        {
            left.Add(l[0]);
            if (right.ContainsKey(l[1]))
            {
                right[l[1]] += 1;
            }
            else
            {
                right[l[1]] = 1;
            }
        }

        var sum = 0;
        foreach (var l in left.Where(v => right.ContainsKey(v)))
        {
            sum += l * right[l];
        }

        return sum;
    }
}