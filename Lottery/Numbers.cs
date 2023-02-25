namespace Lottery;

public class Numbers
{
    public const int Size = 6;
    private readonly int[] _numbers = new int[Size];

    public int this[int index]
    {
        get => _numbers[index];
        set
        {
            if (index is < 0 or > Size - 1)
                throw new ArgumentOutOfRangeException(nameof(index), $"{nameof(Numbers)} element index must be 0 to {Size - 1} inclusive");

            if (value is < 1 or > 59)
                throw new ArgumentOutOfRangeException(nameof(value), $"{nameof(Numbers)} element value must be 1 to 59 inclusive");

            _numbers[index] = value;
        }
    }

    public int Matches(Numbers other) => _numbers.Intersect(other._numbers).Count();

    public override string ToString() => string.Join(",", _numbers);
}