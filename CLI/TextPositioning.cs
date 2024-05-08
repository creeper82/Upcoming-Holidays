namespace CLI;

public static class TextPositioning {
    internal static int UiWidth
    {
        get
        {
            try
            {
                return Console.WindowWidth - 2;
            }

            // Default window width if it couldn't be acquired
            catch (Exception)
            {
                return 64;
            }
        }
    }

    /// <summary>
    /// Returns a string (doesn't display it to console), where <c>text</c> is surrounded by spaces or other character to fill out the
    /// entire screen width.
    /// </summary>
    /// <param name="text">The text to center.</param>
    /// <param name="SurroundChar">The character used to surround the text (default: spaces)</param>
    /// <param name="addInstantTags">Whether to add the TextFormatter class' "instant" tag for the surrounding spaces, so when it is later
    /// displayed formatted, it won't slowly render all the spaces to console.</param>
    /// <remarks>
    /// Use <c>Console.WriteLine()</c> or remember to add a newline when displaying the text somewhere. Filling the whole console line isn't
    /// stable, and sometimes leaves a bit of free space at the line end.
    /// </remarks>
    public static string CenteredText(string text, char SurroundChar = ' ', bool addInstantTags = false)
    {

        if (text == "") return Repeat(SurroundChar, UiWidth);

        if (text.Length > UiWidth) text = Truncate(text, UiWidth);

        float surroundLength = (UiWidth - text.Length) / 2f;

        if (addInstantTags) return (
            "<instant>" + Repeat(SurroundChar, (int)Math.Ceiling(surroundLength)) + "<normal>" +
            text +
            "<instant>" + Repeat(SurroundChar, (int)Math.Floor(surroundLength)) + "\n"
        );

        return (
            Repeat(SurroundChar, (int)Math.Ceiling(surroundLength)) +
            text +
            Repeat(SurroundChar, (int)Math.Floor(surroundLength))
        );
    }

    private static string Repeat(char Char, int length) => new(Char, length);

    /// <summary>
    /// If given string is longer than <c>width</c>, then trim it by adding "..." at the end, and return it.
    /// </summary>
    private static string Truncate(string str, int width)
    {
        if (str.Length > width) return str[..(width - 3)] + "...";
        return str;
    }
}