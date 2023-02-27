namespace Lottery;

public static class NumbersExtensions
{
    public static Ticket ToTicket(this string input)
    {
        var values = input.Split(',').Select(int.Parse);
        return new Ticket(values);
    }

    public static async Task<IEnumerable<Ticket>> LoadTicketsFromFile(this string filepath)
    {
        var tickets = (await File.ReadAllLinesAsync(filepath)).Select(ToTicket).ToArray();
        if (tickets.Length == 0)
            throw new ArgumentException($"File '{filepath}' contains no tickets");

        return tickets;
    }

    public static Matches FindMatches(this IEnumerable<Ticket> tickets, Ticket draw)
    {
        var matchResult = new Matches();

        foreach (var ticket in tickets)
            matchResult.Increment(ticket.Matches(draw));

        return matchResult;
    }

    public static IEnumerable<TripleOccurence> FindTriples(this IEnumerable<Ticket> tickets)
    {
        var tripleCounts = new Dictionary<Triple, int>();

        foreach (var ticket in tickets)
        {
            var ticketTriples = ticket.ToTriples();
            foreach (var triple in ticketTriples)
                if (tripleCounts.ContainsKey(triple))
                    tripleCounts[triple]++;
                else
                    tripleCounts[triple] = 1;
        }

        var triples = tripleCounts.Select(pair => new TripleOccurence(pair.Key, pair.Value)).ToList();
        triples.Sort((triple1, triple2) =>
        {
            if (triple1.Count == triple2.Count)
                return triple1.Triple.CompareTo(triple2.Triple);

            return triple2.Count.CompareTo(triple1.Count);
        });

        return triples;
    }
}