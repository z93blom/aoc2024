using System.Net.WebSockets;
using System.Runtime.InteropServices.ComTypes;
using System.Windows.Markup;
using System.Xml;
using AdventOfCode.Framework;
using AdventOfCode.Utilities;
using LanguageExt;

namespace AdventOfCode.Solutions.Y2024.Day02;

[RegisterKeyedTransient("2024-02")] partial class Solution { }
[RegisterTransient()] partial class Solution { }

partial class Solution : ISolver
{
    public int Year => 2024;
    public int Day => 2;
    public string GetName() => "Red-Nosed Reports";

    public IEnumerable<object> Solve(string input, Func<TextWriter> getOutputFunction)
    {
        // var emptyOutput = () => new NullTextWriter();
        yield return PartOne(input, getOutputFunction);
        yield return PartTwo(input, getOutputFunction);
    }

    static object PartOne(string input, Func<TextWriter> getOutputFunction)
    {
        var lines = input.Lines()
            .Select(l => l.Integers().ToArray())
            .ToArray();
        var deltas = lines.Select(l =>
        {
            var list = new List<int>();
            foreach (var (index, value) in l.Index().Skip(1))
            {
                list.Add(l[index] - l[index - 1]);
            }

            return list;
        });

        var valid = deltas.Select(IsValid);
        return valid.Count(v => v);
    }

    private static bool IsValid(List<int> delta)
    {
        var max = delta.Select(Math.Abs).Max();
        var allDecreasing = delta.All(d => d < 0);
        var allIncreasing = delta.All(d => d > 0);
         return max <= 3 && (allDecreasing || allIncreasing);
    }

    static object PartTwo(string input, Func<TextWriter> getOutputFunction)
    {
        var lines = input.Lines()
            .Select(l => l.Integers().ToArray())
            .ToArray();

        var numberOfValid = 0;
        foreach (var report in lines)
        {
            var valid = Enumerable.Range(0, report.Length)
                .Any(i =>
                {
                    var removedReport = new List<int>(report);
                    removedReport.RemoveAt(i);
                    return CheckValidity(removedReport);
                });

            if (valid)
                numberOfValid++;
        }

        return numberOfValid;
    }

    private static bool CheckValidity(List<int> report)
    { 
        var (_, valid) = report
            .Skip(1)
            .Aggregate((report[0], true),
                (t, value) => (value, t.Item2 && value - t.Item1 >= 1 && value - t.Item1 <= 3));

        if (!valid)
        {
            (_, valid) = report
                .Skip(1)
                .Aggregate((report[0], true),
                    (t, value) => (value, t.Item2 && t.Item1 - value >= 1 && t.Item1 - value <= 3));
        }

        return valid;
    }
}