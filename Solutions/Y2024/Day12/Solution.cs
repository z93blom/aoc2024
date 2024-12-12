using System.Security.Cryptography;
using AdventOfCode.Framework;
using AdventOfCode.Utilities;
using Spectre.Console;
using SuperLinq;

namespace AdventOfCode.Solutions.Y2024.Day12;

[RegisterKeyedTransient("2024-12")] partial class Solution { }
[RegisterTransient()] partial class Solution { }

partial class Solution : ISolver
{
    public int Year => 2024;
    public int Day => 12;
    public string GetName() => "Garden Groups";

    public IEnumerable<object> Solve(string input, Func<TextWriter> getOutputFunction)
    {
        // var emptyOutput = () => new NullTextWriter();
        yield return PartOne(input, getOutputFunction);
        yield return PartTwo(input, getOutputFunction);
    }

    static object PartOne(string input, Func<TextWriter> getOutputFunction)
    {
        var plantToPlots = new Dictionary<char, List<Point2>>();
        var garden = input.ToGrid(YAxisDirection.ZeroAtTop, (c, p) =>
        {
            if (!plantToPlots.ContainsKey(c))
            {
                plantToPlots.Add(c, []);
            }

            plantToPlots[c].Add(p);
            return c;
        });

        var regions = new List<Region>();
        foreach (var plant in plantToPlots.Keys)
        {
            var plantPoints = new HashSet<Point2>(plantToPlots[plant]);
            while (plantPoints.Count > 0)
            {
                // Create a new region.
                var area = 0;
                var perimeter = 0;
                var plots = new HashSet<Point2>();
                var q = new Stack<Point2>();
                q.Push(plantPoints.First());

                while (q.Count > 0)
                {
                    var plot = q.Pop();
                    plantPoints.Remove(plot);
                    plots.Add(plot);
                    var plotsWithSameAdjacentPlant = plot.OrthogonalPoints.Where(p => garden.Contains(p) && plantToPlots[plant].Contains(p)).ToArray();
                    area += 1;
                    perimeter += 4 - plotsWithSameAdjacentPlant.Length;
                    foreach (var adj in plotsWithSameAdjacentPlant.Where(p => !plots.Contains(p) && !q.Contains(p)))
                    {
                        q.Push(adj);
                    }
                }

                var region = new Region(plant, plots, area, perimeter);
                regions.Add(region);
            }

        }

        return regions.Sum(r => r.Area * r.Perimeter);
    }

    private record struct Region(char Plant, HashSet<Point2> Plots, int Area, int Perimeter);

    private record struct Region2(char Plant, HashSet<Point2> Plots, int Area, int Sides);

    private enum Direction
    {
        EastWest,
        NorthSouth
    }

    private record struct Fencing(Point2 First, Point2 Inside, Direction Direction);

    static object PartTwo(string input, Func<TextWriter> getOutputFunction)
    {
        var plantToPlots = new Dictionary<char, List<Point2>>();
        var garden = input.ToGrid(YAxisDirection.ZeroAtTop, (c, p) =>
        {
            if (!plantToPlots.ContainsKey(c))
            {
                plantToPlots.Add(c, []);
            }

            plantToPlots[c].Add(p);
            return c;
        });

        var regions = new List<Region2>();
        foreach (var plant in plantToPlots.Keys)
        {
            var plantPoints = new HashSet<Point2>(plantToPlots[plant]);
            while (plantPoints.Count > 0)
            {
                // Create a new region.
                var area = 0;
                var fencing = new HashSet<Fencing>();
                var plots = new HashSet<Point2>();
                var q = new Stack<Point2>();
                q.Push(plantPoints.First());

                while (q.Count > 0)
                {
                    var plot = q.Pop();
                    plantPoints.Remove(plot);
                    plots.Add(plot);
                    area += 1;

                    var n = plot.Above;
                    if (!garden.Contains(n) || (garden.Contains(n) && garden[n] != plant))
                    {
                        fencing.Add(new Fencing(n, plot, Direction.EastWest));
                    }
                    
                    var s = plot.Below;
                    if (!garden.Contains(s) || (garden.Contains(s) && garden[s] != plant))
                    {
                        fencing.Add(new Fencing(plot, plot, Direction.EastWest));
                    }

                    var e = plot.Left;
                    if (!garden.Contains(e) || (garden.Contains(e) && garden[e] != plant))
                    {
                        fencing.Add(new Fencing(e, plot, Direction.NorthSouth));
                    }

                    var w = plot.Right;
                    if (!garden.Contains(w) || (garden.Contains(w) && garden[w] != plant))
                    {
                        fencing.Add(new Fencing(plot, plot, Direction.NorthSouth));
                    }


                    foreach (var adj in plot.OrthogonalPoints.Where(p => garden.Contains(p) && plantToPlots[plant].Contains(p)).ToArray().Where(p => !plots.Contains(p) && !q.Contains(p)))
                    {
                        q.Push(adj);
                    }
                }

                // Calculate the sides.
                //var dict = fencing.Distinct(f => f.First).ToDictionary(f => f.First, f => f);
                var sides = 0;
                while (fencing.Count > 0)
                {
                    var stack = new Stack<Fencing>();
                    stack.Push(fencing.First());
                    while (stack.Count > 0)
                    {
                        var f = stack.Pop();
                        fencing.Remove(f);

                        if (f.Direction == Direction.NorthSouth)
                        {
                            foreach (var ff in fencing.Where(ff => ff.Direction == f.Direction && (ff.First == f.First.Above ||  ff.First == f.First.Below)))
                            {
                                if (garden[ff.Inside] == garden[f.Inside] && ff.Inside.To(f.Inside).ManhattanDistance == 1)
                                {
                                    stack.Push(ff);
                                }
                            }
                        }
                        else
                        {
                            foreach (var ff in fencing.Where(ff => ff.Direction == f.Direction && (ff.First == f.First.Left || ff.First == f.First.Right)))
                            {
                                if (garden[ff.Inside] == garden[f.Inside] && ff.Inside.To(f.Inside).ManhattanDistance == 1)
                                {
                                    stack.Push(ff);
                                }
                            }
                        }
                    }

                    sides += 1;
                }

                var region = new Region2(plant, plots, area, sides);
                regions.Add(region);
            }
        }

        return regions.Sum(r => r.Area * r.Sides);
    }
}