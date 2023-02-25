namespace LotteryTest;

public class MostCommonTripleTests
{
    [Test]
    public void Test()
    {
        // 1 occurs 3 times
        // 4,5,6 occurs 2 times
        // 4,5,6 is the only triple
        var numbers = new[]
        {
            "1,2,3,4,5,6",
            "4,5,6,7,8,9",
            "1,10,11,12,13,14",
            "1,15,16,17,18,19"
        }.Select(s => s.ToNumbers()).ToArray();

        var actual = numbers.MostCommonTriple();
        var expected = new[] {4, 5, 6};

        Assert.That(actual, Is.EqualTo(expected), $"Expected: {expected.ToReadableString()}, Actual: {actual.ToReadableString()}");
    }
}