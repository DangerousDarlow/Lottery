namespace Lottery;

public static class NumbersExtensions
{
    public static Numbers ToNumbers(this string input)
    {
        var parts = input.Split(',');
        if (parts.Length != Numbers.Size)
            throw new ArgumentException($"String representation of {nameof(Numbers)} must contain {Numbers.Size} elements but has {parts.Length} elements: raw string '{input}'");

        var numbers = new Numbers();
        for (var index = 0; index < Numbers.Size; index++)
        {
            if (!int.TryParse(parts[index], out var number))
                throw new ArgumentException($"String representation of {nameof(Numbers)} element {index} must be an integer but is '{parts[index]}': raw string '{input}'");

            numbers[index] = number;
        }

        return numbers;
    }

    public static async Task<Numbers[]> LoadNumbersFromFile(this string filepath) => (await File.ReadAllLinesAsync(filepath)).Select(ToNumbers).ToArray();

    public static MatchResult Matches(this Numbers[] numbers, Numbers draw)
    {
        var matchResult = new MatchResult();

        foreach (var number in numbers)
            matchResult.Increment(number.Matches(draw));

        return matchResult;
    }
}