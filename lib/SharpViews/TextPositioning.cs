namespace SharpViews;

/// <summary>
/// A set of methods to position the text, especially center it.
/// </summary>
public static class TextPositioning
{
    /// <summary>
    /// Returns a string (doesn't display it), where <c>text</c> is surrounded by spaces or other character to fill out the
    /// entire screen width. If you use formatted text, don't put the colors and tags into <c>text</c>. Instead, add the tags before and after
    /// this function.
    /// <b>Text will be truncated</b> ("...") if it exceeds the console width.
    /// </summary>
    /// <param name="uiWidth">Width of the window.</param>
    /// <param name="text">The text to center.</param>
    /// <param name="SurroundChar">The character used to surround the text (default: spaces)</param>
    /// <remarks>
    /// Remember to add a newline when displaying the text somewhere. Filling the whole line isn't
    /// stable, and sometimes leaves a bit of free space at the line end.
    /// </remarks>
    public static string CenteredText(int uiWidth, string text, char SurroundChar = ' ')
    {

        if (text == "") return Repeat(SurroundChar, uiWidth);

        if (text.Length > uiWidth) text = Truncate(text, uiWidth);

        float surroundLength = (uiWidth - text.Length) / 2f;
        if (surroundLength < 0) surroundLength = 0;

        return (
            Repeat(SurroundChar, (int)Math.Ceiling(surroundLength)) +
            text +
            Repeat(SurroundChar, (int)Math.Floor(surroundLength))
        );
    }

    /// <summary>
    /// Repeat a provided character for a provided number of times.
    /// </summary>
    /// <param name="ch">Character to repeat.</param>
    /// <param name="length">Desired repeat count.</param>
    /// <returns></returns>
    public static string Repeat(char ch, int length) => new(ch, length);

    /// <summary>
    /// Aligns <c>text</c> to the screen's right with spaces, or a char provided in <c>surroundChar</c>. A newline is automatically added at the end.
    /// </summary>
    /// <param name="uiWidth">Width of the window.</param>
    /// <param name="text">Text to align.</param>
    /// <param name="surroundChar">Character to surround the text with. Spaces by default.</param>
    /// <returns>A string aligned to the right.</returns>
    public static string RightAlignedText(int uiWidth, string text, char surroundChar = ' ')
    {
        // if text is empty, simply fill the whole width with surroundChars
        if (text == "") return Repeat(surroundChar, uiWidth) + "\n";

        int surroundLength = uiWidth - text.Length;

        if (surroundLength < 0) return text;

        return Repeat(surroundChar, surroundLength) + text + "\n";
    }

    /// <summary>
    /// Add given <c>marginChar</c>, <c>margin</c> times to the beginning and after the <c>str</c> string.
    /// </summary>
    /// <param name="str">String to apply margin to.</param>
    /// <param name="margin">Length of the margin (both on left and right). Default: 1.</param>
    /// <param name="marginChar">Character to make the margin of. Default: spaces.</param>
    /// <returns></returns>
    public static string Margin(string str, int margin = 1, char marginChar = ' ')
    {
        return (
            Repeat(marginChar, margin) +
            str +
            Repeat(marginChar, margin)
        );
    }

    /// <summary>
    /// If given string is longer than <c>width</c>, then trim it by adding "..." at the end, and return it.
    /// </summary>
    /// <param name="str">String to truncate.</param>
    /// <param name="width">Desired output width (including "...").</param>
    public static string Truncate(string str, int width)
    {
        if (str.Length > width) return str[..(width - 3)] + "...";
        return str;
    }

    internal static string[] DivideStringIntoArray(this string sourceString, int maxElementLength)
    {
        // check if splitting is needed
        if (sourceString.Length <= maxElementLength) return [sourceString];
        else
        {
            // split the string
            int parts = (int)Math.Ceiling(sourceString.Length / (float)maxElementLength);
            string[] dividedString = new string[parts];

            for (int part = 0; part < parts; part++)
            {
                if (part == parts - 1) dividedString[part] = sourceString[(part * maxElementLength)..];
                else dividedString[part] = sourceString.Substring(part * maxElementLength, maxElementLength);
            }

            return dividedString;
        }

    }
}

