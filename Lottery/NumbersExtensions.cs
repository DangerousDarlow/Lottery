namespace Lottery;

public static class NumbersExtensions
{
    public static IEnumerable<int> ToNumbers(this string input)
    {
        var parts = input.Split(',');
        if (parts.Length != 6)
            throw new ArgumentException(
                $"String representation must contain {6} elements but has {parts.Length} elements: raw string '{input}'");

        return parts.Select(ParseLotteryDigit);
    }

    private static int ParseLotteryDigit(string input)
    {
        var value = int.Parse(input);
        if (value is < 1 or > 59)
            throw new ArgumentException($"Lottery digits must be 1 and 59 inclusive but was {value}");

        return value;
    }

    public static async Task<IEnumerable<int>[]> LoadNumbersFromFile(this string filepath)
    {
        var tickets = (await File.ReadAllLinesAsync(filepath)).Select(ToNumbers).ToArray();
        if (tickets.Length == 0)
            throw new ArgumentException($"ERROR: File '{filepath}' contains no tickets");

        return tickets;
    }

    public static int Matches(this IEnumerable<int> numbers1, IEnumerable<int> numbers2) => numbers1.Intersect(numbers2).Count();

    public static Matches Matches(this IEnumerable<int>[] tickets, IEnumerable<int> draw)
    {
        var drawAsList = draw.ToList();
        var matchResult = new Matches();

        foreach (var ticket in tickets)
            matchResult.Increment(ticket.Matches(drawAsList));

        return matchResult;
    }

    public static IEnumerable<IEnumerable<int>> MostCommonTriples(this IEnumerable<int>[] tickets)
    {
        // build a tree of numbers and numbers seen with them
        var tree = new Dictionary<int, Dictionary<int, int>>();
        foreach (var ticket in tickets)
        {
            var ticketAsList = ticket.ToList();
            foreach (var number in ticketAsList)
            {
                var seenWith = ticketAsList.Except(new[] {number});
                foreach (var otherNumber in seenWith)
                    if (!tree.ContainsKey(number))
                    {
                        tree[number] = new Dictionary<int, int> {{otherNumber, 1}};
                    }
                    else
                    {
                        if (!tree[number].ContainsKey(otherNumber))
                            tree[number][otherNumber] = 1;
                        else
                            tree[number][otherNumber]++;
                    }
            }
        }

        // convert the tree to a list with numbers seen with sorted by count
        var list = tree.Select(p1 =>
        {
            var (number, associations) = p1;

            var counts = associations.Select(p2 =>
            {
                var (association, count) = p2;
                return new {association, count};
            }).ToList();

            counts.Sort((a, b) => b.count.CompareTo(a.count));

            return new {number, counts};
        }).ToList();

        // sort the list by the minimum of the counts of the top two associations
        // the top of the list after sorting is the number with the most associations with two other numbers
        list.Sort((a, b) =>
        {
            var minTopCountsA = Math.Min(a.counts[0].count, a.counts[1].count);
            var minTopCountsB = Math.Min(b.counts[0].count, b.counts[1].count);
            return minTopCountsB.CompareTo(minTopCountsA);
        });

        if (list.Count < 6)
            throw new Exception("Insufficient numbers for a single ticket. This should never happen.");

        // minimum of the count of the top two associations of the first number
        var minTopTwoAssociations = Math.Min(list[0].counts[0].count, list[0].counts[1].count);

        // all numbers with the same minimum; these numbers are part of equally common triples
        var numbersInEquallyCommonTriples = list.Where(arg => Math.Min(arg.counts[0].count, arg.counts[1].count) == minTopTwoAssociations);

        var mostCommonTriples = new List<int[]>();
        foreach (var numberInTriple in numbersInEquallyCommonTriples)
        {
            var number = numberInTriple.number;

            // associations with the same minimum count and greater than the number
            var associations = numberInTriple.counts.Where(arg => arg.count == minTopTwoAssociations && arg.association > number).ToList();
            if (associations.Count >= 2)
                mostCommonTriples.Add(new[] {number, associations[0].association, associations[1].association});
        }

        return mostCommonTriples;
    }
}