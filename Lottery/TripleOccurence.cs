namespace Lottery;

public class TripleOccurence
{
    public TripleOccurence(Triple triple, int count)
    {
        Triple = triple;
        Count = count;
    }

    public Triple Triple { get; }
    public int Count { get; }

    public override string ToString() => $"{Triple} ({Count})";
}