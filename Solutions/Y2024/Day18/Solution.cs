using AdventOfCode.Framework;
using AdventOfCode.Utilities;
using QuikGraph;
using QuikGraph.Algorithms;
using Spectre.Console;

namespace AdventOfCode.Solutions.Y2024.Day18;

[RegisterKeyedTransient("2024-18")] partial class Solution { }
[RegisterTransient()] partial class Solution { }

partial class Solution : ISolver
{
    public int Year => 2024;
    public int Day => 18;
    public string GetName() => "RAM Run";

    public IEnumerable<object> Solve(string input, Func<TextWriter> getOutputFunction)
    {
        // var emptyOutput = () => new NullTextWriter();
        yield return PartOne(input, getOutputFunction);
        yield return PartTwo(input, getOutputFunction);
    }

    static object PartOne(string input, Func<TextWriter> getOutputFunction)
    {
        var blocked = input
            .Integers()
            .Chunk(2)
            .Take(1024)
            .Select(a => new Point2(a[0], a[1], YAxisDirection.ZeroAtTop)).ToHashSet();
        var size = 70;
        if (blocked.Count < 1024)
        {
            size = 6;
        }

        var grid = new Grid<char>(size + 1, size + 1, YAxisDirection.ZeroAtTop);

        var vertices = new Dictionary<Point2, Point2Node<char>>();
        var edges = new List<Point2Edge<char>>();
        foreach (var p in grid.Points.Where(p => !blocked.Contains(p)))
        {
            if (!vertices.ContainsKey(p))
            {
                vertices.Add(p, new Point2Node<char>(p, '.'));
            }

            var node = vertices[p];

            foreach (var adj in p.OrthogonalPoints.Where(adj => !blocked.Contains(adj) && grid.Contains(adj)))
            {
                if (!vertices.ContainsKey(adj))
                {
                    vertices.Add(adj, new Point2Node<char>(adj, '.'));
                }

                var adjNode = vertices[adj];
                edges.Add(new Point2Edge<char>(node, adjNode));
            }
        }

        var graph = edges.ToAdjacencyGraph<Point2Node<char>, Point2Edge<char>>(false);

        var tryMethod = graph.ShortestPathsDijkstra(_ => 1, vertices[new Point2(0, 0, YAxisDirection.ZeroAtTop)]);
        if (tryMethod(vertices[new Point2(size, size, YAxisDirection.ZeroAtTop)], out IEnumerable<Point2Edge<char>> result))
        {
            return result.Count();
        }
        return 0;
    }

    static object PartTwo(string input, Func<TextWriter> getOutputFunction)
    {
        var blocked = input
            .Integers()
            .Chunk(2)
            //.Take(1024)
            .Select(a => new Point2(a[0], a[1], YAxisDirection.ZeroAtTop))
            .ToList();
        var size = 70;
        if (blocked.Count < 1024)
        {
            size = 6;
        }

        var grid = new Grid<char>(size + 1, size + 1, YAxisDirection.ZeroAtTop);

        var vertices = new Dictionary<Point2, Point2Node<char>>();
        var edges = new List<Point2Edge<char>>();
        foreach (var p in grid.Points)
        {
            if (!vertices.ContainsKey(p))
            {
                vertices.Add(p, new Point2Node<char>(p, '.'));
            }

            var node = vertices[p];

            foreach (var adj in p.OrthogonalPoints.Where(adj => grid.Contains(adj)))
            {
                if (!vertices.ContainsKey(adj))
                {
                    vertices.Add(adj, new Point2Node<char>(adj, '.'));
                }

                var adjNode = vertices[adj];
                edges.Add(new Point2Edge<char>(node, adjNode));
            }
        }

        
        var graph = edges.ToAdjacencyGraph<Point2Node<char>, Point2Edge<char>>(false);

        var blockedIndex = 0;
        var target = vertices[new Point2(size, size, YAxisDirection.ZeroAtTop)];
        var start = vertices[new Point2(0, 0, YAxisDirection.ZeroAtTop)];
        while (true)
        {
            var p = blocked[blockedIndex];
            var edgesToBeRemoved = graph.Edges.Where(e => e.Target.Point == p).ToArray();
            foreach (var edge in edgesToBeRemoved)
            {
                graph.RemoveEdge(edge);
            }

            var tryMethod = graph.ShortestPathsDijkstra(_ => 1, start);
            if (!tryMethod(target, out _))
            {
                break;
            }

            blockedIndex++;
        }

        var blockingPoint = blocked[blockedIndex];
        return $"{blockingPoint.X},{blockingPoint.Y}";
    }
}