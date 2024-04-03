namespace CLI;

/// <summary>
/// Used for displaying a paginated list you can scroll. Unlike <c>ChoiceList</c>, you can't select any item
/// here. It is only for visual purpose of scrolling, which you can do with <c>MoveForward</c> (scroll down), and
/// <c>MoveBackward</c> (scroll up) methods. Then, use <c>PaginatedChoices</c> to get the current state of list.
/// Set the <c>PaginationCount</c> to determine maximum number of visible elements at one moment.
/// </summary>
/// <typeparam name="T">Type of the list elements.</typeparam>
/// <param name="choices">List elements.</param>
public class ScrollableList<T>(IEnumerable<T> choices)
{
    public int Position = 0;
    public int PaginationCount = 9;

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
    /// <summary>
    /// Scrolls the list down.
    /// </summary>
    public void MoveForward()
    {
        if (Position < MaxAllowedPosition) Position++;
    }

    /// <summary>
    /// Scrolls the list up.
    /// </summary>
    public void MoveBackward()
    {
        if (Position > 0) Position--;
    }

    /// <summary>
    /// Check if the pointer got out of bounds and fix it, if so. It might happen if the list content changes.
    /// </summary>
    public void CheckOutOfBoundsPointer()
    {
        if (Position > MaxAllowedPosition) Position = MaxAllowedPosition;
        if (Position < 0) Position = 0;
    }
}
