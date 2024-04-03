namespace CLI;

public class ScrollableList<T>(IEnumerable<T> choices)
{
    public int Position = 0;
    public int PaginationCount = 10;

    public int MaxAllowedPosition
    {
        get
        {
            return Math.Max(Choices.Count() - PaginationCount, 0);
        }
    }

    public IEnumerable<T> Choices = choices;

    public IEnumerable<T> PaginatedChoices
    {
        get
        {
            return Choices.Skip(Position).Take(PaginationCount);
        }
    }

    public void MoveForward()
    {
        if (Position < MaxAllowedPosition) Position++;
    }

    public void MoveBackward()
    {
        if (Position > 0) Position--;
    }

    public void CheckOutOfBoundsPointer()
    {
        if (Position > MaxAllowedPosition) Position = MaxAllowedPosition;
        if (Position < 0) Position = 0;
    }
}
