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
        var visited = CalculateVisitPart1(grid, grid.Points.First(p => grid[p] == '^'));

        return visited.Count;
    }

    private static HashSet<Point2> CalculateVisitPart1(Grid<char> grid, Point2 start)
    {
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
            if (grid[next] == '.' || grid[next] == '^')
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
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
        }

        return visited;
    }

    private static bool IsLoop(Grid<char> grid, Point2 start)
    {
        var direction = CompassDirection.North;
        HashSet<(Point2, CompassDirection)> visited =
        [
            (start, direction)
        ];
        var guard = start;
        while (true)
        {
            var next = guard.InDirection(direction);
            if (!grid.Contains(next))
                return false;
            if (grid[next] == '.' || grid[next] == '^')
            {
                guard = next;
                if (!visited.Add((guard, direction)))
                {
                    return true;
                }
            }
            else
            {
                direction = direction switch
                {
                    CompassDirection.North => CompassDirection.East,
                    CompassDirection.East => CompassDirection.South,
                    CompassDirection.South => CompassDirection.West,
                    CompassDirection.West => CompassDirection.North,
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
        }
    }

    static object PartTwo(string input, Func<TextWriter> getOutputFunction)
    {
        var grid = input.ToGrid(YAxisDirection.ZeroAtTop, c => c);
        var start = grid.Points.First(p => grid[p] == '^');
        var possibleObstructionPoints = CalculateVisitPart1(grid, start);
        possibleObstructionPoints.Remove(start);
        var loops = 0;
        foreach (var p in possibleObstructionPoints)
        {
            grid[p] = 'O';
            if (IsLoop(grid, start))
            {
                loops++;
            }

            grid[p] = '.';
        }

        return loops;
    }
}