using AdventOfCode.Framework;
using AdventOfCode.Utilities;

namespace AdventOfCode.Solutions.Y2024.Day08;

[RegisterKeyedTransient("2024-08")] partial class Solution { }
[RegisterTransient()] partial class Solution { }

partial class Solution : ISolver
{
    public int Year => 2024;
    public int Day => 8;
    public string GetName() => "Resonant Collinearity";

    public IEnumerable<object> Solve(string input, Func<TextWriter> getOutputFunction)
    {
        // var emptyOutput = () => new NullTextWriter();
        yield return PartOne(input, getOutputFunction);
        yield return PartTwo(input, getOutputFunction);
    }

    static object PartOne(string input, Func<TextWriter> getOutputFunction)
    {
        var grid = input.ToGrid(YAxisDirection.ZeroAtTop, c => c);

        var antennas = new Dictionary<char, List<Point2>>();
        foreach (var p in grid.Points)
        {
            var antenna = grid[p];
            if (antenna != '.')
            {
                if (!antennas.ContainsKey(antenna))
                {
                    antennas.Add(antenna, []);
                }
                antennas[antenna].Add(p);
            }
        }

        var antiNodes = new HashSet<Point2>();
        foreach (var kvp in antennas.Where(kvp => kvp.Value.Count >= 2))
        {
            var (antenna, locations) = kvp;
            var pairs = locations.SelectMany((x, i) => locations.Skip(i + 1), Tuple.Create);
            foreach (var t in pairs)
            {
                var distance = t.Item1.To(t.Item2);
                var antiNode1 = t.Item1 + distance.Inverse;
                var antinode2 = t.Item2 + distance;
                antiNodes.Add(antiNode1);
                antiNodes.Add(antinode2);
            }
        }

        var partOne = antiNodes.Where(grid.Contains);
        return partOne.Count();
    }

    static object PartTwo(string input, Func<TextWriter> getOutputFunction)
    {
        var grid = input.ToGrid(YAxisDirection.ZeroAtTop, c => c);

        var antennas = new Dictionary<char, List<Point2>>();
        foreach (var p in grid.Points)
        {
            var antenna = grid[p];
            if (antenna != '.')
            {
                if (!antennas.ContainsKey(antenna))
                {
                    antennas.Add(antenna, []);
                }
                antennas[antenna].Add(p);
            }
        }

        var antiNodes = new HashSet<Point2>();
        foreach (var kvp in antennas.Where(kvp => kvp.Value.Count >= 2))
        {
            var (antenna, locations) = kvp;
            var pairs = locations.SelectMany((x, i) => locations.Skip(i + 1), Tuple.Create);
            foreach (var t in pairs)
            {
                var distance = t.Item1.To(t.Item2);
                var p = t.Item2 + distance;
                while (grid.Contains(p))
                {
                    antiNodes.Add(p);
                    p += distance;
                }

                var inverse = distance.Inverse;
                p = t.Item1 + inverse;
                while (grid.Contains(p))
                {
                    antiNodes.Add(p);
                    p += inverse;
                }

                if (locations.Count > 2)
                {
                    foreach (var location in locations)
                        antiNodes.Add(location);
                }
            }
        }

        var partTwo = antiNodes.Count;
        return partTwo;
    }
}