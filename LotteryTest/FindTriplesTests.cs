namespace LotteryTest;

public class FindTriplesTests
{
    [Test]
    public void One_ticket_contains_20_three_digit_combinations()
    {
        var tickets = new[]
        {
            "1,2,3,4,5,6"
        }.Select(s => s.ToTicket()).ToList();

        var triples = tickets.FindTriples().ToList();
        Assert.That(triples.Count, Is.EqualTo(20));
        Assert.That(triples.All(t => t.Count == 1));

        var expected = new List<Triple>
        {
            new(1, 2, 3),
            new(1, 2, 4),
            new(1, 2, 5),
            new(1, 2, 6),
            new(1, 3, 4),
            new(1, 3, 5),
            new(1, 3, 6),
            new(1, 4, 5),
            new(1, 4, 6),
            new(1, 5, 6),
            new(2, 3, 4),
            new(2, 3, 5),
            new(2, 3, 6),
            new(2, 4, 5),
            new(2, 4, 6),
            new(2, 5, 6),
            new(3, 4, 5),
            new(3, 4, 6),
            new(3, 5, 6),
            new(4, 5, 6)
        };

        Assert.That(triples.Select(t => t.Triple), Is.EqualTo(expected));
    }

    [Test]
    public void Only_one_triple_occurs_more_than_once()
    {
        var tickets = new[]
        {
            "1,2,3,4,5,6",
            "4,5,6,7,8,9"
        }.Select(s => s.ToTicket()).ToList();

        var triples = tickets.FindTriples().Where(t => t.Count > 1).ToList();
        Assert.That(triples.Count, Is.EqualTo(1));

        var expected = new List<Triple>
        {
            new(4, 5, 6)
        };

        Assert.That(triples.Select(t => t.Triple), Is.EqualTo(expected));
    }

    [Test]
    public void One_triple_occurs_3_times_and_two_other_triples_occur_twice()
    {
        // 10,11,12 occurs 3 times
        // 1,2,3 and 7,8,9 occur 2 times
        var tickets = new[]
        {
            "1,2,3,4,5,6",
            "1,2,3,7,8,9",
            "10,11,12,7,8,9",
            "10,11,12,13,14,15",
            "10,11,12,16,17,18"
        }.Select(s => s.ToTicket()).ToList();

        var triples = tickets.FindTriples().Where(t => t.Count > 1).ToList();
        Assert.That(triples.Count, Is.EqualTo(3));

        var expected = new List<Triple>
        {
            new(10, 11, 12),
            new(1, 2, 3),
            new(7, 8, 9)
        };

        Assert.That(triples.Select(t => t.Triple), Is.EqualTo(expected));

        Assert.That(triples[0].Count, Is.EqualTo(3));
        Assert.That(triples[1].Count, Is.EqualTo(2));
        Assert.That(triples[2].Count, Is.EqualTo(2));
    }

    [Test]
    public void Number_most_highly_associated_is_not_in_most_common_triple()
    {
        // 1,2,55 occurs 2 times
        // No other triple occurs more than once
        var tickets = new[]
        {
            "1,2,3,4,5,55",
            "1,2,6,7,8,55",
            "20,21,22,23,24,25",
            "20,26,27,28,29,25",
            "20,30,31,32,33,25",
            "40,41,42,43,44,25",
            "40,45,46,47,48,25",
            "40,49,50,51,52,25"
        }.Select(s => s.ToTicket()).ToList();

        var triples = tickets.FindTriples().Where(t => t.Count > 1).ToList();
        Assert.That(triples.Count, Is.EqualTo(1));

        var expected = new List<Triple>
        {
            new(1, 2, 55)
        };

        Assert.That(triples.Select(t => t.Triple), Is.EqualTo(expected));
    }
}