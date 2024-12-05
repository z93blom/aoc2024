using AdventOfCode.Framework;
using AdventOfCode.Utilities;

namespace AdventOfCode.Solutions.Y2024.Day05;

[RegisterKeyedTransient("2024-05")] partial class Solution { }
[RegisterTransient()] partial class Solution { }

partial class Solution : ISolver
{
    public int Year => 2024;
    public int Day => 5;
    public string GetName() => "Print Queue";

    public IEnumerable<object> Solve(string input, Func<TextWriter> getOutputFunction)
    {
        // var emptyOutput = () => new NullTextWriter();
        yield return PartOne(input, getOutputFunction);
        yield return PartTwo(input, getOutputFunction);
    }

    static object PartOne(string input, Func<TextWriter> getOutputFunction)
    {
        var parts = input.SplitByDoubleNewline().ToArray();
        var orderingRules = parts[0].SplitBySingleNewline()
            .Select(s => s.Integers().ToArray()).ToArray();

        var isBefore = new Dictionary<int, List<int>>();
        var isAfter = new Dictionary<int, List<int>>();
        foreach (var rule in orderingRules)
        {
            if (!isBefore.ContainsKey(rule[0]))
            {
                isBefore[rule[0]] = new List<int>();
            }

            isBefore[rule[0]].Add(rule[1]);

            if (!isAfter.ContainsKey(rule[1]))
            {
                isAfter[rule[1]] = new List<int>();
            }

            isAfter[rule[1]].Add(rule[0]);
        }

        var updatesInOrder = parts[1].SplitBySingleNewline()
            .Select(s => s.Integers().ToArray()).ToArray();
        var updates = updatesInOrder
            .Select(ToUpdateDictionary)
            .ToArray();

        List<int> correctUpdates = new();
        foreach (var t in updates.Index())
        {
            var (index, u) = t;
            var isCorrect = IsCorrect(u, isBefore, isAfter);

            if (isCorrect)
            {
                correctUpdates.Add(index);
            }
        }

        var sum = 0;
        foreach (var index in correctUpdates)
        {
            var value = updatesInOrder[index][updatesInOrder[index].Length / 2];
            sum += value;
        }

        return sum;
    
    }

    private static Dictionary<int, int> ToUpdateDictionary(IEnumerable<int> a)
    {
        return a.Index().ToDictionary(t => t.Item, t => t.Index);
    }

    private static int IsCorrectlyPlacedBefore(int page, Dictionary<int, int> u, Dictionary<int, List<int>> isBefore)
    {
        if (isBefore.TryGetValue(page, out var pagesThatHaveToAppearAfter))
        {
            foreach (var pageAfter in pagesThatHaveToAppearAfter)
            {
                if (u.ContainsKey(pageAfter) && u[page] > u[pageAfter])
                {
                    return pageAfter;
                }
            }
        }

        return -1;
    }


    private static int IsCorrectlyPlacedAfter(int page, Dictionary<int, int> u, Dictionary<int, List<int>> isAfter)
    {
        if (isAfter.TryGetValue(page, out var pagesThatHaveToAppearBefore))
        {
            foreach (var pageBefore in pagesThatHaveToAppearBefore)
            {
                if (u.ContainsKey(pageBefore) && u[page] < u[pageBefore])
                {
                    return pageBefore;
                }
            }
        }

        return -1;
    }

    private static bool IsCorrect(Dictionary<int, int> u, Dictionary<int, List<int>> isBefore, Dictionary<int, List<int>> isAfter)
    {
        var isCorrect = true;
        foreach (var page in u.Keys)
        {
            if (isBefore.TryGetValue(page, out var pagesThatHaveToAppearAfter))
            {
                foreach (var pageAfter in pagesThatHaveToAppearAfter)
                {
                    if (u.ContainsKey(pageAfter) && u[page] > u[pageAfter])
                    {
                        isCorrect = false;
                        break;
                    }
                }
            }

            if (!isCorrect)
            {
                break;
            }

            if (isAfter.TryGetValue(page, out var pagesThatHaveToAppearBefore))
            {
                foreach (var pageBefore in pagesThatHaveToAppearBefore)
                {
                    if (u.ContainsKey(pageBefore) && u[page] < u[pageBefore])
                    {
                        isCorrect = false;
                        break;
                    }
                }
            }

            if (!isCorrect)
            {
                break;
            }
        }

        return isCorrect;
    }

    static object PartTwo(string input, Func<TextWriter> getOutputFunction)
    {
        var parts = input.SplitByDoubleNewline().ToArray();
        var orderingRules = parts[0].SplitBySingleNewline()
            .Select(s => s.Integers().ToArray()).ToArray();

        var isBefore = new Dictionary<int, List<int>>();
        var isAfter = new Dictionary<int, List<int>>();
        foreach (var rule in orderingRules)
        {
            if (!isBefore.ContainsKey(rule[0]))
            {
                isBefore[rule[0]] = new List<int>();
            }

            isBefore[rule[0]].Add(rule[1]);

            if (!isAfter.ContainsKey(rule[1]))
            {
                isAfter[rule[1]] = new List<int>();
            }

            isAfter[rule[1]].Add(rule[0]);
        }

        var updatesInOrder = parts[1].SplitBySingleNewline()
            .Select(s => s.Integers().ToArray()).ToArray();
        var updates = updatesInOrder
            .Select(a => a.Index().ToDictionary(t => t.Item, t => t.Index))
            .ToArray();

        List<int> incorrectUpdates = new();
        foreach (var t in updates.Index())
        {
            var (index, u) = t;
            var isCorrect = IsCorrect(u, isBefore, isAfter);

            if (!isCorrect)
            {
                incorrectUpdates.Add(index);
            }
        }

        // Sort the incorrect updates correctly.
        var correctedUpdates = new List<List<int>>();
        foreach (var index in incorrectUpdates)
        {
            var update = updatesInOrder[index].ToList();
            var u = ToUpdateDictionary(update);
            var isCorrect = false;
            while (!isCorrect)
            {
                foreach (var page in update)
                {
                    var pageToSwap = -1;
                    do
                    {
                        pageToSwap = IsCorrectlyPlacedBefore(page, u, isBefore);
                        if (pageToSwap > 0)
                        {
                            (u[pageToSwap], u[page]) = (u[page], u[pageToSwap]);
                        }
                    } while (pageToSwap != -1);

                    do
                    {
                        pageToSwap = IsCorrectlyPlacedAfter(page, u, isAfter);
                        if (pageToSwap > 0)
                        {
                            (u[pageToSwap], u[page]) = (u[page], u[pageToSwap]);
                        }
                    } while (pageToSwap != -1);

                }

                isCorrect = IsCorrect(u, isBefore, isAfter);
            }

            var correctedUpdate = new List<int>(u.OrderBy(kvp => kvp.Value).Select(kvp => kvp.Key));
            correctedUpdates.Add(correctedUpdate);

        }

        var sum = 0;
        foreach (var list in correctedUpdates)
        {
            var value = list[list.Count / 2];
            sum += value;
        }

        return sum;
    }
}