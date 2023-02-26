namespace Lottery;

public static class NumbersExtensions
{
    public static IEnumerable<int> ToNumbers(this string input)
    {
        var parts = input.Split(',');
        if (parts.Length != 6)
            throw new ArgumentException(
                $"String representation must contain {6} elements but has {parts.Length} elements: raw string '{input}'");

        return parts.Select(int.Parse);
    }

    public static string ToReadableString(this IEnumerable<int> numbers) => string.Join(",", numbers);

    public static async Task<IEnumerable<int>[]> LoadNumbersFromFile(this string filepath) => (await File.ReadAllLinesAsync(filepath)).Select(ToNumbers).ToArray();

    public static int Matches(this IEnumerable<int> numbers1, IEnumerable<int> numbers2) => numbers1.Intersect(numbers2).Count();

    public static Matches Matches(this IEnumerable<int>[] tickets, IEnumerable<int> draw)
    {
        var drawAsList = draw.ToList();
        var matchResult = new Matches();

        foreach (var ticket in tickets)
            matchResult.Increment(ticket.Matches(drawAsList));

        return matchResult;
    }

    public static IEnumerable<int> MostCommonTriple(this IEnumerable<int>[] tickets)
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

        // sort the list by the lowest of the top two seen with counts
        list.Sort((a, b) =>
        {
            var minTopCountsA = a.counts.Count >= 2 ? Math.Min(a.counts[0].count, a.counts[1].count) : 0;
            var minTopCountsB = b.counts.Count >= 2 ? Math.Min(b.counts[0].count, b.counts[1].count) : 0;
            return minTopCountsB.CompareTo(minTopCountsA);
        });

        return list.Count > 3 ? new[] {list[0].number, list[1].number, list[2].number} : new int[3];
    }
}