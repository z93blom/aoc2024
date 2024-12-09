namespace AdventOfCode.Framework;

public interface ISolver
{
    string GetName();
    
    int Year { get; }
    
    int Day { get; }

    IEnumerable<object> Solve(string input, Func<TextWriter> getOutputFunction);
}