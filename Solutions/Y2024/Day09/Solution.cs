using System.Diagnostics.CodeAnalysis;
using System.Net.WebSockets;
using System.Runtime.ExceptionServices;
using AdventOfCode.Framework;
using AdventOfCode.Utilities;
using LanguageExt.Pipes;

namespace AdventOfCode.Solutions.Y2024.Day09;

[RegisterKeyedTransient("2024-09")] partial class Solution { }
[RegisterTransient()] partial class Solution { }

partial class Solution : ISolver
{
    public int Year => 2024;
    public int Day => 9;
    public string GetName() => "Disk Fragmenter";

    public IEnumerable<object> Solve(string input, Func<TextWriter> getOutputFunction)
    {
        // var emptyOutput = () => new NullTextWriter();
        yield return PartOne(input, getOutputFunction);
        yield return PartTwo(input, getOutputFunction);
    }

    static object PartOne(string input, Func<TextWriter> getOutputFunction)
    {
        var diskMap = input.Chunk(2)
            .SelectMany((a, i) =>
            {
                var fileLength = a[0] - '0';
                var freeSpaceLength = a.Length == 2 ? a[1] - '0' : 0;
                return new Block[] { new File(i, fileLength), new Empty(freeSpaceLength) };
            })
            .ToList();

        var defragged = new List<Block>(diskMap);
        var (indexOfFree, free) = defragged.Index().First(t => t.Item is Empty);
        defragged.RemoveAt(defragged.Count -1);
        while (free != null)
        {
            var file = (File)defragged[^1];
            if (free.Length == file!.Length)
            {
                defragged.RemoveAt(indexOfFree);
                defragged.Insert(indexOfFree, file);
                defragged.RemoveAt(defragged.Count - 1);
                defragged.RemoveAt(defragged.Count - 1);
            }
            else if (free.Length >= file.Length)
            {
                defragged.RemoveAt(indexOfFree);
                defragged.Insert(indexOfFree, file);
                defragged.Insert(indexOfFree + 1, new Empty(free.Length - file.Length));
                defragged.RemoveAt(defragged.Count - 1);
                defragged.RemoveAt(defragged.Count - 1);
            }
            else
            {
                defragged.RemoveAt(indexOfFree);
                defragged.Insert(indexOfFree, new File(file.Id, free.Length));
                defragged.RemoveAt(defragged.Count - 1);
                defragged.Add(new File(file.Id, file.Length - free.Length));
            }

            (indexOfFree, free) = defragged.Index().FirstOrDefault(t => t.Item is Empty);
        }

        var checkSum = CheckSum(defragged);
        return checkSum;
    }

    static object PartTwo(string input, Func<TextWriter> getOutputFunction)
    {
        var remainingFiles = new List<File>();
        var diskMap = input.Chunk(2)
            .SelectMany((a, i) =>
            {
                var fileLength = a[0] - '0';
                var freeSpaceLength = a.Length == 2 ? a[1] - '0' : 0;
                var file = new File(i, fileLength);
                remainingFiles.Add(file);
                var empty = new Empty(freeSpaceLength);
                return new Block[] { file, empty };
            })
            .ToList();

        var defragged = new List<Block>(diskMap);
        remainingFiles.Reverse();
        foreach (var file in remainingFiles)
        {
            var indexOfFile = defragged.IndexOf(file);

            var(indexOfFree, empty)  = GetFirstEmpty(file, defragged);
            if (indexOfFile < indexOfFree)
            {
                continue;
            }

            defragged.RemoveAt(indexOfFree);
            defragged.Remove(file);
            defragged.Insert(indexOfFree, file);
            defragged.Insert(indexOfFile, new Empty(file.Length));

            if (empty.Length - file.Length > 0)
            {
                var newEmpty = new Empty(empty.Length - file.Length);
                defragged.Insert(indexOfFree + 1, newEmpty);
            }
        }

        var checkSum = CheckSum(defragged);
        return checkSum;
    }

    private static (int indexOfFree, Empty empty) GetFirstEmpty(File file, List<Block> defragged)
    {
        var t = defragged.Index().FirstOrDefault(t => t.Item is Empty && t.Item.Length >= file.Length);
        return t == default ? (defragged.Count, new Empty(0)) : (t.Index, (Empty)t.Item);
    }

    private static long CheckSum(List<Block> defragged)
    {
        var blockIndex = 0L;
        var checkSum = 0L;
        foreach (var block in defragged)
        {
            if (block is Empty)
            {
                blockIndex += block.Length;
                continue;
            }

            var file = (File)block;
            for (var i = 0; i < file.Length; i++)
            {
                var blockCheckSum = blockIndex++ * file.Id;
                checkSum += blockCheckSum;
            }
        }

        return checkSum;
    }

    public record Block(int Length);
    public record File(int Id, int Length) : Block(Length);
    public record Empty(int Length) : Block(Length);
}