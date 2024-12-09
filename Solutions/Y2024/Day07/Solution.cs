using AdventOfCode.Framework;
using AdventOfCode.Utilities;

namespace AdventOfCode.Solutions.Y2024.Day07;

[RegisterKeyedTransient("2024-07")] partial class Solution { }
[RegisterTransient()] partial class Solution { }

partial class Solution : ISolver
{
    public int Year => 2024;
    public int Day => 7;
    public string GetName() => "Bridge Repair";

    public IEnumerable<object> Solve(string input, Func<TextWriter> getOutputFunction)
    {
        // var emptyOutput = () => new NullTextWriter();
        yield return PartOne(input, getOutputFunction);
        yield return PartTwo(input, getOutputFunction);
    }

    static object PartOne(string input, Func<TextWriter> getOutputFunction)
    {
        var lines = input.Lines()
            .Select(s => s.Longs().ToArray())
            .ToArray();

        var sum = 0L;
        foreach (var line in lines)
        {
            var stack = new Stack<Test>();
            stack.Push(new Test(line[0], line[0], line.Skip(1).ToArray()));

            while (stack.Count > 0)
            {
                var t = stack.Pop();
                if (t.IsOk())
                {
                    sum += t.TestValue;
                    break;
                }

                if (!t.MaybeCorrect())
                {
                    continue;
                }

                var div = t.IsDivisible();
                if (div != null)
                {
                    stack.Push(div.Value);
                }

                var sub = t.IsSubtractable();
                if (sub != null)
                {
                    stack.Push(sub.Value);
                }
            }
        }

        return sum;
    }

    static object PartTwo(string input, Func<TextWriter> getOutputFunction)
    {
        var lines = input.Lines()
            .Select(s => s.Longs().ToArray())
            .ToArray();

        var okValues = new List<long>();
        foreach (var line in lines)
        {
            var stack = new Stack<Test2>();
            stack.Push(new Test2(line[0], line.Skip(1).ToArray()));

            while (stack.Count > 0)
            {
                var t = stack.Pop();
                if (t.IsOk())
                {
                    okValues.Add(t.TestValue);
                    break;
                }

                if (!t.MaybeCorrect())
                {
                    continue;
                }

                stack.Push(t.Multiply());
                stack.Push(t.Add());
                var concat = t.IsConcatenatable();
                if (concat != null)
                {
                    stack.Push(concat.Value);
                }
            }
        }

        return okValues.Sum();
    }

    private readonly record struct Test(long TestValue, long Current, long[] Remain)
    {
        public bool IsOk()
        {
            return Remain.Length == 1 && Current == Remain[0];
        }

        public bool MaybeCorrect()
        {
            return Remain.Length > 1;
        }

        public Test? IsDivisible()
        {
            return Current % Remain[^1] == 0 ? new Test(TestValue, Current / Remain[^1], Remain.SkipLast().ToArray()) : null;
        }

        public Test? IsSubtractable()
        {
            return Current - Remain[^1] > 0 ? new Test(TestValue, Current - Remain[^1], Remain.SkipLast().ToArray()) : null;
        }

        public Test? IsConcatenatable()
        {
            if (Remain.Length >= 2)
            {
                var newRemain = new long[Remain.Length - 1];
                Array.Copy(Remain, 0, newRemain, 0, Remain.Length - 2);
                newRemain[^1] = long.Parse(Remain[^2].ToString() + Remain[^1]);
                return this with { Remain = newRemain };
            }

            return null;
        }
    }

    private readonly record struct Test2(long TestValue, long[] Remain)
    {
        public bool IsOk()
        {
            return Remain.Length == 1 && TestValue == Remain[0];
        }

        public bool MaybeCorrect()
        {
            return Remain.Length > 1;
        }

        public Test2 Multiply()
        {
            var newRemain = new long[Remain.Length - 1];
            Array.Copy(Remain, 2, newRemain, 1, Remain.Length - 2);
            newRemain[0] = Remain[0] * Remain[1];
            return this with {Remain =  newRemain};
        }

        public Test2 Add()
        {
            var newRemain = new long[Remain.Length - 1];
            Array.Copy(Remain, 2, newRemain, 1, Remain.Length - 2);
            newRemain[0] = Remain[0] + Remain[1];
            return this with { Remain = newRemain };
        }

        public Test2? IsConcatenatable()
        {
            if (Remain.Length >= 2)
            {
                var newRemain = new long[Remain.Length - 1];
                Array.Copy(Remain, 2, newRemain, 1, Remain.Length - 2);
                newRemain[0] = long.Parse(Remain[0].ToString() + Remain[1]);
                return this with { Remain = newRemain };
            }

            return null;
        }
    }
}