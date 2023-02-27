using System.CommandLine;
using Lottery;

var ticketsArgument = new Argument<string>("tickets", "Ticket file path");
var modeOption = new Option<string>("mode", "Mode of operation (count or maximise)");
var drawOption = new Option<string>("draw", "Drawn numbers (required for count mode)");
var minimumOccurenceOption = new Option<int>("min", "Minimum occurence filter");
minimumOccurenceOption.SetDefaultValue(2);

var rootCommand = new RootCommand("Analyse lottery tickets");
rootCommand.AddArgument(ticketsArgument);
rootCommand.AddOption(modeOption);
rootCommand.AddOption(drawOption);
rootCommand.AddOption(minimumOccurenceOption);
rootCommand.SetHandler(async (ticketsFilePath, mode, drawStr, minOccurence) =>
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

            var draw = drawStr.ToTicket();
            var tickets = await ticketsFilePath.LoadTicketsFromFile();
            var matches = tickets.FindMatches(draw);
            Console.WriteLine($"Matches: {matches}");
            break;
        }

        case "maximise":
        {
            var tickets = await ticketsFilePath.LoadTicketsFromFile();
            var triples = tickets.FindTriples().Where(t => t.Count >= minOccurence).ToList();
            Console.WriteLine($"Most common three number combinations ({triples.Count})");
            triples.ForEach(Console.WriteLine);
            break;
        }

        default:
            throw new ArgumentException("Argument `mode` must be either `count` or `maximise`");
    }
}, ticketsArgument, modeOption, drawOption, minimumOccurenceOption);

await rootCommand.InvokeAsync(args);