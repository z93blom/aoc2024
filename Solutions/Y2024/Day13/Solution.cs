using System.Numerics;
using AdventOfCode.Framework;
using AdventOfCode.Utilities;
using SuperLinq;

namespace AdventOfCode.Solutions.Y2024.Day13;

[RegisterKeyedTransient("2024-13")] partial class Solution { }
[RegisterTransient()] partial class Solution { }

partial class Solution : ISolver
{
    public int Year => 2024;
    public int Day => 13;
    public string GetName() => "Claw Contraption";

    public IEnumerable<object> Solve(string input, Func<TextWriter> getOutputFunction)
    {
        // var emptyOutput = () => new NullTextWriter();
        yield return PartOne(input, getOutputFunction);
        yield return PartTwo(input, getOutputFunction);
    }

    static object PartOne(string input, Func<TextWriter> getOutputFunction)
    {
        var machines = input.Longs().Chunk(6).ToArray();
        var tokens = 0L;
        foreach (var m in machines)
        {
            var a = (m[5] * m[2] - m[4] * m[3]) / (m[1] * m[2] - m[0] * m[3]);
            var b = (m[4] - a * m[0]) / m[2];

            var x  = a * m[0] + b * m[2];
            var y = a * m[1] + b * m[3];
            if (x == m[4] && y == m[5] && a <= 100 && b <= 100)
            {
                tokens += a * 3 + b;
            }
        }
        return tokens;
    }

    static object PartTwo(string input, Func<TextWriter> getOutputFunction)
    {
        var machines = input.Longs().Chunk(6).ToArray();
        var tokens = 0L;
        foreach (var m in machines)
        {
            m[4] += 10000000000000;
            m[5] += 10000000000000;
            var a = (m[5] * m[2] - m[4] * m[3]) / (m[1] * m[2] - m[0] * m[3]);
            var b = (m[4] - a * m[0]) / m[2];

            var x = a * m[0] + b * m[2];
            var y = a * m[1] + b * m[3];
            if (x == m[4] && y == m[5])
            {
                tokens += a * 3 + b;
            }
        }
        return tokens; 
    }
}