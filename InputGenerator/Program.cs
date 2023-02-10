using System.CommandLine;

var numberOption = new Option<int>("number", "Number of tickets to generate");
numberOption.SetDefaultValue(10);

var pathOption = new Option<string>("path", "Output file path");
pathOption.SetDefaultValue("tickets.csv");

var rootCommand = new RootCommand("Generate lottery tickets");
rootCommand.AddOption(numberOption);
rootCommand.AddOption(pathOption);
rootCommand.SetHandler(async (number, path) =>
{
    await using var writer = File.CreateText(path);

    while (number-- > 0)
    {
        var random = new Random();
        var numbers = Enumerable.Range(1, 59)
            .OrderBy(_ => random.Next())
            .Take(6)
            .OrderBy(n => n);

        var ticket = string.Join(",", numbers);
        await writer.WriteLineAsync(ticket);
    }
}, numberOption, pathOption);

await rootCommand.InvokeAsync(args);