using AdventOfCode.Model;

namespace AdventOfCode.Generator;

public class SolutionTemplateGenerator {
    public static string Generate(Problem problem)
    {
        return $$"""
                using AdventOfCode.Framework;
                using AdventOfCode.Utilities;

                namespace AdventOfCode.Solutions.Y{{problem.Year}}.Day{{problem.Day:00}};
                
                [RegisterKeyedTransient("{{problem.Year}}-{{problem.Day:00}}")] partial class Solution { }
                [RegisterTransient()] partial class Solution { }

                partial class Solution : ISolver
                {
                    public int Year => {{problem.Year}};
                    public int Day => {{problem.Day}};
                    public string GetName() => "{{problem.Title}}";

                    public IEnumerable<object> Solve(string input, Func<TextWriter> getOutputFunction)
                    {
                        // var emptyOutput = () => new NullTextWriter();
                        yield return PartOne(input, getOutputFunction);
                        yield return PartTwo(input, getOutputFunction);
                    }

                    static object PartOne(string input, Func<TextWriter> getOutputFunction)
                    {
                        return 0;
                    }

                    static object PartTwo(string input, Func<TextWriter> getOutputFunction)
                    {
                        return 0;
                    }
                }
                """;
    }
}