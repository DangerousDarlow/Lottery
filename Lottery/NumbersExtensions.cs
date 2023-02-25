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

    public static Matches Matches(this IEnumerable<int>[] numbers, IEnumerable<int> draw)
    {
        var drawAsList = draw.ToList();

        var matchResult = new Matches();

        foreach (var number in numbers)
            matchResult.Increment(number.Intersect(drawAsList).Count());

        return matchResult;
    }

    public static IEnumerable<int> MostCommonTriple(this IEnumerable<int>[] numbers) => new int[3];
}