using AdventOfCode.Commands;
using Microsoft.Extensions.Hosting;
using AdventOfCode.Framework;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
    {
        services.AutoRegister();
    })
    .Build();

var commandMatchers = new[]
    {
        new CommandMatcher(new ArgumentExpressions("update", "([0-9]+)[/-]([0-9]+)"), () => Update.UpdateSpecificDate),
        new CommandMatcher(new ArgumentExpressions("update", "today"), () => Update.UpdateToday),
        new CommandMatcher(new ArgumentExpressions("solve", "([0-9]+)[/-]([0-9]+)"), () => Solve.SolveSpecificDate),
        new CommandMatcher(new ArgumentExpressions("solve", "[0-9]+"), () => Solve.SolveSpecificYear),
        new CommandMatcher(new ArgumentExpressions("solve", "all"), () => Solve.SolveAll),
        new CommandMatcher(new ArgumentExpressions("solve", "today"), () => Solve.SolveToday),
    };


var commands = commandMatchers
    .Select(m => m.GetCommand(args))
    .Where(c => c != null)
    .ToArray();

if (commands.Length == 0)
{
    Console.WriteLine(ApplicationUsage.Usage());
}
else
{
    foreach (var c in commands)
    {
        await c!(args, host.Services);
    }
}
