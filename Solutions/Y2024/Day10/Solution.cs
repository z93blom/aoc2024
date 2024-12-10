using AdventOfCode.Framework;
using AdventOfCode.Utilities;

namespace AdventOfCode.Solutions.Y2024.Day10;

[RegisterKeyedTransient("2024-10")] partial class Solution { }
[RegisterTransient()] partial class Solution { }

partial class Solution : ISolver
{
    public int Year => 2024;
    public int Day => 10;
    public string GetName() => "Hoof It";

    public IEnumerable<object> Solve(string input, Func<TextWriter> getOutputFunction)
    {
        // var emptyOutput = () => new NullTextWriter();
        yield return PartOne(input, getOutputFunction);
        yield return PartTwo(input, getOutputFunction);
    }

    static object PartOne(string input, Func<TextWriter> getOutputFunction)
    {
        var grid = input.ToGrid(YAxisDirection.ZeroAtTop, c => c - '0');

        var peaks = grid.Points.Where(p => grid[p] == 9).ToList();
        var currentTrails = peaks.Select(p => new Trail(p, new[] { p }.ToHashSet())).ToArray();
        var currentHeight = 9;
        while (currentHeight > 0)
        {
            currentHeight--;
            var nextTrails = new Dictionary<Point2, Trail>();
            foreach (var t in currentTrails)
            {
                foreach (var p in t.Current.OrthogonalPoints.Where(p => grid.Contains(p) && grid[p] == currentHeight))
                {
                    if (!nextTrails.ContainsKey(p))
                    {
                        nextTrails.Add(p, new Trail(p, [..t.Peaks]));
                    }
                    else
                    {
                        foreach (var peak in t.Peaks)
                        {
                            nextTrails[p].Peaks.Add(peak);
                        }
                    }
                }
            }

            currentTrails = nextTrails.Values.ToArray();
        }

        var temp = currentTrails.OrderBy(t => t.Current.Y).ThenBy(t => t.Current.X).ToList();

        return currentTrails.Sum(t => t.Peaks.Count);
    }

    static object PartTwo(string input, Func<TextWriter> getOutputFunction)
    {
        var grid = input.ToGrid(YAxisDirection.ZeroAtTop, c => c - '0');

        var peaks = grid.Points.Where(p => grid[p] == 9).ToList();
        var currentTrails = peaks.Select(p => new Trail2(p, 1)).ToArray();
        var currentHeight = 9;
        while (currentHeight > 0)
        {
            currentHeight--;
            var nextTrails = new Dictionary<Point2, Trail2>();
            foreach (var t in currentTrails)
            {
                foreach (var p in t.Current.OrthogonalPoints.Where(p => grid.Contains(p) && grid[p] == currentHeight))
                {
                    if (!nextTrails.ContainsKey(p))
                    {
                        nextTrails.Add(p, t with { Current = p });
                    }
                    else
                    {
                        nextTrails[p] = nextTrails[p] with {TrailCount = nextTrails[p].TrailCount + t.TrailCount};
                    }
                }
            }

            currentTrails = nextTrails.Values.ToArray();
        }

        var temp = currentTrails.OrderBy(t => t.Current.Y).ThenBy(t => t.Current.X).ToList();

        return currentTrails.Sum(t => t.TrailCount);
    }

    private record struct Trail(Point2 Current, HashSet<Point2> Peaks);
    private record struct Trail2(Point2 Current,int TrailCount);
}