using AdventOfCode.Framework;
using AdventOfCode.Utilities;
using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Y2024.Day03;

[RegisterKeyedTransient("2024-03")] partial class Solution { }
[RegisterTransient()] partial class Solution { }

partial class Solution : ISolver
{
    [GeneratedRegex(@"mul\((\d{1,3}),(\d{1,3})\)|do\(\)|don\'t\(\)")]
    private static partial Regex Operations();

    public int Year => 2024;
    public int Day => 3;
    public string GetName() => "Mull It Over";

    public IEnumerable<object> Solve(string input, Func<TextWriter> getOutputFunction)
    {
        // var emptyOutput = () => new NullTextWriter();
        yield return PartOne(input, getOutputFunction);
        yield return PartTwo(input, getOutputFunction);
    }

    static object PartOne(string input, Func<TextWriter> getOutputFunction)
    {
        var result = input.Matches(@"mul\((\d{1,3}),(\d{1,3})\)")
            .Select(m => int.Parse(m.Groups[1].ValueSpan) * int.Parse(m.Groups[2].ValueSpan))
            .Sum();
        return result;
    }

    static object PartTwo(string input, Func<TextWriter> getOutputFunction)
    {
        var ops = input.Matches(Operations)
            .Select(GetOp)
            .ToList();

        var result = ops.Aggregate((enabled: true, sum: 0), (x, op) =>
            {
                var result = (op, x.enabled) switch
                {
                    (Enable, _) => (true, x.sum),
                    (Disable, _) => (false, x.sum),
                    (Multiply m, true) => (true, x.sum + m.Val),
                    (Multiply m, false) => (false, x.sum),
                    _ => throw new ArgumentOutOfRangeException()
                };
                return result;
            });
        return result.Item2;

        Op GetOp(Match m)
        {
            if (m.Value.StartsWith("mul")) return new Multiply(int.Parse(m.Groups[1].ValueSpan) * int.Parse(m.Groups[2].ValueSpan));
            if (m.Value.StartsWith("don't")) return new Disable();
            if (m.Value.StartsWith("do")) return new Enable();
            throw new ArgumentOutOfRangeException();
        }
    }

    private record Op;
    private record Enable : Op;
    private record Disable : Op;
    private record Multiply(int Val) : Op;
}