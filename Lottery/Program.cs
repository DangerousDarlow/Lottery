using System.CommandLine;
using Lottery;

var ticketsArgument = new Argument<string>("tickets", "Ticket file path");
var modeOption = new Option<string>("mode", "Mode of operation (count or maximise");
var drawOption = new Option<string>("draw", "Drawn numbers (required for count mode)");

var rootCommand = new RootCommand("Analyse lottery tickets");
rootCommand.AddArgument(ticketsArgument);
rootCommand.AddOption(modeOption);
rootCommand.AddOption(drawOption);
rootCommand.SetHandler(async (ticketsFilePath, mode, drawStr) =>
{
    if (string.IsNullOrEmpty(ticketsFilePath) || !File.Exists(ticketsFilePath))
        throw new ArgumentException("Argument `tickets` is required and must be a valid file path");

    if (string.IsNullOrEmpty(mode))
        throw new ArgumentException("Argument `mode` is required and must be either `count` or `maximise`");

    switch (mode)
    {
        case "count":
        {
            if (string.IsNullOrEmpty(drawStr))
                throw new ArgumentException("Argument `draw` is required for `count` mode");

            var draw = drawStr.ToNumbers();
            var tickets = await ticketsFilePath.LoadNumbersFromFile();
            var matches = tickets.Matches(draw);
            Console.WriteLine($"Matches: {matches}");
            break;
        }

        case "maximise":
        {
            var tickets = await ticketsFilePath.LoadNumbersFromFile();
            var mostCommonTriple = tickets.MostCommonTriple();
            Console.WriteLine($"Most common three number combination: {mostCommonTriple}");
            break;
        }

        default:
            throw new ArgumentException("Argument `mode` must be either `count` or `maximise`");
    }
}, ticketsArgument, modeOption, drawOption);

await rootCommand.InvokeAsync(args);