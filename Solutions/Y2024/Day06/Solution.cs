using System.Drawing;
using System.Security.Principal;
using AdventOfCode.Framework;
using AdventOfCode.Utilities;

namespace AdventOfCode.Solutions.Y2024.Day06;

[RegisterKeyedTransient("2024-06")] partial class Solution { }
[RegisterTransient()] partial class Solution { }

partial class Solution : ISolver
{
    public int Year => 2024;
    public int Day => 6;
    public string GetName() => "Guard Gallivant";

    public IEnumerable<object> Solve(string input, Func<TextWriter> getOutputFunction)
    {
        // var emptyOutput = () => new NullTextWriter();
        yield return PartOne(input, getOutputFunction);
        yield return PartTwo(input, getOutputFunction);
    }

    static object PartOne(string input, Func<TextWriter> getOutputFunction)
    {
        var grid = input.ToGrid(YAxisDirection.ZeroAtTop, c => c);
        var start = grid.Points.First(p => grid[p] == '^');
        grid[start] = '.';

    var direction = CompassDirection.North;
        HashSet<Point2> visited =
        [
            start
        ];
        var guard = start;
        while (true)
        {
            var next = guard.InDirection(direction);
            if (!grid.Contains(next))
                break;
            if (grid[next] == '.')
            {
                guard = next;
                visited.Add(guard);
            }
            else
            {
                direction = direction switch
                {
                    CompassDirection.North => CompassDirection.East,
                    CompassDirection.East => CompassDirection.South,
                    CompassDirection.South => CompassDirection.West,
                    CompassDirection.West => CompassDirection.North,
                };
            }
        }

        return visited.Count;
    }

    static object PartTwo(string input, Func<TextWriter> getOutputFunction)
    {
        return 0;
    }
}