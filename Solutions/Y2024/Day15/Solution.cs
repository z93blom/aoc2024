using AdventOfCode.Framework;
using AdventOfCode.Utilities;

namespace AdventOfCode.Solutions.Y2024.Day15;

[RegisterKeyedTransient("2024-15")] partial class Solution { }
[RegisterTransient()] partial class Solution { }

partial class Solution : ISolver
{
    public int Year => 2024;
    public int Day => 15;
    public string GetName() => "Warehouse Woes";

    public IEnumerable<object> Solve(string input, Func<TextWriter> getOutputFunction)
    {
        // var emptyOutput = () => new NullTextWriter();
        yield return PartOne(input, getOutputFunction);
        yield return PartTwo(input, getOutputFunction);
    }

    static object PartOne(string input, Func<TextWriter> getOutputFunction)
    {
        var lines = input.SplitByDoubleNewline().ToArray();

        var grid = lines[0].ToGrid(YAxisDirection.ZeroAtTop, c => c);

        var robot = grid.Points.First(p => grid[p] == '@');
        grid[robot] = '.';

        var allowed = "^><v".ToHashSet();
        var directions = lines[1].Where(c => allowed.Contains(c)).Select(c => c switch
        {
            '^' => CompassDirection.North,
            '>' => CompassDirection.East,
            'v' => CompassDirection.South,
            '<' => CompassDirection.West,
            _ => throw new ArgumentOutOfRangeException()
        });

        foreach (var d in directions)
        {
            var p = robot.Move(d, 1);

            if (grid[p] != '#' && grid[p] != 'O')
            {
                // Nothing in the way - just move to the new location
                robot = p;
                continue;
            }

            var nextEmpty = p;
            while (grid[nextEmpty] == 'O')
            {
                nextEmpty = nextEmpty.Move(d, 1);
            }

            if (grid[nextEmpty] == '#')
            {
                // Nothing happens - the robot cannot move in this direction
                continue;
            }

            grid[nextEmpty] = 'O';
            grid[p] = '.';

            robot = p;
        }

        var gpsSum = grid.Points.Where(p => grid[p] == 'O').Sum(p => p.X + 100 * p.Y);

        return gpsSum;
    }

    static object PartTwo(string input, Func<TextWriter> getOutputFunction)
    {
        var lines = input.SplitByDoubleNewline().ToArray();

        var gridText = new string(lines[0].SelectMany(c =>
        {
            return c switch
            {
                '#' => "##",
                'O' => "[]",
                '.' => "..",
                '@' => "@.",
                '\n' => "\n",
                _ => "",
            };
        }).ToArray());

        var boxes = new Dictionary<Point2, Box>();
        var robot = new Point2(0,0, YAxisDirection.ZeroAtTop);
        var grid = gridText.ToGrid(YAxisDirection.ZeroAtTop, (c, p) =>
        {
            if (c == '[')
            {
                var box = new Box(p);
                boxes[p.Right] = box;
                return box;
            }

            if (c == '@')
            {
                robot = p;
                return Inventory.Empty;
            }


            return c switch
            {
                '.' => Inventory.Empty,
                '#' => Inventory.Wall,
                ']' => boxes[p],
                _ => throw new ArgumentOutOfRangeException()
            };
        });

        var allowed = "^><v".ToHashSet();
        var directions = lines[1].Where(c => allowed.Contains(c)).Select(c => c switch
        {
            '^' => CompassDirection.North,
            '>' => CompassDirection.East,
            'v' => CompassDirection.South,
            '<' => CompassDirection.West,
            _ => throw new ArgumentOutOfRangeException()
        });

        foreach (var d in directions)
        {
            var current = PrintGrid(grid, robot);

            var p = robot.Move(d, 1);

            switch (grid[p])
            {
                case Empty:
                    // Nothing in the way - just move to the new location
                    robot = p;
                    continue;
                case Wall:
                    // Nothing happens - the robot cannot move in this direction
                    continue;
            }

            // There is a box in the way. See if we can move it.

            var canMove = true;

            var boxesToMove = new Stack<Box>();
            var boxesToTry = new Queue<Box>();
            boxesToTry.Enqueue((Box)grid[p]);
            while (canMove && boxesToTry.Count > 0)
            {
                var box = boxesToTry.Dequeue();
                var (_, unMoveable, firstBox, secondBox) = box.GetMoveInformation(d, grid);
                if (unMoveable)
                {
                    // Nothing happens - the robot cannot move in this direction
                    canMove = false;
                    break;
                }

                if (firstBox != null && !boxesToTry.Contains(firstBox))
                {
                    boxesToTry.Enqueue(firstBox);
                }

                if (secondBox != null && !boxesToTry.Contains(secondBox))
                {
                    boxesToTry.Enqueue(secondBox);
                }

                boxesToMove.Push(box);
            }

            if (canMove)
            {
                while (boxesToMove.Count > 0)
                {
                    var box = boxesToMove.Pop();
                    var (canMoveThisBox, _, _, _) = box.GetMoveInformation(d, grid);
                    if (!canMoveThisBox)
                    {
                        throw new InvalidOperationException("Something wrong with the implementation.");
                    }

                    box.Move(grid, d);
                }

                if (grid[p] is not Empty)
                {
                    throw new Exception("Oops");
                }

                // Nothing in the way any longer - just move to the new location
                robot = p;
            }
            else
            {
                // Nothing happens - the robot cannot move in this direction
                continue;
            }
        }

        var gpsSum = grid.Points.Where(p => grid[p] is Box).DistinctBy(p => grid[p]).Sum(p => 100 * p.Y + p.X);
        return gpsSum;
    }

    private static string PrintGrid(Grid<Inventory> g, Point2 robot)
    {
        return g.ToString("", (p, i) =>
        {
            if (p == robot)
            {
                return "@";
            }

            if (i is Box box && p != box.UpperLeft)
            {
                return "";
            }

            return i switch
            {
                Wall => "#",
                Empty => ".",
                Box => "[]",
                _ => throw new ArgumentOutOfRangeException(),
            };
        });
    }
}

public class Inventory
{
    public static Inventory Empty { get; } = new Empty();
    public static Inventory Wall { get; } = new Wall();
};
public class Empty : Inventory;
public class Wall : Inventory;

internal class Box : Inventory
{
    public Point2 UpperLeft { get; private set; }

    public Box(Point2 upperLeft)
    {
        UpperLeft = upperLeft;
    }

    public (bool canBeMoved, bool unMoveable, Box? firstBox, Box? secondBox) GetMoveInformation(CompassDirection direction, Grid<Inventory> grid)
    {
        Inventory first;
        Inventory second;
        switch (direction)
        {
            case CompassDirection.North:
                first = grid[UpperLeft.Above];
                second = grid[UpperLeft.Right.Above];
                break;
            case CompassDirection.East:
                first = grid[UpperLeft.Right.Right];
                second = Empty;
                break;
            case CompassDirection.South:
                first = grid[UpperLeft.Below];
                second = grid[UpperLeft.Right.Below];
                break;
            case CompassDirection.West:
                first = grid[UpperLeft.Left];
                second = Empty;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
        }

        var unMoveable = first is Wall || second is Wall;
        var canBeMoved = first is Empty && second is Empty;
        return (canBeMoved, unMoveable, first as Box, second as Box);
    }

    public void Move(Grid<Inventory> grid, CompassDirection direction)
    {
        switch (direction)
        {
            case CompassDirection.North:
                if (grid[UpperLeft.Above] != Empty || grid[UpperLeft.Right.Above] != Empty)
                {
                    throw new Exception("Oops!");
                }
                grid[UpperLeft.Above] = this;
                grid[UpperLeft.Right.Above] = this;
                grid[UpperLeft] = Empty;
                grid[UpperLeft.Right] = Empty;
                UpperLeft = UpperLeft.Above;
                break;

            case CompassDirection.East:
                if (grid[UpperLeft.Right.Right] != Empty)
                {
                    throw new Exception("Oops!");
                }
                grid[UpperLeft.Right.Right] = this;
                grid[UpperLeft] = Empty;
                UpperLeft = UpperLeft.Right;
                break;

            case CompassDirection.South:
                if (grid[UpperLeft.Below] != Empty || grid[UpperLeft.Right.Below] != Empty)
                {
                    throw new Exception("Oops!");
                }
                grid[UpperLeft.Below] = this;
                grid[UpperLeft.Right.Below] = this;
                grid[UpperLeft] = Empty;
                grid[UpperLeft.Right] = Empty;
                UpperLeft = UpperLeft.Below;
                break;
            
            case CompassDirection.West:
                if (grid[UpperLeft.Left] != Empty)
                {
                    throw new Exception("Oops!");
                }
                grid[UpperLeft.Left] = this;
                grid[UpperLeft.Right] = Empty;
                UpperLeft = UpperLeft.Left;
                break;
            
            default:
                throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
        }
    }
}
