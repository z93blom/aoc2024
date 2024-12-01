
# Advent of Code (2024)
My C# solutions to the advent of code problems.
Check out http://adventofcode.com.

## Setting the session variable
The application depends on an environment variable. To get your identifier, you need to
look at the request headers for your daily data, and take the session identifier from
the cookie in the Request Headers (for example https://adventofcode.com/2024/day/1/input)

Then you can set it:

```
Powershell: $Env:AOCSESSION = "..."

Command Prompt: set AOCSESSION = ...
```

## Running

To run the project:

1. Install .NET
2. Clone the repo
3. Get help with `dotnet run`
```
Usage: dotnet run [arguments]
Supported arguments:

solve [year]-[day]    Solve the specified problems
solve [year]          Solve the whole year
solve today           Solve today's problem (only available during event).

To start working on new problems:

update [year]-[day]   Prepares a folder for the given day, updates the input, 
					the readme and creates a solution template.
update today          Same as above, but for the current day. (only available during event). 

Useful commands during december:
dotnet run update today
dotnet run solve today 

```
