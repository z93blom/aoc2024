using AdventOfCode.Framework;
using AdventOfCode.Utilities;
using QuikGraph;
using QuikGraph.Algorithms;
using QuikGraph.Algorithms.ShortestPath;

namespace AdventOfCode.Solutions.Y2024.Day16;

[RegisterKeyedTransient("2024-16")] partial class Solution { }
[RegisterTransient()] partial class Solution { }

partial class Solution : ISolver
{
    public int Year => 2024;
    public int Day => 16;
    public string GetName() => "Reindeer Maze";

    public IEnumerable<object> Solve(string input, Func<TextWriter> getOutputFunction)
    {
        // var emptyOutput = () => new NullTextWriter();
        yield return PartOne(input, getOutputFunction);
        yield return PartTwo(input, getOutputFunction);
    }

    private const long CostToTurn = 1000L;
    private const long CostToMove = 1L;

    static object PartOne(string input, Func<TextWriter> getOutputFunction)
    {
        var grid = input.ToGrid(YAxisDirection.ZeroAtBottom, c => c);
        var start = new Point2(0, 0, YAxisDirection.ZeroAtBottom);
        var end = new Point2(0, 0, YAxisDirection.ZeroAtBottom); ;
        Dictionary<PointWithDirection, Point2Node<CompassDirection>> nodes = [];
        Dictionary<Point2Edge<CompassDirection>, long> edges = [];
        foreach (var p in grid.Points.Where(p => grid[p] != '#'))
        {
            if (grid[p] == 'S')
            {
                start = p;
            }

            if (grid[p] == 'E')
            {
                end = p;
            }

            foreach (var compassDirection in Enum.GetValues<CompassDirection>())
            {
                var key = new PointWithDirection(p, compassDirection);

                if (!nodes.ContainsKey(key))
                {
                    nodes.Add(key, new Point2Node<CompassDirection>(p, compassDirection));
                }
            }

            // Add the edges from this node to itself.
            // Turning CW
            edges.Add(new Point2Edge<CompassDirection>(
                nodes[new PointWithDirection(p, CompassDirection.North)],
                nodes[new PointWithDirection(p, CompassDirection.East)]),
                CostToTurn);
            edges.Add(new Point2Edge<CompassDirection>(
                nodes[new PointWithDirection(p, CompassDirection.East)],
                nodes[new PointWithDirection(p, CompassDirection.South)]),
                CostToTurn);
            edges.Add(new Point2Edge<CompassDirection>(
                nodes[new PointWithDirection(p, CompassDirection.South)],
                nodes[new PointWithDirection(p, CompassDirection.West)]),
                CostToTurn);
            edges.Add(new Point2Edge<CompassDirection>(
                nodes[new PointWithDirection(p, CompassDirection.West)],
                nodes[new PointWithDirection(p, CompassDirection.North)]),
                CostToTurn);

            // CCW
            edges.Add(new Point2Edge<CompassDirection>(
                nodes[new PointWithDirection(p, CompassDirection.North)],
                nodes[new PointWithDirection(p, CompassDirection.West)]),
                CostToTurn);
            edges.Add(new Point2Edge<CompassDirection>(
                nodes[new PointWithDirection(p, CompassDirection.West)],
                nodes[new PointWithDirection(p, CompassDirection.South)]),
                CostToTurn);
            edges.Add(new Point2Edge<CompassDirection>(
                nodes[new PointWithDirection(p, CompassDirection.South)],
                nodes[new PointWithDirection(p, CompassDirection.East)]),
                CostToTurn);
            edges.Add(new Point2Edge<CompassDirection>(
                nodes[new PointWithDirection(p, CompassDirection.East)],
                nodes[new PointWithDirection(p, CompassDirection.North)]),
                CostToTurn);

            // Then to other points adjacent points
            foreach (var d in Enum.GetValues<CompassDirection>())
            {
                var adj = p.InDirection(d);
                if (!grid.Contains(adj) || grid[adj] == '#')
                    continue;

                var key = new PointWithDirection(adj, d);

                if (!nodes.ContainsKey(key))
                {
                    nodes.Add(key, new Point2Node<CompassDirection>(adj, d));
                }

                edges.Add(new Point2Edge<CompassDirection>(
                        nodes[new PointWithDirection(p, d)],
                        nodes[new PointWithDirection(adj, d)]),
                    CostToMove);
            }
        }

        // Add the end node and the edges to it.
        // Let's just pretend that it faces east.
        var pd = new PointWithDirection(end, CompassDirection.East);
        var endNode = new Point2Node<CompassDirection>(end, pd.Direction);
        //nodes.Add(pd, endNode);
        foreach (var d in Enum.GetValues<CompassDirection>())
        {
            edges.Add(new Point2Edge<CompassDirection>(
                    nodes[new PointWithDirection(end, d)],
                    endNode),
                0);
        }

        var startNode = nodes[new PointWithDirection(start, CompassDirection.East)];

        var graph = new AdjacencyGraph<Point2Node<CompassDirection>, Point2Edge<CompassDirection>>(false);
        graph.AddVertex(endNode);
        foreach (var node in nodes.Values)
        {
            graph.AddVertex(node);
        }

        foreach (var edge in edges.Keys)
        {
            graph.AddEdge(edge);
        }


        var dijkstra =
            new DijkstraShortestPathAlgorithm<Point2Node<CompassDirection>, Point2Edge<CompassDirection>>(graph, edge => edges[edge]);

        dijkstra.SetRootVertex(startNode);
        dijkstra.Compute();
        var cost = dijkstra.GetDistance(endNode);
        return cost;
    }

    public record struct PointWithDirection(Point2 Point, CompassDirection Direction);

    static object PartTwo(string input, Func<TextWriter> getOutputFunction)
    {
        var grid = input.ToGrid(YAxisDirection.ZeroAtBottom, c => c);
        var start = new Point2(0, 0, YAxisDirection.ZeroAtBottom);
        var end = new Point2(0, 0, YAxisDirection.ZeroAtBottom); ;
        Dictionary<PointWithDirection, Point2Node<CompassDirection>> nodes = [];
        Dictionary<Point2Edge<CompassDirection>, long> edges = [];
        foreach (var p in grid.Points.Where(p => grid[p] != '#'))
        {
            if (grid[p] == 'S')
            {
                start = p;
            }

            if (grid[p] == 'E')
            {
                end = p;
            }

            foreach (var compassDirection in Enum.GetValues<CompassDirection>())
            {
                var key = new PointWithDirection(p, compassDirection);

                if (!nodes.ContainsKey(key))
                {
                    nodes.Add(key, new Point2Node<CompassDirection>(p, compassDirection));
                }
            }

            // Add the edges from this node to itself.
            // Turning CW
            edges.Add(new Point2Edge<CompassDirection>(
                nodes[new PointWithDirection(p, CompassDirection.North)],
                nodes[new PointWithDirection(p, CompassDirection.East)]),
                CostToTurn);
            edges.Add(new Point2Edge<CompassDirection>(
                nodes[new PointWithDirection(p, CompassDirection.East)],
                nodes[new PointWithDirection(p, CompassDirection.South)]),
                CostToTurn);
            edges.Add(new Point2Edge<CompassDirection>(
                nodes[new PointWithDirection(p, CompassDirection.South)],
                nodes[new PointWithDirection(p, CompassDirection.West)]),
                CostToTurn);
            edges.Add(new Point2Edge<CompassDirection>(
                nodes[new PointWithDirection(p, CompassDirection.West)],
                nodes[new PointWithDirection(p, CompassDirection.North)]),
                CostToTurn);

            // CCW
            edges.Add(new Point2Edge<CompassDirection>(
                nodes[new PointWithDirection(p, CompassDirection.North)],
                nodes[new PointWithDirection(p, CompassDirection.West)]),
                CostToTurn);
            edges.Add(new Point2Edge<CompassDirection>(
                nodes[new PointWithDirection(p, CompassDirection.West)],
                nodes[new PointWithDirection(p, CompassDirection.South)]),
                CostToTurn);
            edges.Add(new Point2Edge<CompassDirection>(
                nodes[new PointWithDirection(p, CompassDirection.South)],
                nodes[new PointWithDirection(p, CompassDirection.East)]),
                CostToTurn);
            edges.Add(new Point2Edge<CompassDirection>(
                nodes[new PointWithDirection(p, CompassDirection.East)],
                nodes[new PointWithDirection(p, CompassDirection.North)]),
                CostToTurn);

            // Then to other points adjacent points
            foreach (var d in Enum.GetValues<CompassDirection>())
            {
                var adj = p.InDirection(d);
                if (!grid.Contains(adj) || grid[adj] == '#')
                    continue;

                var key = new PointWithDirection(adj, d);

                if (!nodes.ContainsKey(key))
                {
                    nodes.Add(key, new Point2Node<CompassDirection>(adj, d));
                }

                edges.Add(new Point2Edge<CompassDirection>(
                        nodes[new PointWithDirection(p, d)],
                        nodes[new PointWithDirection(adj, d)]),
                    CostToMove);
            }
        }

        // Add the end node and the edges to it.
        // Let's just pretend that it faces east.
        var pd = new PointWithDirection(end, CompassDirection.East);
        var endNode = new Point2Node<CompassDirection>(end, pd.Direction);
        //nodes.Add(pd, endNode);
        foreach (var d in Enum.GetValues<CompassDirection>())
        {
            edges.Add(new Point2Edge<CompassDirection>(
                    nodes[new PointWithDirection(end, d)],
                    endNode),
                0);
        }

        var startNode = nodes[new PointWithDirection(start, CompassDirection.East)];

        var g = edges.Keys.ToBidirectionalGraph<Point2Node<CompassDirection>, Point2Edge<CompassDirection>>(false);
        var paths = g.RankedShortestPathHoffmanPavley(edge => edges[edge], startNode, endNode, 100).ToArray();

        var minimumCost = paths[0].Select(edge => edges[edge]).Sum();
        var tilesVisited = paths[0].SelectMany(edge => new[] { edge.Source, edge.Target })
            .Select(node => node.Point)
            .ToHashSet();

        foreach (var path in paths.Skip(1).Where(path => path.Sum(edge => edges[edge]) == minimumCost))
        {
            tilesVisited.UnionWith(path
                .SelectMany(edge => new[] { edge.Source, edge.Target })
                .Select(node => node.Point));
        }
        
        return tilesVisited.Count;
    }
}