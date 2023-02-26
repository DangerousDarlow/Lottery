namespace LotteryTest;

public class MostCommonTripleTests
{
    [Test]
    public void One_occurrence_each_of_four_different_three_number_combinations()
    {
        var numbers = new[]
        {
            "1,2,3,4,5,6"
        }.Select(s => s.ToNumbers()).ToArray();

        var actual = numbers.MostCommonTriples();
        var expected = new List<int[]>
        {
            new[] {1, 2, 3},
            new[] {2, 3, 4},
            new[] {3, 4, 5},
            new[] {4, 5, 6}
        };

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void Two_occurrences_of_a_single_three_number_combination()
    {
        // 4,5,6 occurs 2 times
        // No other triple occurs more than once
        // 1 occurs 3 times
        var numbers = new[]
        {
            "1,2,3,4,5,6",
            "4,5,6,7,8,9",
            "1,10,11,12,13,14",
            "1,15,16,17,18,19"
        }.Select(s => s.ToNumbers()).ToArray();

        var actual = numbers.MostCommonTriples();
        var expected = new List<int[]>
        {
            new[] {4, 5, 6}
        };

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void Three_occurrences_of_a_triple_and_two_occurrences_of_two_other_triples()
    {
        // 10,11,12 occurs 3 times
        // 1,2,3 and 7,8,9 occur 2 times
        var numbers = new[]
        {
            "1,2,3,4,5,6",
            "1,2,3,7,8,9",
            "10,11,12,7,8,9",
            "10,11,12,13,14,15",
            "10,11,12,16,17,18"
        }.Select(s => s.ToNumbers()).ToArray();

        var actual = numbers.MostCommonTriples();
        var expected = new List<int[]>
        {
            new[] {10, 11, 12}
        };

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void Number_most_highly_associated_is_not_in_most_common_triple()
    {
        // 1,2,55 occurs 2 times
        // No other triple occurs more than once
        var numbers = new[]
        {
            "1,2,3,4,5,55",
            "1,2,6,7,8,55",
            "20,21,22,23,24,25",
            "20,26,27,28,29,25",
            "20,30,31,32,33,25",
            "40,41,42,43,44,25",
            "40,45,46,47,48,25",
            "40,49,50,51,52,25"
        }.Select(s => s.ToNumbers()).ToArray();

        var actual = numbers.MostCommonTriples();
        var expected = new List<int[]>
        {
            new[] {1, 2, 55}
        };

        Assert.That(actual, Is.EqualTo(expected));
    }
}