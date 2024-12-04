using System.Drawing;
using AdventOfCode.Framework;
using AdventOfCode.Utilities;

namespace AdventOfCode.Solutions.Y2024.Day04;

[RegisterKeyedTransient("2024-04")] partial class Solution { }
[RegisterTransient()] partial class Solution { }

partial class Solution : ISolver
{
    public int Year => 2024;
    public int Day => 4;
    public string GetName() => "Ceres Search";

    public IEnumerable<object> Solve(string input, Func<TextWriter> getOutputFunction)
    {
        // var emptyOutput = () => new NullTextWriter();
        yield return PartOne(input, getOutputFunction);
        yield return PartTwo(input, getOutputFunction);
    }

    static object PartOne(string input, Func<TextWriter> getOutputFunction)
    {
        var grid = input.ToGrid(YAxisDirection.ZeroAtBottom, c => c);

        var startingPoints = grid.Points.Where(p => grid[p] == 'X').ToArray();

        var count = 0;
        foreach (var x in startingPoints)
        {
            foreach (var m in x.AdjacentPoints.Where(p => grid.Contains(p) && grid[p] == 'M'))
            {
                var d = x.To(m);
                var a = m + d;
                var s = a + d;
                if (grid.Contains(a) && grid[a] == 'A' &&  grid.Contains(s) && grid[s] == 'S')
                {
                    count++;
                }
            }
        }


        return count;
    }

    static object PartTwo(string input, Func<TextWriter> getOutputFunction)
    {
        var grid = input.ToGrid(YAxisDirection.ZeroAtBottom, c => c);

        var startingPoints = grid.Points.Where(p => grid[p] == 'A').ToArray();

        var count = 0;
        foreach (var a in startingPoints)
        {
            var masCount = 0;
            foreach (var m in a.DiagonalAdjacentPoints.Where(p => grid.Contains(p) && grid[p] == 'M'))
            {
                var s = a + m.To(a);
                if (grid.Contains(s) && grid[s] == 'S')
                {
                    masCount++;
                }
            }

            if (masCount == 2)
            {
                count++;
            }
        }

        return count;
    }
}