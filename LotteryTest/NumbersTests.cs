namespace LotteryTest;

public class NumbersTests
{
    [TestCase("1,2,3,4,5,6", 6)]
    [TestCase("2,3,4,5,6,7", 5)]
    [TestCase("3,4,5,6,7,8", 4)]
    [TestCase("4,5,6,7,8,9", 3)]
    [TestCase("5,6,7,8,9,10", 2)]
    [TestCase("6,7,8,9,10,11", 1)]
    [TestCase("7,8,9,10,11,12", 0)]
    public void MatchesTest(string numbers, int matches) => Assert.That("1,2,3,4,5,6".ToTicket().Matches(numbers.ToTicket()), Is.EqualTo(matches));
}