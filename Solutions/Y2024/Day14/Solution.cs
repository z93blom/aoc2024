using System.Drawing;
using AdventOfCode.Framework;
using AdventOfCode.Utilities;

namespace AdventOfCode.Solutions.Y2024.Day14;

[RegisterKeyedTransient("2024-14")] partial class Solution { }
[RegisterTransient()] partial class Solution { }

partial class Solution : ISolver
{
    public int Year => 2024;
    public int Day => 14;
    public string GetName() => "Restroom Redoubt";

    public IEnumerable<object> Solve(string input, Func<TextWriter> getOutputFunction)
    {
        // var emptyOutput = () => new NullTextWriter();
        yield return PartOne(input, getOutputFunction);
        yield return PartTwo(input, getOutputFunction);
    }

    private record struct Robot(Point2 Location, Vec2 Velocity)
    {
        public Robot Move(Size gridSize)
        {
            var newLocation = Location + Velocity;
            if (Velocity.X > 0 && newLocation.X >= gridSize.Width)
            {
                newLocation = newLocation with { X = newLocation.X - gridSize.Width };
            }
            if (Velocity.X < 0 && newLocation.X < 0)
            {
                newLocation = newLocation with { X = newLocation.X + gridSize.Width };
            }

            if (Velocity.Y > 0 && newLocation.Y >= gridSize.Height)
            {
                newLocation = newLocation with { Y = newLocation.Y - gridSize.Height };
            }
            if (Velocity.Y < 0 && newLocation.Y < 0)
            {
                newLocation = newLocation with { Y = newLocation.Y + gridSize.Height };
            }

            return this with { Location = newLocation };
        }
    }

    static object PartOne(string input, Func<TextWriter> getOutputFunction)
    {
        var gridSize = input.Integers().Chunk(2).Select(e => e.ToArray()).Select(a => new Size(a[0], a[1])).First();

        var robots = input
            .Longs()
            .Skip(2)
            .Chunk(4)
            .Select(v => v.ToArray())
            .Select(a => new Robot(new Point2(a[0], a[1], YAxisDirection.ZeroAtTop), new Vec2(a[2], a[3])))
            .ToList();

        for (var iteration = 0; iteration < 100; iteration++)
        {
            var newPositions = new List<Robot>(robots.Count);
            foreach (var robot in robots)
            {
                newPositions.Add(robot.Move(gridSize));
            }

            robots = newPositions;
        }

        var robotsPerQuadrant = new long[4];

        foreach (var robot in robots)
        {
            if (robot.Location.X < gridSize.Width / 2 && robot.Location.Y < gridSize.Height / 2)
            {
                robotsPerQuadrant[0]++;
            }
            if (robot.Location.X > gridSize.Width / 2 && robot.Location.Y < gridSize.Height / 2)
            {
                robotsPerQuadrant[1]++;
            }
            if (robot.Location.X < gridSize.Width / 2 && robot.Location.Y > gridSize.Height / 2)
            {
                robotsPerQuadrant[2]++;
            }
            if (robot.Location.X > gridSize.Width / 2 && robot.Location.Y > gridSize.Height / 2)
            {
                robotsPerQuadrant[3]++;
            }

        }

        var safetyFactor = robotsPerQuadrant.Aggregate(1L, (v, product) => v* product);
        return safetyFactor;
    }

    static object PartTwo(string input, Func<TextWriter> getOutputFunction)
    {
        return 0;
    }
}