namespace Lottery;

public class Matches
{
    public int Match3 { get; private set; }
    public int Match4 { get; private set; }
    public int Match5 { get; private set; }
    public int Match6 { get; private set; }

    public void Increment(int matchCount)
    {
        switch (matchCount)
        {
            case 3:
                Match3++;
                break;
            case 4:
                Match4++;
                break;
            case 5:
                Match5++;
                break;
            case 6:
                Match6++;
                break;
        }
    }

    public override string ToString() => $"3:{Match3}, 4:{Match4}, 5:{Match5}, 6:{Match6}";
}