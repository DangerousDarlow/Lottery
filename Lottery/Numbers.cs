namespace Lottery;

public abstract class Numbers : IEquatable<Numbers>
{
    protected readonly IReadOnlyList<int> _numbers;

    protected Numbers(IEnumerable<int> values)
    {
        var list = values.ToList();

        if (list.Any(i => i is < 1 or > 59))
            throw new ArgumentException("Values must be 1 to 59 inclusive");

        if (list.Distinct().Count() != list.Count)
            throw new ArgumentException("Values must be unique");

        list.Sort((i1, i2) => i1.CompareTo(i2));
        _numbers = list;
    }

    public bool Equals(Numbers? other)
    {
        if (ReferenceEquals(null, other))
            return false;

        if (ReferenceEquals(this, other))
            return true;

        return _numbers.SequenceEqual(other._numbers);
    }

    public int Matches(Numbers other) => _numbers.Intersect(other._numbers).Count();

    public override bool Equals(object? obj) => Equals(obj as Numbers);

    public override int GetHashCode() => _numbers.Aggregate(HashCode.Combine);

    public override string ToString() => $"[{string.Join(",", _numbers)}]";
}

public class Ticket : Numbers
{
    private const int Size = 6;

    public Ticket(IEnumerable<int> values) : base(values)
    {
        if (values.Count() != Size)
            throw new ArgumentException($"Ticket must have {Size} values");
    }

    public IEnumerable<Triple> ToTriples() => new List<Triple>
    {
        new(_numbers[0], _numbers[1], _numbers[2]),
        new(_numbers[0], _numbers[1], _numbers[3]),
        new(_numbers[0], _numbers[1], _numbers[4]),
        new(_numbers[0], _numbers[1], _numbers[5]),
        new(_numbers[0], _numbers[2], _numbers[3]),
        new(_numbers[0], _numbers[2], _numbers[4]),
        new(_numbers[0], _numbers[2], _numbers[5]),
        new(_numbers[0], _numbers[3], _numbers[4]),
        new(_numbers[0], _numbers[3], _numbers[5]),
        new(_numbers[0], _numbers[4], _numbers[5]),

        new(_numbers[1], _numbers[2], _numbers[3]),
        new(_numbers[1], _numbers[2], _numbers[4]),
        new(_numbers[1], _numbers[2], _numbers[5]),
        new(_numbers[1], _numbers[3], _numbers[4]),
        new(_numbers[1], _numbers[3], _numbers[5]),
        new(_numbers[1], _numbers[4], _numbers[5]),

        new(_numbers[2], _numbers[3], _numbers[4]),
        new(_numbers[2], _numbers[3], _numbers[5]),
        new(_numbers[2], _numbers[4], _numbers[5]),

        new(_numbers[3], _numbers[4], _numbers[5])
    };
}

public class Triple : Numbers, IComparable<Triple>
{
    public Triple(int i1, int i2, int i3) : base(new[] {i1, i2, i3})
    {
    }

    public int CompareTo(Triple? other)
    {
        if (other == null)
            throw new ArgumentNullException(nameof(other));

        if (_numbers[0] == other._numbers[0])
        {
            if (_numbers[1] == other._numbers[1])
                return _numbers[2].CompareTo(other._numbers[2]);

            return _numbers[1].CompareTo(other._numbers[1]);
        }

        return _numbers[0].CompareTo(other._numbers[0]);
    }
}